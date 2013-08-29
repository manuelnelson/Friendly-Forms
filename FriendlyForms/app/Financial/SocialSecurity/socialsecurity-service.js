FormsApp.factory('socialSecurityService', ['$resource',function($resource) {
    var service = {
        socialSecurities: $resource('/api/socialSecurities/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
}]);