var InformationCtrl = function($scope, $routeParams, $location, informationService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    $scope.information = informationService.information.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.information.Id == 'undefined' || $scope.information.Id == 0) {
            //see if garlic has something stored            
            $scope.information = $.jStorage.get($scope.storageKey);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.informationForm.$invalid) {
            menuService.setSubMenuIconClass('Parenting', 'Information', 'icon-pencil icon-red');
            var value = genericService.getFormInput('#informationForm');
            $.jStorage.set($scope.storageKey, value);
            if (!noNavigate)
                $location.path('/Parenting/Participant/' + $scope.information.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.storageKey);
        $scope.information.UserId = $routeParams.userId;
        if (typeof $scope.information.Id == 'undefined' || $scope.information.Id == 0) {
            informationService.information.save(null, $scope.information, function() {
                menuService.setSubMenuIconClass('Parenting', 'Information', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Parenting/Participant/' + $scope.information.UserId);
            });
        } else {
            informationService.information.update({ Id: $scope.information.Id }, $scope.information, function() {
                menuService.setSubMenuIconClass('Parenting', 'Information', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Parenting/Participant/' + $scope.information.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive('Parenting', 'Information')) {
        menuService.setActive('Parenting', 'Information');
    }
};
InformationCtrl.$inject = ['$scope', '$routeParams', '$location', 'informationService', 'menuService', 'genericService', '$rootScope'];