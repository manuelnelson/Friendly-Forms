var DecisionCtrl = function($scope, $routeParams, $location, decisionService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    $scope.decision = decisionService.decisions.get({ UserId: $routeParams.userId }, function() {
        if (typeof $scope.decision.Id == 'undefined' || $scope.decision.Id == 0) {
            //see if garlic has something stored            
            $scope.decision = $.jStorage.get($scope.storageKey);
        }
    });
    $scope.submit = function(noNavigate) {
        if ($scope.decisionForm.$invalid) {
            menuService.setSubMenuIconClass('Parenting', 'Decision', 'icon-pencil icon-red');
            var value = genericService.getFormInput('#decisionForm');
            $.jStorage.set($scope.storageKey, value);
            if (!noNavigate)
                $location.path('/Parenting/Responsibility/' + $scope.decision.UserId);
            return;
        }
        $.jStorage.deleteKey($scope.storageKey);
        $scope.decision.UserId = $routeParams.userId;
        if (typeof $scope.decision.Id == 'undefined' || $scope.decision.Id == 0) {
            decisionService.Decisions.save(null, $scope.decision, function() {
                menuService.setSubMenuIconClass('Parenting', 'Decision', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Parenting/Responsibility/' + $scope.decision.UserId);
            });
        } else {
            decisionService.Decisions.update({ Id: $scope.decision.Id }, $scope.decision, function() {
                menuService.setSubMenuIconClass('Parenting', 'Decision', 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Parenting/Responsibility/' + $scope.decision.UserId);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive('Parenting', 'Decision')) {
        menuService.setActive('Parenting', 'Decision');
    }
};
DecisionCtrl.$inject = ['$scope', '$routeParams', '$location', 'decisionService', 'menuService', 'genericService', '$rootScope'];