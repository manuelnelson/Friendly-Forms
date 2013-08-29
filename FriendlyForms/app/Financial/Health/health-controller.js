﻿var HealthCtrl = function($scope, $routeParams, $location, healthService, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.showErrors = false;
    $scope.health = healthService.healths.get({ UserId: $routeParams.userId }, function () {
        if (typeof $scope.health.Id == 'undefined' || $scope.health.Id == 0) {
            //see if garlic has something stored            
            $scope.health = $.jStorage.get($scope.path);
            if ($scope.health)
                $scope.showErrors = true;
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.healthForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#healthForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                menuService.nextMenu();
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.health.UserId = $routeParams.userId;
        if (typeof $scope.health.Id == 'undefined' || $scope.health.Id == 0) {
            healthService.healths.save(null, $scope.health, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    menuService.nextMenu();
            });
        } else {
            healthService.healths.update({ Id: $scope.health.Id }, $scope.health, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    menuService.nextMenu();
            });
        }
    };
    $rootScope.currentScope = $scope;

    genericService.refreshPage();

};
HealthCtrl.$inject = ['$scope', '$routeParams', '$location', 'healthService', 'menuService', 'genericService', '$rootScope'];