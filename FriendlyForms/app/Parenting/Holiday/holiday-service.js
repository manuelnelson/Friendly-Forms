FormsApp.factory('holidayService', ['$resource', function($resource) {
    var service = {
        holidays: $resource('/api/holidays/:childId', { childId: '@childId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
        extraHolidays: $resource('/api/extraholidays/:childId', { childId: '@childId' },
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
}]);