FormsApp.factory('childService', ['$resource', function ($resource) {
    var childrenService = {
        child: $resource('/api/child/', {},
            {
                update: { method: 'PUT' },
                deleteAll: { method: 'DELETE' }
            }),
        childForm: $resource('/api/ChildForm/', null,
            {
                update: { method: 'PUT' },
                deleteAll: { method: 'DELETE' }
            }),
        formCompletes: $resource('/api/output/formComplete/', {},
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
    };
    return childrenService;
}]);