FormsApp.factory('scheduleBService', function($resource) {
    var service = {
        scheduleBs: $resource('/api/scheduleBs/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
});