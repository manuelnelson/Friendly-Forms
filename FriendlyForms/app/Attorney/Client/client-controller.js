var ClientCtrl = ['$scope', '$routeParams', '$location', 'clientService', 'menuService', 'headerService', 'userService', 'courtService', '$rootScope',
function ($scope, $routeParams, $location, clientService, menuService, headerService, userService, courtService, $rootScope) {
    $scope.userAuth = userService.getUserAuth($routeParams.userId);
    $scope.user = userService.getUserData($routeParams.userId);
    $scope.court = courtService.courts.get({ UserId: $routeParams.userId }, function () {
    });

    clientService.attorneys.getList({ ClientUserId: $routeParams.userId }, function (data) {
        $scope.authorizedPeople = data;
        if (typeof data != 'undefined' && data.length > 0) {
            userService.getUserData(data[0].UserId).then(function (user) {
                userService.getLawFirmUsers(user.LawFirmId).then(function(lawFirmUsers) {
                    $scope.lawFirmUsers = lawFirmUsers;
                });
            });
        }
    });

    $scope.submit = function (noNavigate) {
        if ($scope.clientForm.$invalid) {
            return;
        }
    };
    $scope.notify = function ($event, person) {
        person.NotificationsEnabled = $event.target.checked;
        clientService.clients.update(null, person, function () {
        });
    };
    $rootScope.currentScope = $scope;
    headerService.setTitle("Client Profile");
}];