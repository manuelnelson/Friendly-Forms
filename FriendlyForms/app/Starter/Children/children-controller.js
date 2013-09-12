var ChildrenCtrl = function ($scope, $routeParams, $location, childService, menuService, genericService, $rootScope) {    
    //#region properties
    $scope.continuePressed = false;
    $scope.path = $location.path();
    //#endregion
    
    //#region intialize
    $scope.showErrors = false;
    genericService.refreshPage(function () {
        $rootScope.currentScope = $scope;
    });


    $scope.childForm = childService.childForm.get({ UserId: $routeParams.userId }, function () {
        if (typeof $scope.childForm.Id == 'undefined' || $scope.childForm.Id == 0) {
            //see if garlic has something stored            
            $scope.childForm = $.jStorage.get($scope.path);
            if ($scope.childForm)
                $scope.showErrors = true;
        }
    });
    childService.child.get({ UserId: $routeParams.userId }, function (data) {
        if (data.Children.length == 0)
            $scope.children = [];
        else
            $scope.children = data.Children;
    });
    //#endregion
    
    //#region event handlers
    $scope.submit = function () {
        if (!$scope.childForm || ($scope.childForm.ChildrenInvolved != 1 && $scope.childForm.ChildrenInvolved != 2)) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#childForm');
            $.jStorage.set($scope.path, value);
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.childForm.UserId = $routeParams.userId;
        if (typeof $scope.childForm.Id == 'undefined' || $scope.childForm.Id == 0) {
            childService.childForm.save(null, $scope.childForm, function (data) {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                $scope.childForm = data.ChildForm;
            });
        } else {
            childService.childForm.update(null, $scope.childForm, function () {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
            });
        }
    };
    $scope.addChild = function () {
        $scope.child.UserId = $routeParams.userId;
        $scope.child.ChildFormId = $scope.childForm.Id;
        childService.child.save(null, $scope.child, function (data) {
            menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
            $scope.children.push(data.Child);
            $scope.addChildForm.$setPristine();
            $scope.child = '';
        });
    };
    $scope.editing = false;
    $scope.editChild = function (child) {
        $scope.editing = true;
        $scope.editChildId = child.Id;
    };
    $scope.doneEdit = function (child) {
        $scope.editing = false;
        $scope.editChildId = 0;
        child.DateOfBirth = child.DateOfBirthString;
        childService.child.update({ }, child, function () {
        });
    };
    $scope.deleteChild = function (child) {
        childService.child.delete({Id: child.Id}, function () {
            $scope.children = _.reject($scope.children, function(item) {
                return item.Id == child.Id;
            });
        });
    };
    $scope.continue = function() {
        $scope.continuePressed = true;
    };
    $scope.nextForm = function () {
        menuService.nextMenu();
    };
    //#endregion    
};
ChildrenCtrl.$inject = ['$scope', '$routeParams', '$location', 'childService', 'menuService', 'genericService', '$rootScope'];