FormsApp.factory('clientCasesService', ['$resource', function($resource) {
    var service = {
        clientCases: $resource('/api/clientCases/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
}]);