var AttorneyPageCtrl = ['$scope', '$routeParams', '$location', 'attorneyPageService', 'userService', 'genericService', 'clientService', 'loginMenuService',
    function ($scope, $routeParams, $location, attorneyPageService, userService, genericService, clientService, loginMenuService) {
        $scope.clients = [];
        $scope.attorneyId = $routeParams.attorneyId;
        clientService.getClients($routeParams.attorneyId).then(function (clients) {
            $scope.clients = clients;
        });
        userService.getUserAuth($routeParams.attorneyId).then(function (userAuth) {
            $scope.userAuth = userAuth;
        });

        $scope.openClient = function (client) {
            $location.path('/Attorney/Client/' + client.ClientUserId);
        };
        $scope.archiveClient = function(client) {

        };
        genericService.refreshPage();
        loginMenuService.refresh();

    }];