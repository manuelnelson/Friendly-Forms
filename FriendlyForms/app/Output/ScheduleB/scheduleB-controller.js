var ScheduleBCtrl = function ($scope, $routeParams, $location, scheduleBService, menuService, genericService, headerService, $rootScope) {
    $scope.storageKey = $location.path();
    scheduleBService.scheduleBs.get({ UserId: $routeParams.userId }, function(data) {
        $scope.scheduleB = data;
    });
    $scope.submit = function() {
    };
    $rootScope.currentScope = $scope;
    headerService.hide();
    headerService.showOutputHeader();
};
ScheduleBCtrl.$inject = ['$scope', '$routeParams', '$location', 'scheduleBService', 'menuService', 'genericService', 'headerService','$rootScope'];