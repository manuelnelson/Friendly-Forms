var PricingCtrl = function($scope, $routeParams, $location, pricingService, menuService, genericService, $rootScope) {
    $scope.showErrors = false;
    $scope.submit = function() {
        if ($scope.pricingForm.$invalid) {
            $scope.showErrors = true;
            return;
        }
        $location.path('/Administrator/RegisterFirm/Subscription/' + $scope.pricing.Subscription);
    };
    $scope.disableAutomaticSubmit = true;

    genericService.refreshPage(function() {
        $rootScope.currentScope = $scope;
    });
};
PricingCtrl.$inject = ['$scope', '$routeParams', '$location', 'pricingService', 'menuService', 'genericService', '$rootScope'];