var UnauthorizedCtrl = ['$scope', '$routeParams', '$location', 'unauthorizedService', 'genericService','$rootScope',
function ($scope, $routeParams, $location, unauthorizedService, genericService, $rootScope) {
    $rootScope.currentScope = $scope;
    $scope.submit = function() {

    };
    genericService.refreshPage();
}];
