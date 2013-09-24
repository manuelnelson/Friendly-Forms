var ScheduleDCtrl = ['$scope', '$routeParams', '$rootScope', 'scheduleDService', 'menuService', 'genericService', 'headerService', '$timeout',
    function ($scope, $routeParams, $rootScope, scheduleDService, menuService, genericService, headerService, $timeout) {
    $scope.showPrintButton = false;
    $scope.isLoaded = false;
    scheduleDService.scheduleDs.get({ UserId: $routeParams.userId }, function (data) {
        $scope.scheduleD = data;
        $scope.isLoaded = true;
        $timeout(function () {
            var html = $('#main-content').html();
            html = html.replace(/<form.*>/, "");
            html = html.replace(/<input.*>/g, "");
            html = html.replace(/<footer[^>]*?>([\s\S]*)<\/footer>/, "");
            $('.html').val(html);
            $('.name').val('ScheduleD');
            headerService.showOutputHeader();
            $scope.showPrintButton = true;
        }, 2500);
    });
    $scope.submit = function (noNavigate) {
    };
    $rootScope.currentScope = $scope;
    headerService.hide();
}];
