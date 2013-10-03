var ScheduleACtrl = ['$scope', '$routeParams', '$rootScope', 'scheduleAService', 'menuService', 'genericService', 'headerService', '$timeout', '$location',
    function ($scope, $routeParams, $rootScope, scheduleAService, menuService, genericService, headerService, $timeout, $location) {
    $scope.showPrintButton = false;
    $scope.isLoaded = false;
    scheduleAService.scheduleAs.get({ UserId: $routeParams.userId }, function (data) {
        if (!data.Income.OtherDetails)
            data.Income.OtherDetails = 'There is no reason for having other income.';
        if (!data.OtherIncome.OtherDetails)
            data.Income.OtherDetails = 'There is no reason for having other income.';
        $scope.scheduleA = data;
        $scope.isLoaded = true;
        $timeout(function () {
            var html = $('.widget-content').html();
            html = html.replace(/<form.*>/, "");
            html = html.replace(/<input type="submit".*>/g, "");
            html = html.replace(/<footer[^>]*?>([\s\S]*)<\/footer>/, "");
            $('.html').val(html);
            $('.name').val('ScheduleA');
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
