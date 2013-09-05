var ParentingCtrl = function($scope, $routeParams, $location, parentingService, menuService, genericService, headerService, $rootScope) {
    $scope.storageKey = $location.path();
    parentingService.parentings.get({ UserId: $routeParams.userId }, function(data) {
        $scope.parenting = data;
    });
    $scope.submit = function () {
    };
    $rootScope.currentScope = $scope;
    headerService.hide();
    headerService.showOutputHeader();
};
ParentingCtrl.$inject = ['$scope', '$routeParams', '$location', 'parentingService', 'menuService', 'genericService', 'headerService', '$rootScope'];