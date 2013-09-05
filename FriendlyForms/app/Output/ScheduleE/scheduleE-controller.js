var ScheduleECtrl = function($scope, $routeParams, $location, scheduleEService, menuService, genericService, headerService, $rootScope) {
    $scope.storageKey = $location.path();
    scheduleEService.scheduleEs.get({ UserId: $routeParams.userId }, function(data) {
        $scope.scheduleE = data;
    });
    $scope.submit = function() {
    };
    $rootScope.currentScope = $scope;
    headerService.hide();
    headerService.showOutputHeader();
};
ScheduleECtrl.$inject = ['$scope', '$routeParams', '$location', 'scheduleEService', 'menuService', 'genericService', 'headerService', '$rootScope'];