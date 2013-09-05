var RegisterFirmCtrl = function ($scope, $routeParams, $location, lawFirmService, menuService, headerService, limitToFilter, $http) {
    $scope.submit = function() {
        if ($scope.lawFirmForm.$invalid) {
            return;
        }
        $scope.lawFirm.Subscription = $routeParams.subscription;
        lawFirmService.lawFirms.save(null, $scope.lawFirm, function(data) {
            $location.path('/Administrator/Register/LawFirm/' + data.Id);
        });
    };
    $scope.cities = function (cityName) {
        return $http.get('http://ws.geonames.org/searchJSON?country=US&name_startsWith=' + cityName).then(function (response) {
            var names = _.map(response.data.geonames, function (geoName) {
                return geoName.name + ', ' + geoName.adminCode1;
            });
            return limitToFilter(names, 8);
        });
    };
    headerService.setTitle("Register Law Firm");
};
RegisterFirmCtrl.$inject = ['$scope', '$routeParams', '$location', 'lawFirmService', 'menuService', 'headerService', 'limitToFilter', '$http'];