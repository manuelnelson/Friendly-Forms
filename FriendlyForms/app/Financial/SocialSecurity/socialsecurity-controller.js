var SocialSecurityCtrl = function($scope, $routeParams, $location, socialSecurityService, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.socialSecurity = socialSecurityService.socialSecurities.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.socialSecurity.Id == 'undefined' || $scope.socialSecurity.Id == 0) {
            //see if garlic has something stored            
            $scope.socialSecurity = $.jStorage.get($scope.path);
        }
    });
    $scope.submit = function (noNavigate) {
        var isOtherParent = $routeParams.isOtherParent;
        if ($scope.socialSecurityForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#socialSecurityForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                $location.path('/Financial/Support/' + $scope.socialSecurity.UserId + "/" + isOtherParent);
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.socialSecurity.UserId = $routeParams.userId;
        $scope.socialSecurity.IsOtherParent = $routeParams.isOtherParent;
        if (typeof $scope.socialSecurity.Id == 'undefined' || $scope.socialSecurity.Id == 0) {
            socialSecurityService.socialSecurities.save(null, $scope.socialSecurity, function () {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Financial/Support/' + $scope.socialSecurity.UserId + "/" + isOtherParent);
            });
        } else {
            socialSecurityService.socialSecurities.update({ Id: $scope.socialSecurity.Id }, $scope.socialSecurity, function () {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Financial/Support/' + $scope.socialSecurity.UserId + "/" + isOtherParent);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive($scope.path)) {
        menuService.setActive($scope.path);
    }
};
SocialSecurityCtrl.$inject = ['$scope', '$routeParams', '$location', 'socialSecurityService', 'menuService', 'genericService', '$rootScope'];