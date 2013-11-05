FormsApp.factory('menuService', ['$location', '$rootScope', '$resource', '$route', '$q', function ($location, $rootScope, $resource, $route, $q) {
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
        getFirstChildId: function () {
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
        getMenu: function (userId) {
            var deferred = $q.defer();
            if(userId)
                service.userId = userId;
            
            if (service.userId === 0 && typeof $route.current.params.userId != 'undefined') {
                service.userId = $route.current.params.userId;
            }               
            service.menu.getList({ Route: $location.path(), UserId: service.userId }, function (menuItems) {
                service.setItems(menuItems);
                service.isInitialized = true;
                deferred.resolve(menuItems);
            });
            return deferred.promise;
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
        //This can be handy when navigating to someone's case, to just go to the first form
        goToFirstFormMenu: function () {
            //go to first menuItem that has submenus
            var menuItem = _.find(service.menuItems, function (item) {
                return item.subMenuItems && item.subMenuItems.length > 0;
            });
            if (menuItem)
                $location.path(menuItem.subMenuItems[0].path);
            else {
                if (typeof service.menuItems[1] != 'undefined' && service.menuItems[1].text != 'Log out') {
                    //href has hashbang...remove this
                    $location.path(service.menuItems[1].path.replace("/#",""));
                } else {
                    //just go home man...you're drunk
                    $location.path('/');
                }
            }
        },
        enableMenu: function(path) {
            var menuGroup = service.getMenuGroupByPath(path);
            menuGroup.subMenuItem.disabled = false;
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
            {   //DateTime is specifically used as a cache breaker for IE.  cache false doesn't appear to work.
                getList: { method: 'GET', cache: false, isArray: true, params: { format: 'json', DateTime: new Date().getTime() } },
            }),
        nextMenu: function () {
            //Get current menu from the current path
            var menuGroup = service.getMenuGroupByPath($location.path());
            var ndx = _.indexOf(_.pluck(menuGroup.menuItem.subMenuItems, 'path'), menuGroup.subMenuItem.path);
            //if we are at the last menu item, go to the form completion page
            if (ndx == menuGroup.menuItem.subMenuItems.length - 1) {
                //go to form complete page
                var path = '/Output/FormComplete/' + menuGroup.menuItem.text.replace(' ', '') + '/user/' + $route.current.params.userId;
                $location.path(path);
                return;
            }
            var nextSubMenu = menuGroup.menuItem.subMenuItems[ndx + 1];
            ////whenever nextMenu is called, the form is already saved.  Let's set next menu active without saving form.
            $location.path(nextSubMenu.path);
        },
        previousMenu: function () {
            //Get current menu from the current path
            var menuGroup = service.getMenuGroupByPath($location.path());
            var ndx = _.indexOf(_.pluck(menuGroup.menuItem.subMenuItems, 'path'), menuGroup.subMenuItem.path);
            var nextSubMenu = menuGroup.menuItem.subMenuItems[ndx - 1];
            ////whenever previousMenu is called, the form is already saved.  Let's set next menu active without saving form.
            $location.path(nextSubMenu.path);
        },
        saveCurrentForm: function () {
            //check to see if submenu/form is currently open.  If so, we need to save this form;
            var subMenuItem;
            for (var i = 0; i < service.menuItems.length; i++) {
                var item = service.menuItems[i];
                subMenuItem = _.find(item.subMenuItems, function (subItem) {
                    //return subItem.itemClass === 'active';
                    return subItem.iconClass === 'icon-white icon-pencil';
                });
                if (subMenuItem) {
                    var currentFormScope = $rootScope.$root.currentScope;
                    currentFormScope.submit(true); //true disables automatic navigation in controller. Navigation will be completed in the menuService setActive handler                    
                    break;
                }
            }
        },
        setActive: function (path, saveForm) {
            if (typeof saveForm === 'undefined') {
                saveForm = true;
            }
            if (!service.isInitialized) {                
                service.getMenu().then(function () {
                    //never save the form if menu isn't initialized...doesn't make sense
                    service.setActiveCallback(path, false);
                });
            } else {
                service.setActiveCallback(path, saveForm);
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
        setActiveCallback: function (path, saveForm) {
            if (saveForm)
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
                menuGroup.subMenuItem.iconClass = 'icon-white icon-pencil';
                //Navigate to new path if we are not already there.
                if ($location.path() !== menuGroup.subMenuItem.path)
                    $location.path(menuGroup.subMenuItem.path);
            }
        },
        //#endregion
    };

    return service;

}]);
