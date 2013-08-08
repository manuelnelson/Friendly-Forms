var VehicleCtrl = function($scope, $routeParams, $location, vehicleService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    $scope.model = vehicleService.vehicles.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.model.Id == 'undefined' || $scope.model.Id == 0) {
            //see if garlic has something stored            
            $scope.model = $.jStorage.get($scope.storageKey);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.modelForm.$invalid) {
            menuService.setSubMenuIconClass('Domestic', 'Vehicle', 'icon-pencil icon-red');
            var value = genericService.getFormInput('#modelForm');
            $.jStorage.set($scope.storageKey, value);
            if (!noNavigate)
                $location.path('/Domestic/Debt/' + $scope.model.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.storageKey);
        $scope.model.UserId = $routeParams.userId;
        if (typeof $scope.model.Id == 'undefined' || $scope.model.Id == 0) {
            vehicleService.vehicles.save(null, $scope.model, function() {
                menuService.setSubMenuIconClass('Domestic', 'Vehicle', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Debt/' + $scope.model.UserId);
            });
        } else {
            vehicleService.vehicles.update({ Id: $scope.model.Id }, $scope.model, function() {
                menuService.setSubMenuIconClass('Domestic', 'Vehicle', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Debt/' + $scope.model.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive('Domestic', 'Vehicle')) {
        menuService.setActive('Domestic', 'Vehicle');
    }
};
VehicleCtrl.$inject = ['$scope', '$routeParams', '$location', 'vehicleService', 'menuService', 'genericService', '$rootScope'];