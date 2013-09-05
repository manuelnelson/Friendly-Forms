FormsApp.factory('lawFirmService', function($resource) {
    var service = {
        lawFirms: $resource('/api/lawFirms', { },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return service;
});