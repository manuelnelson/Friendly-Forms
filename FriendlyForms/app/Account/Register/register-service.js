FormsApp.factory('registerService', ['$resource',function ($resource) {
    var service = {
        register: $resource('/api/register/', {},
            {
                post: { method: 'POST', params: {  } },
            }),
        authUser: null,
    };
    return service;
}]);