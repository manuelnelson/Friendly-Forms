FormsApp.factory('decisionService', ['$resource',function ($resource) {
    var service = {
        decisions: $resource('/api/decisions/:childId', { childId: '@childId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
        extraDecisions: $resource('/api/extradecisions/:childId', { childId: '@childId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
        children: $resource('/api/child/:userId', { userId: '@userId' },
        {
            update: { method: 'PUT' },
            deleteAll: { method: 'DELETE' }
        }),

    };
    return service;
}]);