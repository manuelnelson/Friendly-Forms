FormsApp.factory('addendumService', function($resource) {
    var service = {
        addendums: $resource('/api/addendums/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
});