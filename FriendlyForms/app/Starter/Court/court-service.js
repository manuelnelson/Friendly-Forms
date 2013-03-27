//Todoservice
FormsApp.factory('courtService', function ($resource) {
    var service = {
        court: $resource('/api/court/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
});