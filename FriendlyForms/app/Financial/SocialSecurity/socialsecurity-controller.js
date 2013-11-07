var SocialSecurityCtrl = function($scope, $routeParams, $location, socialSecurityService, menuService, genericService, userService, $rootScope) {
    $scope.path = $location.path();
    $scope.showErrors = false;
    $scope.isLoaded = false;
    $scope.socialSecurity = socialSecurityService.socialSecurities.get({ UserId: $routeParams.userId, IsOtherParent: $routeParams.isOtherParent }, function () {
        $scope.isLoaded = true;
        if (typeof $scope.socialSecurity.Id == 'undefined' || $scope.socialSecurity.Id == 0) {
            //see if garlic has something stored            
            $scope.socialSecurity = $.jStorage.get($scope.path);
            if ($scope.socialSecurity)
                $scope.showErrors = true;
        }
    });
    $scope.submit = function (noNavigate) {
        if (!$scope.socialSecurity || ($scope.socialSecurity.ReceiveSocial != 1 && $scope.socialSecurity.ReceiveSocial != 2)) {
            menuService.setSubMenuIconClass($scope.path, 'icon-exclamation icon-red');
            var value = genericService.getFormInput('#socialSecurityForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                menuService.nextMenu();
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.socialSecurity.UserId = userService.getFormUserId();
        $scope.socialSecurity.IsOtherParent = $routeParams.isOtherParent;
        if (typeof $scope.socialSecurity.Id == 'undefined' || $scope.socialSecurity.Id == 0) {
            socialSecurityService.socialSecurities.save(null, $scope.socialSecurity, function () {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    menuService.nextMenu();
            });
        } else {
            socialSecurityService.socialSecurities.update({ Id: $scope.socialSecurity.Id }, $scope.socialSecurity, function () {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    menuService.nextMenu();
            });
        }
    };
    genericService.refreshPage(function () {
        $rootScope.currentScope = $scope;
    });

};
SocialSecurityCtrl.$inject = ['$scope', '$routeParams', '$location', 'socialSecurityService', 'menuService', 'genericService', 'userService', '$rootScope'];