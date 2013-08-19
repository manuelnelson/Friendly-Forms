var TaxCtrl = function($scope, $routeParams, $location, taxService, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.tax = taxService.taxes.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.tax.Id == 'undefined' || $scope.tax.Id == 0) {
            //see if garlic has something stored            
            $scope.tax = $.jStorage.get($scope.path);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.taxForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#taxForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                $location.path('/Output/FormComplete/DomesticMediation/user/' + $scope.tax.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.tax.UserId = $routeParams.userId;
        if (typeof $scope.tax.Id == 'undefined' || $scope.tax.Id == 0) {
            taxService.taxes.save(null, $scope.tax, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Output/FormComplete/DomesticMediation/user/' + $scope.tax.UserId);
            });
        } else {
            taxService.taxes.update({ Id: $scope.tax.Id }, $scope.tax, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Output/FormComplete/DomesticMediation/user/' + $scope.tax.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive($scope.path)) {
        menuService.setActive($scope.path);
    }
};
TaxCtrl.$inject = ['$scope', '$routeParams', '$location', 'taxService', 'menuService', 'genericService', '$rootScope'];