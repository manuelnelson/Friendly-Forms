var SupportCtrl = function($scope, $routeParams, $location, supportService, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.showAddChild = false;
    $scope.support = supportService.supports.get({ UserId: $routeParams.userId, IsOtherParent: $routeParams.isOtherParent }, function() {
        if (typeof $scope.support.Id == 'undefined' || $scope.support.Id == 0) {
            //see if garlic has something stored            
            $scope.support = $.jStorage.get($scope.path);
        }
    });
    supportService.courts.get({ UserId: $routeParams.userId, IsOtherParent: $routeParams.isOtherParent }, function (result) {
        if (result.PreexistingSupports.length == 0)
            $scope.courts = [];
        else
            $scope.courts = courts.PreexistingSupports;
    });
    $scope.addCourt = function() {
        $scope.court.UserId = $routeParams.userId;
        $scope.court.IsOtherParent = $routeParams.isOtherParent;
        supportService.courts.save(null, $scope.court, function (data) {
            $scope.courts.push(data);
        });
    };
    $scope.showChildren = function(court) {
        supportService.children.get({ PreexistingSupportId: court.Id }, function (data) {
            if (data.Children.length == 0)
                $scope.children = [];
            else
                $scope.children = courts.Children;
            $scope.showAddChild = true;
        });
    };
    $scope.addChild = function() {
        $scope.child.UserId = $routeParams.userId;
        $scope.child.PreexistingSupportId = $scope.court.Id;
        supportService.children.save(null, $scope.child, function (data) {
            $scope.children.push(data.Child);
        });
    };
    $scope.submit = function (noNavigate) {
        var isOtherParent = $routeParams.isOtherParent;
        if ($scope.supportForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#supportForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                $location.path('/Financial/OtherChildren/' + $scope.support.UserId + "/" + isOtherParent);
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.support.UserId = $routeParams.userId;
        $scope.support.IsOtherParent = isOtherParent;
        if (typeof $scope.support.Id == 'undefined' || $scope.support.Id == 0) {
            supportService.supports.save(null, $scope.support, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Financial/OtherChildren/' + $scope.support.UserId + "/" + isOtherParent);
            });
        } else {
            supportService.supports.update({ Id: $scope.support.Id }, $scope.support, function() {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    $location.path('/Financial/OtherChildren/' + $scope.support.UserId + "/" + isOtherParent);
            });
        }
    };
    $rootScope.currentScope = $scope;
    if (!menuService.isActive($scope.path)) {
        menuService.setActive($scope.path);
    }
};
SupportCtrl.$inject = ['$scope', '$routeParams', '$location', 'supportService', 'menuService', 'genericService', '$rootScope'];