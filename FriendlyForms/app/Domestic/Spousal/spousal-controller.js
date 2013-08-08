var SpousalCtrl = function($scope, $routeParams, $location, spousalService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    $scope.spousal = spousalService.spousals.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.spousal.Id == 'undefined' || $scope.spousal.Id == 0) {
            //see if garlic has something stored            
            $scope.spousal = $.jStorage.get($scope.storageKey);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.spousalForm.$invalid) {
            menuService.setSubMenuIconClass('Domestic', 'Spousal', 'icon-pencil icon-red');
            var value = genericService.getFormInput('#spousalForm');
            $.jStorage.set($scope.storageKey, value);
            if (!noNavigate)
                $location.path('/Domestic/Tax/' + $scope.spousal.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.storageKey);
        $scope.spousal.UserId = $routeParams.userId;
        if (typeof $scope.spousal.Id == 'undefined' || $scope.spousal.Id == 0) {
            spousalService.spousals.save(null, $scope.spousal, function() {
                menuService.setSubMenuIconClass('Domestic', 'Spousal', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Tax/' + $scope.spousal.UserId);
            });
        } else {
            spousalService.spousals.update({ Id: $scope.spousal.Id }, $scope.spousal, function() {
                menuService.setSubMenuIconClass('Domestic', 'Spousal', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Domestic/Tax/' + $scope.spousal.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive('Domestic', 'Spousal')) {
        menuService.setActive('Domestic', 'Spousal');
    }
};
SpousalCtrl.$inject = ['$scope', '$routeParams', '$location', 'spousalService', 'menuService', 'genericService', '$rootScope'];