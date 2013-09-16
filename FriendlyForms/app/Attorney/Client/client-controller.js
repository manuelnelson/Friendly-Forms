var ClientCtrl = ['$scope', '$routeParams', '$location', 'clientService', 'menuService', 'headerService', '$rootScope', function ($scope, $routeParams, $location, clientService, menuService, headerService, $rootScope) {
    $scope.path = $location.path();
    $scope.client = clientService.clients.get({ UserId: $routeParams.userId }, function() {
    });
    $scope.submit = function(noNavigate) {
        if ($scope.clientForm.$invalid) {
            return;
        }
        $scope.client.UserId = $routeParams.userId;
        clientService.clients.update({ Id: $scope.client.Id }, $scope.client, function() {
            menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
            if (!noNavigate)
                menuService.nextMenu();
        });
    };
    $rootScope.currentScope = $scope;
    headerService.refreshPage();
}];