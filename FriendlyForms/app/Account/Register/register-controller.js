var RegisterCtrl = ['$scope', '$routeParams', '$location', 'registerService', 'headerService', 'userService', 
    function ($scope, $routeParams, $location, registerService, headerService, userService) {
    $scope.submit = function () {
		if ($scope.userForm.$invalid) {
			return;
		}
		$scope.user.AutoLogin = true;
		$scope.user.UserName = $scope.user.Email;		
		registerService.register.save(null, $scope.user, function () {
		    userService.getCurrentUserSession().then(function(userData) {
		        $location.path('/Account/Payment/User/' + userData.CustomId);
		    });
		});
    };
    headerService.setTitle('Register');
}];
