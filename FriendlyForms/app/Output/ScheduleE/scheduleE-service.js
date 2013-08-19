FormsApp.factory('scheduleEService', function($resource) {
    var service = {
        scheduleEs: $resource('/api/scheduleEs/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
});