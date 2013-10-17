var CreateAttorneyCtrl = ['$scope', '$routeParams', '$location', 'menuService', 'headerService', 'registerService', 'userService', 'attorneyPageService',
    function ($scope, $routeParams, $location, menuService, headerService, registerService, userService, attorneyPageService) {
        $scope.submit = function () {
            if ($scope.registerAttorneyForm.$invalid) {
                return;
            }
            $scope.user.UserName = $scope.user.Email;
            userService.register.save(null, $scope.user, function (userAuth) {
                userService.getUserData($routeParams.adminId).then(function (userData) {
                    //Create Attorney Page
                    attorneyPageService.attorneyPages.save(null, {
                        UserId: userAuth.UserId,
                        LawFirmId: userData.LawFirmId,
                        PageName: $scope.attorney.PageName
                    }, function (attorneyPage) {
                        //Tie law firm Id
                        registerService.users.update(null, {
                            Id: userAuth.UserId,
                            UserAuthId: userAuth.UserAuthId,
                            LawFirmId: userData.LawFirmId,
                            Position: $scope.user.Position,
                        }, function () {
                            //Add attorney role to user
                            userService.roles.save(null, {
                                UserName: userAuth.UserName,
                                Roles: ['Lawyer'],
                            }, function () {
                                $location.path('/Attorney/AttorneyPage/Attorney/' + userAuth.UserId);
                            });
                        });
                    });
                });
            });
        };
        headerService.setTitle('Add Attorneys and Coworkers');
    }];