FormsApp.factory('extraExpenseService', function($resource) {
    var service = {
        extraExpenses: $resource('/api/extraExpenses/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
});