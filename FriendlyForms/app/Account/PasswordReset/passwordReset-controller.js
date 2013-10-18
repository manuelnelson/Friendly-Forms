var PasswordResetCtrl = ['$scope', '$route', '$location', 'passwordResetService', 'headerService', 'menuService', 'forgotPasswordService', 'loginService',
    function ($scope, $route, $location, passwordResetService, headerService, menuService, forgotPasswordService, loginService) {
        $scope.submit = function () {
            $scope.user.Id = $route.current.params.id;
            forgotPasswordService.forgotPasswords.update(null, $scope.user, function (data) {
                loginService.login({ UserName: data.Email, Password: $scope.user.Password });
            });
        };
        headerService.setTitle('Reset Password');
    }];