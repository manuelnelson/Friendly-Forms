FormsApp.factory('loginMenuService', ['$resource', 'menuService', 'userService', '$q', '$route','$location',
   function ($resource, menuService, userService, $q, $route, $location) {
       var service = {
           auth: $resource('/api/auth/logout', {},
               {
                   get: { method: 'GET', cache:false, params: { format: 'json' } },
               }),
           refresh: function (userId) {
               var deferred = $q.defer();
               userService.getCurrentUserSession().then(function (userData) {
                   if (userData != null && userData.IsAuthenticated) {
                       service.authUser = userData;
                       if (typeof $route.current.params.userId == 'undefined' && !userId)
                           userId = userData.CustomId;
                       menuService.getMenu(userId).then(function () {
                           deferred.resolve();
                       });
                   } else {
                       service.authUser = null;
                       menuService.getMenu().then(function () {
                           deferred.resolve();
                       });
                   }
               });
               return deferred.promise;
           },
           logoff: function () {
               var deferred = $q.defer();
               service.auth.save(null, {}, function (data) {
                   return deferred.resolve(data);
               });
               return deferred.promise;
           },
       };
       return service;
   }]);