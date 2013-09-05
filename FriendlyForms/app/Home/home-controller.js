var HomeCtrl = function ($scope, $routeParams, $route, $location, menuService, genericService, headerService) {
    menuService.setActive($location.path(), false);
    headerService.refresh();
};
HomeCtrl.$inject = ['$scope', '$routeParams', '$route','$location', 'menuService', 'genericService', 'headerService'];