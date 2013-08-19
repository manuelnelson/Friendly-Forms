var ScheduleECtrl = function($scope, $routeParams, $location, scheduleEService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    scheduleEService.scheduleEs.get({ UserId: $routeParams.userId }, function(data) {
        $scope.scheduleE = data;
    });
    $scope.submit = function() {
    };
    $rootScope.currentScope = $scope;
    //if (!menuService.isActive('Output', 'ScheduleE')) {
    //    menuService.setActive('Output', 'ScheduleE');
    //}
};
ScheduleECtrl.$inject = ['$scope', '$routeParams', '$location', 'scheduleEService', 'menuService', 'genericService', '$rootScope'];