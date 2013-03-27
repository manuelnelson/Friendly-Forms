FormsApp.factory('taxesService', function($resource) {
    var service = {
        taxes: $resource('/api/taxes/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
});