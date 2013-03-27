var PropertiesCtrl = function($scope, $routeParams, $location, propertiesService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    $scope.properties = propertiesService.properties.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.properties.Id == 'undefined' || $scope.properties.Id == 0) {
            //see if garlic has something stored            
            $scope.properties = $.jStorage.get($scope.storageKey);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.propertiesForm.$invalid) {
            menuService.setSubMenuIconClass('Domestic', 'Properties', 'icon-pencil icon-red');
            var value = genericService.getFormInput('#propertiesForm');
            $.jStorage.set($scope.storageKey, value);
            if (!noNavigate)
                $location.path('/Domestic/Participant/' + $scope.properties.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.storageKey);
        $scope.properties.UserId = $routeParams.userId;
        if (typeof $scope.properties.Id == 'undefined' || $scope.properties.Id == 0) {
            propertiesService.properties.save(null, $scope.properties, function() {
                menuService.setSubMenuIconClass('Domestic', 'Properties', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Participant/' + $scope.properties.UserId);
            });
        } else {
            propertiesService.properties.update({ Id: $scope.properties.Id }, $scope.properties, function() {
                menuService.setSubMenuIconClass('Domestic', 'Properties', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Participant/' + $scope.properties.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive('Domestic', 'Properties')) {
        menuService.setActive('Domestic', 'Properties');
    }
};
PropertiesCtrl.$inject = ['$scope', '$routeParams', '$location', 'propertiesService', 'menuService', 'genericService', '$rootScope'];