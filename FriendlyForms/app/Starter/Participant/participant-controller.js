var ParticipantCtrl = function($scope, $routeParams, $location, participantService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    $scope.participant = participantService.participant.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.participant.Id == 'undefined' || $scope.participant.Id == 0) {
            //see if garlic has something stored            
            $scope.participant = $.jStorage.get($scope.storageKey);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.participantForm.$invalid) {
            menuService.setSubMenuIconClass('Starter', 'Participant', 'icon-pencil icon-red');
            var value = genericService.getFormInput('#participantForm');
            $.jStorage.set($scope.storageKey, value);
            if (!noNavigate)
                $location.path('/Starter/Participant/' + $scope.participant.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.storageKey);
        $scope.participant.UserId = $routeParams.userId;
        if (typeof $scope.participant.Id == 'undefined' || $scope.participant.Id == 0) {
            participantService.participant.save(null, $scope.participant, function() {
                menuService.setSubMenuIconClass('Starter', 'Participant', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Starter/Children/' + $scope.participant.UserId);
            });
        } else {
            participantService.participant.update({ Id: $scope.participant.Id }, $scope.participant, function() {
                menuService.setSubMenuIconClass('Starter', 'Participant', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Starter/Children/' + $scope.participant.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive('Starter', 'Participant')) {
        menuService.setActive('Starter', 'Participant');
    }
};
ParticipantCtrl.$inject = ['$scope', '$routeParams', '$location', 'participantService', 'menuService', 'genericService', '$rootScope'];