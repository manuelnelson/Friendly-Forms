var SupportCtrl = function($scope, $routeParams, $location, supportService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    $scope.support = supportService.supports.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.support.Id == 'undefined' || $scope.support.Id == 0) {
            //see if garlic has something stored            
            $scope.support = $.jStorage.get($scope.storageKey);
        }
    });
    $scope.submit = function (noNavigate) {
        var isOtherParent = $routeParams.isOtherParent;
        if ($scope.supportForm.$invalid) {
            menuService.setSubMenuIconClass('Financial', 'Support', 'icon-pencil icon-red');
            var value = genericService.getFormInput('#supportForm');
            $.jStorage.set($scope.storageKey, value);
            if (!noNavigate)
                $location.path('/Financial/OtherChildren/' + $scope.support.UserId + "/" + isOtherParent);
            return;
        }
        $.jStorage.deleteKey($scope.storageKey);
        $scope.support.UserId = $routeParams.userId;
        if (typeof $scope.support.Id == 'undefined' || $scope.support.Id == 0) {
            supportService.supportsFixedPosition.save(null, $scope.support, function() {
                menuService.setSubMenuIconClass('Financial', 'Support', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Financial/OtherChildren/' + $scope.support.UserId + "/" + isOtherParent);
            });
        } else {
            supportService.supportsFixedPosition.update({ Id: $scope.support.Id }, $scope.support, function() {
                menuService.setSubMenuIconClass('Financial', 'Support', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Financial/OtherChildren/' + $scope.support.UserId + "/" + isOtherParent);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive('Financial', 'Support')) {
        menuService.setActive('Financial', 'Support');
    }
};
SupportCtrl.$inject = ['$scope', '$routeParams', '$location', 'supportService', 'menuService', 'genericService', '$rootScope'];