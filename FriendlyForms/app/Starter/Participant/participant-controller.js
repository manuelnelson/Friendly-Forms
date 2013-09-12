var ParticipantCtrl = function ($scope, $routeParams, $location, participantService, menuService, genericService, $rootScope) {
    $scope.path = $location.path();
    $scope.showErrors = false;
    $scope.participant = participantService.participant.get({ UserId: $routeParams.userId }, function () {
        if (typeof $scope.participant.Id == 'undefined' || $scope.participant.Id == 0) {
            //see if garlic has something stored            
            $scope.participant = $.jStorage.get($scope.path);
            if ($scope.participant)
                $scope.showErrors = true;
        }
    });
    $scope.submit = function (noNavigate) {
        if ($scope.participantForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#participantForm');
            $.jStorage.set($scope.path, value);
            if (!noNavigate)
                menuService.nextMenu();
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.participant.UserId = $routeParams.userId;
        if (typeof $scope.participant.Id == 'undefined' || $scope.participant.Id == 0) {
            participantService.participant.save(null, $scope.participant, function () {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    menuService.nextMenu();
            });
        } else {
            participantService.participant.update({ Id: $scope.participant.Id }, $scope.participant, function () {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
                if (!noNavigate)
                    menuService.nextMenu();
            });
        }
    };
    $scope.changeRelationship = function (type) {
        if (type === 'Plaintiff') {
            var value = parseInt($scope.participant.PlaintiffRelationship);
            $scope.participant.DefendantRelationship = getOtherRelationshipValue(value);
        }
        else {
            var value = parseInt($scope.participant.DefendantRelationship);
            $scope.participant.PlaintiffRelationship = getOtherRelationshipValue(parseInt(value));
        }
    };
    $scope.changeCustody = function (type) {
        if (type === 'Plaintiff') {
            var value = parseInt($scope.participant.PlaintiffCustodialParent);
            $scope.participant.DefendantCustodialParent = getOtherCustodyValue(value);
        }
        else {
            var value = parseInt($scope.participant.DefendantCustodialParent);
            $scope.participant.PlaintiffCustodialParent = getOtherCustodyValue(parseInt(value));
        }
    };
    function getOtherRelationshipValue(value) {
        switch (value) {
            case 1:
                return 2;
            case 2:
                return 1;
            case 3:
                return 4;
            case 4:
                return 3;
        }
        return 1;
    }
    function getOtherCustodyValue(value) {
        switch (value) {
            case 1:
                return 2;
            case 2:
                return 1;
            case 3:
                return 3;
        }
        return 1;
    }
    genericService.refreshPage(function () {
        $rootScope.currentScope = $scope;
    });

};
ParticipantCtrl.$inject = ['$scope', '$routeParams', '$location', 'participantService', 'menuService', 'genericService', '$rootScope'];