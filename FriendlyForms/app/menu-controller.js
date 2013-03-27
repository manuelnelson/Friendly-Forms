var MenuCtrl = function ($scope, $routeParams, $location, menuService) {
    $scope.$watch(function () { return menuService.menuItems; }, function () {
        $scope.menuItems = menuService.menuItems;
    });
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
            menuService.setActive($curItem.text);
        }            
    };
    $scope.subMenuClick = function (menuText, subMenuText) {
        $scope.isSubMenuClick = true;
        menuService.setActive(menuText, subMenuText);
    };
};
MenuCtrl.$inject = ['$scope', '$routeParams', '$location', 'menuService'];