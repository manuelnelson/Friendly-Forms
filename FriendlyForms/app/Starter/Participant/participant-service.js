FormsApp.factory('participantService', function($resource) {
    var service = {
        participant: $resource('/api/participant/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
});