FormsApp.factory('clientService', ['$resource', function($resource) {
    var service = {
        clients: $resource('/api/AttorneyClients/clients', {},
            {
                get: { method: 'GET', params: { format: 'json' } },
                getList: { method: 'GET', isArray: true, params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
        attorneys: $resource('/api/AttorneyClients/attorneys', {},
            {
                get: { method: 'GET', params: { format: 'json' } },
                getList: { method: 'GET', isArray: true, params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
}]);