FormsApp.factory('paymentService', function($resource) {
    var service = {
        payments: $resource('/api/payments/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
});