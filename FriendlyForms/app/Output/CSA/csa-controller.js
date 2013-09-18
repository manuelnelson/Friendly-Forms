var CSACtrl = function ($scope, $routeParams, $rootScope, csaService, menuService, genericService, headerService, $timeout) {
    $scope.showPrintButton = false;
    csaService.csas.get({ UserId: $routeParams.userId }, function (data) {
        $scope.csa = data;
        $timeout(function () {
            var html = $('#main-content').html();
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
    };
    $rootScope.currentScope = $scope;
    headerService.hide();
};
CSACtrl.$inject = ['$scope', '$routeParams', '$rootScope', 'csaService', 'menuService', 'genericService', 'headerService', '$timeout'];