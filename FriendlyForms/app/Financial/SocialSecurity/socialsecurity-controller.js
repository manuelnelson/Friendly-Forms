var SocialSecurityCtrl = function($scope, $routeParams, $location, socialSecurityService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    $scope.socialSecurity = socialSecurityService.socialsecurities.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.socialSecurity.Id == 'undefined' || $scope.socialSecurity.Id == 0) {
            //see if garlic has something stored            
            $scope.socialSecurity = $.jStorage.get($scope.storageKey);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.socialSecurityForm.$invalid) {
            menuService.setSubMenuIconClass('Financial', 'SocialSecurity', 'icon-pencil icon-red');
            var value = genericService.getFormInput('#socialSecurityForm');
            $.jStorage.set($scope.storageKey, value);
            if (!noNavigate)
                $location.path('/Financial/Participant/' + $scope.socialSecurity.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.storageKey);
        $scope.socialSecurity.UserId = $routeParams.userId;
        if (typeof $scope.socialSecurity.Id == 'undefined' || $scope.socialSecurity.Id == 0) {
            socialSecurityService.socialsecurities.save(null, $scope.socialSecurity, function() {
                menuService.setSubMenuIconClass('Financial', 'SocialSecurity', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Financial/Participant/' + $scope.socialSecurity.UserId);
            });
        } else {
            socialSecurityService.socialsecurities.update({ Id: $scope.socialSecurity.Id }, $scope.socialSecurity, function() {
                menuService.setSubMenuIconClass('Financial', 'SocialSecurity', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Financial/Participant/' + $scope.socialSecurity.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive('Financial', 'SocialSecurity')) {
        menuService.setActive('Financial', 'SocialSecurity');
    }
};
SocialSecurityCtrl.$inject = ['$scope', '$routeParams', '$location', 'socialsecuritiesService', 'menuService', 'genericService', '$rootScope'];