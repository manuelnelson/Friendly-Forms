var OtherChildCtrl = function ($scope, $routeParams, $location, otherChildService, menuService, genericService, $rootScope) {
    //#region properties
    $scope.continuePressed = false;
    $scope.path = $location.path();
    $scope.showErrors = false;
    //#endregion

    //#region intialize
    otherChildService.otherChildren.get({ UserId: $routeParams.userId }, function (data) {
        if (typeof data.Id == 'undefined' || data.Id == 0) {
            //see if garlic has something stored            
            $scope.otherChildren = $.jStorage.get($scope.path);
            if ($scope.otherChildren)
                $scope.showErrors = true;
        } else {
            $scope.otherChildren = data;
        }
        if (typeof $scope.otherChildren !== 'undefined' && $scope.otherChildren !== null && $scope.otherChildren.Id > 0) {
            otherChildService.otherChild.get({ OtherChildrenId: $routeParams.userId }, function (data) {
                if (data.OtherChildren.length == 0)
                    $scope.children = [];
                else
                    $scope.children = data.OtherChildren;
            });
        } else {
            $scope.children = [];
        }
    });
    //#endregion

    //#region event handlers
    $scope.submit = function (noNavigate) {
        if ($scope.otherChildrenForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#otherChildrenForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate) {
                menuService.nextMenu();
            }
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.otherChildren.UserId = $routeParams.userId;
        $scope.otherChildren.IsOtherParent = $routeParams.isOtherParent;
        if (typeof $scope.otherChildren.Id == 'undefined' || $scope.otherChildren.Id == 0) {
            otherChildService.otherChildren.save(null, $scope.otherChildren, function () {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate) {
                    menuService.nextMenu();
                }
            });
        } else {
            otherChildService.otherChildren.update(null, $scope.otherChildren, function () {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate) {
                    menuService.nextMenu();
                }
            });
        }
    };
    $scope.addOtherChild = function () {
        $scope.otherChild.UserId = $routeParams.userId;
        $scope.otherChild.OtherChildrenId = $scope.otherChildren.Id;
        otherChildService.otherChild.save(null, $scope.otherChild, function (data) {
            menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
            $scope.children.push(data.OtherChild);
            $scope.otherChildForm.$setPristine();
            $scope.otherChild = '';
        });
    };
    $scope.deleteOtherChild = function (otherChild) {
        otherChildService.otherChild.delete({ Id: otherChild.Id }, function () {
            $scope.children = _.reject($scope.children, function (item) {
                return item.Id == otherChild.Id;
            });
        });
    };
    $scope.continue = function () {
        $scope.submit();
    };
    //#endregion    
    $rootScope.currentScope = $scope;
    genericService.refreshPage();

};
OtherChildCtrl.$inject = ['$scope', '$routeParams', '$location', 'otherChildService', 'menuService', 'genericService', '$rootScope'];
