var MenuCtrl = function ($scope, $routeParams, $location, menuService) {
    $scope.$watch(function () { return menuService.menuItems; }, function () {
        $scope.menuItems = menuService.menuItems;
    }, true);
    $scope.menuClick = function () {
        if ($scope.isSubMenuClick) {
            $scope.isSubMenuClick = false;
            return;
        }
        var $curItem = $scope.menuItems[this.$index];
        if ($curItem.subMenuItems.length > 0) {
            //show/collapse menu
            $curItem.showSubMenu = !$curItem.showSubMenu;
        } else {
            menuService.setActive($curItem.path);
        }            
    };
    $scope.subMenuClick = function (path) {
        $scope.isSubMenuClick = true;
        menuService.setActive(path);
    };
};
MenuCtrl.$inject = ['$scope', '$routeParams', '$location', 'menuService'];