FormsApp.factory('childCareService', function($resource) {
    var service = {
        childCares: $resource('/api/childCares/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
});