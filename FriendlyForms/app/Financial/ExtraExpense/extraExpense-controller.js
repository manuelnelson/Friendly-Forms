var ExtraExpenseCtrl = function($scope, $routeParams, $location, extraExpenseService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    $scope.extraExpense = extraExpenseService.extraExpenses.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.extraExpense.Id == 'undefined' || $scope.extraExpense.Id == 0) {
            //see if garlic has something stored            
            $scope.extraExpense = $.jStorage.get($scope.storageKey);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.extraExpenseForm.$invalid) {
            menuService.setSubMenuIconClass('Financial', 'ExtraExpense', 'icon-pencil icon-red');
            var value = genericService.getFormInput('#extraExpenseForm');
            $.jStorage.set($scope.storageKey, value);
            if (!noNavigate)
                $location.path('/Financial/Participant/' + $scope.extraExpense.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.storageKey);
        $scope.extraExpense.UserId = $routeParams.userId;
        if (typeof $scope.extraExpense.Id == 'undefined' || $scope.extraExpense.Id == 0) {
            extraExpenseService.extraExpenses.save(null, $scope.extraExpense, function() {
                menuService.setSubMenuIconClass('Financial', 'ExtraExpense', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Financial/Participant/' + $scope.extraExpense.UserId);
            });
        } else {
            extraExpenseService.extraExpenses.update({ Id: $scope.extraExpense.Id }, $scope.extraExpense, function() {
                menuService.setSubMenuIconClass('Financial', 'ExtraExpense', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Financial/Participant/' + $scope.extraExpense.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive('Financial', 'ExtraExpense')) {
        menuService.setActive('Financial', 'ExtraExpense');
    }
};
ExtraExpenseCtrl.$inject = ['$scope', '$routeParams', '$location', 'extraExpenseService', 'menuService', 'genericService', '$rootScope'];