FormsApp.factory('menuService', function () {
    var service = {
        menuItems: [],
        setItems: function (menuItems) {
            service.menuItems = menuItems;
        },
        setupMenu: function() {
            var userId = $('#user-id').val();
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
                    path: '#/Starter/Court/' + userId,
                    iconClass: 'icon-pencil icon-blue',
                    text: 'Court',                    
                }, {
                    itemClass: '',
                    path: '#/Starter/Participants/' + userId,
                    iconClass: '',
                    text: 'Participants',
                }, {
                    itemClass: '',
                    path: '#/Starter/Children/' + userId,
                    iconClass: '',
                    text: 'Children',
                }],
            }, {
                itemClass: '',
                path: '/Account/LogOff',
                iconClass: 'icon icon-share-alt',
                text: 'Log Out',
                subMenuItems: [],
            }];
            service.setItems(menuItems);
        },
        setActive: function (activeItem) {
            var currItem;
            angular.forEach(service.menuItems, function (item) {
                for (var i = 0; i < item.subMenuItems.length; i++) {
                    var subItem = item.subMenuItems[i];
                    if (subItem.text === activeItem) {
                        currItem = subItem;
                        break;
                    }
                }
            });
            currItem.itemClass = 'active';
        },
    };
    return service;

});