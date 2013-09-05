var RegisterAdminCtrl = ['$scope', '$routeParams', '$location', 'registerAdminService', 'loginMenuService', 'genericService', 'userService', 'headerService', 'registerService',
    function ($scope, $routeParams, $location, registerAdminService, loginMenuService, genericService, userService,headerService, registerService) {
    $scope.submit = function () {
        $scope.user.AutoLogin = true;
        $scope.user.UserName = $scope.user.Email;
        registerService.register.save(null, $scope.user, function () {
            loginMenuService.refresh();
            userService.getUserData().then(function(userData) {
                //Tie law firm Id
                registerService.users.update(null, {
                    Id: userData.CustomId,
                    UserAuthId: userData.UserAuthId,
                    LawFirmId: $routeParams.lawFirmId
                }, function() {
                    $location.path('/Administrator/Agreement/User/' + userData.CustomId);
                });
                
            });
        });
    };
    headerService.setTitle('Register Administrator');
}];