var HealthCtrl = function($scope, $routeParams, $location, healthService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    $scope.health = healthService.healths.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.health.Id == 'undefined' || $scope.health.Id == 0) {
            //see if garlic has something stored            
            $scope.health = $.jStorage.get($scope.storageKey);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.healthForm.$invalid) {
            menuService.setSubMenuIconClass('Financial', 'Health', 'icon-pencil icon-red');
            var value = genericService.getFormInput('#healthForm');
            $.jStorage.set($scope.storageKey, value);
            if (!noNavigate)
                $location.path('/Financial/Participant/' + $scope.health.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.storageKey);
        $scope.health.UserId = $routeParams.userId;
        if (typeof $scope.health.Id == 'undefined' || $scope.health.Id == 0) {
            healthService.healths.save(null, $scope.health, function() {
                menuService.setSubMenuIconClass('Financial', 'Health', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Financial/Participant/' + $scope.health.UserId);
            });
        } else {
            healthService.healths.update({ Id: $scope.health.Id }, $scope.health, function() {
                menuService.setSubMenuIconClass('Financial', 'Health', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Financial/Participant/' + $scope.health.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive('Financial', 'Health')) {
        menuService.setActive('Financial', 'Health');
    }
};
HealthCtrl.$inject = ['$scope', '$routeParams', '$location', 'healthsService', 'menuService', 'genericService', '$rootScope'];