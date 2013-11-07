var TaxCtrl = function($scope, $routeParams, $location, taxService, menuService, genericService, userService, $rootScope) {
    $scope.path = $location.path();
    $scope.showErrors = false;
    $scope.isLoaded = false;
    $scope.tax = taxService.taxes.get({ UserId: $routeParams.userId }, function () {
        if (typeof $scope.tax.Id == 'undefined' || $scope.tax.Id == 0) {
            //see if garlic has something stored            
            $scope.tax = $.jStorage.get($scope.path);
            if ($scope.tax)
                $scope.showErrors = true;
        }
        $scope.isLoaded = true;
    });
    $scope.submit = function(noNavigate) {
        if ($scope.taxForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-exclamation icon-red');
            var value = genericService.getFormInput('#taxForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                menuService.nextMenu();
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.tax.UserId = userService.getFormUserId();
        if (typeof $scope.tax.Id == 'undefined' || $scope.tax.Id == 0) {
            taxService.taxes.save(null, $scope.tax, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    menuService.nextMenu();
            });
        } else {
            taxService.taxes.update({ Id: $scope.tax.Id }, $scope.tax, function() {
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
TaxCtrl.$inject = ['$scope', '$routeParams', '$location', 'taxService', 'menuService', 'genericService', 'userService', '$rootScope'];