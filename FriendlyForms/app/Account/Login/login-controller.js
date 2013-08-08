var LoginCtrl = function ($scope, $routeParams, $location, loginService) {
    $scope.login = function() {
        loginService.login.post();
    };
};
LoginCtrl.$inject = ['$scope', '$routeParams', '$location', 'loginService'];