FormsApp.factory('loadingService', function () {
    var service = {
        requestCount: 0,
        isLoading: function () {
            return service.requestCount > 0;
        },
        //use these to manually show loading
        increment: function () {
            service.requestCount++;
        },
        decrement: function () {
            service.requestCount--;
            service.message = Application.properties.defaultMessage;
        },
        message: Application.properties.defaultMessage
    };
    return service;
});

FormsApp.factory('onStartInterceptor', ['loadingService', function (loadingService) {
    return function (data, headersGetter) {
        loadingService.requestCount++;
        return data;
    };
}]);

FormsApp.factory('onCompleteInterceptor', ['loadingService', 'messageService', function (loadingService, messageService) {
    return function (promise) {
        //successful response
        var decrementRequestCount = function (response) {
            loadingService.requestCount--;
            //always return to default message
            loadingService.message = Application.properties.defaultMessage;
            return response;
        };
        //Error
        var decrementRequestCountError = function (response) {
            loadingService.requestCount--;
            //return to default message for next loading... call
            loadingService.message = Application.properties.defaultMessage;
            //show error message
            messageService.handleError(response);
            return response;
        };
        return promise.then(decrementRequestCount, decrementRequestCountError);
    };
}]);

FormsApp.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.responseInterceptors.push('onCompleteInterceptor');
}]);

FormsApp.run(['$http', 'onStartInterceptor', function ($http, onStartInterceptor) {
    $http.defaults.transformRequest.push(onStartInterceptor);
}]);
