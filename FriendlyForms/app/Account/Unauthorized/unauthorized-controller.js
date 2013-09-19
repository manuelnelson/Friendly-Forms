var UnauthorizedCtrl = ['$scope', '$routeParams', '$location', 'unauthorizedService', 'headerService','$rootScope',
function ($scope, $routeParams, $location, unauthorizedService, headerService, $rootScope) {
    $rootScope.currentScope = $scope;
    $scope.submit = function() {

    };
    headerService.setTitle('Unauthorized');
}];
