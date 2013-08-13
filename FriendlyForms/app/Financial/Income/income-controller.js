var IncomeCtrl = function($scope, $routeParams, $location, incomeService, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.income = incomeService.incomes.get({ UserId: $routeParams.userId, IsOtherParent: $routeParams.isOtherParent }, function () {
        if (typeof $scope.income.Id == 'undefined' || $scope.income.Id == 0) {
            //see if garlic has something stored            
            $scope.income = $.jStorage.get($scope.path);
        }
    });
    $scope.submit = function(noNavigate) {
        var isOtherParent = $routeParams.isOtherParent;
        if ($scope.incomeForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#incomeForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                $location.path('/Financial/SocialSecurity/' + $scope.income.UserId + "/" + isOtherParent);
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.income.UserId = $routeParams.userId;
        if (typeof $scope.income.Id == 'undefined' || $scope.income.Id == 0) {
            incomeService.incomes.save(null, $scope.income, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Financial/SocialSecurity/' + $scope.income.UserId + "/" + isOtherParent);
            });
        } else {
            incomeService.incomes.update({ Id: $scope.income.Id }, $scope.income, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Financial/SocialSecurity/' + $scope.income.UserId + "/" + isOtherParent);
            });
        }
    };
    $rootScope.currentScope = $scope;
    var test = $scope.path;
    if (!menuService.isActive($scope.path)) {
        menuService.setActive($scope.path);
    }
};
IncomeCtrl.$inject = ['$scope', '$routeParams', '$location', 'incomeService', 'menuService', 'genericService', '$rootScope'];