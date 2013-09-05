var LoginCtrl = function ($scope, $routeParams, $location, loginService, headerService) {
    $scope.login = function() {
        loginService.login.post();
    };
    headerService.setTitle('Login');
};
LoginCtrl.$inject = ['$scope', '$routeParams', '$location', 'loginService', 'headerService'];