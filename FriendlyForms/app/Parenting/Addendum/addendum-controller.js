var AddendumCtrl = function($scope, $routeParams, $location, addendumService, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.showErrors = false;
    $scope.addendum = addendumService.addendums.get({ UserId: $routeParams.userId }, function () {
        if (typeof $scope.addendum.Id == 'undefined' || $scope.addendum.Id == 0) {
            //see if garlic has something stored            
            $scope.addendum = $.jStorage.get($scope.path);
            if ($scope.addendum)
                $scope.showErrors = true;
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.addendumForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#addendumForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                menuService.nextMenu();
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.addendum.UserId = $routeParams.userId;
        if (typeof $scope.addendum.Id == 'undefined' || $scope.addendum.Id == 0) {
            addendumService.addendums.save(null, $scope.addendum, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    menuService.nextMenu();
            });
        } else {
            addendumService.addendums.update({ Id: $scope.addendum.Id }, $scope.addendum, function() {
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
AddendumCtrl.$inject = ['$scope', '$routeParams', '$location', 'addendumService', 'menuService', 'genericService', '$rootScope'];