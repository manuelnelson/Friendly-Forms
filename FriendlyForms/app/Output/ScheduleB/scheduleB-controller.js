var ScheduleBCtrl = ['$scope', '$routeParams', '$rootScope', 'scheduleBService', 'menuService', 'genericService', 'headerService', '$timeout',
    function ($scope, $routeParams, $rootScope, scheduleBService, menuService, genericService, headerService, $timeout) {
    $scope.showPrintButton = false;
    $scope.isLoaded = false;
    scheduleBService.scheduleBs.get({ UserId: $routeParams.userId }, function (data) {
        $scope.scheduleB = data;
        $scope.isLoaded = true;
        $timeout(function () {
            var html = $('#main-content').html();
            html = html.replace(/<form.*>/, "");
            html = html.replace(/<input.*>/g, "");
            html = html.replace(/<footer[^>]*?>([\s\S]*)<\/footer>/, "");
            $('.html').val(html);
            $('.name').val('ScheduleB');
            headerService.showOutputHeader();
            $scope.showPrintButton = true;
        }, 2500);
    });
    $scope.submit = function (noNavigate) {
    };
    $rootScope.currentScope = $scope;
    headerService.hide();
}];
