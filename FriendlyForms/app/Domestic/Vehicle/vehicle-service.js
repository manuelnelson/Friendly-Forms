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
    };
    return service;
}]);