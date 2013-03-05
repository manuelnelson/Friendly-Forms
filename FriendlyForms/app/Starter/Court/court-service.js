//Todoservice
FormsApp.factory('courtService', ['$resource', function ($resource) {
    return $resource('/api/court/:Id', { Id: '@Id' },
    {
        update: { method: 'PUT' },
        deleteAll: { method: 'DELETE' }
    });
}]);