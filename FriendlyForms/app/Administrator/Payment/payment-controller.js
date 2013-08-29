var PaymentCtrl = function($scope, $routeParams, $location, paymentService, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.payment = paymentService.payment.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.payment.Id == 'undefined' || $scope.payment.Id == 0) {
            //see if garlic has something stored            
            $scope.payment = $.jStorage.get($scope.path);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.paymentForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#paymentForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                menuService.nextMenu();
            return;
        }
        $.jStorage.deleteKey($scope.storageKey);
        $scope.payment.UserId = $routeParams.userId;
        if (typeof $scope.payment.Id == 'undefined' || $scope.payment.Id == 0) {
            paymentService.payment.save(null, $scope.payment, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    menuService.nextMenu();
            });
        } else {
            paymentService.payment.update({ Id: $scope.payment.Id }, $scope.payment, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    menuService.nextMenu();
            });
        }
    };
    $rootScope.currentScope = $scope;
    genericService.refreshPage();
};
PaymentCtrl.$inject = ['$scope', '$routeParams', '$location', 'paymentService', 'menuService', 'genericService', '$rootScope'];