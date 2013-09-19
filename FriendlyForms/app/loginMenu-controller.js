var LoginMenuCtrl = ['$scope', '$routeParams', '$location', 'loginMenuService', 'constantsService', function ($scope, $routeParams, $location, loginMenuService, constantsService) {
    //Called here to ensure constants initialization
    constantsService.initializeConstants();

    $scope.$watch(function () { return loginMenuService.authUser; }, function () {
        $scope.user = loginMenuService.authUser;
    }, true);
    loginMenuService.refresh();    
}];
