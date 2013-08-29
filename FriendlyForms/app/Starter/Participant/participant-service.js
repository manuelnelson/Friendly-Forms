FormsApp.factory('participantService', ['$resource', function($resource) {
    var service = {
        participant: $resource('/api/participants/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
}]);