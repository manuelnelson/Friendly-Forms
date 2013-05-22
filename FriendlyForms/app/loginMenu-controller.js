var LoginMenuCtrl = function ($scope, $routeParams, $location, loginMenuService) {
    $scope.user = loginMenuService.userAuth.get();
};
MenuCtrl.$inject = ['$scope', '$routeParams', '$location', 'loginMenuService'];