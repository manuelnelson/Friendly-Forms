var ForgotPasswordCtrl = ['$scope', '$routeParams', '$location', 'forgotPasswordService', 'messageService', 'genericService',
    function ($scope, $routeParams, $location, forgotPasswordService, messageService, genericService) {

    $scope.submit = function() {
        if ($scope.forgotPasswordForm.$invalid) {
            return;
        }
        forgotPasswordService.forgotPasswords.post(null, $scope.forgotPassword, function () {
            $scope.forgotPasswordForm.$setPristine();
            $scope.forgotPassword = '';
            messageService.showMessage("E-mail Sent", "An e-mail has been sent to your address with instructions to reset your password.", Application.properties.messageType.Success);
        });

    };
    genericService.refreshPage();

}];