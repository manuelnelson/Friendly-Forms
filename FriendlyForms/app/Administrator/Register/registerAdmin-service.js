FormsApp.factory('registerAdminService', ['$resource',function($resource) {
    var service = {
        registerAdmins: $resource('/api/register/', { },
            {
            }),
    };
    return service;
}]);