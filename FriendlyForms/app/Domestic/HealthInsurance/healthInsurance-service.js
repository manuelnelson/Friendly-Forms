FormsApp.factory('healthInsuranceService', function($resource) {
    var service = {
        healthInsurances: $resource('/api/healthInsurances/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
});