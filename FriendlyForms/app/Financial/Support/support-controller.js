﻿var SupportCtrl = function($scope, $routeParams, $location, supportService, menuService, genericService, userService, $rootScope, $q) {
    $scope.path = $location.path();
    $scope.showAddChild = false;
    $scope.showErrors = false;
    $scope.isLoaded = false;
    supportService.states.getList({}, function (data) {
        $scope.states = data;
        supportService.courts.get({ UserId: $routeParams.userId, IsOtherParent: $routeParams.isOtherParent }, function (result) {
            if (result.PreexistingSupports.length == 0)
                $scope.courts = [];
            else {
                $scope.courts = result.PreexistingSupports;
                _.each($scope.courts, function (item) {
                    var courtState = _.find($scope.states, function (state) {
                        return state.Id == item.StateId;
                    });
                    item.State = courtState.Name;
                });
            }
        });
    });

    $scope.support = supportService.supports.get({ UserId: $routeParams.userId, IsOtherParent: $routeParams.isOtherParent }, function () {
        $scope.isLoaded = true;
        if (typeof $scope.support.Id == 'undefined' || $scope.support.Id == 0) {
            //see if garlic has something stored            
            $scope.support = $.jStorage.get($scope.path);
            if (typeof $scope.support === 'undefined' || $scope.support === null) {
                $scope.showErrors = true;
                $scope.support = {
                    Support: 0
                };
            }
        }
    });
    $scope.getState = function(courtAdded) {
        var courtState = _.find($scope.states, function (state) {
            return state.Id == courtAdded.StateId;
        });
        courtAdded.State = courtState.Name;
    };

    $scope.addCourt = function() {
        $scope.court.UserId = userService.getFormUserId();
        $scope.court.IsOtherParent = $routeParams.isOtherParent;
        supportService.courts.save(null, $scope.court, function (data) {
            $scope.courts.push(data);
            $scope.courtForm.$setPristine();
            $scope.court = '';
        });
    };
    $scope.courtEditing = false;
    $scope.editCourt = function (court) {
        $scope.courtEditing = true;
        $scope.editCourtId = court.Id;
    };
    $scope.doneCourtEdit = function (court) {
        $scope.courtEditing = false;
        $scope.editCourtId = 0;
        supportService.courts.update({}, court, function () {
        });
    };
    $scope.deleteCourt = function (court) {
        supportService.children.delete({ PreexistingSupportId: court.Id }, function () {
        });
        supportService.courts.delete(court, function () {
            $scope.courts = _.reject($scope.courts, function (item) {
                return item.Id == court.Id;
            });
        });
    };



    $scope.showChildren = function (court) {
        supportService.children.get({ PreexistingSupportId: court.Id }, function (data) {
            if (data.Children.length == 0)
                $scope.children = [];
            else
                $scope.children = data.Children;
            $scope.showAddChild = true;
            $scope.PreexistingSupportId = court.Id;
        });
    };
    $scope.hideAddChild = function () {
        $scope.showAddChild = false;
    };    
    $scope.addChild = function() {
        $scope.child.UserId = userService.getFormUserId();
        $scope.child.PreexistingSupportId = $scope.PreexistingSupportId;
        supportService.children.save(null, $scope.child, function (data) {
            $scope.children.push(data.Child);
            $scope.childForm.$setPristine();
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
        supportService.children.update({}, child, function () {
        });
    };
    $scope.deleteChild = function (child) {
        supportService.children.delete({ Id: child.Id }, function () {
            $scope.children = _.reject($scope.children, function (item) {
                return item.Id == child.Id;
            });
        });
    };




    $scope.submit = function (noNavigate) {
        if (!$scope.support || ($scope.support.Support != "1" && $scope.support.Support != "2")) {
            menuService.setSubMenuIconClass($scope.path, 'icon-exclamation icon-red');
            return;
        }
        $scope.showErrors = false;
        $scope.support.UserId = userService.getFormUserId();
        $scope.support.IsOtherParent = $routeParams.isOtherParent;
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
    genericService.refreshPage(function() {
        $rootScope.currentScope = $scope;
    });

};
SupportCtrl.$inject = ['$scope', '$routeParams', '$location', 'supportService', 'menuService', 'genericService', 'userService', '$rootScope', '$q'];