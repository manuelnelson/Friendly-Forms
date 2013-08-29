var CSACtrl = function($scope, $routeParams, $location, csaService, menuService, genericService, headerService, $rootScope) {
    $scope.storageKey = $location.path();
    csaService.csas.get({ UserId: $routeParams.userId }, function (data) {
        $scope.csa = data;
    });
    $scope.submit = function() {
    };
    $rootScope.currentScope = $scope;
    headerService.hide();
};
CSACtrl.$inject = ['$scope', '$routeParams', '$location', 'csaService', 'menuService', 'genericService', 'headerService', '$rootScope'];