var FormCompleteCtrl = function($scope, $routeParams, $location, formCompleteService, menuService, genericService, headerService, $rootScope) {
    //#region Initialize
    $scope.storageKey = $location.path();
    $scope.formName = $routeParams.formName;
    $scope.CheckingFormProgress = true;
    $scope.NoErrors = true;
    checkProgress();
    $scope.isStarter = $routeParams.formName == 'Starter';
    //#endregion
    function checkProgress() {
        formCompleteService.formCompletes.get({ FormName: $routeParams.formName, UserId: $routeParams.userId }, function (result) {
            if (result.IncompleteForms.length === 0) {
                $scope.NoErrors = true;
            } else {
                $scope.IncompleteForms = result.IncompleteForms;//.join(", ");
                $scope.NoErrors = false;
            }
            $scope.CheckingFormProgress = false;
        });
    }

    $scope.submit = function (noNavigate) {
        if (!noNavigate) {
            switch ($routeParams.formName) {
                case 'ParentingPlan':
                    $location.path('/Output/Parenting/User/' + $routeParams.userId);
                    break;
                case 'MediationAgreement':
                    $location.path('/Output/DomesticMediation/User/' + $routeParams.userId);
                    break;
                case 'FinancialForm':
                    $location.path('/Output/ScheduleA/User/' + $routeParams.userId);
                    break;
                case 'Starter':
                    menuService.getMenu(function () {
                        formCompleteService.child.get({ UserId: $routeParams.userId }, function (data) {
                                $location.path('/Domestic/House/user/' + $routeParams.userId);
                        });
                    });
                    break;
            }
        }
    };
    $rootScope.currentScope = $scope;
    headerService.setTitle('Form Completed');
};
FormCompleteCtrl.$inject = ['$scope', '$routeParams', '$location', 'formCompleteService', 'menuService', 'genericService', 'headerService','$rootScope'];