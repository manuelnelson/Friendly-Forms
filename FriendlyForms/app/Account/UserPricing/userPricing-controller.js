var UserPricingCtrl = ['$scope', '$routeParams', '$location', 'userService', 'menuService', 'headerService',
    function ($scope, $routeParams, $location, userService, menuService, headerService) {
        $scope.showErrors = false;
        $scope.submit = function () {
            if ($scope.pricingForm.$invalid) {
                $scope.showErrors = true;
                return;
            }
            $location.path('/Account/Payment/User/' + $routeParams.userId + '/Amount/' + $scope.pricing.AmountId);
        };
        headerService.setTitle("Pricing");
    }];