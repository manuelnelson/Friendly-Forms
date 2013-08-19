var FormCompleteCtrl = function($scope, $routeParams, $location, formCompleteService, menuService, genericService, $rootScope) {
    //#region Initialize
    $scope.storageKey = $location.path();
    $scope.formName = $routeParams.formName;
    $scope.CheckingFormProgress = true;
    $scope.NoErrors = true;
    checkProgress();
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

    $scope.submit = function () {
        switch ($routeParams.formName) {
            case 'Parenting':
                $location.path('/Output/Parenting/User/' + $routeParams.userId);
            case 'DomesticMediation':
                $location.path('/Output/DomesticMediation/User/' + $routeParams.userId);
            case 'Financial':
                $location.path('/Output/ScheduleA/User/' + $routeParams.userId);
        }
    };
    $rootScope.currentScope = $scope;
};
FormCompleteCtrl.$inject = ['$scope', '$routeParams', '$location', 'formCompleteService', 'menuService', 'genericService', '$rootScope'];