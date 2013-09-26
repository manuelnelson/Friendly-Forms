var LoginCtrl = ['$scope', '$routeParams', '$location', 'loginService', 'headerService', 'loginMenuService', 'menuService',
    function ($scope, $routeParams, $location, loginService, headerService, loginMenuService, menuService) {
        $scope.submit = function () {
            loginService.login.post(null, $scope.login, function () {
                loginMenuService.refresh().then(function() {
                    menuService.goToFirstFormMenu();
                });
            }, function() {
                $scope.loginForm.$setPristine();
                $scope.login = '';
            });
        };
        headerService.setTitle('Login');
    }];