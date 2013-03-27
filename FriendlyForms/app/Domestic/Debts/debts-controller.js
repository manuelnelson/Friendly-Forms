var DebtsCtrl = function($scope, $routeParams, $location, debtsService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    $scope.debts = debtsService.debts.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.debts.Id == 'undefined' || $scope.debts.Id == 0) {
            //see if garlic has something stored            
            $scope.debts = $.jStorage.get($scope.storageKey);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.debtsForm.$invalid) {
            menuService.setSubMenuIconClass('Domestic', 'Debts', 'icon-pencil icon-red');
            var value = genericService.getFormInput('#debtsForm');
            $.jStorage.set($scope.storageKey, value);
            if (!noNavigate)
                $location.path('/Domestic/Participant/' + $scope.debts.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.storageKey);
        $scope.debts.UserId = $routeParams.userId;
        if (typeof $scope.debts.Id == 'undefined' || $scope.debts.Id == 0) {
            debtsService.debts.save(null, $scope.debts, function() {
                menuService.setSubMenuIconClass('Domestic', 'Debts', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Participant/' + $scope.debts.UserId);
            });
        } else {
            debtsService.debts.update({ Id: $scope.debts.Id }, $scope.debts, function() {
                menuService.setSubMenuIconClass('Domestic', 'Debts', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Participant/' + $scope.debts.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive('Domestic', 'Debts')) {
        menuService.setActive('Domestic', 'Debts');
    }
};
DebtsCtrl.$inject = ['$scope', '$routeParams', '$location', 'debtsService', 'menuService', 'genericService', '$rootScope'];