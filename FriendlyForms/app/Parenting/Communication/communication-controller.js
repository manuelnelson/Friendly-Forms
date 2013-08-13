var CommunicationCtrl = function($scope, $routeParams, $location, communicationService, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.communication = communicationService.communications.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.communication.Id == 'undefined' || $scope.communication.Id == 0) {
            //see if garlic has something stored            
            $scope.communication = $.jStorage.get($scope.path);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.communicationForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#communicationForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                $location.path('/Parenting/Schedule/' + $scope.communication.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.communication.UserId = $routeParams.userId;
        if (typeof $scope.communication.Id == 'undefined' || $scope.communication.Id == 0) {
            communicationService.communications.save(null, $scope.communication, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Parenting/Schedule/' + $scope.communication.UserId);
            });
        } else {
            communicationService.communications.update({ Id: $scope.communication.Id }, $scope.communication, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Parenting/Schedule/' + $scope.communication.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive($scope.path)) {
        menuService.setActive($scope.path);
    }
};
CommunicationCtrl.$inject = ['$scope', '$routeParams', '$location', 'communicationService', 'menuService', 'genericService', '$rootScope'];