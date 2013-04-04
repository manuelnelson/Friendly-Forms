FormsApp.factory('houseService', function($resource) {
    var service = {
        houses: $resource('/api/houses/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
});