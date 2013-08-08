FormsApp.factory('decisionService', function($resource) {
    var service = {
        decisions: $resource('/api/decisions/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
});