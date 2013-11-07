var AgreementCtrl = ['$scope', '$routeParams', '$location', 'agreementService', 'menuService', 'genericService',
    function ($scope, $routeParams, $location, agreementService, menuService, genericService) {
        $scope.submit = function () {
            if ($scope.agreementForm.$invalid) {
                return;
            }
            $location.path('/Administrator/Payment/Admin/' + $routeParams.adminId + '/Subscription/' + $routeParams.subscription);
        };
        genericService.refreshPage();
    }];