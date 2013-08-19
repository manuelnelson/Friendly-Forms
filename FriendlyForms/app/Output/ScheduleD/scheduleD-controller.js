var ScheduleDCtrl = function($scope, $routeParams, $location, scheduleDService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    scheduleDService.scheduleDs.get({ UserId: $routeParams.userId }, function(data) {
        $scope.scheduleD = data;
    });
    $scope.submit = function() {
    };
    $rootScope.currentScope = $scope;
    //if (!menuService.isActive('Output', 'ScheduleD')) {
    //    menuService.setActive('Output', 'ScheduleD');
    //}
};
ScheduleDCtrl.$inject = ['$scope', '$routeParams', '$location', 'scheduleDService', 'menuService', 'genericService', '$rootScope'];