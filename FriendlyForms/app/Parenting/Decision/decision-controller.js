var DecisionCtrl = function ($scope, $routeParams, $location, decisionService, menuService, genericService, $rootScope) {
    //#region Intialize
    $scope.path = $location.path();
    $scope.showErrors = false;
    $scope.showMessage = false;
    $scope.showExtraErrors = false;
    $rootScope.currentScope = $scope;
    decisionService.children.get({ UserId: $routeParams.userId }, function (data) {
        $scope.children = data.Children;
        $scope.childNdx = _.indexOf(_.pluck($scope.children, 'Id'), parseInt($routeParams.childId));
        $scope.childName = $scope.children[$scope.childNdx].Name;
        $scope.menuPath = '/Parenting/Decision/user/' + $routeParams.userId + '/child/' + $scope.children[0].Id;
        if (!menuService.isActive($scope.menuPath)) {
            menuService.setActive($scope.menuPath);
        }
    });
    //#endregion
    
    //#region Event Handlers
    $scope.getChildDecision = function (childId) {
        $scope.decision = decisionService.decisions.get({ ChildId: childId }, function () {
            if (typeof $scope.decision.Id == 'undefined' || $scope.decision.Id == 0) {
                //see if garlic has something stored            
                $scope.decision = $.jStorage.get($scope.path);
            }
        });
        decisionService.extraDecisions.get({ ChildId: childId }, function (data) {
            if (data.ExtraDecisions.length === 0) {
                $scope.extraDecisions = [];
            }
            $scope.extraDecisions = data.ExtraDecisions;
        });
    };
    $scope.addExtraDecision = function() {
        if ($scope.addDecisionForm.$invalid) {
            $scope.showExtraErrors = true;
            return;
        }
        $scope.showExtraErrors = false;
        $scope.extraDecision.ChildId = $routeParams.childId;
        decisionService.extraDecisions.save(null, $scope.extraDecision, function(data) {
            $scope.extraDecisions.push(data);
            $scope.extraDecision.DecisionMaker = -1;
            $scope.extraDecision.Description = '';
            $scope.addDecisionForm.$setPristine();
        });
        
    };
    $scope.submit = function (noNavigate, callback) {
        if ($scope.decisionForm.$invalid) {
            menuService.setSubMenuIconClass($scope.menuPath, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#decisionForm');
            $.jStorage.set($scope.path, value);
            $scope.showErrors = true;
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.decision.UserId = $routeParams.userId;
        $scope.decision.ChildId = $routeParams.childId;
        if (typeof $scope.decision.Id == 'undefined' || $scope.decision.Id == 0) {
            decisionService.decisions.save(null, $scope.decision, function () {
            });
        } else {
            decisionService.decisions.update({ Id: $scope.decision.Id }, $scope.decision, function () {
            });
        }
        menuService.setSubMenuIconClass($scope.menuPath, 'icon-ok icon-green');
        if ($scope.extraDecisions.length > 0) {
            //Post each extraDecision
            var completedCount = 0;
            _.each($scope.extraDecisions, function(item) {
                decisionService.extraDecisions.update(null, item, function() {
                    //Wait till all extraDecisions are updated before doing callback
                    completedCount++;
                    if ($scope.extraDecisions.length === completedCount && callback) {
                        callback();
                    }
                });
            });
        } else {
            if (callback)
                callback();
        }
    };
    $scope.previousChild = function () {
        $scope.submit(false, function() {
            $scope.childNdx = _.indexOf(_.pluck($scope.children, 'Id'), parseInt($routeParams.childId));
            if ($scope.childNdx === 0) {
                //Navigate else where
                menuService.previousMenu();
                return;
            }
            $scope.childNdx = $scope.childNdx - 1;
            var childId = $scope.children[$scope.childNdx].Id;
            $location.path('/Parenting/Decision/user/' + $routeParams.userId + '/child/' + childId);
        });
    };
    $scope.nextChild = function () {
        $scope.submit(false, function() {
            $scope.childNdx = _.indexOf(_.pluck($scope.children, 'Id'), parseInt($routeParams.childId));
            if ($scope.childNdx === ($scope.children.length - 1)) {
                //Navigate to next item
                menuService.nextMenu();
                return;
            }
            $scope.childNdx = $scope.childNdx + 1;
            var childId = $scope.children[$scope.childNdx].Id;
            menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
            $location.path('/Parenting/Decision/user/' + $routeParams.userId + '/child/' + childId);
        });
    };
    $scope.copyChild = function(childId) {
        //first submit current child
        $scope.submit(false, function () {
            if (childId === 0) {
                //copy to all children
                _.each($scope.children, function(item) {
                    if (item.childId != $routeParams.childId)
                        copyToChild(item.Id);
                });
            } else {
                copyToChild(childId);
            }
        });
    };
    function copyToChild(childId) {
        //add extra decisions if they don't already exist for 
        //this is what we are copying
        var currentExtraDecisions = angular.copy($scope.extraDecisions);
        decisionService.extraDecisions.get({ ChildId: childId }, function (data) {
            _.each(data.ExtraDecisions, function(extraDecision) {
                var match = _.find(currentExtraDecisions, function (item) {
                    return item.Description == extraDecision.Description;
                });
                //if we have a match, we just want to overwrite, so update Id and ChildId
                if (match) {
                    match.Id = extraDecision.Id;
                    match.ChildId = extraDecision.ChildId;
                    decisionService.extraDecisions.update(null, match, function () {
                    });
                    //remove current decisions
                    currentExtraDecisions = _.reject(currentExtraDecisions, function (item) {
                        return item.Description == extraDecision.Description;
                    });
                }
            });
            //Add new extra decisions
            _.each(currentExtraDecisions, function (item) {
                item.ChildId = childId;
                decisionService.extraDecisions.save(null, item, function() {

                });
            });
            //Now...update decision
            decisionService.decisions.get({ ChildId: childId }, function (decision) {
                var copyDecision = angular.copy($scope.decision);
                copyDecision.UserId = $routeParams.userId;
                copyDecision.ChildId = childId;
                //update if decision exists, post otherwise
                if (typeof decision.Id !== 'undefined') {
                    copyDecision.Id = decision.Id;
                    decisionService.decisions.update(null, copyDecision, function () {
                        $scope.showMessage = true;
                    });
                } else {                    
                    decisionService.decisions.save(null, copyDecision, function () {
                        $scope.showMessage = true;
                    });
                }
            });
        });
    }   
    //#endregion
    
    $scope.getChildDecision($routeParams.childId);
    genericService.refreshPage();

};
DecisionCtrl.$inject = ['$scope', '$routeParams', '$location', 'decisionService', 'menuService', 'genericService', '$rootScope'];