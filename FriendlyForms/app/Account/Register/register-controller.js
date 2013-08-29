var RegisterCtrl = function ($scope, $routeParams, $location, registerService, headerService, loginMenuService) {
    $scope.submit = function () {
        $scope.user.AutoLogin = true;

        $scope.user.UserName = $scope.user.Email;//.replace("@","_").replace(".","_");
        registerService.register.save(null, $scope.user, function () {
            loginMenuService.refresh();
            $location.path('/');
        });
    };
    headerService.setTitle('Register');
};
RegisterCtrl.$inject = ['$scope', '$routeParams', '$location', 'registerService', 'headerService', 'loginMenuService'];