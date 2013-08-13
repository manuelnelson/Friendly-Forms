var InformationCtrl = function($scope, $routeParams, $location, informationService, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.information = informationService.information.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.information.Id == 'undefined' || $scope.information.Id == 0) {
            //see if garlic has something stored            
            $scope.information = $.jStorage.get($scope.path);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.informationForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#informationForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                $location.path('/Parenting/Decision/' + $scope.information.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.information.UserId = $routeParams.userId;
        if (typeof $scope.information.Id == 'undefined' || $scope.information.Id == 0) {
            informationService.information.save(null, $scope.information, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Parenting/Decision/' + $scope.information.UserId);
            });
        } else {
            informationService.information.update({ Id: $scope.information.Id }, $scope.information, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Parenting/Decision/' + $scope.information.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive($scope.path)) {
        menuService.setActive($scope.path);
    }
};
InformationCtrl.$inject = ['$scope', '$routeParams', '$location', 'informationService', 'menuService', 'genericService', '$rootScope'];