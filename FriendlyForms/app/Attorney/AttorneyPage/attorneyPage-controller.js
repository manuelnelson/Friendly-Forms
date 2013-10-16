var AttorneyPageCtrl = ['$scope', '$routeParams', '$location', 'attorneyPageService', 'userService', 'menuService', 'headerService', 'clientService', 'loginMenuService',
    function ($scope, $routeParams, $location, attorneyPageService, userService, menuService, headerService, clientService, loginMenuService) {
        $scope.clients = [];
        $scope.attorneyId = $routeParams.attorneyId;
        clientService.getClients($routeParams.attorneyId).then(function (clients) {
            $scope.clients = clients;
        });

        $scope.openClient = function (client) {
            $location.path('/Attorney/Client/' + client.ClientUserId);
        };
        $scope.archiveClient = function(client) {

        };
        headerService.setTitle("Attorney Page");
        loginMenuService.refresh();

    }];