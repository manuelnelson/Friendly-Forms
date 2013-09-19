var ResponsibilityCtrl = function($scope, $routeParams, $location, responsibilityService, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.showErrors = false;
    $scope.isLoaded = false;
    $scope.responsibility = responsibilityService.responsibilities.get({ UserId: $routeParams.userId }, function () {
        $scope.isLoaded = true;
        if (typeof $scope.responsibility.Id == 'undefined' || $scope.responsibility.Id == 0) {
            //see if garlic has something stored            
            $scope.responsibility = $.jStorage.get($scope.path);
            if ($scope.responsibility)
                $scope.showErrors = true;
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.responsibilityForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#responsibilityForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                menuService.nextMenu();
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.responsibility.UserId = $routeParams.userId;
        if (typeof $scope.responsibility.Id == 'undefined' || $scope.responsibility.Id == 0) {
            responsibilityService.responsibilities.save(null, $scope.responsibility, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    menuService.nextMenu();
            });
        } else {
            responsibilityService.responsibilities.update({ Id: $scope.responsibility.Id }, $scope.responsibility, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    menuService.nextMenu();
            });
        }
    };
    $scope.CalculatePercentage = function(parent) {
        if (parent === 'Mother') {
            $scope.responsibility.FatherPercentage = genericService.calculateRemainingPercentage($scope.responsibility.MotherPercentage);
        } else {
            $scope.responsibility.MotherPercentage = genericService.calculateRemainingPercentage($scope.responsibility.FatherPercentage);
        }
    }
    genericService.refreshPage(function () {
        $rootScope.currentScope = $scope;
    });

};
ResponsibilityCtrl.$inject = ['$scope', '$routeParams', '$location', 'responsibilityService', 'menuService', 'genericService', '$rootScope'];