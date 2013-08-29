var SupportCtrl = function($scope, $routeParams, $location, supportService, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.showAddChild = false;
    $scope.showErrors = false;
    $scope.support = supportService.supports.get({ UserId: $routeParams.userId, IsOtherParent: $routeParams.isOtherParent }, function () {
        if (typeof $scope.support.Id == 'undefined' || $scope.support.Id == 0) {
            //see if garlic has something stored            
            $scope.support = $.jStorage.get($scope.path);
            if ($scope.support)
                $scope.showErrors = true;

        }
    });
    supportService.courts.get({ UserId: $routeParams.userId, IsOtherParent: $routeParams.isOtherParent }, function (result) {
        if (result.PreexistingSupports.length == 0)
            $scope.courts = [];
        else
            $scope.courts = result.PreexistingSupports;
    });
    $scope.addCourt = function() {
        $scope.court.UserId = $routeParams.userId;
        $scope.court.IsOtherParent = $routeParams.isOtherParent;
        supportService.courts.save(null, $scope.court, function (data) {
            $scope.courts.push(data);
            $scope.courtForm.$setPristine();
            $scope.court = '';
        });
    };
    $scope.showChildren = function(court) {
        supportService.children.get({ PreexistingSupportId: court.Id }, function (data) {
            if (data.Children.length == 0)
                $scope.children = [];
            else
                $scope.children = data.Children;
            $scope.showAddChild = true;
            $scope.PreexistingSupportId = court.Id;
        });
    };
    $scope.addChild = function() {
        $scope.child.UserId = $routeParams.userId;
        $scope.child.PreexistingSupportId = $scope.PreexistingSupportId;
        supportService.children.save(null, $scope.child, function (data) {
            $scope.children.push(data.Child);
            $scope.childForm.$setPristine();
            $scope.child = '';
        });
    };
    $scope.submit = function () {
        var isOtherParent = $routeParams.isOtherParent;
        if ($scope.supportForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#supportForm');
            $.jStorage.set($scope.path, value);
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.support.UserId = $routeParams.userId;
        $scope.support.IsOtherParent = isOtherParent;
        if (typeof $scope.support.Id == 'undefined' || $scope.support.Id == 0) {
            supportService.supports.save(null, $scope.support, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
            });
        } else {
            supportService.supports.update({ Id: $scope.support.Id }, $scope.support, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
            });
        }
    };
    $scope.continue = function() {
        menuService.nextMenu();
    };
    $rootScope.currentScope = $scope;

    genericService.refreshPage();

};
SupportCtrl.$inject = ['$scope', '$routeParams', '$location', 'supportService', 'menuService', 'genericService', '$rootScope'];