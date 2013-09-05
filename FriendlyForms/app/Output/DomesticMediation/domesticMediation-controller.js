var DomesticMediationCtrl = function ($scope, $routeParams, $location, $timeout, domesticMediationService, menuService, genericService, headerService,$rootScope) {
    $scope.storageKey = $location.path();
    domesticMediationService.domesticMediations.get({ UserId: $routeParams.userId }, function (data) {
        $scope.domesticMediation = data;
        $timeout(function () {
            var html = $('#main-content').html();
            html = html.replace(/<form.*>/, "");
            html = html.replace(/<input.*>/g, "");
            html = html.replace(/<footer[^>]*?>([\s\S]*)<\/footer>/, "");
            $('.html').val(html);
        }, 2000);
        //TODO: Find a non-jquery dependency way of doing this - angulars jqLite seems to be able to handle this
    });
    $scope.submit = function () {
    };
    $rootScope.currentScope = $scope;
    headerService.hide();
    headerService.showOutputHeader();
};
DomesticMediationCtrl.$inject = ['$scope', '$routeParams', '$location', '$timeout', 'domesticMediationService', 'menuService', 'genericService', 'headerService','$rootScope'];