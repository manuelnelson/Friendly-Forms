FormsApp.factory('formCompleteService', function($resource) {
    var service = {
        formCompletes: $resource('/api/output/formComplete/', { },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
});