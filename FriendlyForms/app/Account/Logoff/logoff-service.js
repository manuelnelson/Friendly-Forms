FormsApp.factory('logoffService', ['loginMenuService', '$location', 'menuService', function (loginMenuService, $location, menuService) {
    var service = {
        logout: function () {
            loginMenuService.auth.logout({}, function () {
                loginMenuService.refresh();
                menuService.getMenu();
                $location.path('/');
            });
        }
    };
    return service;
}]);