var ParticipantCtrl = function($scope, $routeParams, $location, participantService, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.participant = participantService.participant.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.participant.Id == 'undefined' || $scope.participant.Id == 0) {
            //see if garlic has something stored            
            $scope.participant = $.jStorage.get($scope.path);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.participantForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#participantForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                $location.path('/Starter/Participant/' + $scope.participant.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.participant.UserId = $routeParams.userId;
        if (typeof $scope.participant.Id == 'undefined' || $scope.participant.Id == 0) {
            participantService.participant.save(null, $scope.participant, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Starter/Children/' + $scope.participant.UserId);
            });
        } else {
            participantService.participant.update({ Id: $scope.participant.Id }, $scope.participant, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Starter/Children/' + $scope.participant.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive($scope.path)) {
        menuService.setActive($scope.path);
    }
};
ParticipantCtrl.$inject = ['$scope', '$routeParams', '$location', 'participantService', 'menuService', 'genericService', '$rootScope'];