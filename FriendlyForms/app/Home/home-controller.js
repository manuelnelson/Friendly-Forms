var HomeCtrl = function ($scope, $routeParams, $location,  menuService) {
    menuService.setupMenu();
};
HomeCtrl.$inject = ['$scope', '$routeParams', '$location', 'menuService'];