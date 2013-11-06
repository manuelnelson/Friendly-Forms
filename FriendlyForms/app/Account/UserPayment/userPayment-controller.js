﻿var UserPaymentCtrl = ['$scope', '$routeParams', '$location', 'paymentService', 'menuService', 'genericService', 'headerService', 'loginMenuService',
    function ($scope, $routeParams, $location, paymentService, menuService, genericService, headerService, loginMenuService) {
    headerService.setTitle('Payment');
    $scope.submit = function () {
        if ($scope.paymentForm.$invalid) {
            return;
        }
        $scope.payment.AmountId = $routeParams.amountId;
        paymentService.oneTime.save(null, $scope.payment, function () {            
            loginMenuService.refresh();
            $location.path('/');
        });
    };
}];