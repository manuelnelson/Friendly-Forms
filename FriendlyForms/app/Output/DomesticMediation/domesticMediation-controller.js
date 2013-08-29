var DomesticMediationCtrl = function ($scope, $routeParams, $location, domesticMediationService, menuService, genericService, headerService,$rootScope) {
    $scope.storageKey = $location.path();
    domesticMediationService.domesticMediations.get({ UserId: $routeParams.userId }, function (data) {
        $scope.domesticMediation = data;
    });
    $scope.submit = function () {
    };
    $rootScope.currentScope = $scope;
    headerService.hide();
};
DomesticMediationCtrl.$inject = ['$scope', '$routeParams', '$location', 'domesticMediationService', 'menuService', 'genericService', 'headerService','$rootScope'];