var SpousalsCtrl = function($scope, $routeParams, $location, spousalsService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    $scope.spousals = spousalsService.spousals.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.spousals.Id == 'undefined' || $scope.spousals.Id == 0) {
            //see if garlic has something stored            
            $scope.spousals = $.jStorage.get($scope.storageKey);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.spousalsForm.$invalid) {
            menuService.setSubMenuIconClass('Domestic', 'Spousals', 'icon-pencil icon-red');
            var value = genericService.getFormInput('#spousalsForm');
            $.jStorage.set($scope.storageKey, value);
            if (!noNavigate)
                $location.path('/Domestic/Participant/' + $scope.spousals.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.storageKey);
        $scope.spousals.UserId = $routeParams.userId;
        if (typeof $scope.spousals.Id == 'undefined' || $scope.spousals.Id == 0) {
            spousalsService.spousals.save(null, $scope.spousals, function() {
                menuService.setSubMenuIconClass('Domestic', 'Spousals', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Participant/' + $scope.spousals.UserId);
            });
        } else {
            spousalsService.spousals.update({ Id: $scope.spousals.Id }, $scope.spousals, function() {
                menuService.setSubMenuIconClass('Domestic', 'Spousals', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Participant/' + $scope.spousals.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive('Domestic', 'Spousals')) {
        menuService.setActive('Domestic', 'Spousals');
    }
};
SpousalsCtrl.$inject = ['$scope', '$routeParams', '$location', 'spousalsService', 'menuService', 'genericService', '$rootScope'];