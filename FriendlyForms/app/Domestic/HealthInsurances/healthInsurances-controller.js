var HealthInsurancesCtrl = function($scope, $routeParams, $location, healthInsurancesService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    $scope.healthInsurances = healthInsurancesService.healthInsurances.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.healthInsurances.Id == 'undefined' || $scope.healthInsurances.Id == 0) {
            //see if garlic has something stored            
            $scope.healthInsurances = $.jStorage.get($scope.storageKey);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.healthInsurancesForm.$invalid) {
            menuService.setSubMenuIconClass('Domestic', 'HealthInsurances', 'icon-pencil icon-red');
            var value = genericService.getFormInput('#healthInsurancesForm');
            $.jStorage.set($scope.storageKey, value);
            if (!noNavigate)
                $location.path('/Domestic/Participant/' + $scope.healthInsurances.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.storageKey);
        $scope.healthInsurances.UserId = $routeParams.userId;
        if (typeof $scope.healthInsurances.Id == 'undefined' || $scope.healthInsurances.Id == 0) {
            healthInsurancesService.healthInsurances.save(null, $scope.healthInsurances, function() {
                menuService.setSubMenuIconClass('Domestic', 'HealthInsurances', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Participant/' + $scope.healthInsurances.UserId);
            });
        } else {
            healthInsurancesService.healthInsurances.update({ Id: $scope.healthInsurances.Id }, $scope.healthInsurances, function() {
                menuService.setSubMenuIconClass('Domestic', 'HealthInsurances', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Participant/' + $scope.healthInsurances.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive('Domestic', 'HealthInsurances')) {
        menuService.setActive('Domestic', 'HealthInsurances');
    }
};
HealthInsurancesCtrl.$inject = ['$scope', '$routeParams', '$location', 'healthInsurancesService', 'menuService', 'genericService', '$rootScope'];