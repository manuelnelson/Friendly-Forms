var ScheduleCtrl = function($scope, $routeParams, $location, scheduleService, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.showErrors = false;
    $scope.schedule = scheduleService.schedules.get({ UserId: $routeParams.userId }, function () {
        if (typeof $scope.schedule.Id == 'undefined' || $scope.schedule.Id == 0) {
            //see if garlic has something stored            
            $scope.schedule = $.jStorage.get($scope.path);
            if ($scope.schedule)
                $scope.showErrors = true;
            //The default time for control makes it dirty. Undo this
            $scope.scheduleForm.PickedUp.$dirty = false;
            $scope.scheduleForm.DroppedOff.$dirty = false;
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.scheduleForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#scheduleForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                menuService.nextMenu();
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.schedule.UserId = $routeParams.userId;
        if (typeof $scope.schedule.Id == 'undefined' || $scope.schedule.Id == 0) {
            scheduleService.schedules.save(null, $scope.schedule, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    menuService.nextMenu();
            });
        } else {
            scheduleService.schedules.update({ Id: $scope.schedule.Id }, $scope.schedule, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    menuService.nextMenu();
            });
        }
    };
    $scope.nonCustodialChange = function (val) {
        switch (val) {
            case 1:
                $scope.schedule.CustodianWeekend = 1;
                break;
            case 2:
                $scope.schedule.CustodianWeekend = 4;
                break;
            case 3:
                $scope.schedule.CustodianWeekend = 4;
                break;
            case 4:
                $scope.schedule.CustodianWeekend = 3;
                break;
            case 5:
                $scope.schedule.CustodianWeekend = 5;
                break;                
        }
    };
    $scope.custodialChange = function (val) {
        switch (val) {
            case 1:
                $scope.schedule.NonCustodianWeekend = 1;
                break;
            case 2:
                $scope.schedule.NonCustodianWeekend = 4;
                break;
            case 3:
                $scope.schedule.NonCustodianWeekend = 4;
                break;
            case 4:
                $scope.schedule.NonCustodianWeekend = 3;
                break;
            case 5:
                $scope.schedule.NonCustodianWeekend = 5;
                break;
        }
    };
    $rootScope.currentScope = $scope;

    genericService.refreshPage();

};
ScheduleCtrl.$inject = ['$scope', '$routeParams', '$location', 'scheduleService', 'menuService', 'genericService', '$rootScope'];