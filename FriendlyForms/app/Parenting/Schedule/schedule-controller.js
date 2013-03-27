var ScheduleCtrl = function($scope, $routeParams, $location, scheduleService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    $scope.schedule = scheduleService.schedules.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.schedule.Id == 'undefined' || $scope.schedule.Id == 0) {
            //see if garlic has something stored            
            $scope.schedule = $.jStorage.get($scope.storageKey);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.scheduleForm.$invalid) {
            menuService.setSubMenuIconClass('Parenting', 'Schedule', 'icon-pencil icon-red');
            var value = genericService.getFormInput('#scheduleForm');
            $.jStorage.set($scope.storageKey, value);
            if (!noNavigate)
                $location.path('/Parenting/Participant/' + $scope.schedule.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.storageKey);
        $scope.schedule.UserId = $routeParams.userId;
        if (typeof $scope.schedule.Id == 'undefined' || $scope.schedule.Id == 0) {
            scheduleService.schedules.save(null, $scope.schedule, function() {
                menuService.setSubMenuIconClass('Parenting', 'Schedule', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Parenting/Participant/' + $scope.schedule.UserId);
            });
        } else {
            scheduleService.schedules.update({ Id: $scope.schedule.Id }, $scope.schedule, function() {
                menuService.setSubMenuIconClass('Parenting', 'Schedule', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Parenting/Participant/' + $scope.schedule.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive('Parenting', 'Schedule')) {
        menuService.setActive('Parenting', 'Schedule');
    }
};
ScheduleCtrl.$inject = ['$scope', '$routeParams', '$location', 'schedulesService', 'menuService', 'genericService', '$rootScope'];