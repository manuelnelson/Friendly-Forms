var PrivacyCtrl = function($scope, $routeParams, $location, privacyService, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.privacy = privacyService.privacies.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.privacy.Id == 'undefined' || $scope.privacy.Id == 0) {
            //see if garlic has something stored            
            $scope.privacy = $.jStorage.get($scope.path);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.privacyForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#privacyForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                $location.path('/Parenting/Information/' + $scope.privacy.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.privacy.UserId = $routeParams.userId;
        if (typeof $scope.privacy.Id == 'undefined' || $scope.privacy.Id == 0) {
            privacyService.privacies.save(null, $scope.privacy, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Parenting/Information/' + $scope.privacy.UserId);
            });
        } else {
            privacyService.privacies.update({ Id: $scope.privacy.Id }, $scope.privacy, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Parenting/Information/' + $scope.privacy.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive($scope.path)) {
        menuService.setActive($scope.path);
    }
};
PrivacyCtrl.$inject = ['$scope', '$routeParams', '$location', 'privacyService', 'menuService', 'genericService', '$rootScope'];