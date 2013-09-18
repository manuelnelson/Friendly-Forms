var ScheduleACtrl = ['$scope', '$routeParams', '$rootScope', 'scheduleAService', 'menuService', 'genericService', 'headerService', '$timeout', 
    function ($scope, $routeParams, $rootScope, scheduleAService, menuService, genericService, headerService, $timeout) {
    $scope.showPrintButton = false;
    scheduleAService.scheduleAs.get({ UserId: $routeParams.userId }, function (data) {
        if (!data.Income.OtherDetails)
            data.Income.OtherDetails = 'There is no reason for having other income.';
        if (!data.OtherIncome.OtherDetails)
            data.Income.OtherDetails = 'There is no reason for having other income.';
        $scope.scheduleA = data;
        $timeout(function () {
            var html = $('#main-content').html();
            html = html.replace(/<form.*>/, "");
            html = html.replace(/<input.*>/g, "");
            html = html.replace(/<footer[^>]*?>([\s\S]*)<\/footer>/, "");
            $('.html').val(html);
            $('.name').val('ScheduleA');
            headerService.showOutputHeader();
            $scope.showPrintButton = true;
        }, 2500);
    });
    $scope.submit = function (noNavigate) {
    };
    $rootScope.currentScope = $scope;
    headerService.hide();
}];
