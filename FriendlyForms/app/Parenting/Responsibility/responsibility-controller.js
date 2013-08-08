var ResponsibilityCtrl = function($scope, $routeParams, $location, responsibilityService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    $scope.responsibility = responsibilityService.responsibilities.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.responsibility.Id == 'undefined' || $scope.responsibility.Id == 0) {
            //see if garlic has something stored            
            $scope.responsibility = $.jStorage.get($scope.storageKey);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.responsibilityForm.$invalid) {
            menuService.setSubMenuIconClass('Parenting', 'Responsibility', 'icon-pencil icon-red');
            var value = genericService.getFormInput('#responsibilityForm');
            $.jStorage.set($scope.storageKey, value);
            if (!noNavigate)
                $location.path('/Parenting/Communication/' + $scope.responsibility.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.storageKey);
        $scope.responsibility.UserId = $routeParams.userId;
        if (typeof $scope.responsibility.Id == 'undefined' || $scope.responsibility.Id == 0) {
            responsibilityService.responsibilities.save(null, $scope.responsibility, function() {
                menuService.setSubMenuIconClass('Parenting', 'Responsibility', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Parenting/Communication/' + $scope.responsibility.UserId);
            });
        } else {
            responsibilityService.responsibilities.update({ Id: $scope.responsibility.Id }, $scope.responsibility, function() {
                menuService.setSubMenuIconClass('Parenting', 'Responsibility', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Parenting/Communication/' + $scope.responsibility.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive('Parenting', 'Responsibility')) {
        menuService.setActive('Parenting', 'Responsibility');
    }
};
ResponsibilityCtrl.$inject = ['$scope', '$routeParams', '$location', 'responsibilityService', 'menuService', 'genericService', '$rootScope'];