FormsApp.factory('loginMenuService', ['$resource', 'menuService', 'userService', function ($resource, menuService, userService) {
    var service = {
        auth: $resource('/api/auth/logout', {},
            {
                logout: { method: 'GET', params: { format: 'json' } },
            }),
        refresh: function () {
            userService.getCurrentUserSession().then(function (userData) {
                if (userData != null && userData.IsAuthenticated) {
                    service.authUser = userData;
                    menuService.userId = service.authUser.CustomId;
                    menuService.getMenu();
                } else {
                    service.authUser = null;
                    menuService.userId = 0;
                    menuService.getMenu();
                }
            });
        }
    };
    return service;
}]);