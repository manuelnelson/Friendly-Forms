var AssetsCtrl = function($scope, $routeParams, $location, assetsService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    $scope.assets = assetsService.assets.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.assets.Id == 'undefined' || $scope.assets.Id == 0) {
            //see if garlic has something stored            
            $scope.assets = $.jStorage.get($scope.storageKey);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.assetsForm.$invalid) {
            menuService.setSubMenuIconClass('Domestic', 'Assets', 'icon-pencil icon-red');
            var value = genericService.getFormInput('#assetsForm');
            $.jStorage.set($scope.storageKey, value);
            if (!noNavigate)
                $location.path('/Domestic/Participant/' + $scope.assets.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.storageKey);
        $scope.assets.UserId = $routeParams.userId;
        if (typeof $scope.assets.Id == 'undefined' || $scope.assets.Id == 0) {
            assetsService.assets.save(null, $scope.assets, function() {
                menuService.setSubMenuIconClass('Domestic', 'Assets', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Participant/' + $scope.assets.UserId);
            });
        } else {
            assetsService.assets.update({ Id: $scope.assets.Id }, $scope.assets, function() {
                menuService.setSubMenuIconClass('Domestic', 'Assets', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Participant/' + $scope.assets.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive('Domestic', 'Assets')) {
        menuService.setActive('Domestic', 'Assets');
    }
};
AssetsCtrl.$inject = ['$scope', '$routeParams', '$location', 'assetsService', 'menuService', 'genericService', '$rootScope'];