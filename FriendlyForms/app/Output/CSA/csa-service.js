FormsApp.factory('csaService',['$resource', function($resource) {
    var service = {
        csas: $resource('/api/output/csa/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
}]);