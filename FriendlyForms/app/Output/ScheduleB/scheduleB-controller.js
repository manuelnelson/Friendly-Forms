var ScheduleBCtrl = function($scope, $routeParams, $location, scheduleBService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    scheduleBService.scheduleBs.get({ UserId: $routeParams.userId }, function(data) {
        $scope.scheduleB = data;
    });
    $scope.submit = function() {
    };
    $rootScope.currentScope = $scope;
    //if (!menuService.isActive('Output', 'ScheduleB')) {
    //    menuService.setActive('Output', 'ScheduleB');
    //}
};
ScheduleBCtrl.$inject = ['$scope', '$routeParams', '$location', 'scheduleBService', 'menuService', 'genericService', '$rootScope'];