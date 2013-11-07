var MessageCtrl = function ($scope, messageService) {
    $scope.$watch(function () { return messageService.isShowing(); }, function (value) {
        $scope.Title = messageService.messageOptions.title;
        $scope.AlertType = messageService.messageOptions.alertType;
        $scope.Message = messageService.messageOptions.message;
        $scope.showMessage = value;
    });
    $scope.close = function () {
        messageService.closeMessage();
    };
    //close message on route change
    $scope.$on("$locationChangeStart", function (event, nextLocation, currentLocation) {
        // Logic goes here
        $scope.close();
    });
};
MessageCtrl.$inject = ['$scope', 'messageService'];