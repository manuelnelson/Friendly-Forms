var ChildSupportCtrl = function($scope, $routeParams, $location, childSupportService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    childSupportService.childSupports.get({ UserId: $routeParams.userId }, function (data) {
        $scope.childSupport = data;
    });
    $scope.submit = function () {
    };

    $rootScope.currentScope = $scope;
    //if (!menuService.isActive('Output', 'ChildSupport')) {
    //    menuService.setActive('Output', 'ChildSupport');
    //}
};
ChildSupportCtrl.$inject = ['$scope', '$routeParams', '$location', 'childSupportService', 'menuService', 'genericService', '$rootScope'];