var SpousalCtrl = function($scope, $routeParams, $location, spousalService, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.spousal = spousalService.spousals.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.spousal.Id == 'undefined' || $scope.spousal.Id == 0) {
            //see if garlic has something stored            
            $scope.spousal = $.jStorage.get($scope.path);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.spousalForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#spousalForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                $location.path('/Domestic/Tax/' + $scope.spousal.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.spousal.UserId = $routeParams.userId;
        if (typeof $scope.spousal.Id == 'undefined' || $scope.spousal.Id == 0) {
            spousalService.spousals.save(null, $scope.spousal, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Tax/' + $scope.spousal.UserId);
            });
        } else {
            spousalService.spousals.update({ Id: $scope.spousal.Id }, $scope.spousal, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Tax/' + $scope.spousal.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive($scope.path)) {
        menuService.setActive($scope.path);
    }
};
SpousalCtrl.$inject = ['$scope', '$routeParams', '$location', 'spousalService', 'menuService', 'genericService', '$rootScope'];