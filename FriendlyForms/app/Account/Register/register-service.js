FormsApp.factory('registerService', function ($resource) {
    var service = {
        register: $resource('/api/register/', {},
            {
                post: { method: 'POST', params: { format: 'json' } },
            }),
        authUser: null,
    };
    return service;
});