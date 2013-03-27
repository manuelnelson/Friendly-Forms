FormsApp.factory('communicationService', function($resource) {
    var service = {
        communications: $resource('/api/communications/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
});