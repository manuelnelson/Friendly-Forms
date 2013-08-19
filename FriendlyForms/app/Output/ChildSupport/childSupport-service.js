FormsApp.factory('childSupportService', function($resource) {
    var service = {
        childSupports: $resource('/api/childSupports/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
});