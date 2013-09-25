var BetaAgreementCtrl = ['$scope', '$routeParams', '$location', 'registerService', 'menuService', 'genericService', '$rootScope', 'userService',
    function ($scope, $routeParams, $location, registerService, menuService, genericService, $rootScope, userService) {
        $scope.path = $location.path();
        $scope.submit = function (noNavigate) {
            userService.getUserData($routeParams.userId).then(function (userData) {
                registerService.users.update(null, {
                    Id: userData.Id,
                    UserAuthId: userData.UserAuthId,
                    Verified: true,
                }, function () {
                    menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                    if (!noNavigate)
                        menuService.nextMenu();
                });
            });
        };

        genericService.refreshPage(function () {
            $rootScope.currentScope = $scope;
        });
    }];