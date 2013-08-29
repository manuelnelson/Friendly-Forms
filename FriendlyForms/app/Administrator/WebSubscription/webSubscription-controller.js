var WebSubscriptionCtrl = function($scope, $routeParams, $location, webSubscriptionService, menuService, headerService, $rootScope) {
    $scope.path = $location.path();
    $scope.submit = function() {
    };
    $rootScope.currentScope = $scope;
    headerService.setTitle('Web Subscription');
};
WebSubscriptionCtrl.$inject = ['$scope', '$routeParams', '$location', 'webSubscriptionService', 'menuService', 'headerService', '$rootScope'];