FormsApp.factory('menuService', function ($location, $rootScope, $resource) {
    var service = {
        menuItems: [],
        isInitialized: false,
        setItems: function (menuItems) {
            service.menuItems = menuItems;            
        },
        menu: $resource('/api/menus/:userId', { userId: '@userId' },
            {
                getList: { method: 'GET', isArray:true, params: { format: 'json' } },
            }),
        getMenu: function (callback) {
            var userId = service.userId;
            if (typeof userId === 'undefined')
                userId = 0;
            service.menu.getList({ Route: $location.path(), UserId: userId }, function (menuItems) {
                service.setItems(menuItems);
                service.isInitialized = true;
                if (callback)
                    callback();
            });
        },
        setSubMenuIconClass: function (pathIdentifier, subMenuPathIdentifier, iconClass) {
            var menuItem = _.find(service.menuItems, function (item) {
                return item.pathIdentifier === pathIdentifier;
            });
            var subMenuItem = _.find(menuItem.subMenuItems, function (subItem) {
                return subItem.pathIdentifier === subMenuPathIdentifier;
            });
            subMenuItem.iconClass = iconClass;
        },
        isActive: function (pathIdentifier, subMenuPathIdentifier) {
            if (service.isInitialized) {
                var menuItem = _.find(service.menuItems, function(item) {
                    return item.pathIdentifier === pathIdentifier;
                });
                var subMenuItem = _.find(menuItem.subMenuItems, function(subItem) {
                    return subItem.pathIdentifier === subMenuPathIdentifier;
                });
                return subMenuItem.itemClass === 'active';
            }
            return false;
        },
        setActive: function (pathIdentifier, subMenuPathIdentifier) {
            if (!service.isInitialized) {
                service.getMenu(service.setActiveCallback(pathIdentifier, subMenuPathIdentifier));                
            } else {
                service.setActiveCallback(pathIdentifier, subMenuPathIdentifier)();
            }
        },
        //Since Menu needs to load before we run this, we make this a callback function - made a closure so that we can pass args to the callback
        setActiveCallback: function (pathIdentifier, subMenuPathIdentifier) {
            return function() {
                service.checkForm();
                service.clearActive();
                var menuItem = _.find(service.menuItems, function(item) {
                    return item.pathIdentifier === pathIdentifier;
                });
                if (subMenuPathIdentifier === null) {
                    //no submenu to worry about
                    menuItem.itemClass = 'active';
                } else {
                    menuItem.showSubMenu = true;
                    menuItem.itemClass = 'submenu active';
                    var subMenuItem = _.find(menuItem.subMenuItems, function(subItem) {
                        return subItem.pathIdentifier === subMenuPathIdentifier;
                    });
                    subMenuItem.itemClass = 'active';
                    subMenuItem.iconClass = 'icon-blue icon-pencil';
                    $location.path(encodeURI(subMenuItem.path));
                }
            };
        },
        clearActive: function() {
            angular.forEach(service.menuItems, function (item) {
                for (var i = 0; i < item.subMenuItems.length; i++) {
                    item.subMenuItems[i].itemClass = '';
                }
                item.itemClass = '';
            });
        },
        checkForm: function() {
            //check to see if submenu/form is currently open.  If so, we need to save this form;
            var subMenuItem;
            for(var i=0; i<service.menuItems.length; i++) {
                var item = service.menuItems[i];
                subMenuItem = _.find(item.subMenuItems, function (subItem) {
                    return subItem.iconClass === 'icon-blue icon-pencil';
                });
                if (subMenuItem) {
                    var currentFormScope = $rootScope.$root.currentScope;
                    currentFormScope.submit(true);//true disables automatic navigation in controller. Navigation will be completed in the menuService setActive handler                    
                    break;
                }
            }
        },
        userId: 0,
    };
    return service;

});
