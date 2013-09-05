FormsApp.factory('registerService', ['$resource',function ($resource) {
    var service = {
        register: $resource('/api/register/', {},
            {
                post: { method: 'POST', params: {  } },
            }),
        users: $resource('/api/users/', {},
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
        authUser: null,
    };
    return service;
}]);