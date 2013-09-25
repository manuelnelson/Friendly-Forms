var CSACtrl = ['$scope', '$routeParams', '$rootScope', 'csaService', 'menuService', 'genericService', 'headerService', '$timeout','$location',
    function ($scope, $routeParams, $rootScope, csaService, menuService, genericService, headerService, $timeout, $location) {
    $scope.showPrintButton = false;
    $scope.isLoaded = false;
    csaService.csas.get({ UserId: $routeParams.userId }, function (data) {
        $scope.csa = data;
        $scope.isLoaded = true;
        $timeout(function () {
            var html = $('.widget-content').html();
            html = html.replace(/<form[^>]*?>([\s\S]*)<\/form>/, "");
            //html = html.replace(/<input.*>/g, "");
            html = html.replace(/<footer[^>]*?>([\s\S]*)<\/footer>/, "");
            $('.html').val(html);
            $('.name').val('ChildSupportAddendum');
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