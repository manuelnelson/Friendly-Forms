var HealthInsuranceCtrl = function($scope, $routeParams, $location, healthInsuranceService, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.showErrors = false;
    $scope.healthInsurance = healthInsuranceService.healthInsurances.get({ UserId: $routeParams.userId }, function () {
        if (typeof $scope.healthInsurance.Id == 'undefined' || $scope.healthInsurance.Id == 0) {
            //see if garlic has something stored            
            $scope.healthInsurance = $.jStorage.get($scope.path);
            if ($scope.healthInsurance)
                $scope.showErrors = true;
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.healthInsuranceForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#healthInsuranceForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                menuService.nextMenu();
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.healthInsurance.UserId = $routeParams.userId;
        if (typeof $scope.healthInsurance.Id == 'undefined' || $scope.healthInsurance.Id == 0) {
            healthInsuranceService.healthInsurances.save(null, $scope.healthInsurance, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    menuService.nextMenu();
            });
        } else {
            healthInsuranceService.healthInsurances.update({ Id: $scope.healthInsurance.Id }, $scope.healthInsurance, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    menuService.nextMenu();
            });
        }
    };
    $rootScope.currentScope = $scope;
    genericService.refreshPage();

    genericService.refreshPage();


};
HealthInsuranceCtrl.$inject = ['$scope', '$routeParams', '$location', 'healthInsuranceService', 'menuService', 'genericService', '$rootScope'];