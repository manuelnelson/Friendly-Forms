FormsApp.factory('clientService', ['$resource', 'userService', '$q', function($resource, userService,$q) {
    var service = {
        clients: $resource('/api/AttorneyClients/clients', {},
            {
                get: { method: 'GET', params: { format: 'json' } },
                getList: { method: 'GET', isArray: true, params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
        attorneys: $resource('/api/AttorneyClients/attorneys', {},
            {
                get: { method: 'GET', params: { format: 'json' } },
                getList: { method: 'GET', isArray: true, params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
        getClients: function (userId) {
            var deferred = $q.defer();
            service.clients.getList({ UserId: userId }, function (attorneyClients) {
                //this is just a list of id's of the clients.  Fetch these
                var userIds = _.pluck(attorneyClients, 'ClientUserId');
                if (userIds && userIds.length > 0) {
                    userService.userAuths.getList({ UserIds: userIds }, function (clientsUserInfo) {
                        var clients = _.map(attorneyClients, function (item) {
                            var clientUserInfo = _.find(clientsUserInfo, function (userInfo) {
                                return userInfo.CustomId == item.ClientUserId;
                            });
                            return {
                                UserId: item.UserId,
                                ClientUserId: item.ClientUserId,
                                CaseNumber: item.CaseNumber,
                                Name: clientUserInfo.DisplayName,
                                CreatedDate: clientUserInfo.CreatedDate,
                            };
                        });
                        deferred.resolve(clients);
                    });
                }
            });
            return deferred.promise;
        }
    };
    return service;
}]);