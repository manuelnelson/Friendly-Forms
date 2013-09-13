FormsApp.factory('caseService', ['$resource', function($resource) {
    var service = {
        cases: $resource('/api/cases/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
}]);