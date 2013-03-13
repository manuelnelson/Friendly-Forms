var ParticipantCtrl = function ($scope, $routeParams, $location, participantService) {
    $scope.participant = participantService.get({ UserId: $routeParams.userId });

    $scope.update = function () {
        participantService.update({ Id: $scope.participant.Id }, $scope.participant, function () {
            $location.path('/Forms/Starter/Participants/:userId');
        });
    };
};
ParticipantCtrl.$inject = ['$scope', '$routeParams', '$location', 'participantService'];