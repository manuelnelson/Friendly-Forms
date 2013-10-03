﻿var DomesticMediationCtrl = function ($scope, $routeParams, $location, $timeout, domesticMediationService, menuService, genericService, headerService,$rootScope) {
    $scope.showPrintButton = false;
    $scope.isLoaded = false;
    domesticMediationService.domesticMediations.get({ UserId: $routeParams.userId }, function (data) {
        $scope.domesticMediation = data;
        $scope.isLoaded = true;
        //TODO: Find a non-jquery dependency way of doing this - angulars jqLite seems to be able to handle this
        $timeout(function () {
            var html = $('.widget-content').html();
            html = html.replace(/<form.*>/, "");
            html = html.replace(/<input type="submit".*>/g, "");
            html = html.replace(/<footer[^>]*?>([\s\S]*)<\/footer>/, "");
            $('.html').val(html);
            $('.name').val('MediationAgreement');
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
};
DomesticMediationCtrl.$inject = ['$scope', '$routeParams', '$location', '$timeout', 'domesticMediationService', 'menuService', 'genericService', 'headerService','$rootScope'];