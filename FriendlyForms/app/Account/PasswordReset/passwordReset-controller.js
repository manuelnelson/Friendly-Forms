var PasswordResetCtrl = ['$scope', '$route', '$location', 'passwordResetService', 'genericService', 'menuService', 'forgotPasswordService', 'loginService',
    function ($scope, $route, $location, passwordResetService, genericService, menuService, forgotPasswordService, loginService) {
        $scope.submit = function () {
            $scope.user.Id = $route.current.params.id;
            forgotPasswordService.forgotPasswords.update(null, $scope.user, function (data) {
                loginService.login({ UserName: data.Email, Password: $scope.user.Password });
            });
        };
        genericService.refreshPage();
    }];