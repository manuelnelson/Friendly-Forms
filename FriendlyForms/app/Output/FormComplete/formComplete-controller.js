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
                var outputPaths = formCompleteService.getOutputPaths($routeParams.formName, $routeParams.userId);
                for (var i = 0; i < outputPaths.length; i++) {
                    menuService.enableMenu(outputPaths[i]);
                }
            } else {
                $scope.IncompleteForms = result.IncompleteForms;//.join(", ");
                $scope.NoErrors = false;
            }
            $scope.CheckingFormProgress = false;
        });
    }

    $scope.submit = function (noNavigate) {
        if (noNavigate)
            return;
        //special case for starter since we need to reload menu
        if ($routeParams.formName === 'Starter') {
            menuService.getMenu().then(function() {
                $location.path('/Domestic/House/user/' + $routeParams.userId);
            });
        } else {
            $location.path(formCompleteService.getOutputPaths($routeParams.formName, $routeParams.userId)[0]);
        }
    };
    $rootScope.currentScope = $scope;
    headerService.setTitle('Form Completed');
};
FormCompleteCtrl.$inject = ['$scope', '$routeParams', '$location', 'formCompleteService', 'menuService', 'genericService', 'headerService','$rootScope'];