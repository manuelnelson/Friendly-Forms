var MenuCtrl = function ($scope, $routeParams, $location, menuService) {
    $scope.$watch(function () { return menuService.menuItems; }, function () {
        $scope.menuItems = menuService.menuItems;
    });
    $scope.menuClick = function() {
        angular.forEach($scope.menuItems, function (val, key) {
            val.itemClass = '';
        });
        var $curItem = $scope.menuItems[this.$index];
        if ($curItem.subMenuItems.length > 0) {
            $curItem.showSubMenu = !$curItem.showSubMenu;
        }            
        else
            $curItem.itemClass = 'active';
    };
    $scope.subMenuClick = function () {
        angular.forEach($scope.menuItems, function (val, key) {
            val.itemClass = '';
        });
        //var $curItem = $scope.menuItems[this.$index];
        //if ($curItem.subMenuItems.length > 0) {
        //    $curItem.itemClass = 'submenu';
        //}
        //else
        //    $curItem.itemClass = 'active';
    };
};
MenuCtrl.$inject = ['$scope', '$routeParams', '$location', 'menuService'];