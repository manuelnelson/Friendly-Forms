FormsApp.factory('informationService', ['$resource',function($resource) {
    var service = {
        information: $resource('/api/information/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
}]);