FormsApp.factory('menuService', ['$location', '$rootScope', '$resource', '$routeParams', function ($location, $rootScope, $resource, $routeParams) {
    var service = {
        //#region Props
        menuItems: [],
        userId: 0,
        firstChildId: 0,
        isInitialized: false,
        //#endregion

        //#region Events
        //Get's the menu item from the menuList by the path
        clearActive: function () {
            angular.forEach(service.menuItems, function (item) {
                for (var i = 0; i < item.subMenuItems.length; i++) {
                    item.subMenuItems[i].itemClass = '';
                }
                item.itemClass = '';
            });
        },
        saveCurrentForm: function () {
            //check to see if submenu/form is currently open.  If so, we need to save this form;
            var subMenuItem;
            for (var i = 0; i < service.menuItems.length; i++) {
                var item = service.menuItems[i];
                subMenuItem = _.find(item.subMenuItems, function (subItem) {
                    return subItem.iconClass === 'icon-blue icon-pencil';
                });
                if (subMenuItem) {
                    var currentFormScope = $rootScope.$root.currentScope;
                    currentFormScope.submit(true); //true disables automatic navigation in controller. Navigation will be completed in the menuService setActive handler                    
                    break;
                }
            }
        },
        children: $resource('/api/child/:userId', { userId: '@userId' },
            {
                update: { method: 'PUT' },
            }),
        getFirstChildId: function() {
            for (var i = 0; i < service.menuItems.length; i++) {
                var item = service.menuItems[i];
                var subMenuItem = _.find(item.subMenuItems, function (subItem) {
                    return subItem.path.toUpperCase().indexOf('/CHILD/') > -1;
                });
                if (subMenuItem) {
                    var regex = /\/(child|Child)\/(\d*)/;
                    var firstChildId = subMenuItem.path.match(regex)[2];
                    service.firstChildId = firstChildId;
                }
            }
        },
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
            //children don't have their own menu. Only the first child does.  So if path has child, replace childId with FirstChildId to get proper menu item
            if (path.indexOf('/child/') > -1 || path.indexOf('/Child/') > -1) {
                if (service.firstChildId == 0)
                    service.getFirstChildId();
                var regex = /\/(child|Child)\/(\d*)/;
                path = path.replace(regex, "/$1/" + service.firstChildId);
            }
            for (var i = 0; i < service.menuItems.length; i++) {
                var item = service.menuItems[i];
                //Return just menu item if match
                if (item.path.toUpperCase() === path.toUpperCase()) {
                    return {
                        menuItem: item,
                        subMenuItem: null
                    };
                }
                //else look for submenuItem
                var subMenuItem = _.find(item.subMenuItems, function (subItem) {
                    return subItem.path.toUpperCase() === path.toUpperCase();
                });
                if (subMenuItem) {
                    return {
                        menuItem: item,
                        subMenuItem: subMenuItem
                    };
                }
            }
        },
        isActive: function (path) {
            if (service.isInitialized) {
                var menuGroup = service.getMenuGroupByPath(path);
                if (menuGroup.subMenuItem)
                    return menuGroup.subMenuItem.itemClass === 'active';
            }
            return false;
        },
        menu: $resource('/api/menus/:userId', { userId: '@userId' },
            {
                getList: { method: 'GET', isArray: true, params: { format: 'json' } },
            }),
        nextMenu: function () {
            //Get current menu from the current path
            var menuGroup = service.getMenuGroupByPath($location.path());
            var ndx = _.indexOf(_.pluck(menuGroup.menuItem.subMenuItems, 'path'), menuGroup.subMenuItem.path);
            //if we are at the last menu item, go to the form completion page
            if (ndx == menuGroup.menuItem.subMenuItems.length - 1) {
                //go to form complete page
                var path = '/Output/FormComplete/' + menuGroup.menuItem.text.replace(' ', '') + '/user/' + $routeParams.userId;
                $location.path(path);
                return;
            }
            var nextSubMenu = menuGroup.menuItem.subMenuItems[ndx + 1];
            $location.path(nextSubMenu.path);
        },
        previousMenu: function () {
            //Get current menu from the current path
            var menuGroup = service.getMenuGroupByPath($location.path());
            var ndx = _.indexOf(_.pluck(menuGroup.menuItem.subMenuItems, 'path'), menuGroup.subMenuItem.path);
            var nextSubMenu = menuGroup.menuItem.subMenuItems[ndx - 1];
            $location.path(nextSubMenu.path);
        },
        setActive: function (path, saveForm) {
            if (typeof saveForm === 'undefined') {
                saveForm = true;
            }
            if (!service.isInitialized) {
                service.getMenu(service.setActiveCallback(path, saveForm));
            } else {
                service.setActiveCallback(path, saveForm)();
            }
        },
        setItems: function (menuItems) {
            service.menuItems = menuItems;
        },
        setSubMenuIconClass: function (path, iconClass) {
            var menuGroup = service.getMenuGroupByPath(path);
            if (menuGroup.subMenuItem)
                menuGroup.subMenuItem.iconClass = iconClass;
        },
        //Since Menu needs to load before we run this, we make this a callback function - made a closure so that we can pass args to the callback
        setActiveCallback: function (path, saveForm) {
            return function () {
                if(saveForm)
                    service.saveCurrentForm();
                service.clearActive();
                //Check to see if first menu level is the path
                var menuItem = _.find(service.menuItems, function (item) {
                    return item.path === path;
                });
                if (menuItem) {
                    menuItem.itemClass = 'active';
                } else {
                    //Must be subMenu Level
                    var menuGroup = service.getMenuGroupByPath(path);
                    menuGroup.menuItem.showSubMenu = true;
                    menuGroup.menuItem.itemClass = 'submenu active';
                    menuGroup.subMenuItem.itemClass = 'active';
                    menuGroup.subMenuItem.iconClass = 'icon-blue icon-pencil';
                    //Navigate to new path if we are not already there.
                    if($location.path() !== menuGroup.subMenuItem.path)
                        $location.path(menuGroup.subMenuItem.path);
                }
            };
        },
        //#endregion
    };

    return service;

}]);
