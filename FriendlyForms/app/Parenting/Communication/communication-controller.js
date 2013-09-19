var CommunicationCtrl = function($scope, $routeParams, $location, communicationService, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.showErrors = false;
    $scope.isLoaded = false;
    $scope.communication = communicationService.communications.get({ UserId: $routeParams.userId }, function () {
        $scope.isLoaded = true;
        if (typeof $scope.communication.Id == 'undefined' || $scope.communication.Id == 0) {
            //see if garlic has something stored            
            $scope.communication = $.jStorage.get($scope.path);
            if ($scope.communication)
                $scope.showErrors = true;
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.communicationForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#communicationForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                menuService.nextMenu();
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.communication.UserId = $routeParams.userId;
        if (typeof $scope.communication.Id == 'undefined' || $scope.communication.Id == 0) {
            communicationService.communications.save(null, $scope.communication, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    menuService.nextMenu();
            });
        } else {
            communicationService.communications.update({ Id: $scope.communication.Id }, $scope.communication, function() {
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
CommunicationCtrl.$inject = ['$scope', '$routeParams', '$location', 'communicationService', 'menuService', 'genericService', '$rootScope'];