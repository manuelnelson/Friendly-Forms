var HouseCtrl = function($scope, $routeParams, $location, houseService, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.house = houseService.houses.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.house.Id == 'undefined' || $scope.house.Id == 0) {
            //see if garlic has something stored            
            $scope.house = $.jStorage.get($scope.path);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.houseForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#houseForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                $location.path('/Domestic/Property/' + $scope.house.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.house.UserId = $routeParams.userId;
        if (typeof $scope.house.Id == 'undefined' || $scope.house.Id == 0) {
            houseService.houses.save(null, $scope.house, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Property/' + $scope.house.UserId);
            });
        } else {
            houseService.houses.update({ Id: $scope.house.Id }, $scope.house, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Property/' + $scope.house.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive($scope.path)) {
        menuService.setActive($scope.path);
    }
};
HouseCtrl.$inject = ['$scope', '$routeParams', '$location', 'houseService', 'menuService', 'genericService', '$rootScope'];