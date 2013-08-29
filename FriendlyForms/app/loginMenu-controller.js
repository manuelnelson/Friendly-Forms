var LoginMenuCtrl = function ($scope, $routeParams, $location, loginMenuService) {
    $scope.$watch(function () { return loginMenuService.authUser; }, function () {
        $scope.user = loginMenuService.authUser;
    }, true);
    loginMenuService.refresh();    
};
LoginMenuCtrl.$inject = ['$scope', '$routeParams', '$location', 'loginMenuService'];