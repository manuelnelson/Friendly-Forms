FormsApp.factory('assetService', function($resource) {
    var service = {
        assets: $resource('/api/assets/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
});