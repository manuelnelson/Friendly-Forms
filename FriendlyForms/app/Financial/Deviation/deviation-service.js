FormsApp.factory('deviationService', ['$resource',function($resource) {
    var service = {
        deviations: $resource('/api/deviations/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
}]);