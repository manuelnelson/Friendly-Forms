FormsApp.factory('vehicleService', ['$resource', function ($resource) {
    var service = {
        vehicles: $resource('/api/vehicles/', {},
            {
                update: { method: 'PUT' },
                deleteAll: { method: 'DELETE' }
            }),
        vehicleForm: $resource('/api/vehicleForm/', null,
            {
                update: { method: 'PUT' },
                deleteAll: { method: 'DELETE' }
            }),
        custody: $resource('/api/participants/CustodyInfomation/', { },
        {
            get: { method: 'GET', params: { format: 'json' } },
            update: { method: 'PUT', params: { format: 'json' } }
        }),
    };
    return service;
}]);