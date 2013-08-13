var HealthInsuranceCtrl = function($scope, $routeParams, $location, healthInsuranceService, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.healthInsurance = healthInsuranceService.healthInsurances.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.healthInsurance.Id == 'undefined' || $scope.healthInsurance.Id == 0) {
            //see if garlic has something stored            
            $scope.healthInsurance = $.jStorage.get($scope.path);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.healthInsuranceForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#healthInsuranceForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                $location.path('/Domestic/Spousal/' + $scope.healthInsurance.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.healthInsurance.UserId = $routeParams.userId;
        if (typeof $scope.healthInsurance.Id == 'undefined' || $scope.healthInsurance.Id == 0) {
            healthInsuranceService.healthInsurances.save(null, $scope.healthInsurance, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Spousal/' + $scope.healthInsurance.UserId);
            });
        } else {
            healthInsuranceService.healthInsurances.update({ Id: $scope.healthInsurance.Id }, $scope.healthInsurance, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Spousal/' + $scope.healthInsurance.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive($scope.path)) {
        menuService.setActive($scope.path);
    }
};
HealthInsuranceCtrl.$inject = ['$scope', '$routeParams', '$location', 'healthInsuranceService', 'menuService', 'genericService', '$rootScope'];