var HealthInsuranceCtrl = function($scope, $routeParams, $location, healthInsuranceService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    $scope.healthInsurance = healthInsuranceService.healthInsurances.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.healthInsurance.Id == 'undefined' || $scope.healthInsurance.Id == 0) {
            //see if garlic has something stored            
            $scope.healthInsurance = $.jStorage.get($scope.storageKey);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.healthInsuranceForm.$invalid) {
            menuService.setSubMenuIconClass('Domestic', 'HealthInsurance', 'icon-pencil icon-red');
            var value = genericService.getFormInput('#healthInsuranceForm');
            $.jStorage.set($scope.storageKey, value);
            if (!noNavigate)
                $location.path('/Domestic/Participant/' + $scope.healthInsurance.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.storageKey);
        $scope.healthInsurance.UserId = $routeParams.userId;
        if (typeof $scope.healthInsurance.Id == 'undefined' || $scope.healthInsurance.Id == 0) {
            healthInsuranceService.healthInsurances.save(null, $scope.healthInsurance, function() {
                menuService.setSubMenuIconClass('Domestic', 'HealthInsurance', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Participant/' + $scope.healthInsurance.UserId);
            });
        } else {
            healthInsuranceService.healthInsurances.update({ Id: $scope.healthInsurance.Id }, $scope.healthInsurance, function() {
                menuService.setSubMenuIconClass('Domestic', 'HealthInsurance', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Participant/' + $scope.healthInsurance.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive('Domestic', 'HealthInsurance')) {
        menuService.setActive('Domestic', 'HealthInsurance');
    }
};
HealthInsuranceCtrl.$inject = ['$scope', '$routeParams', '$location', 'healthInsurancesService', 'menuService', 'genericService', '$rootScope'];