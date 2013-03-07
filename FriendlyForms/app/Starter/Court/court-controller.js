var CourtCtrl = function ($scope, $routeParams, $location, courtService) {
    $scope.court = courtService.get({ UserId: $routeParams.userId });

    $scope.update = function () {        
        courtService.update({ Id: $scope.court.Id }, $scope.court, function () {
            $location.path('/Forms/Starter/Participants/:userId');
        });
    };
};
CourtCtrl.$inject = ['$scope', '$routeParams', '$location', 'courtService'];