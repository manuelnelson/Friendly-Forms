var RegisterCtrl = function ($scope, $routeParams, $location, registerService) {
    $scope.submit = function() {
        registerService.register.post();
    };
};
RegisterCtrl.$inject = ['$scope', '$routeParams', '$location', 'registerService'];