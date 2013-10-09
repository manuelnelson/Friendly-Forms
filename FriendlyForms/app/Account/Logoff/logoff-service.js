FormsApp.factory('logoffService', ['loginMenuService', '$location', 'menuService', function (loginMenuService, $location, menuService) {
    var service = {
        logout: function () {
            loginMenuService.logoff().then(function () {
                loginMenuService.refresh();
                menuService.getMenu();
                $location.path('/');
            });
        }
    };
    return service;
}]);