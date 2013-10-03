var ParentingCtrl = ['$scope', '$routeParams', '$rootScope', 'parentingService', 'menuService', 'genericService', 'headerService', '$timeout', '$location',
function ($scope, $routeParams, $rootScope, parentingService, menuService, genericService, headerService, $timeout,$location) {
    $scope.showPrintButton = false;
    $scope.isLoaded = false;
    parentingService.parentings.get({ UserId: $routeParams.userId }, function (data) {
        $scope.parenting = data;
        $scope.isLoaded = true;
        $timeout(function () {
            var html = $('.widget-content').html();
            html = html.replace(/<form.*>/, "");
            html = html.replace(/<input type="submit".*>/g, "");
            html = html.replace(/<footer[^>]*?>([\s\S]*)<\/footer>/, "");
            $('.html').val(html);
            $('.name').val('Parenting');
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
