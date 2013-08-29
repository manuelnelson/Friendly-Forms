FormsApp.factory('otherChildService', ['$resource', function($resource) {
    var service = {
        otherChildren: $resource('/api/otherChildren/', {},
            {
                update: { method: 'PUT' },
                deleteAll: { method: 'DELETE' }
            }),
        otherChild: $resource('/api/otherChild/', null,
            {
                update: { method: 'PUT' },
                deleteAll: { method: 'DELETE' }
            })
    };
    return service;
}]);