var AssetCtrl = function($scope, $routeParams, $location, assetService, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.asset = assetService.assets.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.asset.Id == 'undefined' || $scope.asset.Id == 0) {
            //see if garlic has something stored            
            $scope.asset = $.jStorage.get($scope.path);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.assetForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#assetForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                $location.path('/Domestic/HealthInsurance/' + $scope.asset.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.asset.UserId = $routeParams.userId;
        if (typeof $scope.asset.Id == 'undefined' || $scope.asset.Id == 0) {
            assetService.assets.save(null, $scope.asset, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/HealthInsurance/' + $scope.asset.UserId);
            });
        } else {
            assetService.assets.update({ Id: $scope.asset.Id }, $scope.asset, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/HealthInsurance/' + $scope.asset.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive($scope.path)) {
        menuService.setActive($scope.path);
    }
};
AssetCtrl.$inject = ['$scope', '$routeParams', '$location', 'assetService', 'menuService', 'genericService', '$rootScope'];