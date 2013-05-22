var LoginCtrl = function ($scope, $routeParams, $location, loginService) {
    $scope.login = loginService.login.post();
};
MenuCtrl.$inject = ['$scope', '$routeParams', '$location', 'loginService'];