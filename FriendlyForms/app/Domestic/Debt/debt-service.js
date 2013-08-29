FormsApp.factory('debtService', ['$resource',function($resource) {
    var service = {
        debts: $resource('/api/debts/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
}]);