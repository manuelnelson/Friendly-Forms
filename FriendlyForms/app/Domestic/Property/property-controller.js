var PropertyCtrl = function($scope, $routeParams, $location, propertyService, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.showErrors = false;
    $scope.isLoaded = false;
    $scope.property = propertyService.properties.get({ UserId: $routeParams.userId }, function () {
        if (typeof $scope.property.Id == 'undefined' || $scope.property.Id == 0) {
            //see if garlic has something stored            
            $scope.property = $.jStorage.get($scope.path);
            if ($scope.property)
                $scope.showErrors = true;
        }
        $scope.isLoaded = true;
    });
    $scope.submit = function(noNavigate) {
        if ($scope.propertyForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-exclamation icon-red');
            var value = genericService.getFormInput('#propertyForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                menuService.nextMenu();
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.property.UserId = $routeParams.userId;
        if (typeof $scope.property.Id == 'undefined' || $scope.property.Id == 0) {
            propertyService.properties.save(null, $scope.property, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    menuService.nextMenu();
            });
        } else {
            propertyService.properties.update({ Id: $scope.property.Id }, $scope.property, function() {
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
PropertyCtrl.$inject = ['$scope', '$routeParams', '$location', 'propertyService', 'menuService', 'genericService', '$rootScope'];