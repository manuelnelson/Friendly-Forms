﻿var ClientCtrl = ['$scope', '$routeParams', '$location', 'clientService', 'menuService', 'headerService', 'userService', 'courtService', '$rootScope',
function ($scope, $routeParams, $location, clientService, menuService, headerService, userService, courtService, $rootScope) {
    $scope.userAuth = userService.getUserAuth($routeParams.userId);
    $scope.user = userService.getUserData($routeParams.userId);
    $scope.court = courtService.courts.get({ UserId: $routeParams.userId }, function () {
    });

    clientService.attorneys.getList({ ClientUserId: $routeParams.userId }, function (data) {
        $scope.authorizedPeople = data;
        var authorizedUsersIds = _.pluck(data, 'UserId');
        if (typeof data != 'undefined' && data.length > 0) {
            userService.getUserData(data[0].UserId).then(function (user) {
                userService.getLawFirmUsers(user.LawFirmId).then(function (lawFirmUsers) {
                    //Checks if lawfirm user already exists in authorized people list (has access)
                    _.each(lawFirmUsers, function (item) {                        
                        item.hasAccess = authorizedUsersIds.indexOf(item.Id) > -1;                        
                    });
                    $scope.lawFirmUsers = lawFirmUsers;
                });
            });
        }
    });
    $scope.allowAccess = function(person) {
        if (person.hasAccess) {
            //post user to attorneyclient table
            clientService.clients.save(null, {
                UserId: person.Id,
                ClientUserId: $routeParams.userId,
                ChangeNotification: false,
                PrintNotification: false
            }, function (client) {
                clientService.attorneys.get({ Id: client.Id }, function (attorney) {
                    $scope.authorizedPeople.push(attorney);
                });
            });
        } else {
            //delete user from attorney client table
            var attorneyClient = _.find($scope.authorizedPeople, function(item) {
                return item.UserId == person.Id;
            });
            clientService.clients.delete(attorneyClient, function () {
                $scope.authorizedPeople = _.reject($scope.authorizedPeople, function (item) {
                    return item.UserId == person.Id;
                });
            });
        }
    };
    $scope.submit = function (noNavigate) {
        if ($scope.clientForm.$invalid) {
            return;
        }
    };
    $scope.notify = function (person) {
        clientService.clients.update(null, person, function () {
        });
    };
    $rootScope.currentScope = $scope;
    headerService.setTitle("Client Profile");
}];