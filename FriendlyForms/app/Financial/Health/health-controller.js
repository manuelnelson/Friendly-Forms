var HealthCtrl = function($scope, $routeParams, $location, healthService, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.health = healthService.healths.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.health.Id == 'undefined' || $scope.health.Id == 0) {
            //see if garlic has something stored            
            $scope.health = $.jStorage.get($scope.path);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.healthForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#healthForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                $location.path('/Financial/Income/' + $scope.health.UserId + "/false");
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.health.UserId = $routeParams.userId;
        if (typeof $scope.health.Id == 'undefined' || $scope.health.Id == 0) {
            healthService.healths.save(null, $scope.health, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Financial/Income/' + $scope.health.UserId + "/false");
            });
        } else {
            healthService.healths.update({ Id: $scope.health.Id }, $scope.health, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Financial/Income/' + $scope.health.UserId + "/false");
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive($scope.path)) {
        menuService.setActive($scope.path);
    }
};
HealthCtrl.$inject = ['$scope', '$routeParams', '$location', 'healthService', 'menuService', 'genericService', '$rootScope'];