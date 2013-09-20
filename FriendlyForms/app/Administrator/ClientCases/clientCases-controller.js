var ClientCasesCtrl = ['$scope', '$routeParams', '$location', 'clientCasesService', 'menuService', 'headerService', '$rootScope', 'clientService', 'userService',
    function ($scope, $routeParams, $location, clientCasesService, menuService, headerService, $rootScope, clientService, userService) {
        $scope.clients = [];
        $scope.userId = $routeParams.userId;
        $scope.isLoaded = false;
        userService.getUserData($routeParams.userId).then(function(data) {
            $scope.admin = data;
            userService.getLawFirmUsers(data.LawFirmId).then(function(lawFirmUsers) {
                $scope.attorneys = lawFirmUsers;
                $scope.isLoaded = true;
            });
        });
        clientService.getClients($routeParams.userId).then(function(clients) {
            $scope.clients = clients;
        });
        $scope.openClient = function (client) {
            $location.path('/Attorney/Client/' + client.ClientUserId);
        };
        $scope.archiveClient = function (client) {

        };
        $scope.openAttorney = function(attorney) {
            $location.path('/Attorney/AttorneyPage/Attorney/' + attorney.Id);
        };
        headerService.setTitle("Administrator");

    }];