var DebtCtrl = function($scope, $routeParams, $location, debtService, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.showErrors = false;
    $scope.debt = debtService.debts.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.debt.Id == 'undefined' || $scope.debt.Id == 0) {
            //see if garlic has something stored            
            $scope.debt = $.jStorage.get($scope.path);
            if ($scope.debt)
                $scope.showErrors = true;
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.debtForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#debtForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                menuService.nextMenu();
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.debt.UserId = $routeParams.userId;
        if (typeof $scope.debt.Id == 'undefined' || $scope.debt.Id == 0) {
            debtService.debts.save(null, $scope.debt, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    menuService.nextMenu();
            });
        } else {
            debtService.debts.update({ Id: $scope.debt.Id }, $scope.debt, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    menuService.nextMenu();
            });
        }
    };
    genericService.refreshPage(function () {
        $rootScope.currentScope = $scope;
    });
};
DebtCtrl.$inject = ['$scope', '$routeParams', '$location', 'debtService', 'menuService', 'genericService', '$rootScope'];