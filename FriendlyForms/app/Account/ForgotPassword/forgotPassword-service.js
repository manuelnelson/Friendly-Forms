FormsApp.factory('forgotPasswordService', ['$resource', function ($resource) {
    var service = {
        forgotPasswords: $resource('/api/passwordreset/', {},
            {
                update: { method: 'PUT', params: { format: 'json' } },
                post: { method: 'POST', params: { format: 'json' } }
}),
    };
    return service;
}]);