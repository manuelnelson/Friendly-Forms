FormsApp.factory('menuService', function ($location, $rootScope) {
    var service = {
        menuItems: [],
        isSetup: false,
        setItems: function (menuItems) {
            service.menuItems = menuItems;            
        },
        setupMenu: function() {
            var userId = service.userId;
            var menuItems = [{
                itemClass: 'active',
                path: '#/',
                iconClass: 'icon icon-home',
                text: 'Home',
                subMenuItems: [],
            }, {
                itemClass: 'submenu',
                path: '',
                iconClass: 'icon icon-th-list',
                text: 'Starter',
                showSubMenu: false,
                subMenuItems: [{
                    itemClass: '',
                    path: '/Starter/Court/' + userId,
                    iconClass: '',
                    text: 'Court',
                    formName: 'Court'
                }, {
                    itemClass: '',
                    path: '/Starter/Participant/' + userId,
                    iconClass: '',
                    text: 'Participants',
                    formName: 'Participant'
                }, {
                    itemClass: '',
                    path: '/Starter/Children/' + userId,
                    iconClass: '',
                    text: 'Children',
                    formName: 'Children'
                }],
            }, {
                itemClass: '',
                path: '/Account/LogOff',
                iconClass: 'icon icon-share-alt',
                text: 'Log Out',
                subMenuItems: [],
            }];
            service.setItems(menuItems);
            service.isSetup = true;
        },
        setSubMenuIconClass: function(menuItemText, subMenuItemText, iconClass) {
            var menuItem = _.find(service.menuItems, function (item) {
                return item.text === menuItemText;
            });
            var subMenuItem = _.find(menuItem.subMenuItems, function (subItem) {
                return subItem.text === subMenuItemText;
            });
            subMenuItem.iconClass = iconClass;
        },
        isActive: function(menuItemText, subMenuItemText) {
            if (service.isSetup) {
                var menuItem = _.find(service.menuItems, function(item) {
                    return item.text === menuItemText;
                });
                var subMenuItem = _.find(menuItem.subMenuItems, function(subItem) {
                    return subItem.formName === subMenuItemText;
                });
                return subMenuItem.itemClass === 'active';
            }
            return false;
        },
        setActive: function (menuItemText, subMenuItemText) {
            if (!service.isSetup) {
                service.setupMenu();
            }
            service.checkForm();
            service.clearActive();
            var menuItem = _.find(service.menuItems, function(item) {
                return item.text === menuItemText;
            });
            if (subMenuItemText === null) {
                //no submenu to worry about
                menuItem.itemClass = 'active';
            } else {
                menuItem.showSubMenu = true;
                menuItem.itemClass = 'submenu active';
                var subMenuItem = _.find(menuItem.subMenuItems, function (subItem) {
                    return subItem.formName === subMenuItemText;
                });
                subMenuItem.itemClass = 'active';
                subMenuItem.iconClass = 'icon-blue icon-pencil';
                $location.path(subMenuItem.path);
            }
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
        userId: 0
    };
    return service;

});
