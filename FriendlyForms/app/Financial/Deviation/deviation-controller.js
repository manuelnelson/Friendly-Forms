var DeviationCtrl = function($scope, $routeParams, $location, deviationService, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.deviation = deviationService.deviations.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.deviation.Id == 'undefined' || $scope.deviation.Id == 0) {
            //see if garlic has something stored            
            $scope.deviation = $.jStorage.get($scope.path);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.deviationForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#deviationForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                $location.path('/Financial/Participant/' + $scope.deviation.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.deviation.UserId = $routeParams.userId;
        if (typeof $scope.deviation.Id == 'undefined' || $scope.deviation.Id == 0) {
            deviationService.deviations.save(null, $scope.deviation, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Financial/Participant/' + $scope.deviation.UserId);
            });
        } else {
            deviationService.deviations.update({ Id: $scope.deviation.Id }, $scope.deviation, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Financial/Participant/' + $scope.deviation.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive($scope.path)) {
        menuService.setActive($scope.path);
    }
};
DeviationCtrl.$inject = ['$scope', '$routeParams', '$location', 'deviationService', 'menuService', 'genericService', '$rootScope'];