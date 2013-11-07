﻿var CourtCtrl = function ($scope, $routeParams, $location, courtService, menuService, genericService, userService, $rootScope) {
    $scope.path = $location.path();
    $scope.showErrors = false;
    $scope.court = courtService.courts.get({ UserId: $routeParams.userId }, function () {
        if (typeof $scope.court.Id == 'undefined' || $scope.court.Id == 0) {
            //see if garlic has something stored            
            $scope.court = $.jStorage.get($scope.path);
            if ($scope.court)
                $scope.showErrors = true;
        }
    });
    courtService.counties.get({ }, function (data) {
        $scope.counties = data.Counties;
    });
    $scope.submit = function (noNavigate) {
        if ($scope.courtForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-exclamation icon-red');            
            var value = genericService.getFormInput('#courtForm');
            $.jStorage.set($scope.path, value);
            if(!noNavigate)
                menuService.nextMenu();
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.court.UserId = userService.getFormUserId();
        if (typeof $scope.court.Id == 'undefined' || $scope.court.Id == 0) {
            courtService.courts.save(null, $scope.court, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    menuService.nextMenu();
            });
        } else {
            courtService.courts.update({ Id: $scope.court.Id }, $scope.court, function () {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    menuService.nextMenu();
            });
        }
    };
    genericService.refreshPage(function () {
        $rootScope.currentScope = $scope;
    });

};
CourtCtrl.$inject = ['$scope', '$routeParams', '$location', 'courtService', 'menuService', 'genericService', 'userService', '$rootScope'];