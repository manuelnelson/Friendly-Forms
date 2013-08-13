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
        getMenuGroupByPath: function (path) {
            for (var i = 0; i < service.menuItems.length; i++) {
                var item = service.menuItems[i];
                //Return just menu item if match
                if (item.path === path) {
                    return {
                        menuItem: item,
                        subMenuItem: null
                    };
                }
                //else look for submenuItem
                var subMenuItem = _.find(item.subMenuItems, function (subItem) {
                    return subItem.path === path;
                });
                if (subMenuItem) {                    
                    return {
                        menuItem: item,
                        subMenuItem: subMenuItem
                    };
                }
            }
        },
        setSubMenuIconClass: function (path, iconClass) {
            var menuGroup = service.getMenuGroupByPath(path);
            if (menuGroup.subMenuItem)
                menuGroup.subMenuItem.iconClass = iconClass;
        },
        isActive: function (path) {
            if (service.isInitialized) {
                var menuGroup = service.getMenuGroupByPath(path);
                if(menuGroup.subMenuItem)
                    return menuGroup.subMenuItem.itemClass === 'active';
            }
            return false;
        },
        setActive: function (path) {
            if (!service.isInitialized) {
                service.getMenu(service.setActiveCallback(path));                
            } else {
                service.setActiveCallback(path)();
            }
        },
        //Since Menu needs to load before we run this, we make this a callback function - made a closure so that we can pass args to the callback
        setActiveCallback: function (path) {
            return function() {
                service.checkForm();
                service.clearActive();
                //Check to see if first menu level is the path
                var menuItem = _.find(service.menuItems, function (item) {
                    return item.path === path;
                });
                if (menuItem) {
                    menuItem.itemClass = 'active';
                }
                //Must be subMenu Level
                var menuGroup = service.getMenuGroupByPath(path);
                menuGroup.menuItem.showSubMenu = true;
                menuGroup.menuItem.itemClass = 'submenu active';
                menuGroup.subMenuItem.itemClass = 'active';
                menuGroup.subMenuItem.iconClass = 'icon-blue icon-pencil';
                $location.path(menuGroup.subMenuItem.path);
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
