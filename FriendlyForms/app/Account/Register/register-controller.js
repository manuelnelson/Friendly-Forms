var RegisterCtrl = ['$scope', '$routeParams', '$location', 'registerService', 'genericService', 'userService', 
    function ($scope, $routeParams, $location, registerService, genericService, userService) {
    $scope.submit = function () {
		if ($scope.userForm.$invalid) {
			return;
		}
		$scope.user.AutoLogin = true;
		$scope.user.UserName = $scope.user.Email;		
		registerService.register.save(null, $scope.user, function () {
		    userService.getCurrentUserSession().then(function(userData) {
		        $location.path('/Account/Pricing/User/' + userData.CustomId);
		    });
		});
    };
    genericService.refreshPage();
}];
