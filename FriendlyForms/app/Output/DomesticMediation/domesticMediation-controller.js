var DomesticMediationCtrl = function ($scope, $routeParams, $location, domesticMediationService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    domesticMediationService.domesticMediations.get({ UserId: $routeParams.userId }, function (data) {
        $scope.domesticMediation = data;
    });
    $scope.submit = function () {
    };
    $rootScope.currentScope = $scope;
    //if (!menuService.isActive('Output', 'DomesticMediation')) {
    //    menuService.setActive('Output', 'DomesticMediation');
    //}
};
DomesticMediationCtrl.$inject = ['$scope', '$routeParams', '$location', 'domesticMediationService', 'menuService', 'genericService', '$rootScope'];