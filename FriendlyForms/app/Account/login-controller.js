var LoginCtrl = function ($scope, $routeParams, $location, loginService) {
    $scope.login = loginService.login.post();
};
LoginCtrl.$inject = ['$scope', '$routeParams', '$location', 'loginService'];