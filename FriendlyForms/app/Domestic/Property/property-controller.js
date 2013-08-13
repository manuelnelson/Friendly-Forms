var PropertyCtrl = function($scope, $routeParams, $location, propertyService, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.property = propertyService.properties.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.property.Id == 'undefined' || $scope.property.Id == 0) {
            //see if garlic has something stored            
            $scope.property = $.jStorage.get($scope.path);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.propertyForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#propertyForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                $location.path('/Domestic/Vehicle/' + $scope.property.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.property.UserId = $routeParams.userId;
        if (typeof $scope.property.Id == 'undefined' || $scope.property.Id == 0) {
            propertyService.properties.save(null, $scope.property, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Vehicle/' + $scope.property.UserId);
            });
        } else {
            propertyService.properties.update({ Id: $scope.property.Id }, $scope.property, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Vehicle/' + $scope.property.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive($scope.path)) {
        menuService.setActive($scope.path);
    }
};
PropertyCtrl.$inject = ['$scope', '$routeParams', '$location', 'propertyService', 'menuService', 'genericService', '$rootScope'];