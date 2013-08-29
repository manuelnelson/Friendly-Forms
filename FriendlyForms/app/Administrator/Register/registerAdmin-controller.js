var RegisterAdminCtrl = function ($scope, $routeParams, $location, registerAdminService, menuService, genericService, headerService, $rootScope) {
    $scope.path = $location.path();
    $scope.submit = function () {
        if ($scope.registerAdminForm.$invalid) {
            var value = genericService.getFormInput('#registerAdminForm');
            $.jStorage.set($scope.path, value);
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.user.UserName = $scope.user.Email.replace("@", "_").replace(".", "_");
        registerAdminService.registerAdmins.post(null, $scope.user, function () {
            var role = {
                UserName: $scope.Email,
                Roles: ['FirmAdmin', 'Attorney']
            };
            registerAdminService.roles.save(null, role, function() {
                $location.path('/');
            });            
        });
    };
    $rootScope.currentScope = $scope;
    headerService.setTitle('Register Administrator');
};
RegisterAdminCtrl.$inject = ['$scope', '$routeParams', '$location', 'registerAdminService', 'menuService', 'genericService', 'headerService', '$rootScope'];