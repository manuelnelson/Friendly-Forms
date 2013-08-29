FormsApp.factory('logoffService', ['loginMenuService', '$location', function (loginMenuService, $location) {
    var service = {
        logout: function () {
            loginMenuService.auth.logout({}, function () {
                loginMenuService.refresh();
                $location.path('/');
            });
        }
    };
    return service;
}]);