var IncomeCtrl = function($scope, $routeParams, $location, incomeService, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.showErrors = false;
    $scope.isLoaded = false;
    $scope.income = incomeService.incomes.get({ UserId: $routeParams.userId, IsOtherParent: $routeParams.isOtherParent }, function () {
        $scope.isLoaded = true;
        if (typeof $scope.income.Id == 'undefined' || $scope.income.Id == 0) {
            //see if garlic has something stored            
            $scope.income = $.jStorage.get($scope.path);
            if ($scope.income)
                $scope.showErrors = true;
        }
    });
    $scope.submit = function(noNavigate) {
        if (!$scope.income || ($scope.income.HaveSalary != "1" && $scope.income.HaveSalary != "2")) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#incomeForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                menuService.nextMenu();
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.income.UserId = $routeParams.userId;
        $scope.income.isOtherParent = $routeParams.isOtherParent;
        if (typeof $scope.income.Id == 'undefined' || $scope.income.Id == 0) {
            incomeService.incomes.save(null, $scope.income, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    menuService.nextMenu();
            });
        } else {
            incomeService.incomes.update({ Id: $scope.income.Id }, $scope.income, function() {
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
IncomeCtrl.$inject = ['$scope', '$routeParams', '$location', 'incomeService', 'menuService', 'genericService', '$rootScope'];