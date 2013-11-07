var HomeCtrl = ['$scope', '$routeParams', '$route', '$location', 'menuService', 'genericService', 'headerService',
    function ($scope, $routeParams, $route, $location, menuService, genericService, headerService) {
        menuService.setActive($location.path(), false);
        $scope.showMore = function (boxNumber) {
            switch (boxNumber) {
                case 3:
                    $scope.content = '<div class="popout"><p>Don’t be deceived by "Get a Divorce in 1 Hour" claims. Here’s why "Get a divorce in an hour" is just so much hype. </p>' +
                        '<p>Any website which makes such claims of "getting a divorce in an hour" is, to put it bluntly, misleading you. Depending on the complexity of your assets, debts, children and other factors, ' +
                        'simply entering all the data necessary to get a divorce can take anywhere from an hour to several hours. But that is not the same as getting a divorce. There are very few legal shortcuts to getting a divorce. In a best case scenario; one where both parties are in absolute agreement on every issue and are working together to get through the divorce as quickly as possible, we calculate the divorce will take approximately 44 days from the date of filing the Petition for Divorce. Split Solutions is dedicated to getting your documentation in order and providing as much information as we can to help you.</p>' +
                        '<p><a href ng-click="showMore(0)">Back</a><p></div>';
                    break;
                default:
                    $scope.content = '<img style="margin: 0 auto;" src="../../Content/images/Front-Page-Image.jpg" class="img-responsive popout" />';
                    break;
            }
        };
        $scope.showMore(0);
        //TODO: uggghh...had to do with Jquery
        var boxes = $('.slide-panel .popout');
        var maxItem = _.max(boxes, function (item) {
            return $(item).height();
        });
        boxes.height($(maxItem).height());
        
        genericService.refreshPage(function() {
            headerService.setTitle('Split Solutions: The Divorce Solution for Georgia Residents');
        });
    }];
