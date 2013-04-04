var HouseCtrl = function($scope, $routeParams, $location, houseService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    $scope.house = houseService.houses.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.house.Id == 'undefined' || $scope.house.Id == 0) {
            //see if garlic has something stored            
            $scope.house = $.jStorage.get($scope.storageKey);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.houseForm.$invalid) {
            menuService.setSubMenuIconClass('Domestic', 'House', 'icon-pencil icon-red');
            var value = genericService.getFormInput('#houseForm');
            $.jStorage.set($scope.storageKey, value);
            if (!noNavigate)
                $location.path('/Domestic/Participant/' + $scope.house.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.storageKey);
        $scope.house.UserId = $routeParams.userId;
        if (typeof $scope.house.Id == 'undefined' || $scope.house.Id == 0) {
            houseService.houses.save(null, $scope.house, function() {
                menuService.setSubMenuIconClass('Domestic', 'House', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Participant/' + $scope.house.UserId);
            });
        } else {
            houseService.houses.update({ Id: $scope.house.Id }, $scope.house, function() {
                menuService.setSubMenuIconClass('Domestic', 'House', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Participant/' + $scope.house.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive('Domestic', 'House')) {
        menuService.setActive('Domestic', 'House');
    }
};
HouseCtrl.$inject = ['$scope', '$routeParams', '$location', 'housesService', 'menuService', 'genericService', '$rootScope'];