FormsApp.factory('supportService', ['$resource', function($resource) {
    var service = {
        supports: $resource('/api/PreexistingSupportForms/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
        courts: $resource('/api/supports/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
        children: $resource('/api/PreexistingSupportChildren/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
        states: $resource('/api/States/:Id', { Id: '@Id' },
            {
                getList: { method: 'GET', isArray:true, params: { format: 'json' } },
            }),
    };
    return service;
}]);