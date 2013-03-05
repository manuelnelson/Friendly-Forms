//Todoservice
FormsApp.factory('childService', ['$resource', function ($resource) {
    return $resource('/api/child/:Id', { Id: '@Id' },
    {
        update: { method: 'PUT' },
        deleteAll: { method: 'DELETE' }
    });
}]);