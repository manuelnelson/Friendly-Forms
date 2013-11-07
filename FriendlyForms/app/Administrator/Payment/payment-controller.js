var PaymentCtrl = ['$scope', '$routeParams', '$location', 'paymentService', 'menuService', 'genericService', 'userService', 'constantsService',
    function ($scope, $routeParams, $location, paymentService, menuService, genericService, userService, constantsService) {
        $scope.submit = function () {
            if ($scope.paymentForm.$invalid) {
                return;
            }
            $scope.AmountId = $routeParams.subscription;
            paymentService.recurring.save(null, $scope.payment, function () {
                var adminRole = constantsService.constants.AdminRole;
                var attorneyRole = constantsService.constants.AttorneyRole;
                userService.getCurrentUserSession().then(function(userData) {
                    userService.roles.save(null, {
                            UserName: userData.UserName,
                            Roles: [adminRole, attorneyRole],
                        }, function() {
                            //need to update usersession as well
                            $location.path('/Administrator/ClientCases/Admin/' + $routeParams.adminId);
                        });
                });
            });
        };
        genericService.refreshPage();
    }];