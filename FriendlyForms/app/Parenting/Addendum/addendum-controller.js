var AddendumCtrl = function($scope, $routeParams, $location, addendumService, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.addendum = addendumService.addendums.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.addendum.Id == 'undefined' || $scope.addendum.Id == 0) {
            //see if garlic has something stored            
            $scope.addendum = $.jStorage.get($scope.path);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.addendumForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#addendumForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                $location.path('/Parenting/Participant/' + $scope.addendum.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.addendum.UserId = $routeParams.userId;
        if (typeof $scope.addendum.Id == 'undefined' || $scope.addendum.Id == 0) {
            addendumService.addendums.save(null, $scope.addendum, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Parenting/Participant/' + $scope.addendum.UserId);
            });
        } else {
            addendumService.addendums.update({ Id: $scope.addendum.Id }, $scope.addendum, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Parenting/Participant/' + $scope.addendum.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive($scope.path)) {
        menuService.setActive($scope.path);
    }
};
AddendumCtrl.$inject = ['$scope', '$routeParams', '$location', 'addendumService', 'menuService', 'genericService', '$rootScope'];