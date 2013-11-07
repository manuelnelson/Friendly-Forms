var HealthCtrl = function($scope, $routeParams, $location, healthService, menuService, genericService, userService, $rootScope) {
    $scope.path = $location.path();
    $scope.showErrors = false;
    $scope.isLoaded = false;
    $scope.health = healthService.healths.get({ UserId: $routeParams.userId }, function () {
        if (typeof $scope.health.Id == 'undefined' || $scope.health.Id == 0) {
            //see if garlic has something stored            
            $scope.health = $.jStorage.get($scope.path);
            if ($scope.health)
                $scope.showErrors = true;
        }
        $scope.isLoaded = true;
    });
    $scope.submit = function(noNavigate) {
        if ($scope.healthForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-exclamation icon-red');
            var value = genericService.getFormInput('#healthForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                menuService.nextMenu();
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.health.UserId = userService.getFormUserId();
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
    genericService.refreshPage(function () {
        $rootScope.currentScope = $scope;
    });

};
HealthCtrl.$inject = ['$scope', '$routeParams', '$location', 'healthService', 'menuService', 'genericService', 'userService', '$rootScope'];