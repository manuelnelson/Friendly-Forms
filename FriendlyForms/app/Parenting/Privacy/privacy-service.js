FormsApp.factory('privacyService', ['$resource',function($resource) {
    var service = {
        privacies: $resource('/api/privacies/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
}]);