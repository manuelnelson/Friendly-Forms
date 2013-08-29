FormsApp.factory('headerService', ['menuService', '$location', '$resource', function (menuService, $location, $resource) {
    var service = {
        menuGroup: null,
        hide: function() {
            service.showHeader = false;
        },        
        show: function () {
            service.showHeader = true;
        }, emails: $resource('/api/emails/feedback', {},
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
            service.showHeader = true;
        },
        setTitle: function(title) {
            if (title)
                service.Title = title;
            else if(service.menuGroup){
                service.Title = service.menuGroup.subMenuItem ? service.menuGroup.subMenuItem.text : service.menuGroup.menuItem.text;
            }
            service.showHeader = true;
        },
        setBreadCrumbs: function() {
            service.levels = [];
            if (service.menuGroup && service.menuGroup.menuItem) {
                service.levels.push(service.menuGroup.menuItem);
                if (service.menuGroup.subMenuItem)
                    service.levels.push(service.menuGroup.subMenuItem);
            }
            service.showHeader = true;
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