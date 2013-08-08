var LoginMenuCtrl = function ($scope, $routeParams, $location, loginMenuService, menuService) {
    $scope.getAuthStatus = function() {
        $scope.user = loginMenuService.userAuth.get({}, function (data) {
            if (data.UserSession != null) {
                loginMenuService.authUser = data.UserSession;
                menuService.userId = loginMenuService.authUser.CustomId;
                menuService.getMenu();
            }
        });
    };     
    $scope.logOff = function () {
        loginMenuService.auth.logout({}, function () {
            $scope.getAuthStatus();
            $location.path('/');
        });
    };
    $scope.getAuthStatus();
};
LoginMenuCtrl.$inject = ['$scope', '$routeParams', '$location', 'loginMenuService', 'menuService'];