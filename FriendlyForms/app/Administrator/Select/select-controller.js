var SelectCtrl = ['$scope', '$routeParams', '$location', 'selectService', 'menuService', 'headerService', '$rootScope', function($scope, $routeParams, $location, selectService, menuService, headerService, $rootScope) {
    $scope.showErrors = false;
    $scope.submit = function() {
        if ($scope.selectForm.$invalid) {
            $scope.showErrors = true;
            return;
        }
        switch ($scope.select.direction) {
            case '1':
                $location.path('');
            case '2':
                $location.path('');
            case '3':
                $location.path('');        
        }
    };
    $scope.goHome = function() {
        $location.path('/');
    };
    headerService.setTitle('Select');
}];