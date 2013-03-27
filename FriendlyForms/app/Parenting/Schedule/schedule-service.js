FormsApp.factory('scheduleService', function($resource) {
    var service = {
        schedules: $resource('/api/schedules/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
});