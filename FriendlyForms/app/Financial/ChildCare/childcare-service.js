FormsApp.factory('childCareService', function ($resource) {
    var service = {
        childCares: $resource('/api/childCares/:childId', { childId: '@childId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
        childCareForms: $resource('/api/childCareForms/:userId', { userId: '@userId' },
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
});