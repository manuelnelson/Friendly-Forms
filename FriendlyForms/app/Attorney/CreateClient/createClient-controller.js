var CreateClientCtrl = ['$scope', '$routeParams', '$location', 'clientService', 'courtService', 'headerService', 'userService',
    function ($scope, $routeParams, $location, clientService, courtService, headerService, userService) {

    $scope.submit = function() {
        if ($scope.createClientForm.$invalid) {
            return;
        }
        if($scope.user.Email.indexOf('@' < 0))
            $scope.user.Email = $scope.user.Email + '@ourlawfirm.com';
        userService.register.save(null, $scope.user, function (userAuth) {
            var court = {
                UserId: userAuth.UserId,
                County: 0,
                CaseNumber: $scope.court.CaseNumber,
                AuthorOfPlan: 1,
                PlanType: 1
            };
            //link client to attorney
            var clientAttorney = {
                UserId: $routeParams.userId,
                ClientUserId: userAuth.UserId
            };
            clientService.clients.save(null, clientAttorney, function () {
                courtService.courts.save(null, court, function() {
                    $location.path('/Attorney/Case/' + clientAttorney.ClientUserId);
                });
            });
        });
    };
    headerService.setTitle('Create A Client');
}];