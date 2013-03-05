window.Application = window.Application || {};
//put specific application properties here
Application.properties = {
    messageType: {
        Warning: '',
        Success: 'alert-success',
        Error: 'alert-error'
    },
    defaultMessage: 'Loading...'
};

var FormsApp = angular.module("FormsApp", ["ngResource"], ["$routeProvider", function ($routeProvider) {
    $routeProvider.
        when('/Forms/Starter/Children/:userId', { controller: ChildrenCtrl, templateUrl: '/app/Starter/Children/children.html' }).
        when('/Forms/Starter/Court/:userId', { controller: CourtCtrl, templateUrl: '/app/Starter/Court/court.html' }).
        otherwise({ redirectTo: '/' });
}]);