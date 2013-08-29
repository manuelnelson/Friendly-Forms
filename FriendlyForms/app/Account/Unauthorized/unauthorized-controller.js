var UnauthorizedCtrl = function($scope, $routeParams, $location, unauthorizedService, menuService, headerService) {
    headerService.setTitle('Unauthorized');
};
UnauthorizedCtrl.$inject = ['$scope', '$routeParams', '$location', 'unauthorizedService', 'menuService', 'headerService'];