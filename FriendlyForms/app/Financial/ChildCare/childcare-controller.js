var ChildCareCtrl = function ($scope, $routeParams, $location, childCareService, menuService, genericService, $rootScope) {
    //#region Intialize
    $scope.path = $location.path();
    $scope.showErrors = false;
    $scope.showMessage = false;
    $scope.isLoaded = false;
    childCareService.children.get({ UserId: $routeParams.userId }, function (data) {
        $scope.children = data.Children;
        $scope.childNdx = _.indexOf(_.pluck($scope.children, 'Id'), parseInt($routeParams.childId));
        $scope.childName = $scope.children[$scope.childNdx].Name;
    });
    //#endregion

    //#region Event Handlers
    $scope.getChildChildCare = function (childId) {
        $scope.childCare = childCareService.childCares.get({ ChildId: childId }, function () {
            $scope.isLoaded = true;
            if (typeof $scope.childCare.Id == 'undefined' || $scope.childCare.Id == 0) {
                //see if garlic has something stored            
                $scope.childCare = $.jStorage.get($scope.path);
            }
        });
    };
    $scope.childCareForm = childCareService.childCareForms.get({ UserId: $routeParams.userId }, function () {
        if (typeof $scope.childCareForm.Id == 'undefined' || $scope.childCareForm.Id == 0) {
            $scope.showErrors = true;
        } 
    });
    $scope.submit = function () {
        if (!$scope.childCareForm || ($scope.childCareForm.ChildrenInvolved != "1" && $scope.childCareForm.ChildrenInvolved != "2")) {
            menuService.setSubMenuIconClass($scope.path, 'icon-exclamation icon-red');
            return;
        }
        $scope.showErrors = false;
        $scope.childCareForm.UserId = $routeParams.userId;
        if (typeof $scope.childCareForm.Id == 'undefined' || $scope.childCareForm.Id == 0) {
            childCareService.childCareForms.save(null, $scope.childCareForm, function (data) {
                $scope.childCareForm.Id = data.Id;
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
            });
        } else {
            childCareService.childCareForms.update(null, $scope.childCareForm, function () {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
            });
        }
    };
    $scope.submitChildCare = function(callback) {
        if ($scope.childCareChildForm.$invalid || $scope.childCare === null) {
            menuService.setSubMenuIconClass($scope.path, 'icon-exclamation icon-red');
            var value = genericService.getFormInput('#childCareChildForm');
            $.jStorage.set($scope.path, value);
            $scope.showErrors = true;
            if (callback)
                callback();
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.childCare.UserId = $routeParams.userId;
        $scope.childCare.ChildId = $scope.children[$scope.childNdx].Id;
        if (typeof $scope.childCare.Id == 'undefined' || $scope.childCare.Id == 0) {
            childCareService.childCares.save(null, $scope.childCare, function () {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                callback();
            });
        } else {
            childCareService.childCares.update({ Id: $scope.childCare.Id }, $scope.childCare, function () {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                callback();
            });
        }
    };
    $scope.previousChild = function () {
        $scope.submitChildCare(function () {
            $scope.childNdx = _.indexOf(_.pluck($scope.children, 'Id'), parseInt($routeParams.childId));
            if ($scope.childNdx === 0) {
                //Navigate else where                
                return;
            }
            $scope.childNdx = $scope.childNdx - 1;
            var childId = $scope.children[$scope.childNdx].Id;
            $location.path('/Financial/ChildCare/user/' + $routeParams.userId + '/child/' + childId);
        });
    };
    $scope.nextChild = function () {
        $scope.submitChildCare(function () {
            //if radio button not selected or set to no, no need to cycle through children
            if ((!$scope.childCareForm.ChildrenInvolved || $scope.childCareForm.ChildrenInvolved == 2)) {
                menuService.nextMenu();
                return;
            }
            $scope.childNdx = _.indexOf(_.pluck($scope.children, 'Id'), parseInt($routeParams.childId));
            if ($scope.childNdx === ($scope.children.length - 1)) {
                //Navigate to next item
                menuService.nextMenu();
                return;
            }
            $scope.childNdx = $scope.childNdx + 1;
            var childId = $scope.children[$scope.childNdx].Id;
            menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
            $location.path('/Financial/ChildCare/user/' + $routeParams.userId + '/child/' + childId);
        });
    };

    //#endregion

    $scope.getChildChildCare($routeParams.childId);
    genericService.refreshPage(function () {
        $rootScope.currentScope = $scope;
    });
};
ChildCareCtrl.$inject = ['$scope', '$routeParams', '$location', 'childCareService', 'menuService', 'genericService', '$rootScope'];