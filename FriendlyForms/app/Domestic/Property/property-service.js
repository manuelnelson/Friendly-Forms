FormsApp.factory('propertyService', function($resource) {
    var service = {
        properties: $resource('/api/properties/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
});