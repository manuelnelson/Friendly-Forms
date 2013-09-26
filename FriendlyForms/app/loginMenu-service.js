FormsApp.factory('loginMenuService', ['$resource', 'menuService', 'userService','$q',
   function ($resource, menuService, userService, $q) {
    var service = {
        auth: $resource('/api/auth/logout', {},
            {
                logout: { method: 'GET', params: { format: 'json' } },
            }),
        refresh: function () {
            var deferred = $q.defer();
            userService.getCurrentUserSession().then(function (userData) {
                if (userData != null && userData.IsAuthenticated) {
                    service.authUser = userData;
                    menuService.userId = service.authUser.CustomId;
                    menuService.getMenu().then(function() {
                        deferred.resolve();
                    });
                } else {
                    service.authUser = null;
                    menuService.userId = 0;
                    menuService.getMenu().then(function () {
                        deferred.resolve();
                    });
                }
            });
            return deferred.promise;
        }
    };
    return service;
}]);