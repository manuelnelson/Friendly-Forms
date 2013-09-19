var CreateClientCtrl = ['$scope', '$routeParams', '$location', 'clientService', 'courtService', 'headerService', 'userService',
    function ($scope, $routeParams, $location, clientService, courtService, headerService, userService) {

        $scope.submit = function () {
            if ($scope.createClientForm.$invalid) {
                return;
            }
            var email = $scope.user.Email;
            if (email.indexOf('@' < 0))
                email = $scope.user.Email + '@ourlawfirm.com';
            userService.register.save(null, {
                DisplayName: $scope.user.DisplayName,
                Email: email,
                Password: $scope.user.Password,
                ConfirmPassword: $scope.user.ConfirmPassword,
                PrimaryEmail: $scope.user.PrimaryEmail,
            }, function (userAuth) {
                var court = {
                    UserId: userAuth.UserId,
                    County: 0,
                    CaseNumber: $scope.court.CaseNumber,
                    AuthorOfPlan: 1,
                    PlanType: 1
                };
                courtService.courts.save(null, court, function () {
                    var clientAttorney = {
                        UserId: $routeParams.userId,
                        ClientUserId: userAuth.UserId
                    };
                    clientService.clients.save(null, clientAttorney, function () {
                        $location.path('/Attorney/Client/' + clientAttorney.ClientUserId);
                    });
                });
                //link client to attorney
            });
        };
        headerService.setTitle('Create A Client');
    }];