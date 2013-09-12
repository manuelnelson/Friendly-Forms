var ClientCasesCtrl = ['$scope', '$routeParams', '$location', 'clientCasesService', 'menuService', 'genericService', '$rootScope',
    function ($scope, $routeParams, $location, clientCasesService, menuService, genericService, $rootScope) {
        $scope.path = $location.path();
        $scope.clientCases = clientCasesService.clientCases.get({ UserId: $routeParams.userId }, function () {
            if (typeof $scope.clientCases.Id == 'undefined' || $scope.clientCases.Id == 0) {
                //see if garlic has something stored            
                $scope.clientCases = $.jStorage.get($scope.path);
            }
        });
        $scope.submit = function (noNavigate) {
            if ($scope.clientCasesForm.$invalid) {
                menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
                var value = genericService.getFormInput('#clientCasesForm');
                $.jStorage.set($scope.path, value);
                if (!noNavigate)
                    menuService.nextMenu();
                return;
            }
            $.jStorage.deleteKey($scope.storageKey);
            $scope.clientCases.UserId = $routeParams.userId;
            if (typeof $scope.clientCases.Id == 'undefined' || $scope.clientCases.Id == 0) {
                clientCasesService.clientCases.save(null, $scope.clientCases, function () {
                    menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                    if (!noNavigate)
                        menuService.nextMenu();
                });
            } else {
                clientCasesService.clientCases.update({ Id: $scope.clientCases.Id }, $scope.clientCases, function () {
                    menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                    if (!noNavigate)
                        menuService.nextMenu();
                });
            }
        };
        genericService.refreshPage();
    }];