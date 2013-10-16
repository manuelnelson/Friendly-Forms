var AgreementCtrl = ['$scope', '$routeParams', '$location', 'agreementService', 'menuService', 'headerService', function($scope, $routeParams, $location, agreementService, menuService, headerService) {
    $scope.submit = function() {
        if ($scope.agreementForm.$invalid) {
            return;
        }        
        $location.path('/Administrator/Payment/Admin/' + $routeParams.adminId);
        
    };
    headerService.setTitle('Agreement');
}];