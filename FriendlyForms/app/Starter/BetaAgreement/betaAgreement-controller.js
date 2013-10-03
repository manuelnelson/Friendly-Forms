var BetaAgreementCtrl = ['$scope', '$routeParams', '$location', 'registerService', 'menuService', 'genericService', '$rootScope', 'userService',
    function ($scope, $routeParams, $location, registerService, menuService, genericService, $rootScope, userService) {
        $scope.path = $location.path();
        $scope.submit = function (noNavigate) {
            userService.getUserData($routeParams.userId).then(function (userData) {
                if (userData.Verified) {
                    menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                } else {
                    menuService.setSubMenuIconClass($scope.path, 'icon-exclamation icon-red');
                }
                if (!noNavigate)
                    menuService.nextMenu();
            });
        };
        $scope.accept = function() {
            userService.getUserData($routeParams.userId).then(function (userData) {
                registerService.users.update(null, {
                    Id: userData.Id,
                    UserAuthId: userData.UserAuthId,
                    Verified: true,
                }, function () {
                    menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                    menuService.nextMenu();
                });
            });
        };
        genericService.refreshPage(function () {
            $rootScope.currentScope = $scope;
        });
    }];