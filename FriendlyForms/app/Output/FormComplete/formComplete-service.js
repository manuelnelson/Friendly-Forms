FormsApp.factory('formCompleteService', ['$resource', function ($resource) {
    var service = {
        formCompletes: $resource('/api/output/formComplete/', {},
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
        child: $resource('/api/child/', {},
        {
            update: { method: 'PUT' },
            deleteAll: { method: 'DELETE' }
        }),
    };
    return service;
}]);