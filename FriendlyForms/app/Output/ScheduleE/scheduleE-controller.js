var ScheduleECtrl = ['$scope', '$routeParams', '$rootScope', 'scheduleEService', 'menuService', 'genericService', 'headerService', '$timeout',
    function ($scope, $routeParams, $rootScope, scheduleEService, menuService, genericService, headerService, $timeout) {
    $scope.showPrintButton = false;
    scheduleEService.scheduleEs.get({ UserId: $routeParams.userId }, function (data) {
        $scope.scheduleE = data;
        $timeout(function () {
            var html = $('#main-content').html();
            html = html.replace(/<form.*>/, "");
            html = html.replace(/<input.*>/g, "");
            html = html.replace(/<footer[^>]*?>([\s\S]*)<\/footer>/, "");
            $('.html').val(html);
            $('.name').val('ScheduleE');
            headerService.showOutputHeader();
            $scope.showPrintButton = true;
        }, 2500);
    });
    $scope.submit = function (noNavigate) {
    };
    $rootScope.currentScope = $scope;
    headerService.hide();
}];
