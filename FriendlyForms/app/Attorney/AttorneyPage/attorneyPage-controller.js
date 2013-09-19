﻿var AttorneyPageCtrl = ['$scope', '$routeParams', '$location', 'attorneyPageService', 'userService', 'menuService', 'headerService', 'clientService',
    function ($scope, $routeParams, $location, attorneyPageService, userService, menuService, headerService, clientService) {
        $scope.clients = [];
        $scope.userId = $routeParams.userId;
        clientService.getClients($routeParams.userId).then(function (clients) {
            $scope.clients = clients;
        });

        $scope.openClient = function (client) {
            $location.path('/Attorney/Client/' + client.ClientUserId);
        };
        $scope.archiveClient = function(client) {

        };
        headerService.setTitle("Attorney Page");
    }];