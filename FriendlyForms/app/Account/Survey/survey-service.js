FormsApp.factory('surveyService', ['$resource', function($resource) {
    var service = {
        surveys: $resource('/api/emails/survey', {  },
            {
            }),
    };
    return service;
}]);