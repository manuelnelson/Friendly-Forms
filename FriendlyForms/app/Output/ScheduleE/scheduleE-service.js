FormsApp.factory('scheduleEService', ['$resource', function($resource) {
    var service = {
        scheduleEs: $resource('/api/output/scheduleE/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
}]);