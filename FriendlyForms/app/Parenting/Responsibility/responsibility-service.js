FormsApp.factory('responsibilityService', function($resource) {
    var service = {
        responsibilities: $resource('/api/responsibilities/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
});