var ScheduleACtrl = function($scope, $routeParams, $location, scheduleAService, menuService, genericService, headerService, $rootScope) {
    $scope.storageKey = $location.path();
    scheduleAService.scheduleAs.get({ UserId: $routeParams.userId }, function (data) {
        $scope.scheduleA = data;
    });
    $scope.submit = function () {
    };
    $rootScope.currentScope = $scope;
    headerService.hide();
    headerService.showOutputHeader();
};
ScheduleACtrl.$inject = ['$scope', '$routeParams', '$location', 'scheduleAService', 'menuService', 'genericService', 'headerService','$rootScope'];