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

var FormsApp = angular.module("FormsApp", ["ngResource", "ui", "ui.bootstrap"], ["$routeProvider", function ($routeProvider) {
    $routeProvider.
        when('/Starter/Court/user/:userId', { caseInsensitiveMatch: true, controller: CourtCtrl, templateUrl: '/app/Starter/Court/court.html' }).
        when('/Starter/Participant/user/:userId', { caseInsensitiveMatch: true, controller: ParticipantCtrl, templateUrl: '/app/Starter/Participant/participant.html' }).
        when('/Starter/Children/user/:userId', { caseInsensitiveMatch: true, controller: ChildrenCtrl, templateUrl: '/app/Starter/Children/children.html' }).
        when('/Domestic/Asset/user/:userId', { caseInsensitiveMatch: true, controller: AssetCtrl, templateUrl: '/app/Domestic/Asset/asset.html' }).
        when('/Domestic/Debt/user/:userId', { caseInsensitiveMatch: true, controller: DebtCtrl, templateUrl: '/app/Domestic/Debt/debt.html' }).
        when('/Domestic/HealthInsurance/user/:userId', { caseInsensitiveMatch: true, controller: HealthInsuranceCtrl, templateUrl: '/app/Domestic/HealthInsurance/healthInsurance.html' }).
        when('/Domestic/House/user/:userId', { caseInsensitiveMatch: true, controller: HouseCtrl, templateUrl: '/app/Domestic/House/house.html' }).
        when('/Domestic/Property/user/:userId', { caseInsensitiveMatch: true, controller: PropertyCtrl, templateUrl: '/app/Domestic/Property/Property.html' }).
        when('/Domestic/Spousal/user/:userId', { caseInsensitiveMatch: true, controller: SpousalCtrl, templateUrl: '/app/Domestic/Spousal/Spousal.html' }).
        when('/Domestic/Tax/user/:userId', { caseInsensitiveMatch: true, controller: TaxCtrl, templateUrl: '/app/Domestic/Tax/Tax.html' }).
        when('/Domestic/Vehicle/user/:userId', { caseInsensitiveMatch: true, controller: VehicleCtrl, templateUrl: '/app/Domestic/Vehicle/Vehicle.html' }).
        when('/Parenting/Supervision/user/:userId', { caseInsensitiveMatch: true, controller: PrivacyCtrl, templateUrl: '/app/Parenting/Privacy/Privacy.html' }).
        when('/Parenting/Information/user/:userId', { caseInsensitiveMatch: true, controller: InformationCtrl, templateUrl: '/app/Parenting/Information/Information.html' }).
        when('/Parenting/Decision/user/:userId/child/:childId', { caseInsensitiveMatch: true, controller: DecisionCtrl, templateUrl: '/app/Parenting/Decision/Decision.html' }).
        when('/Parenting/Responsibility/user/:userId', { caseInsensitiveMatch: true, controller: ResponsibilityCtrl, templateUrl: '/app/Parenting/Responsibility/Responsibility.html' }).
        when('/Parenting/Communication/user/:userId', { caseInsensitiveMatch: true, controller: CommunicationCtrl, templateUrl: '/app/Parenting/Communication/Communication.html' }).
        when('/Parenting/Schedule/user/:userId', { caseInsensitiveMatch: true, controller: ScheduleCtrl, templateUrl: '/app/Parenting/Schedule/Schedule.html' }).
        when('/Parenting/Holiday/user/:userId/child/:childId', { caseInsensitiveMatch: true, controller: HolidayCtrl, templateUrl: '/app/Parenting/Holiday/Holiday.html' }).
        when('/Parenting/Addendum/user/:userId', { caseInsensitiveMatch: true, controller: AddendumCtrl, templateUrl: '/app/Parenting/Addendum/Addendum.html' }).
        when('/Financial/ChildCare/user/:userId/child/:childId', { caseInsensitiveMatch: true, controller: ChildCareCtrl, templateUrl: '/app/Financial/ChildCare/ChildCare.html' }).
        when('/Financial/ChildSupport/user/:userId', { caseInsensitiveMatch: true, controller: ChildSupportCtrl, templateUrl: '/app/Financial/ChildSupport/ChildSupport.html' }).
        when('/Financial/Health/user/:userId', { caseInsensitiveMatch: true, controller: HealthInsuranceCtrl, templateUrl: '/app/Financial/Health/Health.html' }).
        when('/Financial/ExtraExpense/user/:userId/child/:childId', { caseInsensitiveMatch: true, controller: ExtraExpenseCtrl, templateUrl: '/app/Financial/ExtraExpense/ExtraExpense.html' }).
        when('/Financial/Income/user/:userId/:isOtherParent', { caseInsensitiveMatch: true, controller: IncomeCtrl, templateUrl: '/app/Financial/Income/Income.html' }).
        when('/Financial/SocialSecurity/user/:userId/:isOtherParent', { caseInsensitiveMatch: true, controller: SocialSecurityCtrl, templateUrl: '/app/Financial/SocialSecurity/SocialSecurity.html' }).
        when('/Financial/Support/user/:userId/:isOtherParent', { caseInsensitiveMatch: true, controller: SupportCtrl, templateUrl: '/app/Financial/Support/Support.html' }).
        when('/Financial/OtherChild/user/:userId/:isOtherParent', { caseInsensitiveMatch: true, controller: OtherChildCtrl, templateUrl: '/app/Financial/OtherChild/OtherChild.html' }).
        when('/Financial/Deviation/user/:userId', { caseInsensitiveMatch: true, controller: DeviationCtrl, templateUrl: '/app/Financial/Deviation/Deviation.html' }).
        when('/Output/FormComplete/:formName/user/:userId', { caseInsensitiveMatch: true, controller: FormCompleteCtrl, templateUrl: '/app/Output/FormComplete/FormComplete.html' }).
        when('/Output/Parenting/User/:userId', { caseInsensitiveMatch: true, controller: ParentingCtrl, templateUrl: '/app/Output/Parenting/Parenting.html' }).
        when('/Output/DomesticMediation/User/:userId', { caseInsensitiveMatch: true, controller: DomesticMediationCtrl, templateUrl: '/app/Output/DomesticMediation/DomesticMediation.html' }).
        when('/Output/ScheduleA/User/:userId', { caseInsensitiveMatch: true, controller: ScheduleACtrl, templateUrl: '/app/Output/ScheduleA/ScheduleA.html' }).
        when('/Output/ScheduleB/User/:userId', { caseInsensitiveMatch: true, controller: ScheduleBCtrl, templateUrl: '/app/Output/ScheduleB/ScheduleB.html' }).
        when('/Output/ScheduleD/User/:userId', { caseInsensitiveMatch: true, controller: ScheduleDCtrl, templateUrl: '/app/Output/ScheduleD/ScheduleD.html' }).
        when('/Output/ScheduleE/User/:userId', { caseInsensitiveMatch: true, controller: ScheduleECtrl, templateUrl: '/app/Output/ScheduleE/ScheduleE.html' }).
        when('/Output/ChildSupport/User/:userId', { caseInsensitiveMatch: true, controller: ChildSupportOutputCtrl, templateUrl: '/app/Output/ChildSupport/ChildSupport.html' }).
        when('/Output/CSA/User/:userId', { caseInsensitiveMatch: true, controller: CSACtrl, templateUrl: '/app/Output/CSA/CSA.html' }).
        when('/Administrator/Register/LawFirm/:lawFirmId', { caseInsensitiveMatch: true, controller: RegisterAdminCtrl, templateUrl: '/app/Administrator/Register/RegisterAdmin.html' }).
        when('/Administrator/RegisterFirm/Subscription/:subscription', { caseInsensitiveMatch: true, controller: RegisterFirmCtrl, templateUrl: '/app/Administrator/RegisterFirm/RegisterFirm.html' }).
        when('/Administrator/Pricing', { caseInsensitiveMatch: true, controller: PricingCtrl, templateUrl: '/app/Administrator/Pricing/Pricing.html' }).
        when('/Administrator/Payment/User/:userId', { caseInsensitiveMatch: true, controller: PaymentCtrl, templateUrl: '/app/Administrator/Payment/Payment.html' }).
        when('/Administrator/Agreement/User/:userId', { caseInsensitiveMatch: true, controller: AgreementCtrl, templateUrl: '/app/Administrator/Agreement/Agreement.html' }).
        when('/Administrator/CreateAttorney/User/:userId', { caseInsensitiveMatch: true, controller: CreateAttorneyCtrl, templateUrl: '/app/Administrator/CreateAttorney/CreateAttorney.html' }).
        when('/Administrator/ClientCases/User/:userId', { caseInsensitiveMatch: true, controller: ClientCasesCtrl, templateUrl: '/app/Administrator/ClientCases/ClientCases.html' }).
        when('/Attorney/AttorneyPage/Attorney/:userId', { caseInsensitiveMatch: true, controller: AttorneyPageCtrl, templateUrl: '/app/Attorney/AttorneyPage/AttorneyPage.html' }).
        when('/Attorney/CreateClient/Attorney/:userId', { caseInsensitiveMatch: true, controller: CreateClientCtrl, templateUrl: '/app/Attorney/CreateClient/CreateClient.html' }).
        when('/Account/Login/', { caseInsensitiveMatch: true, controller: LoginCtrl, templateUrl: '/app/Account/Login/Login.html' }).
        when('/Account/Logoff/', { caseInsensitiveMatch: true, controller: LogoffCtrl, templateUrl: '/app/Account/Logoff/Logoff.html' }).
        when('/Account/Unauthorized/', { caseInsensitiveMatch: true, controller: UnauthorizedCtrl, templateUrl: '/app/Account/Unauthorized/Unauthorized.html' }).
        when('/Account/Register/', { caseInsensitiveMatch: true, controller: RegisterCtrl, templateUrl: '/app/Account/Register/Register.html' }).
        when('/', { caseInsensitiveMatch: true, controller: HomeCtrl, templateUrl: '/app/Home/home.html' }).
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

function integerFormatter(value) {
    if (value) {
        return parseInt(value);
    }
}
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
            ctrl.$formatters.push(integerFormatter);
        }
    };
});

