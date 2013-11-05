FormsApp.factory('paymentService', function($resource) {
    var service = {
        oneTime: $resource('/api/payments/onetime', { },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
        recurring: $resource('/api/payments/recurring', {},
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
});