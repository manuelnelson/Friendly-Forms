FormsApp.factory('registerAdminService', ['$resource',function($resource) {
    var service = {
        registerAdmins: $resource('/api/register/', { },
            {
            }),
        roles: $resource('/api/userauths/addroles/', {},
            {
            }),
    };
    return service;
}]);