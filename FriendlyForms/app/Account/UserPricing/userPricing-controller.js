var UserPricingCtrl = ['$scope', '$routeParams', '$location', 'genericService',
    function ($scope, $routeParams, $location, genericService) {
        $scope.showErrors = false;
        $scope.submit = function () {
            if ($scope.pricingForm.$invalid) {
                $scope.showErrors = true;
                return;
            }
            $location.path('/Account/Payment/User/' + $routeParams.userId + '/Amount/' + $scope.pricing.AmountId);
        };
        genericService.refreshPage();
    }];