var LoginCtrl = ['$scope', '$routeParams', '$location', 'loginService', 'headerService', 'loginMenuService',
    function ($scope, $routeParams, $location, loginService, headerService, loginMenuService) {
        $scope.submit = function () {
            loginService.login.post(null, $scope.login, function () {
                loginMenuService.refresh();
                $location.path('/');
            }, function() {
                $scope.loginForm.$setPristine();
                $scope.login = '';
            });
        };
        headerService.setTitle('Login');
    }];