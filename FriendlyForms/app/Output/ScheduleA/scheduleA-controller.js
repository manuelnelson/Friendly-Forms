var ScheduleACtrl = function($scope, $routeParams, $location, scheduleAService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    scheduleAService.scheduleAs.get({ UserId: $routeParams.userId }, function (data) {
        $scope.scheduleA = data;
    });
    $scope.submit = function () {
    };
    $rootScope.currentScope = $scope;
    //if (!menuService.isActive('Output', 'ScheduleA')) {
    //    menuService.setActive('Output', 'ScheduleA');
    //}
};
ScheduleACtrl.$inject = ['$scope', '$routeParams', '$location', 'scheduleAService', 'menuService', 'genericService', '$rootScope'];