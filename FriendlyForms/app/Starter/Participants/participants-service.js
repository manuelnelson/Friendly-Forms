//Todoservice
FormsApp.factory('participantService', ['$resource', function ($resource) {
    return $resource('/api/participant/:Id', { Id: '@Id' },
    {
        update: { method: 'PUT' },
        deleteAll: { method: 'DELETE' }
    });
}]);