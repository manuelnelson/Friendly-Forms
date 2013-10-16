var ClientCasesCtrl = ['$scope', '$routeParams', '$location', 'clientCasesService', 'menuService', 'headerService', '$rootScope', 'clientService', 'userService', 'loginMenuService',
    function ($scope, $routeParams, $location, clientCasesService, menuService, headerService, $rootScope, clientService, userService, loginMenuService) {
        $scope.clients = [];
        $scope.adminId = $routeParams.adminId;
        $scope.isLoaded = false;
        userService.getUserData($routeParams.adminId).then(function(data) {
            $scope.admin = data;
            userService.getLawFirmUsers(data.LawFirmId).then(function(lawFirmUsers) {
                $scope.attorneys = lawFirmUsers;
                $scope.isLoaded = true;
            });
        });
        clientService.getClients($routeParams.adminId).then(function(clients) {
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
        loginMenuService.refresh($routeParams.adminId).then(function() {
            //TODO: we really need a way of getting location.path to equal hrefs
            var path = $location.path();
            if (path.indexOf('/#') == -1)
                path = "/#" + path;
            menuService.setActive(path, false);
        });
    }];