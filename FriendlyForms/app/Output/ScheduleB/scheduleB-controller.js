var ScheduleBCtrl = ['$scope', '$routeParams', '$rootScope', 'scheduleBService', 'menuService', 'genericService', 'headerService', '$timeout', '$location',
    function ($scope, $routeParams, $rootScope, scheduleBService, menuService, genericService, headerService, $timeout, $location) {
    $scope.showPrintButton = false;
    $scope.isLoaded = false;
    scheduleBService.scheduleBs.get({ UserId: $routeParams.userId }, function (data) {
        $scope.scheduleB = data;
        $scope.showFatherOtherChildren = scheduleBService.showOtherChildren(data.ScheduleB);
        $scope.showMotherOtherChildren =  scheduleBService.showOtherChildren(data.OtherScheduleB);
        $scope.showPreexistingChildren = scheduleBService.showPreexistingChildren(data.ScheduleB) || scheduleBService.showPreexistingChildren(data.OtherScheduleB);
        $scope.isLoaded = true;
        $timeout(function () {
            var html = $('.widget-content').html();
            html = html.replace(/<form.*>/, "");
            html = html.replace(/<input type="submit".*>/g, "");
            html = html.replace(/<footer[^>]*?>([\s\S]*)<\/footer>/, "");
            $('.html').val(html);
            $('.name').val('ScheduleB');
            headerService.showOutputHeader();
            $scope.showPrintButton = true;
        }, 2500);
    });
    $scope.submit = function (noNavigate) {
        var menuGroup = menuService.getMenuGroupByPath($location.path());
        menuGroup.subMenuItem.iconClass = "";
    };
    $rootScope.currentScope = $scope;
    headerService.hide();
}];
