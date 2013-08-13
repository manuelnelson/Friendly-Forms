FormsApp.factory('extraExpenseService', function($resource) {
    var service = {
        extraExpenses: $resource('/api/extraExpenses/:childId', { childId: '@childId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
        extraExpenseForms: $resource('/api/extraExpenseForms/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
        children: $resource('/api/child/:userId', { userId: '@userId' },
            {
                update: { method: 'PUT' },
                deleteAll: { method: 'DELETE' }
            }),
    };
    return service;

});