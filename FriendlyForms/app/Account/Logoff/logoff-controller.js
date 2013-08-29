var LogoffCtrl = function($scope, $routeParams, $location, logoffService, headerService) {
    headerService.hide();
    logoffService.logout();
};
LogoffCtrl.$inject = ['$scope', '$routeParams', '$location', 'logoffService', 'headerService'];