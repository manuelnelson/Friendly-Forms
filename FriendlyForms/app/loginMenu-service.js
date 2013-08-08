FormsApp.factory('loginMenuService', function ($resource) {
    var service = {
        userAuth: $resource('/api/userauths/', {},
            {
                get: { method: 'GET', params: { format: 'json' } },
            }),
        auth: $resource('/api/auth/logout', {},
            {
                logout: { method: 'GET', params: { format: 'json' } },
            }),
    };
    return service;
});