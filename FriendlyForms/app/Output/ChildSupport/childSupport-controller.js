﻿var ChildSupportOutputCtrl = ['$scope', '$routeParams', '$rootScope', 'childSupportOutputService', 'menuService', 'headerService', '$timeout',
    function ($scope, $routeParams, $rootScope, childSupportOutputService, menuService, headerService, $timeout) {
    $scope.showPrintButton = false;
    childSupportOutputService.childSupports.get({ UserId: $routeParams.userId }, function (data) {
        $scope.childSupport = data;
        $timeout(function () {
            var html = $('#main-content').html();
            html = html.replace(/<form.*>/, "");
            html = html.replace(/<input.*>/g, "");
            html = html.replace(/<footer[^>]*?>([\s\S]*)<\/footer>/, "");
            $('.html').val(html);
            $('.name').val('ChildSupport');
            headerService.showOutputHeader();
            $scope.showPrintButton = true;
        }, 2500);
    });
    $scope.submit = function (noNavigate) {
    };
    $rootScope.currentScope = $scope;
    headerService.hide();
}];