FormsApp.factory('loginService', ['$resource','loginMenuService', 'menuService', 
    function ($resource, loginMenuService, menuService) {
    var service = {
        logins: $resource('/api/auth/credentials/', {},
            {
                post: { method: 'POST', params: { format: 'json' } },
            }),
        login: function(login, errorCallback) {
            service.logins.post(null, login, function () {
                loginMenuService.refresh().then(function () {
                    menuService.goToFirstFormMenu();
                });
            }, errorCallback);
        },
        authUser: null,
    };
    return service;
}]);