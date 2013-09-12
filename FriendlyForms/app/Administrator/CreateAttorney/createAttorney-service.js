FormsApp.factory('createAttorneyService', ['$resource', function($resource) {
    var service = {
        attorneyPages: $resource('/api/attorneyPages', { },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
}]);