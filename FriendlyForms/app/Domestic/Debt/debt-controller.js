var DebtCtrl = function($scope, $routeParams, $location, debtService, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.debt = debtService.debts.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.debt.Id == 'undefined' || $scope.debt.Id == 0) {
            //see if garlic has something stored            
            $scope.debt = $.jStorage.get($scope.path);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.debtForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#debtForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                $location.path('/Domestic/Asset/' + $scope.debt.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.debt.UserId = $routeParams.userId;
        if (typeof $scope.debt.Id == 'undefined' || $scope.debt.Id == 0) {
            debtService.debts.save(null, $scope.debt, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Asset/' + $scope.debt.UserId);
            });
        } else {
            debtService.debts.update({ Id: $scope.debt.Id }, $scope.debt, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Asset/' + $scope.debt.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive($scope.path)) {
        menuService.setActive($scope.path);
    }
};
DebtCtrl.$inject = ['$scope', '$routeParams', '$location', 'debtService', 'menuService', 'genericService', '$rootScope'];