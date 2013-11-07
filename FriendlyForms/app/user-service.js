FormsApp.factory('userService', ['$resource', '$q', '$route', '$location', '$rootScope', function ($resource, $q, $route, $location,$rootScope) {
    var service = {
        userData: null,
        register: $resource('/api/userauths/register/', {},
            {
            }),
        roles: $resource('/api/userauths/addroles/', {},
            {
            }),
        users: $resource('/api/users/:Id/', { Id: '@Id' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                getList: { method: 'GET', isArray: true, params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
        userAuths: $resource('/api/userauths/', {},
        {
            get: { method: 'GET', params: { format: 'json' } },
            getList: { method: 'GET', isArray: true, params: { format: 'json' } },
        }),
        userSession: $resource('/api/usersession/', {},
        {//DateTime used as a cache-breaker
            get: { method: 'GET', cache:false, params: { format: 'json', DateTime: new Date().getTime() } },
        }),
        getCurrentUserSession: function () {
            var deferred = $q.defer();
            service.userSession.get({}, function (data) {
                service.userData = data.UserSession;
                deferred.resolve(service.userData);
            });
            return deferred.promise;
        },
        //note, userId is the apps userId, userAuthId is the Id ServiceStack uses.
        getUserAuth: function (userId, userAuthId) {
            var deferred = $q.defer();
            if (typeof userId != 'undefined' && userId != null) {
                service.userAuths.get({ UserId: userId }, function(data) {
                    deferred.resolve(data);
                });
            } 
            if (typeof userAuthId != 'undefined' && userAuthId != null) {
                service.userAuths.get({ UserAuthId: userAuthId }, function (data) {
                    deferred.resolve(data);
                });
            }
            return deferred.promise;
        },
        getUserData: function (userId) {
            var deferred = $q.defer();
            service.users.get({ Id: userId }, function (data) {
                deferred.resolve(data);
            });
            return deferred.promise;
        },
        getLawFirmUsers: function(lawFirmId) {
            var deferred = $q.defer();
            service.users.getList({ LawFirmId: lawFirmId }, function (data) {
                deferred.resolve(data);
            });
            return deferred.promise;
        },
        getFormUserId: function () {
            var path = $rootScope.$root.currentScope.path;
            if ($route.current.params.userId) {
                return $route.current.params.userId;
            } else if (path && path.indexOf('/User/') > -1) {
                var regex = /\/(user|User)\/(\d*)/;
                return path.match(regex)[2];
            }
        },
    };
    return service;
}]);