﻿window.Application = window.Application || {};
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
        when('/Domestic/Asset/:userId', { controller: AssetCtrl, templateUrl: '/app/Domestic/Asset/asset.html' }).
        when('/Domestic/Debt/:userId', { controller: DebtCtrl, templateUrl: '/app/Domestic/Debt/debt.html' }).
        when('/Domestic/HealthInsurance/:userId', { controller: HealthInsuranceCtrl, templateUrl: '/app/Domestic/HealthInsurance/healthInsurance.html' }).
        when('/Domestic/House/:userId', { controller: HouseCtrl, templateUrl: '/app/Domestic/House/house.html' }).
        when('/Domestic/Property/:userId', { controller: PropertyCtrl, templateUrl: '/app/Domestic/Property/Property.html' }).
        when('/Domestic/Spousal/:userId', { controller: SpousalCtrl, templateUrl: '/app/Domestic/Spousal/Spousal.html' }).
        when('/Domestic/Tax/:userId', { controller: TaxCtrl, templateUrl: '/app/Domestic/Tax/Tax.html' }).
        when('/Domestic/Vehicle/:userId', { controller: VehicleCtrl, templateUrl: '/app/Domestic/Vehicle/Vehicle.html' }).
        when('/Parenting/Supervision/:userId', { controller: PrivacyCtrl, templateUrl: '/app/Parenting/Privacy/Privacy.html' }).
        when('/Parenting/Information/:userId', { controller: InformationCtrl, templateUrl: '/app/Parenting/Information/Information.html' }).
        when('/Parenting/Decision/:userId/:childId', { controller: DecisionCtrl, templateUrl: '/app/Parenting/Decision/Decision.html' }).
        when('/Parenting/Responsibility/:userId', { controller: ResponsibilityCtrl, templateUrl: '/app/Parenting/Responsibility/Responsibility.html' }).
        when('/Parenting/Communication/:userId', { controller: CommunicationCtrl, templateUrl: '/app/Parenting/Communication/Communication.html' }).
        when('/Parenting/Schedule/:userId', { controller: ScheduleCtrl, templateUrl: '/app/Parenting/Schedule/Schedule.html' }).
        when('/Parenting/Holiday/:userId/:childId', { controller: HolidayCtrl, templateUrl: '/app/Parenting/Holiday/Holiday.html' }).
        when('/Parenting/Addendum/:userId', { controller: AddendumCtrl, templateUrl: '/app/Parenting/Addendum/Addendum.html' }).
        when('/Financial/ChildCare/:userId/:childId', { controller: ChildCareCtrl, templateUrl: '/app/Financial/ChildCare/ChildCare.html' }).
        when('/Financial/Health/:userId', { controller: HealthInsuranceCtrl, templateUrl: '/app/Financial/Health/Health.html' }).
        when('/Financial/ExtraExpense/:userId/:childId', { controller: ExtraExpenseCtrl, templateUrl: '/app/Financial/ExtraExpense/ExtraExpense.html' }).
        when('/Financial/Income/:userId/:isOtherParent', { controller: IncomeCtrl, templateUrl: '/app/Financial/Income/Income.html' }).
        when('/Financial/SocialSecurity/:userId/:isOtherParent', { controller: SocialSecurityCtrl, templateUrl: '/app/Financial/SocialSecurity/SocialSecurity.html' }).
        when('/Financial/Support/:userId/:isOtherParent', { controller: SupportCtrl, templateUrl: '/app/Financial/Support/Support.html' }).
        when('/Financial/OtherChild/:userId/:isOtherParent', { controller: OtherChildCtrl, templateUrl: '/app/Financial/OtherChild/OtherChild.html' }).
        when('/Financial/Deviation/:userId', { controller: DeviationCtrl, templateUrl: '/app/Financial/Deviation/Deviation.html' }).
        when('/Output/FormComplete/:formName/user/:userId', { controller: FormCompleteCtrl, templateUrl: '/app/Output/FormComplete/FormComplete.html' }).
        when('/Output/Parenting/User/:userId', { controller: ParentingCtrl, templateUrl: '/app/Output/Parenting/Parenting.html' }).
        when('/Output/DomesticMediation/User/:userId', { controller: DomesticMediationCtrl, templateUrl: '/app/Output/DomesticMediation/DomesticMediation.html' }).
        when('/Account/Login/', { controller: LoginCtrl, templateUrl: '/app/Account/Login/Login.html' }).
        when('/Account/Register/', { controller: RegisterCtrl, templateUrl: '/app/Account/Register/Register.html' }).
        when('/', { controller: HomeCtrl, templateUrl: '/app/Home/home.html' }).
        otherwise({ redirectTo: '/' });
}]);
FormsApp.value('ui.config', {
    jq: {
        popover: {
            placement: 'left',
            title: 'Tip',
            trigger: 'hover'
        },
        timepicker: {
            minuteStep: 15,
            showInputs: false,
            disableFocus: true
        },
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