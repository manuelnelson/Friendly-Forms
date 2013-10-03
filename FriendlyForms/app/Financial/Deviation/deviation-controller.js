var DeviationCtrl = ['$scope', '$routeParams', '$location', 'deviationService', 'menuService', 'genericService', '$rootScope', 'scheduleBService',
    function ($scope, $routeParams, $location, deviationService, menuService, genericService, $rootScope, scheduleBService) {
    $scope.path = $location.path();
    $scope.showErrors = false;
    $scope.isLoaded = false;
    $scope.deviation = deviationService.deviations.get({ UserId: $routeParams.userId }, function () {
        if (typeof $scope.deviation.Id == 'undefined' || $scope.deviation.Id == 0) {
            //see if garlic has something stored            
            $scope.deviation = $.jStorage.get($scope.path);
            if ($scope.deviation)
                $scope.showErrors = true;
        }
        $scope.isLoaded = true;
    });
        scheduleBService.scheduleBs.get({ UserId: $routeParams.userId }, function(data) {
            $scope.IncomeHigherAmount = parseInt(data.ScheduleB.AdjustedSupport) - 30000;
        });
        $scope.submit = function (noNavigate) {
        if ($scope.deviationForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-exclamation icon-red');
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

}];