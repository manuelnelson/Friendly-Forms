var TaxesCtrl = function($scope, $routeParams, $location, taxesService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    $scope.taxes = taxesService.taxes.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.taxes.Id == 'undefined' || $scope.taxes.Id == 0) {
            //see if garlic has something stored            
            $scope.taxes = $.jStorage.get($scope.storageKey);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.taxesForm.$invalid) {
            menuService.setSubMenuIconClass('Domestic', 'Taxes', 'icon-pencil icon-red');
            var value = genericService.getFormInput('#taxesForm');
            $.jStorage.set($scope.storageKey, value);
            if (!noNavigate)
                $location.path('/Domestic/Participant/' + $scope.taxes.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.storageKey);
        $scope.taxes.UserId = $routeParams.userId;
        if (typeof $scope.taxes.Id == 'undefined' || $scope.taxes.Id == 0) {
            taxesService.taxes.save(null, $scope.taxes, function() {
                menuService.setSubMenuIconClass('Domestic', 'Taxes', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Participant/' + $scope.taxes.UserId);
            });
        } else {
            taxesService.taxes.update({ Id: $scope.taxes.Id }, $scope.taxes, function() {
                menuService.setSubMenuIconClass('Domestic', 'Taxes', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Participant/' + $scope.taxes.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive('Domestic', 'Taxes')) {
        menuService.setActive('Domestic', 'Taxes');
    }
};
TaxesCtrl.$inject = ['$scope', '$routeParams', '$location', 'taxesService', 'menuService', 'genericService', '$rootScope'];