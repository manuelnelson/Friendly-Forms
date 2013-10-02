var ChildSupportOutputCtrl = ['$scope', '$routeParams', '$rootScope', 'childSupportOutputService', 'menuService', 'headerService', '$timeout', '$location',
    function ($scope, $routeParams, $rootScope, childSupportOutputService, menuService, headerService, $timeout, $location) {
    $scope.showPrintButton = false;
    $scope.isLoaded = false;
    childSupportOutputService.childSupports.get({ UserId: $routeParams.userId }, function (data) {
        $scope.childSupport = data;
        $scope.isLoaded = true;
        $timeout(function () {
            var html = $('.widget-content').html();
            html = html.replace(/<form.*>/, "");
            html = html.replace(/<div class="col-lg-4"><input class="form-control".*>/g, "");
            html = html.replace(/<footer[^>]*?>([\s\S]*)<\/footer>/, "");
            $('.html').val(html);
            $('.name').val('ChildSupportWorksheet');
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