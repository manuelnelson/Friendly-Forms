var HomeCtrl = function ($scope, $routeParams, $route, $location, menuService, genericService) {
    genericService.refreshPage();
};
HomeCtrl.$inject = ['$scope', '$routeParams', '$route','$location', 'menuService', 'genericService'];