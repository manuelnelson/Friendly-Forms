var MenuCtrl = ['$scope', '$routeParams', '$location', 'menuService', '$filter',
    function ($scope, $routeParams, $location, menuService, $filter) {
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
        $scope.subMenuClick = function (subMenuItem) {
            if (subMenuItem.disabled)
                return;
            $scope.isSubMenuClick = true;
            menuService.setActive(subMenuItem.path);
        };
        $scope.enableFilter = function (item) {
            return item.disabled === false;
        };
    }];
