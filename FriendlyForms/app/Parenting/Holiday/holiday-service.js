FormsApp.factory('holidayService', function($resource) {
    var service = {
        holidays: $resource('/api/holidays/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
});