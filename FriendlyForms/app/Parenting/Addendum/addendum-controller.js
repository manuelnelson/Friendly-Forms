var AddendumCtrl = function($scope, $routeParams, $location, addendumService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    $scope.addendum = addendumService.addendums.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.addendum.Id == 'undefined' || $scope.addendum.Id == 0) {
            //see if garlic has something stored            
            $scope.addendum = $.jStorage.get($scope.storageKey);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.addendumForm.$invalid) {
            menuService.setSubMenuIconClass('Parenting', 'Addendum', 'icon-pencil icon-red');
            var value = genericService.getFormInput('#addendumForm');
            $.jStorage.set($scope.storageKey, value);
            if (!noNavigate)
                $location.path('/Parenting/Participant/' + $scope.addendum.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.storageKey);
        $scope.addendum.UserId = $routeParams.userId;
        if (typeof $scope.addendum.Id == 'undefined' || $scope.addendum.Id == 0) {
            addendumService.addendums.save(null, $scope.addendum, function() {
                menuService.setSubMenuIconClass('Parenting', 'Addendum', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Parenting/Participant/' + $scope.addendum.UserId);
            });
        } else {
            addendumService.addendums.update({ Id: $scope.addendum.Id }, $scope.addendum, function() {
                menuService.setSubMenuIconClass('Parenting', 'Addendum', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Parenting/Participant/' + $scope.addendum.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive('Parenting', 'Addendum')) {
        menuService.setActive('Parenting', 'Addendum');
    }
};
AddendumCtrl.$inject = ['$scope', '$routeParams', '$location', 'addendumService', 'menuService', 'genericService', '$rootScope'];