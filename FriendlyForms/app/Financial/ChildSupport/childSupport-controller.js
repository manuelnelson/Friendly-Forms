var ChildSupportCtrl = function($scope, $routeParams, $location, childSupportService, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.showErrors = false;

    $scope.childSupport = childSupportService.childSupports.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.childSupport.Id == 'undefined' || $scope.childSupport.Id == 0) {
            //see if garlic has something stored            
            $scope.childSupport = $.jStorage.get($scope.path);
            if ($scope.childSupport)
                $scope.showErrors = true;
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.childSupportForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#childSupportForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                menuService.nextMenu();
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.childSupport.UserId = $routeParams.userId;
        if (typeof $scope.childSupport.Id == 'undefined' || $scope.childSupport.Id == 0) {
            childSupportService.childSupports.save(null, $scope.childSupport, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    menuService.nextMenu();
            });
        } else {
            childSupportService.childSupports.update({ Id: $scope.childSupport.Id }, $scope.childSupport, function() {
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
ChildSupportCtrl.$inject = ['$scope', '$routeParams', '$location', 'childSupportService', 'menuService', 'genericService', '$rootScope'];