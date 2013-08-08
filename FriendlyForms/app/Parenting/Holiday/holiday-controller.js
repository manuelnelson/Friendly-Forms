var HolidayCtrl = function($scope, $routeParams, $location, holidayService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    $scope.holiday = holidayService.holidays.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.holiday.Id == 'undefined' || $scope.holiday.Id == 0) {
            //see if garlic has something stored            
            $scope.holiday = $.jStorage.get($scope.storageKey);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.holidayForm.$invalid) {
            menuService.setSubMenuIconClass('Parenting', 'Holiday', 'icon-pencil icon-red');
            var value = genericService.getFormInput('#holidayForm');
            $.jStorage.set($scope.storageKey, value);
            if (!noNavigate)
                $location.path('/Parenting/Addendum/' + $scope.holiday.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.storageKey);
        $scope.holiday.UserId = $routeParams.userId;
        if (typeof $scope.holiday.Id == 'undefined' || $scope.holiday.Id == 0) {
            holidayService.Holidays.save(null, $scope.holiday, function() {
                menuService.setSubMenuIconClass('Parenting', 'Holiday', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Parenting/Addendum/' + $scope.holiday.UserId);
            });
        } else {
            holidayService.Holidays.update({ Id: $scope.holiday.Id }, $scope.holiday, function() {
                menuService.setSubMenuIconClass('Parenting', 'Holiday', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Parenting/Addendum/' + $scope.holiday.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive('Parenting', 'Holiday')) {
        menuService.setActive('Parenting', 'Holiday');
    }
};
HolidayCtrl.$inject = ['$scope', '$routeParams', '$location', 'holidayService', 'menuService', 'genericService', '$rootScope'];