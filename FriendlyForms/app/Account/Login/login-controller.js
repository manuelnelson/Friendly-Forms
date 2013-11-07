var LoginCtrl = ['$scope', '$routeParams', '$location', 'loginService', 'headerService', 'genericService',
    function ($scope, $routeParams, $location, loginService, headerService, genericService) {
        $scope.submit = function () {
            loginService.login($scope.login, function() {
                    $scope.loginForm.$setPristine();
                    $scope.login = '';
            });
        }; 
        genericService.refreshPage();
    }];