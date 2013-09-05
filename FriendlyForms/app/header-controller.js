var HeaderCtrl = function($scope, $routeParams, $location, headerService, messageService, $rootScope) {
    headerService.initialize();
    $scope.$watch(function () { return headerService.Title; }, function () {
        $scope.PageTitle = headerService.Title;
    }, true);
    $scope.$watch(function () { return headerService.levels; }, function () {
        $scope.levels = headerService.levels;
    }, true);
    $scope.$watch(function () { return headerService.showFeedbackHeader; }, function () {
        $scope.showFeedbackHeader = headerService.showFeedbackHeader;
    }, true);
    $scope.$watch(function () { return headerService.showOutput; }, function () {
        $scope.showOutput = headerService.showOutput;
    }, true);
    $scope.showIssue = false;
    $scope.showIssueForm = function() {
        $scope.showIssue = !$scope.showIssue;
    };
    $scope.submitIssues = function () {
        $scope.feedback.Path = $location.path();
        headerService.emails.save(null, $scope.feedback, function() {
            $scope.feedbackForm.$setPristine();
            $scope.feedback = '';
            $scope.showIssue = false;
            messageService.showMessage("Feedback Sent!", "Your feedback has been sent and is appreciated! You should hear from us within 48 hours.", Application.properties.messageType.Success);
        });
    };
};
HeaderCtrl.$inject = ['$scope', '$routeParams', '$location', 'headerService', 'messageService', '$rootScope'];