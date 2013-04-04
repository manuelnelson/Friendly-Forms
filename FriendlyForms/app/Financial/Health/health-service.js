FormsApp.factory('healthService', function($resource) {
    var service = {
        healths: $resource('/api/healths/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
});