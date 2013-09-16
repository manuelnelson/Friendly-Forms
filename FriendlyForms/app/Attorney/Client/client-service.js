FormsApp.factory('clientService', ['$resource', function($resource) {
    var service = {
        clients: $resource('/api/AttorneyClients/', {},
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
}]);