var LoginMenuCtrl = ['$scope', '$routeParams', '$location', 'loginMenuService', 'constantsService', function ($scope, $routeParams, $location, loginMenuService, constantsService) {
    $scope.isAdmin = false;
    $scope.isLawyer = false;
    $scope.$watch(function () { return loginMenuService.authUser; }, function () {
        $scope.user = loginMenuService.authUser;
        //Called here to ensure constants initialization
        constantsService.initializeConstants().then(function () {
            if ($scope.user && $scope.user.Roles && $scope.user.Roles.length > 0) {
                $scope.isAdmin = $scope.user.Roles.indexOf(constantsService.constants.AdminRole) > -1;
                if (!$scope.isAdmin)
                    $scope.isLawyer = $scope.user.Roles.indexOf(constantsService.constants.AttorneyRole) > -1;
            }
        });
    }, true);
    loginMenuService.refresh();
}];
