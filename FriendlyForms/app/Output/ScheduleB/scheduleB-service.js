FormsApp.factory('scheduleBService', ['$resource', function($resource) {
    var service = {
        scheduleBs: $resource('/api/output/scheduleB/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
}]);