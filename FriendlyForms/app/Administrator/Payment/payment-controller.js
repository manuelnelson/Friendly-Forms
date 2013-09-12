var PaymentCtrl = ['$scope', '$routeParams', '$location', 'paymentService', 'menuService', 'headerService', 'userService',
    function ($scope, $routeParams, $location, paymentService, menuService, headerService, userService) {
        headerService.setTitle('Payment');
        $scope.submit = function () {
            if ($scope.paymentForm.$invalid) {
                return;
            }
            var userId = $routeParams.userId;
            userService.getCurrentUserSession().then(function (userData) {
                userService.roles.save(null, {
                    UserName: userData.UserName,
                    Roles: ['FirmAdmin', 'Lawyer'],
                }, function () {
                    $location.path('/Administration/ClientCases/' + userId);
                });
            });
        };
    }];