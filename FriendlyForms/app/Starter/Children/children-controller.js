var ChildrenCtrl = function ($scope, $routeParams, $location, childService, menuService, genericService, $rootScope) {
    $scope.storageKey = $location.path();
    childService.childForm.get({ UserId: $routeParams.userId }, function (data) {
        if (typeof data.Id == 'undefined' || data.Id == 0) {
            //see if garlic has something stored            
            $scope.childForm = $.jStorage.get($scope.storageKey);
        } else {
            $scope.childForm = data;
        }
    });
    childService.child.get({ UserId: $routeParams.userId }, function (data) {
        if (data.Children.length == 0)
            $scope.children = [];
        else
            $scope.children = data.Children;
    });
    $scope.submitChildForm = function () {
        if ($scope.childForm.$invalid) {
            menuService.setSubMenuIconClass('Starter', 'Children', 'icon-pencil icon-red');
            var value = genericService.getFormInput('#childForm');
            $.jStorage.set($scope.storageKey, value);
            return;
        }
        $.jStorage.deleteKey($scope.storageKey);
        $scope.childForm.UserId = $routeParams.userId;
        if (typeof $scope.childForm.Id == 'undefined' || $scope.childForm.Id == 0) {
            childService.childForm.save(null, $scope.childForm, function () {
                menuService.setSubMenuIconClass('Starter', 'Children', 'icon-ok icon-green');
            });
        } else {
            childService.childForm.update(null, $scope.childForm, function () {
                menuService.setSubMenuIconClass('Starter', 'Children', 'icon-ok icon-green');
            });
        }
    };
    $scope.addChild = function () {
        $scope.child.UserId = $routeParams.userId;
        $scope.child.ChildFormId = $scope.childForm.Id;
        childService.child.save(null, $scope.child, function (data) {
            menuService.setSubMenuIconClass('Starter', 'Children', 'icon-ok icon-green');
            $scope.children.push(data.Child);
        });
    };
    $scope.deleteChild = function (child) {
        childService.child.delete({Id: child.Id}, function () {
            $scope.children = _.reject($scope.children, function(item) {
                return item.Id == child.Id;
            });
        });
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive('Starter', 'Children')) {
        menuService.setActive('Starter', 'Children');
    }
};
ChildrenCtrl.$inject = ['$scope', '$routeParams', '$location', 'childService', 'menuService', 'genericService', '$rootScope'];