var HomeCtrl = function ($scope, $routeParams, $route, $location, menuService, genericService, headerService) {
    menuService.setActive($location.path(), false);
    headerService.refresh();
    //TODO: uggghh...had to do with Jquery
    var boxes = $('.slide-panel .popout');
    var maxItem = _.max(boxes, function (item) {
        return $(item).height();
    });
    boxes.height($(maxItem).height());
    headerService.setTitle('Split Solutions: The Divorce Solution for Georgia Residents')
};
HomeCtrl.$inject = ['$scope', '$routeParams', '$route', '$location', 'menuService', 'genericService', 'headerService'];