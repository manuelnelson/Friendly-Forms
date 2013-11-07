﻿var AssetCtrl = function($scope, $routeParams, $location, assetService, menuService, genericService, userService, $rootScope) {
    $scope.path = $location.path();
    $scope.showErrors = false;
    $scope.isLoaded = false;
    $scope.asset = assetService.assets.get({ UserId: $routeParams.userId }, function () {
        if (typeof $scope.asset.Id == 'undefined' || $scope.asset.Id == 0) {
            //see if garlic has something stored            
            $scope.asset = $.jStorage.get($scope.path);
            if ($scope.asset)
                $scope.showErrors = true;
        }
        $scope.isLoaded = true;
    });
    $scope.submit = function(noNavigate) {
        if ($scope.assetForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-exclamation icon-red');
            var value = genericService.getFormInput('#assetForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                menuService.nextMenu();
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.asset.UserId = userService.getFormUserId();
        if (typeof $scope.asset.Id == 'undefined' || $scope.asset.Id == 0) {
            assetService.assets.save(null, $scope.asset, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    menuService.nextMenu();
            });
        } else {
            assetService.assets.update({ Id: $scope.asset.Id }, $scope.asset, function() {
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
AssetCtrl.$inject = ['$scope', '$routeParams', '$location', 'assetService', 'menuService', 'genericService', 'userService', '$rootScope'];