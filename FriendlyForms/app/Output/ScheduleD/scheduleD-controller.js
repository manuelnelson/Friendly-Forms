var ScheduleDCtrl = function($scope, $routeParams, $location, scheduleDService, menuService, genericService, headerService, $rootScope) {
    $scope.storageKey = $location.path();
    scheduleDService.scheduleDs.get({ UserId: $routeParams.userId }, function(data) {
        $scope.scheduleD = data;
    });
    $scope.submit = function() {
    };
    $rootScope.currentScope = $scope;
    headerService.hide();
};
ScheduleDCtrl.$inject = ['$scope', '$routeParams', '$location', 'scheduleDService', 'menuService', 'genericService', 'headerService','$rootScope'];