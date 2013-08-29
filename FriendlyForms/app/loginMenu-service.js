FormsApp.factory('loginMenuService', ['$resource','menuService',function ($resource, menuService) {
    var service = {
        userAuth: $resource('/api/userauths/', {},
            {
                get: { method: 'GET', params: { format: 'json' } },
            }),
        auth: $resource('/api/auth/logout', {},
            {
                logout: { method: 'GET', params: { format: 'json' } },
            }),
        refresh: function() {
            service.userAuth.get({}, function(data) {
                if (data.UserSession != null) {
                    service.authUser = data.UserSession;
                    menuService.userId = service.authUser.CustomId;
                    menuService.getMenu();
                }
            });
        }
    };
    return service;
}]);