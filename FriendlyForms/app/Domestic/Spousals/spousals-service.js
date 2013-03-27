FormsApp.factory('spousalsService', function($resource) {
    var service = {
        spousals: $resource('/api/spousals/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
});