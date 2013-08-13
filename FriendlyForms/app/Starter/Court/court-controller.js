var CourtCtrl = function ($scope, $routeParams, $location, courtService, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.court = courtService.court.get({ UserId: $routeParams.userId }, function () {        
        if (typeof $scope.court.Id == 'undefined' || $scope.court.Id == 0) {
            //see if garlic has something stored            
            $scope.court = $.jStorage.get($scope.path);
        }
    });
    $scope.submit = function (noNavigate) {
        if ($scope.courtForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');            
            var value = genericService.getFormInput('#courtForm');
            $.jStorage.set($scope.path, value);
            if(!noNavigate)
                $location.path('/Starter/Participant/' + $scope.court.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.court.UserId = $routeParams.userId;
        if (typeof $scope.court.Id == 'undefined' || $scope.court.Id == 0) {
            courtService.court.save(null, $scope.court, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Starter/Participant/' + $scope.court.UserId);
            });
        } else {
            courtService.court.update({ Id: $scope.court.Id }, $scope.court, function () {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Starter/Participant/' + $scope.court.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive($scope.path)) {
        menuService.setActive($scope.path);
    }
};
CourtCtrl.$inject = ['$scope', '$routeParams', '$location', 'courtService', 'menuService', 'genericService', '$rootScope'];