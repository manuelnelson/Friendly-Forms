var CreateClientCtrl = ['$scope', '$routeParams', '$location', 'clientService', 'courtService', 'headerService', 'userService',
    function ($scope, $routeParams, $location, clientService, courtService, headerService, userService) {

        $scope.submit = function () {
            if ($scope.createClientForm.$invalid) {
                return;
            }
            var email = $scope.user.Email;
            userService.register.save(null, {
                DisplayName: $scope.user.DisplayName,
                Email: email,
                Password: $scope.user.Password,
                ConfirmPassword: $scope.user.ConfirmPassword,
            }, function (userAuth) {
                //link client to attorney
                var clientAttorney = {
                    UserId: $routeParams.attorneyId,
                    ClientUserId: userAuth.UserId
                };
                clientService.clients.save(null, clientAttorney, function () {
                    var court = {
                        UserId: userAuth.UserId,
                        County: 0,
                        CaseNumber: $scope.court.CaseNumber,
                        AuthorOfPlan: 1,
                        PlanType: 1
                    };
                    courtService.courts.save(null, court, function () {
                        $location.path('/Attorney/Client/' + clientAttorney.ClientUserId);
                    });
                });
            });
        };
        headerService.setTitle('Create A Client');
    }];