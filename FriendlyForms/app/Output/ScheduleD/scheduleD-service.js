FormsApp.factory('scheduleDService', ['$resource', function($resource) {
    var service = {
        scheduleDs: $resource('/api/output/scheduleD/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
}]);