FormsApp.factory('constantsService', ['$resource', '$q', function($resource, $q) {
    var service = {
        constantResource: $resource('/api/constants/', { },
            {
                get: { method: 'GET', params: { format: 'json' } },
            }),
        constants: {},
        //Note: This method is called by the loginMenuService (since it's available on every page and will guarantee intialization)
        //Every other service should access constants directly
        initializeConstants: function () {
            var deferred = $q.defer();
            if (typeof service.constants.length === 'undefined') {
                service.constantResource.get({}, function(data) {
                    service.constants = data;
                    deferred.resolve(data);
                });
            } else {
                deferred.resolve(service.constants);
            }
            return deferred.promise;
        }
    };
    return service;
}]);