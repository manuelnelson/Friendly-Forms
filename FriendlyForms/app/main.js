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
        when('/Account/Login/', { controller: LoginCtrl, templateUrl: '/app/Account/Login.html' }).
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
});

var INTEGER_REGEXP = /^\-?\d*$/;
FormsApp.directive('integer', function () {
    return {
        require: 'ngModel',
        link: function (scope, elm, attrs, ctrl) {
            ctrl.$parsers.unshift(function (viewValue) {
                if (INTEGER_REGEXP.test(viewValue)) {
                    // it is valid
                    ctrl.$setValidity('integer', true);
                    return viewValue;
                } else {
                    // it is invalid, return undefined (no model update)
                    ctrl.$setValidity('integer', false);
                    return undefined;
                }
            });
        }
    };
});