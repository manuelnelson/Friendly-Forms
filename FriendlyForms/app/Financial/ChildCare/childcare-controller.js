﻿var ChildCareCtrl = function ($scope, $routeParams, $location, childCareService, menuService, genericService, $rootScope) {
    //#region Intialize
    $scope.path = $location.path();
    $scope.showErrors = false;
    $scope.showMessage = false;
    $rootScope.currentScope = $scope;
    childCareService.children.get({ UserId: $routeParams.userId }, function (data) {
        $scope.children = data.Children;
        $scope.childNdx = _.indexOf(_.pluck($scope.children, 'Id'), parseInt($routeParams.childId));
        $scope.childName = $scope.children[$scope.childNdx].Name;
    });
    //#endregion

    //#region Event Handlers
    $scope.getChildChildCare = function (childId) {
        $scope.childCare = childCareService.childCares.get({ ChildId: childId }, function () {
            if (typeof $scope.childCare.Id == 'undefined' || $scope.childCare.Id == 0) {
                //see if garlic has something stored            
                $scope.childCare = $.jStorage.get($scope.path);
            }
        });
    };
    childCareService.childCareForms.get({ UserId: $routeParams.userId }, function (data) {
        if (typeof data.Id == 'undefined' || data.Id == 0) {
            //see if garlic has something stored            
            $scope.childCareForm = $.jStorage.get($scope.path);
        } else {
            $scope.childCareForm = data;
        }
    });
    $scope.submit = function (noNavigate) {
        if ($scope.childCareForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            return;
        }
        $scope.childCareForm.UserId = $routeParams.userId;
        if (typeof $scope.childCareForm.Id == 'undefined' || $scope.childCareForm.Id == 0) {
            childCareService.childCareForms.save(null, $scope.childCareForm, function () {
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
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#childCareChildForm');
            $.jStorage.set($scope.path, value);
            $scope.showErrors = true;
            if (callback)
                callback();
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.childCare.UserId = $routeParams.userId;
        $scope.childCare.ChildId = $routeParams.childId;
        if (typeof $scope.childCare.Id == 'undefined' || $scope.childCare.Id == 0) {
            childCareService.childCares.save(null, $scope.childCare, function () {
                callback();
            });
        } else {
            childCareService.childCares.update({ Id: $scope.childCare.Id }, $scope.childCare, function () {
                callback();
            });
        }
        menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
    };
    $scope.previousChild = function () {
        $scope.submitChildCare(function () {
            $scope.childNdx = _.indexOf(_.pluck($scope.children, 'Id'), parseInt($routeParams.childId));
            if ($scope.childNdx === 0) {
                //Navigate else where
                menuService.previousMenu();
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
            if (!$scope.childCareForm.ChildrenInvolved || $scope.childCareForm.ChildrenInvolved == 2) {
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
    genericService.refreshPage();
};
ChildCareCtrl.$inject = ['$scope', '$routeParams', '$location', 'childCareService', 'menuService', 'genericService', '$rootScope'];