var ChildSupportOutputCtrl = function($scope, $routeParams, $location, childSupportOutputService, menuService, genericService, headerService, $rootScope) {
    $scope.storageKey = $location.path();
    childSupportOutputService.childSupports.get({ UserId: $routeParams.userId }, function (data) {
        $scope.childSupport = data;
    });
    $scope.submit = function () {
    };

    $rootScope.currentScope = $scope;
    headerService.hide();
    headerService.showOutputHeader();
};
ChildSupportOutputCtrl.$inject = ['$scope', '$routeParams', '$location', 'childSupportOutputService', 'menuService', 'genericService', 'headerService', '$rootScope'];