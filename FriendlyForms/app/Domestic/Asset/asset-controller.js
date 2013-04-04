var AssetCtrl = function($scope, $routeParams, $location, assetService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    $scope.asset = assetService.assets.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.asset.Id == 'undefined' || $scope.asset.Id == 0) {
            //see if garlic has something stored            
            $scope.asset = $.jStorage.get($scope.storageKey);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.assetForm.$invalid) {
            menuService.setSubMenuIconClass('Domestic', 'Asset', 'icon-pencil icon-red');
            var value = genericService.getFormInput('#assetForm');
            $.jStorage.set($scope.storageKey, value);
            if (!noNavigate)
                $location.path('/Domestic/Participant/' + $scope.asset.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.storageKey);
        $scope.asset.UserId = $routeParams.userId;
        if (typeof $scope.asset.Id == 'undefined' || $scope.asset.Id == 0) {
            assetService.assets.save(null, $scope.asset, function() {
                menuService.setSubMenuIconClass('Domestic', 'Asset', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Participant/' + $scope.asset.UserId);
            });
        } else {
            assetService.assets.update({ Id: $scope.asset.Id }, $scope.asset, function() {
                menuService.setSubMenuIconClass('Domestic', 'Asset', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Participant/' + $scope.asset.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive('Domestic', 'Asset')) {
        menuService.setActive('Domestic', 'Asset');
    }
};
AssetCtrl.$inject = ['$scope', '$routeParams', '$location', 'assetsService', 'menuService', 'genericService', '$rootScope'];