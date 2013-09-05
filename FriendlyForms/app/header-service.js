FormsApp.factory('headerService', ['menuService', '$location', '$resource', function (menuService, $location, $resource) {
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
        },
        setTitle: function(title) {
            if (title)
                service.Title = title;
            else if(service.menuGroup){
                service.Title = service.menuGroup.subMenuItem ? service.menuGroup.subMenuItem.text : service.menuGroup.menuItem.text;
            }
            service.showFeedbackHeader = true;
        },
        setBreadCrumbs: function() {
            service.levels = [];
            if (service.menuGroup && service.menuGroup.menuItem) {
                service.levels.push(service.menuGroup.menuItem);
                if (service.menuGroup.subMenuItem)
                    service.levels.push(service.menuGroup.subMenuItem);
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