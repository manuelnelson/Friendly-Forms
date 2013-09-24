var ChildSupportOutputCtrl = ['$scope', '$routeParams', '$rootScope', 'childSupportOutputService', 'menuService', 'headerService', '$timeout',
    function ($scope, $routeParams, $rootScope, childSupportOutputService, menuService, headerService, $timeout) {
    $scope.showPrintButton = false;
    $scope.isLoaded = false;
    childSupportOutputService.childSupports.get({ UserId: $routeParams.userId }, function (data) {
        $scope.childSupport = data;
        $scope.isLoaded = true;
        $timeout(function () {
            var html = $('#main-content').html();
            html = html.replace(/<form.*>/, "");
            html = html.replace(/<input.*>/g, "");
            html = html.replace(/<footer[^>]*?>([\s\S]*)<\/footer>/, "");
            $('.html').val(html);
            $('.name').val('ChildSupportWorksheet');
            headerService.showOutputHeader();
            $scope.showPrintButton = true;
        }, 2500);
    });
    $scope.submit = function (noNavigate) {
    };
    $rootScope.currentScope = $scope;
    headerService.hide();
}];