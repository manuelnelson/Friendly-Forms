FormsApp.factory('supportService', function($resource) {
    var service = {
        supports: $resource('/api/supports/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
});