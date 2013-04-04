var TaxCtrl = function($scope, $routeParams, $location, taxService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    $scope.tax = taxService.taxes.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.tax.Id == 'undefined' || $scope.tax.Id == 0) {
            //see if garlic has something stored            
            $scope.tax = $.jStorage.get($scope.storageKey);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.taxForm.$invalid) {
            menuService.setSubMenuIconClass('Domestic', 'Tax', 'icon-pencil icon-red');
            var value = genericService.getFormInput('#taxForm');
            $.jStorage.set($scope.storageKey, value);
            if (!noNavigate)
                $location.path('/Domestic/Participant/' + $scope.tax.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.storageKey);
        $scope.tax.UserId = $routeParams.userId;
        if (typeof $scope.tax.Id == 'undefined' || $scope.tax.Id == 0) {
            taxService.taxes.save(null, $scope.tax, function() {
                menuService.setSubMenuIconClass('Domestic', 'Tax', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Participant/' + $scope.tax.UserId);
            });
        } else {
            taxService.taxes.update({ Id: $scope.tax.Id }, $scope.tax, function() {
                menuService.setSubMenuIconClass('Domestic', 'Tax', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Participant/' + $scope.tax.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive('Domestic', 'Tax')) {
        menuService.setActive('Domestic', 'Tax');
    }
};
TaxCtrl.$inject = ['$scope', '$routeParams', '$location', 'taxesService', 'menuService', 'genericService', '$rootScope'];