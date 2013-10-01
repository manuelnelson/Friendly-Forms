var SurveyCtrl = ['$scope', '$routeParams', '$location', 'surveyService', 'menuService', 'headerService', '$rootScope', 'messageService', 'userService',
    function ($scope, $routeParams, $location, surveyService, menuService, headerService, $rootScope, messageService, userService) {
        $scope.path = $location.path();
        userService.getCurrentUserSession().then(function(userSession) {
            if (!userSession.IsAuthenticated)
                messageService.showMessage("Please Login", "Please login before filling out the survey.");
        });
        $scope.submit = function (noNavigate) {
        };
        $scope.sendResult = function () {
            surveyService.surveys.save(null, $scope.survey, function () {
                $scope.surveyForm.$setPristine();
                $scope.survey = '';
                messageService.showMessage("Survey Sent!", "Your survey has been sent and is greatly appreciated!", Application.properties.messageType.Success);
            });
        };
        $rootScope.currentScope = $scope;
        headerService.setTitle('Survey');
    }];