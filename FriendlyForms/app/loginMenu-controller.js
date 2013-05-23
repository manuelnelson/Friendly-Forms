var LoginMenuCtrl = function ($scope, $routeParams, $location, loginMenuService, menuService) {
    $scope.user = loginMenuService.userAuth.get({}, function (data) {
        if (data.UserSession != null) {
            loginMenuService.authUser = data.UserSession;
            menuService.userId = loginMenuService.authUser.CustomId;
            menuService.setupMenu();
        }
    });
};
LoginMenuCtrl.$inject = ['$scope', '$routeParams', '$location', 'loginMenuService', 'menuService'];