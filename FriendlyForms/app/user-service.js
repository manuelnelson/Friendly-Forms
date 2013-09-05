FormsApp.factory('userService', ['$resource', '$q', function ($resource, $q) {
    var service = {
        userData: null,
        users: $resource('/api/users/', {},
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
        userAuth: $resource('/api/userauths/', {},
        {
            get: { method: 'GET', params: { format: 'json' } },
        }),
        roles: $resource('/api/userauths/addroles/', {},
            {
            }),
        getUserData: function () {
            var deferred = $q.defer();
            service.userAuth.get({}, function(data) {
                service.userData = data.UserSession;
                deferred.resolve(service.userData);
            });
            return deferred.promise;
        }
    };
    return service;
}]);