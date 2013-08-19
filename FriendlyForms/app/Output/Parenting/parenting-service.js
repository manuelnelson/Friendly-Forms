FormsApp.factory('parentingService', function ($resource) {
    var service = {
        parentings: $resource('/api/output/parenting/', {},
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
});