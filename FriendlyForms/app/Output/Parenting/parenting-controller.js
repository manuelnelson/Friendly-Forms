var ParentingCtrl = function($scope, $routeParams, $location, parentingService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    parentingService.parentings.get({ UserId: $routeParams.userId }, function(data) {
        $scope.parenting = data;
    });
    $scope.submit = function () {
    };
    $rootScope.currentScope = $scope;
    //if (!menuService.isActive('Output', 'Praenting')) {
    //    menuService.setActive('Output', 'Praenting');
    //}
};
ParentingCtrl.$inject = ['$scope', '$routeParams', '$location', 'parentingService', 'menuService', 'genericService', '$rootScope'];