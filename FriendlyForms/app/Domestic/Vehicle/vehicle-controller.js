var VehicleCtrl = ['$scope', '$routeParams', '$location', 'vehicleService', 'menuService', 'genericService', '$rootScope', 'participantService',
    function($scope, $routeParams, $location, vehicleService, menuService, genericService, $rootScope, participantService) {
    //#region properties
    $scope.continuePressed = false;
    $scope.path = $location.path();
    //#endregion

    //#region intialize
    genericService.refreshPage(function () {
        $rootScope.currentScope = $scope;
    });
    $scope.showErrors = false;

    vehicleService.vehicleForm.get({ UserId: $routeParams.userId }, function (data) {
        if (typeof data.Id == 'undefined' || data.Id == 0) {
            //see if garlic has something stored            
            $scope.vehicleForm = $.jStorage.get($scope.path);
            $scope.showErrors = true;
        } else {
            $scope.vehicleForm = data;
        }
    });
    participantService.custody.get({ UserId: $routeParams.userId }, function (data) {
        $scope.custodianNames = data.CustodyInformation.CustodianNames;
    });
    vehicleService.vehicles.get({ UserId: $routeParams.userId }, function (data) {
        if (data.Vehicles.length == 0)
            $scope.vehicles = [];
        else
            $scope.vehicles = data.Vehicles;
    });
    //#endregion

    //#region event handlers
    $scope.submit = function () {
        if ($scope.vehicleForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#vehicleForm');
            $.jStorage.set($scope.path, value);
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.vehicleForm.UserId = $routeParams.userId;
        if (typeof $scope.vehicleForm.Id == 'undefined' || $scope.vehicleForm.Id == 0) {
            vehicleService.vehicleForm.save(null, $scope.vehicleForm, function () {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
            });
        } else {
            vehicleService.vehicleForm.update(null, $scope.vehicleForm, function () {
                menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
            });
        }
    };
    $scope.addVehicle = function () {
        $scope.vehicle.UserId = $routeParams.userId;
        $scope.vehicle.vehicleFormId = $scope.vehicleForm.Id;
        vehicleService.vehicles.save(null, $scope.vehicle, function (data) {
            menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
            $scope.vehicles.push(data.Vehicle);
            $scope.addVehicleForm.$setPristine();
            $scope.vehicle = '';

        });
    };
    $scope.deleteVehicle = function (vehicle) {
        vehicleService.vehicles.delete({ Id: vehicle.Id }, function () {
            $scope.vehicles = _.reject($scope.vehicles, function (item) {
                return item.Id == vehicle.Id;
            });
        });
    };
    $scope.editing = false;
    $scope.editVehicle = function (vehicle) {
        $scope.editing = true;
        $scope.editVehicleId = vehicle.Id;
    };
    $scope.doneEdit = function (vehicle) {
        $scope.editing = false;
        $scope.editVehicleId = 0;
        vehicleService.vehicles.update({}, vehicle, function () {
        });
    };

    $scope.continue = function () {
        if ($scope.vehicleForm.$invalid) {
            menuService.setSubMenuIconClass($scope.path, 'icon-pencil icon-red');
        } else {
            menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
        }
        menuService.nextMenu();
    };
    //#endregion    
}];