FormsApp.factory('attorneyPageService', ['$resource', function ($resource) {
    var service = {
        attorneyPages: $resource('/api/attorneyPages', { },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
        attorneyClients: $resource('/api/attorneyClients', {},
            {
                get: { method: 'GET', params: { format: 'json' } },
                getList: { method: 'GET', isArray:true, params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
}]);