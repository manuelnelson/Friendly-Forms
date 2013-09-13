var AttorneyPageCtrl = ['$scope', '$routeParams', '$location', 'attorneyPageService', 'userService', 'menuService', 'headerService', '$rootScope',
    function ($scope, $routeParams, $location, attorneyPageService, userService, menuService, headerService, $rootScope) {
        $scope.clients = [];
        $scope.userId = $routeParams.userId;
        attorneyPageService.attorneyClients.getList({ UserId: $routeParams.userId }, function (attorneyClients) {
            //this is just a list of id's of the clients.  Fetch these
            var userIds = _.pluck(attorneyClients, 'ClientUserId');
            if (userIds && userIds.length > 0) {
                userService.userAuths.getList({ Ids: userIds }, function (clientsUserInfo) {
                    var clients = _.map(attorneyClients, function(item) {
                        var clientUserInfo = _.find(clientsUserInfo, function(userInfo) {
                            return userInfo.Id === item.ClientUserId;
                        });
                        return {
                            UserId: item.UserId,
                            ClientUserId: item.ClientUserId,
                            CaseNumber: item.CaseNumber,
                            Name: clientUserInfo.DisplayName,
                            Date: clientUserInfo.Date,
                        };
                    });
                    $scope.clients = clients;
                });
            }
        });

        //$rootScope.currentScope = $scope;
        headerService.setTitle("Attorney Page");
    }];