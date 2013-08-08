FormsApp.factory('vehicleService', function($resource) {
    var service = {
        vehicles: $resource('/api/vehicles/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
});