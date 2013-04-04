FormsApp.factory('incomeService', function($resource) {
    var service = {
        incomes: $resource('/api/incomes/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
});