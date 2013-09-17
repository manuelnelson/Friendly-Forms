var IntroductionCtrl = ['$scope', '$routeParams', '$location', 'menuService', 'genericService', '$rootScope', function ($scope, $routeParams, $location, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.submit = function (noNavigate) {
        menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
        menuService.nextMenu();
    };
    genericService.refreshPage(function () {
        $rootScope.currentScope = $scope;
    });
}];