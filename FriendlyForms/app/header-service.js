FormsApp.factory('headerService', ['menuService', '$location', '$resource', 'userService', 'constantsService', '$routeParams', 'participantService',
    function (menuService, $location, $resource, userService, constantsService, $routeParams, participantService) {
    var service = {
        menuGroup: null,
        hide: function() {
            service.showFeedbackHeader = false;
            service.showOutput = false;
        },
        show: function () {
            service.showFeedbackHeader = true;
            service.showOutput = false;
        },
        showOutputHeader: function() {
            service.showFeedbackHeader = false;
            service.showOutput = true;
        },
        hideOutputHeader: function () {
            service.showOutput = false;
        },
        emails: $resource('/api/emails/feedback', {},
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
        initialize: function (path) {
            if (!path)
                service.path = $location.path();
            else
                service.path = path;
            service.menuGroup = menuService.getMenuGroupByPath(service.path);
            service.showFeedbackHeader = true;
            service.showOutput = false;
        },
        setTitle: function(title) {
            if (title)
                service.Title = title;
            else if(service.menuGroup){
                service.Title = service.menuGroup.subMenuItem ? service.menuGroup.subMenuItem.text : service.menuGroup.menuItem.text;
            }
            userService.getCurrentUserSession().then(function(userData) {
                if (_.indexOf(userData.Roles, constantsService.constants.AdminRole) > -1 || _.indexOf(userData.Roles, constantsService.constants.AttorneyRole) > -1) {
                    if (typeof $routeParams.userId != 'undefined' && $routeParams.userId != userData.CustomId) {
                        //If we're on a page that isn't our own, let's display the case information
                        participantService.participant.get({ UserId: $routeParams.userId }, function(data) {
                            if (data.DefendantsName != null && data.DefendantsName != "")
                                service.SecondaryTitle = data.PlaintiffsName + " vs. " + data.DefendantsName;
                        });
                    } else {
                        service.SecondaryTitle = '';
                    }
                }
            });            
            service.showFeedbackHeader = true;
            service.showOutput = false;
        },
        setBreadCrumbs: function() {
            service.levels = [];
            if (service.menuGroup && service.menuGroup.menuItem) {
                if (service.menuGroup.menuItem.text == 'Home')//ignore home since we're already there
                    return;
                service.levels.push(service.menuGroup.menuItem);
                if (service.menuGroup.subMenuItem)
                    service.levels.push(service.menuGroup.subMenuItem);
            } else {
                //make own breadcrumbs using path
                var path = service.path.split("/");
                if (path.length > 2) {
                    service.levels.push({
                        text: path[1].replace(/([A-Z])/g, ' $1').replace(/^./, function (str) { return str.toUpperCase(); }),
                        path: ''
                    });
                    service.Title = path[2].replace(/([A-Z])/g, ' $1').replace(/^./, function(str) { return str.toUpperCase(); });
                    service.levels.push({
                        text: service.Title,
                        path: service.path.substring(1, service.path.length)
                    });
                }
            }
            service.showFeedbackHeader = true;
        },
        path: null,
        refresh: function(path) {
            service.initialize(path);
            service.setTitle();
            service.setBreadCrumbs();            
        }
    };
    return service;
}]);