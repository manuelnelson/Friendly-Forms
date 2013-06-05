//Todoservice
FormsApp.factory('childService', ['$resource', function ($resource) {
    var childrenService = {
        child: $resource('/api/child/:Id', { Id: '@Id' },
            {
                update: { method: 'PUT' },
                deleteAll: { method: 'DELETE' }
            }),
        childForm: $resource('/api/ChildForm/', null,
            {
                update: { method: 'PUT' },
                deleteAll: { method: 'DELETE' }
            })
    };
    return childrenService;
}]);