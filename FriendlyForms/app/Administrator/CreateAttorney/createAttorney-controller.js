var CreateAttorneyCtrl = ['$scope', '$routeParams', '$location', 'menuService', 'headerService', 'registerService', 'userService', 'createAttorneyService',
    function ($scope, $routeParams, $location, menuService, headerService, registerService, userService, createAttorneyService) {
        $scope.submit = function () {
            if ($scope.registerAttorneyForm.$invalid) {
                return;
            }
            $scope.user.UserName = $scope.user.Email;
            userService.register.save(null, $scope.user, function (userAuth) {
                userService.getUserData($routeParams.userId).then(function (userData) {
                    //Create Attorney Page
                    createAttorneyService.attorneyPages.save(null, {
                        UserId: userAuth.UserId,
                        LawFirmId: userData.LawFirmId,
                        PageName: $scope.attorney.PageName
                    }, function () {
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
                            });
                        });
                    });
                });
            });
        };
        headerService.setTitle('Add Attorneys');
    }];