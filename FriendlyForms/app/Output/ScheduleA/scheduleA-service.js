FormsApp.factory('scheduleAService', function($resource) {
    var service = {
        scheduleAs: $resource('/api/scheduleAs/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
});