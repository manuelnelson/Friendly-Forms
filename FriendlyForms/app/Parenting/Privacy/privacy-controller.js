﻿var PrivacyCtrl = function($scope, $routeParams, $location, privacyService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    $scope.privacy = privacyService.privacies.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.privacy.Id == 'undefined' || $scope.privacy.Id == 0) {
            //see if garlic has something stored            
            $scope.privacy = $.jStorage.get($scope.storageKey);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.privacyForm.$invalid) {
            menuService.setSubMenuIconClass('Parenting', 'Supervision', 'icon-pencil icon-red');
            var value = genericService.getFormInput('#privacyForm');
            $.jStorage.set($scope.storageKey, value);
            if (!noNavigate)
                $location.path('/Parenting/Information/' + $scope.privacy.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.storageKey);
        $scope.privacy.UserId = $routeParams.userId;
        if (typeof $scope.privacy.Id == 'undefined' || $scope.privacy.Id == 0) {
            privacyService.privacies.save(null, $scope.privacy, function() {
                menuService.setSubMenuIconClass('Parenting', 'Supervision', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Parenting/Information/' + $scope.privacy.UserId);
            });
        } else {
            privacyService.privacies.update({ Id: $scope.privacy.Id }, $scope.privacy, function() {
                menuService.setSubMenuIconClass('Parenting', 'Supervision', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Parenting/Information/' + $scope.privacy.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive('Parenting', 'Supervision')) {
        menuService.setActive('Parenting', 'Supervision');
    }
};
PrivacyCtrl.$inject = ['$scope', '$routeParams', '$location', 'privacyService', 'menuService', 'genericService', '$rootScope'];