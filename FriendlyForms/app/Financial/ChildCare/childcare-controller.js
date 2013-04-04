var ChildCareCtrl = function($scope, $routeParams, $location, childCareService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    $scope.childCare = childCareService.childCares.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.childCare.Id == 'undefined' || $scope.childCare.Id == 0) {
            //see if garlic has something stored            
            $scope.childCare = $.jStorage.get($scope.storageKey);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.childCareForm.$invalid) {
            menuService.setSubMenuIconClass('Financial', 'ChildCare', 'icon-pencil icon-red');
            var value = genericService.getFormInput('#childCareForm');
            $.jStorage.set($scope.storageKey, value);
            if (!noNavigate)
                $location.path('/Financial/Participant/' + $scope.childCare.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.storageKey);
        $scope.childCare.UserId = $routeParams.userId;
        if (typeof $scope.childCare.Id == 'undefined' || $scope.childCare.Id == 0) {
            childCareService.childCares.save(null, $scope.childCare, function() {
                menuService.setSubMenuIconClass('Financial', 'ChildCare', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Financial/Participant/' + $scope.childCare.UserId);
            });
        } else {
            childCareService.childCares.update({ Id: $scope.childCare.Id }, $scope.childCare, function() {
                menuService.setSubMenuIconClass('Financial', 'ChildCare', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Financial/Participant/' + $scope.childCare.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive('Financial', 'ChildCare')) {
        menuService.setActive('Financial', 'ChildCare');
    }
};
ChildCareCtrl.$inject = ['$scope', '$routeParams', '$location', 'childCaresService', 'menuService', 'genericService', '$rootScope'];