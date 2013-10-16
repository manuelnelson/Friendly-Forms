var PaymentCtrl = ['$scope', '$routeParams', '$location', 'paymentService', 'menuService', 'headerService', 'userService', 'constantsService',
    function ($scope, $routeParams, $location, paymentService, menuService, headerService, userService, constantsService) {
        headerService.setTitle('Payment');
        $scope.submit = function () {
            if ($scope.paymentForm.$invalid) {
                return;
            }
            var adminRole = constantsService.constants.AdminRole;
            var attorneyRole = constantsService.constants.AttorneyRole;
            userService.getCurrentUserSession().then(function (userData) {
                userService.roles.save(null, {
                    UserName: userData.UserName,
                    Roles: [adminRole, attorneyRole],
                }, function () {
                    $location.path('/Administrator/ClientCases/Admin/' + $routeParams.adminId);
                });
            });
        };
    }];