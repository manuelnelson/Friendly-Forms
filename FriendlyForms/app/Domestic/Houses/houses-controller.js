var HousesCtrl = function($scope, $routeParams, $location, housesService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    $scope.houses = housesService.houses.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.houses.Id == 'undefined' || $scope.houses.Id == 0) {
            //see if garlic has something stored            
            $scope.houses = $.jStorage.get($scope.storageKey);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.housesForm.$invalid) {
            menuService.setSubMenuIconClass('Domestic', 'Houses', 'icon-pencil icon-red');
            var value = genericService.getFormInput('#housesForm');
            $.jStorage.set($scope.storageKey, value);
            if (!noNavigate)
                $location.path('/Domestic/Participant/' + $scope.houses.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.storageKey);
        $scope.houses.UserId = $routeParams.userId;
        if (typeof $scope.houses.Id == 'undefined' || $scope.houses.Id == 0) {
            housesService.houses.save(null, $scope.houses, function() {
                menuService.setSubMenuIconClass('Domestic', 'Houses', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Participant/' + $scope.houses.UserId);
            });
        } else {
            housesService.houses.update({ Id: $scope.houses.Id }, $scope.houses, function() {
                menuService.setSubMenuIconClass('Domestic', 'Houses', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Participant/' + $scope.houses.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive('Domestic', 'Houses')) {
        menuService.setActive('Domestic', 'Houses');
    }
};
HousesCtrl.$inject = ['$scope', '$routeParams', '$location', 'housesService', 'menuService', 'genericService', '$rootScope'];