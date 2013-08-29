FormsApp.factory('scheduleAService', ['$resource', function($resource) {
    var service = {
        scheduleAs: $resource('/api/output/scheduleA/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
}]);