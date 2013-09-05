var PricingCtrl = function($scope, $routeParams, $location, pricingService, menuService, headerService, $rootScope) {
    $scope.showErrors = false;
    $scope.submit = function() {
        if ($scope.pricingForm.$invalid) {
            $scope.showErrors = true;
            return;
        }
        $location.path('/Administrator/RegisterFirm/Subscription/' + $scope.pricing.Subscription);
    };
    $rootScope.currentScope = $scope;
    headerService.setTitle("Pricing");
};
PricingCtrl.$inject = ['$scope', '$routeParams', '$location', 'pricingService', 'menuService', 'headerService', '$rootScope'];