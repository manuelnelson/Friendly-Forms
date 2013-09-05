FormsApp.factory('domesticMediationService', ['$resource',function ($resource) {
    var service = {
        domesticMediations: $resource('/api/output/domesticMediation/', {},
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
        output: $resource('/api/output/Pdf/', {},
            {
                post: { method: 'POST', params: { format: 'pdf' }, headers: { 'Accept': 'application/pdf' } }
            }),
    };
    return service;
}]);