var CommunicationCtrl = function($scope, $routeParams, $location, communicationService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    $scope.communication = communicationService.communications.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.communication.Id == 'undefined' || $scope.communication.Id == 0) {
            //see if garlic has something stored            
            $scope.communication = $.jStorage.get($scope.storageKey);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.communicationForm.$invalid) {
            menuService.setSubMenuIconClass('Parenting', 'Communication', 'icon-pencil icon-red');
            var value = genericService.getFormInput('#communicationForm');
            $.jStorage.set($scope.storageKey, value);
            if (!noNavigate)
                $location.path('/Parenting/Schedule/' + $scope.communication.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.storageKey);
        $scope.communication.UserId = $routeParams.userId;
        if (typeof $scope.communication.Id == 'undefined' || $scope.communication.Id == 0) {
            communicationService.communications.save(null, $scope.communication, function() {
                menuService.setSubMenuIconClass('Parenting', 'Communication', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Parenting/Schedule/' + $scope.communication.UserId);
            });
        } else {
            communicationService.communications.update({ Id: $scope.communication.Id }, $scope.communication, function() {
                menuService.setSubMenuIconClass('Parenting', 'Communication', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Parenting/Schedule/' + $scope.communication.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive('Parenting', 'Communication')) {
        menuService.setActive('Parenting', 'Communication');
    }
};
CommunicationCtrl.$inject = ['$scope', '$routeParams', '$location', 'communicationService', 'menuService', 'genericService', '$rootScope'];