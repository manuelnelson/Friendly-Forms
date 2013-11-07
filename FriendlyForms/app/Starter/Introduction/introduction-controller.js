var IntroductionCtrl = ['$scope', '$routeParams', '$location', 'menuService', 'genericService', '$rootScope', function ($scope, $routeParams, $location, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.submit = function () {
        menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
        menuService.nextMenu();
    };
    $scope.disableAutomaticSubmit = true;
    genericService.refreshPage(function () {
        $rootScope.currentScope = $scope;
    });
}];