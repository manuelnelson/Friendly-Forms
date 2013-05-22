FormsApp.factory('loginService', function ($resource) {
    var service = {
        login: $resource('/api/auth/credentials/', {},
            {
                post: { method: 'GET', params: { format: 'json' } },
            }),
    };
    return service;
});