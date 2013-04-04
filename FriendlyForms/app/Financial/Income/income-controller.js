var IncomeCtrl = function($scope, $routeParams, $location, incomeService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    $scope.income = incomeService.incomes.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.income.Id == 'undefined' || $scope.income.Id == 0) {
            //see if garlic has something stored            
            $scope.income = $.jStorage.get($scope.storageKey);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.incomeForm.$invalid) {
            menuService.setSubMenuIconClass('Financial', 'Income', 'icon-pencil icon-red');
            var value = genericService.getFormInput('#incomeForm');
            $.jStorage.set($scope.storageKey, value);
            if (!noNavigate)
                $location.path('/Financial/Participant/' + $scope.income.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.storageKey);
        $scope.income.UserId = $routeParams.userId;
        if (typeof $scope.income.Id == 'undefined' || $scope.income.Id == 0) {
            incomeService.incomes.save(null, $scope.income, function() {
                menuService.setSubMenuIconClass('Financial', 'Income', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Financial/Participant/' + $scope.income.UserId);
            });
        } else {
            incomeService.incomes.update({ Id: $scope.income.Id }, $scope.income, function() {
                menuService.setSubMenuIconClass('Financial', 'Income', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Financial/Participant/' + $scope.income.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive('Financial', 'Income')) {
        menuService.setActive('Financial', 'Income');
    }
};
IncomeCtrl.$inject = ['$scope', '$routeParams', '$location', 'incomesService', 'menuService', 'genericService', '$rootScope'];