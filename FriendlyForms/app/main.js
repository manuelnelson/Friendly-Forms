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

var FormsApp = angular.module("FormsApp", ["ngResource", "ui"], ["$routeProvider", function ($routeProvider) {
    $routeProvider.
        when('/Starter/Court/:userId', { controller: CourtCtrl, templateUrl: '/app/Starter/Court/court.html' }).
        when('/Starter/Participant/:userId', { controller: ParticipantCtrl, templateUrl: '/app/Starter/Participant/participant.html' }).
        when('/Starter/Children/:userId', { controller: ChildrenCtrl, templateUrl: '/app/Starter/Children/children.html' }).
        when('/', { controller: HomeCtrl, templateUrl: '/app/Home/home.html' }).
        otherwise({ redirectTo: '/' });
}]);
FormsApp.value('ui.config', {
    jq: {
        popover: {
            placement: 'left',
            title: 'Tip',
            trigger: 'hover'
        }
    }
})