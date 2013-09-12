FormsApp.factory('loginService', ['$resource',function ($resource) {
    var service = {
        login: $resource('/api/auth/credentials/', {},
            {
                post: { method: 'POST', params: { format: 'json' } },
            }),
        authUser: null,
    };
    return service;
}]);