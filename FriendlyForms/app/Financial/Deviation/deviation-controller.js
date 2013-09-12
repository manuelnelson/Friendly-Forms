var DeviationCtrl = function ($scope, $routeParams, $location, deviationService, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.showErrors = false;
    $scope.deviation = deviationService.deviations.get({ UserId: $routeParams.userId }, function () {
        if (typeof $scope.deviation.Id == 'undefined' || $scope.deviation.Id == 0) {
            //see if garlic has something stored            
            $scope.deviation = $.jStorage.get($scope.path);
            if ($scope.deviation)
                $scope.showErrors = true;
        }
    });
    $scope.submit = function (noNavigate) {
        if ($scope.deviationForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#deviationForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                menuService.nextMenu();
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.deviation.UserId = $routeParams.userId;
        if (typeof $scope.deviation.Id == 'undefined' || $scope.deviation.Id == 0) {
            deviationService.deviations.save(null, $scope.deviation, function () {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    menuService.nextMenu();
            });
        } else {
            deviationService.deviations.update({ Id: $scope.deviation.Id }, $scope.deviation, function () {
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
DeviationCtrl.$inject = ['$scope', '$routeParams', '$location', 'deviationService', 'menuService', 'genericService', '$rootScope'];