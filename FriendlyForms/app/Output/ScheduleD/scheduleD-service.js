FormsApp.factory('scheduleDService', function($resource) {
    var service = {
        scheduleDs: $resource('/api/scheduleDs/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
});