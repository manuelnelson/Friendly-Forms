var ExtraExpenseCtrl = function ($scope, $routeParams, $location, extraExpenseService, menuService, genericService, $rootScope) {
    //#region Intialize
    $scope.path = $location.path();
    $scope.showErrors = false;
    $scope.showMessage = false;
    $rootScope.currentScope = $scope;
    extraExpenseService.children.get({ UserId: $routeParams.userId }, function (data) {
        $scope.children = data.Children;
        $scope.childNdx = _.indexOf(_.pluck($scope.children, 'Id'), parseInt($routeParams.childId));
        $scope.childName = $scope.children[$scope.childNdx].Name;
        $scope.menuPath = '/Financial/ExtraExpense/' + $routeParams.userId + '/' + $scope.children[0].Id;
        if (!menuService.isActive($scope.menuPath)) {
            menuService.setActive($scope.menuPath);
        }
    });
    //#endregion

    //#region Event Handlers
    $scope.getChildExtraExpense = function (childId) {
        $scope.extraExpense = extraExpenseService.extraExpenses.get({ ChildId: childId }, function () {
            if (typeof $scope.extraExpense.Id == 'undefined' || $scope.extraExpense.Id == 0) {
                //see if garlic has something stored            
                $scope.extraExpense = $.jStorage.get($scope.path);
            }
        });
    };
    $scope.extraExpense = extraExpenseService.extraExpenseForms.get({ UserId: $routeParams.userId }, function (data) {
        if (typeof $scope.extraExpense.Id == 'undefined' || $scope.extraExpense.Id == 0) {
            //see if garlic has something stored            
            $scope.extraExpenseForm = $.jStorage.get($scope.path);
        }
        $scope.extraExpenseForm = data;
    });
    
    $scope.submit = function (noNavigate) {
        if ($scope.extraExpenseForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#extraExpenseForm');
            $.jStorage.set($scope.path, value);
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.extraExpenseForm.UserId = $routeParams.userId;
        if (typeof $scope.extraExpenseForm.Id == 'undefined' || $scope.extraExpenseForm.Id == 0) {
            extraExpenseService.extraExpenseForms.save(null, $scope.extraExpenseForm, function () {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
            });
        } else {
            extraExpenseService.extraExpenseForms.update(null, $scope.extraExpenseForm, function () {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
            });
        }
    };
    $scope.submitExtraExpense = function (callback) {
        if ($scope.extraExpenseChildForm.$invalid) {
            menuService.setSubMenuIconClass($scope.menuPath, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#extraExpenseChildForm');
            $.jStorage.set($scope.path, value);
            $scope.showErrors = true;
            if (callback)
                callback();
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.extraExpense.UserId = $routeParams.userId;
        $scope.extraExpense.ChildId = $routeParams.childId;
        if (typeof $scope.extraExpense.Id == 'undefined' || $scope.extraExpense.Id == 0) {
            extraExpenseService.extraExpenses.save(null, $scope.extraExpense, function () {
                callback();
            });
        } else {
            extraExpenseService.extraExpenses.update({ Id: $scope.extraExpense.Id }, $scope.extraExpense, function () {
                callback();
            });
        }
        menuService.setSubMenuIconClass($scope.menuPath, 'icon-ok icon-green');
    };
    $scope.previousChild = function () {
        $scope.submitExtraExpense(function () {
            $scope.childNdx = _.indexOf(_.pluck($scope.children, 'Id'), parseInt($routeParams.childId));
            if ($scope.childNdx < 0) {
                //Navigate to previous menu
                return;
            }
            $scope.childNdx = $scope.childNdx - 1;
            var childId = $scope.children[$scope.childNdx].Id;
            $location.path('/Financial/ExtraExpense/' + $routeParams.userId + '/' + childId);
        });
    };
    $scope.nextChild = function () {
        $scope.submitExtraExpense(function () {
            $scope.childNdx = _.indexOf(_.pluck($scope.children, 'Id'), parseInt($routeParams.childId));
            if ($scope.childNdx === ($scope.children.length - 1)) {
                //Navigate to next item
                return;
            }
            $scope.childNdx = $scope.childNdx + 1;
            var childId = $scope.children[$scope.childNdx].Id;
            menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
            $location.path('/Financial/ExtraExpense/' + $routeParams.userId + '/' + childId);
        });
    };

    //#endregion

    $scope.getChildExtraExpense($routeParams.childId);

};
ExtraExpenseCtrl.$inject = ['$scope', '$routeParams', '$location', 'extraExpenseService', 'menuService', 'genericService', '$rootScope'];