var LoginCtrl = ['$scope', '$routeParams', '$location', 'loginService', 'headerService',
    function ($scope, $routeParams, $location, loginService, headerService) {
        $scope.submit = function () {
            loginService.login($scope.login, function() {
                    $scope.loginForm.$setPristine();
                    $scope.login = '';
            });
        }; 
        headerService.setTitle('Login');
    }];