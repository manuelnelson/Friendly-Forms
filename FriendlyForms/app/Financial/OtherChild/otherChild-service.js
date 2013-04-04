FormsApp.factory('otherChildService', function($resource) {
    var service = {
        otherChildren: $resource('/api/otherChildren/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
});