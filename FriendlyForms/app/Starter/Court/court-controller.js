var CourtCtrl = function ($scope, $routeParams, $location, courtService, menuService) {
    $scope.court = courtService.get({ UserId: $routeParams.userId });

    $scope.update = function () {        
        courtService.update({ Id: $scope.court.Id }, $scope.court, function () {
            $location.path('/Starter/Participants/:userId');
        });
    };

    menuService.setActive('Court');
};
CourtCtrl.$inject = ['$scope', '$routeParams', '$location', 'courtService', 'menuService'];