var DeviationCtrl = function($scope, $routeParams, $location, deviationService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    $scope.deviation = deviationService.deviations.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.deviation.Id == 'undefined' || $scope.deviation.Id == 0) {
            //see if garlic has something stored            
            $scope.deviation = $.jStorage.get($scope.storageKey);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.deviationForm.$invalid) {
            menuService.setSubMenuIconClass('Financial', 'Deviation', 'icon-pencil icon-red');
            var value = genericService.getFormInput('#deviationForm');
            $.jStorage.set($scope.storageKey, value);
            if (!noNavigate)
                $location.path('/Financial/Participant/' + $scope.deviation.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.storageKey);
        $scope.deviation.UserId = $routeParams.userId;
        if (typeof $scope.deviation.Id == 'undefined' || $scope.deviation.Id == 0) {
            deviationService.deviations.save(null, $scope.deviation, function() {
                menuService.setSubMenuIconClass('Financial', 'Deviation', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Financial/Participant/' + $scope.deviation.UserId);
            });
        } else {
            deviationService.deviations.update({ Id: $scope.deviation.Id }, $scope.deviation, function() {
                menuService.setSubMenuIconClass('Financial', 'Deviation', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Financial/Participant/' + $scope.deviation.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive('Financial', 'Deviation')) {
        menuService.setActive('Financial', 'Deviation');
    }
};
DeviationCtrl.$inject = ['$scope', '$routeParams', '$location', 'deviationService', 'menuService', 'genericService', '$rootScope'];