var CaseCtrl = ['$scope', '$routeParams', '$location', 'caseService', 'menuService', 'genericService', '$rootScope', function($scope, $routeParams, $location, caseService, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.case = caseService.case.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.case.Id == 'undefined' || $scope.case.Id == 0) {
            //see if garlic has something stored            
            $scope.case = $.jStorage.get($scope.path);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.caseForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#caseForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                menuService.nextMenu();
            return;
        }
        $.jStorage.deleteKey($scope.storageKey);
        $scope.case.UserId = $routeParams.userId;
        if (typeof $scope.case.Id == 'undefined' || $scope.case.Id == 0) {
            caseService.case.save(null, $scope.case, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    menuService.nextMenu();
            });
        } else {
            caseService.case.update({ Id: $scope.case.Id }, $scope.case, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    menuService.nextMenu();
            });
        }
    };
    $rootScope.currentScope = $scope;
    genericService.refreshPage();
}];