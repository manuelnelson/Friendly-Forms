var SupportCtrl = function($scope, $routeParams, $location, supportService, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.support = supportService.supports.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.support.Id == 'undefined' || $scope.support.Id == 0) {
            //see if garlic has something stored            
            $scope.support = $.jStorage.get($scope.path);
        }
    });
    $scope.submit = function (noNavigate) {
        var isOtherParent = $routeParams.isOtherParent;
        if ($scope.supportForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#supportForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                $location.path('/Financial/OtherChildren/' + $scope.support.UserId + "/" + isOtherParent);
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.support.UserId = $routeParams.userId;
        if (typeof $scope.support.Id == 'undefined' || $scope.support.Id == 0) {
            supportService.supportsFixedPosition.save(null, $scope.support, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Financial/OtherChildren/' + $scope.support.UserId + "/" + isOtherParent);
            });
        } else {
            supportService.supportsFixedPosition.update({ Id: $scope.support.Id }, $scope.support, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Financial/OtherChildren/' + $scope.support.UserId + "/" + isOtherParent);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive($scope.path)) {
        menuService.setActive($scope.path);
    }
};
SupportCtrl.$inject = ['$scope', '$routeParams', '$location', 'supportService', 'menuService', 'genericService', '$rootScope'];