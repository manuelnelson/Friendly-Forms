﻿var HouseCtrl = function($scope, $routeParams, $location, houseService, menuService, genericService, userService, limitToFilter, $http, $rootScope) {
    $scope.path = $location.path();
    $scope.showErrors = false;
    $scope.isLoaded = false;
    $scope.house = houseService.houses.get({ UserId: $routeParams.userId }, function () {
        $scope.isLoaded = true;
        if (typeof $scope.house.Id == 'undefined' || $scope.house.Id == 0) {
            //see if garlic has something stored            
            $scope.house = $.jStorage.get($scope.path);
            if($scope.house)
                $scope.showErrors = true;
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.houseForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-exclamation icon-red');
            var value = genericService.getFormInput('#houseForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                menuService.nextMenu();
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.house.UserId = userService.getFormUserId();
        if (typeof $scope.house.Id == 'undefined' || $scope.house.Id == 0) {
            houseService.houses.save(null, $scope.house, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    menuService.nextMenu();
            });
        } else {
            houseService.houses.update({ Id: $scope.house.Id }, $scope.house, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    menuService.nextMenu();
            });
        }
    };
    $scope.cities = function(cityName) {
        return $http.get('http://ws.geonames.org/searchJSON?country=US&name_startsWith=' + cityName).then(function (response) {
            var names = _.map(response.data.geonames, function(geoName) {
                return geoName.name + ', ' + geoName.adminCode1;
            });
            return limitToFilter(names, 8);
        });
    };

    genericService.refreshPage(function () {
        $rootScope.currentScope = $scope;
    });

};
HouseCtrl.$inject = ['$scope', '$routeParams', '$location', 'houseService', 'menuService', 'genericService', 'userService', 'limitToFilter', '$http', '$rootScope'];