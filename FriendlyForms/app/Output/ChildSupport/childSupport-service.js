FormsApp.factory('childSupportOutputService', ['$resource',function($resource) {
    var service = {
        childSupports: $resource('/api/output/childSupport/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
}]);