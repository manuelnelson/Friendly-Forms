var OtherChildCtrl = function($scope, $routeParams, $location, otherChildService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    $scope.otherChild = otherChildService.otherChildren.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.otherChild.Id == 'undefined' || $scope.otherChild.Id == 0) {
            //see if garlic has something stored            
            $scope.otherChild = $.jStorage.get($scope.storageKey);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.otherChildForm.$invalid) {
            menuService.setSubMenuIconClass('Financial', 'OtherChild', 'icon-pencil icon-red');
            var value = genericService.getFormInput('#otherChildForm');
            $.jStorage.set($scope.storageKey, value);
            if (!noNavigate)
                $location.path('/Financial/Participant/' + $scope.otherChild.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.storageKey);
        $scope.otherChild.UserId = $routeParams.userId;
        if (typeof $scope.otherChild.Id == 'undefined' || $scope.otherChild.Id == 0) {
            otherChildService.otherChildren.save(null, $scope.otherChild, function() {
                menuService.setSubMenuIconClass('Financial', 'OtherChild', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Financial/Participant/' + $scope.otherChild.UserId);
            });
        } else {
            otherChildService.otherChildren.update({ Id: $scope.otherChild.Id }, $scope.otherChild, function() {
                menuService.setSubMenuIconClass('Financial', 'OtherChild', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Financial/Participant/' + $scope.otherChild.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive('Financial', 'OtherChild')) {
        menuService.setActive('Financial', 'OtherChild');
    }
};
OtherChildCtrl.$inject = ['$scope', '$routeParams', '$location', 'otherChildrenService', 'menuService', 'genericService', '$rootScope'];