FormsApp.factory('formCompleteService', ['$resource', 'constantsService', function ($resource, constantsService) {
    var service = {
        formCompletes: $resource('/api/output/formComplete/', {},
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
        child: $resource('/api/child/', {},
        {
            update: { method: 'PUT' },
            deleteAll: { method: 'DELETE' }
        }),
        getOutputPaths: function (formName, userId) {            
            switch (formName) {
                case constantsService.constants.ParentingPlan:
                    return ['/Output/Parenting/User/' + userId];
                case constantsService.constants.DomesticMediation:
                    return ['/Output/DomesticMediation/User/' + userId];
                case constantsService.constants.FinancialForm:
                    return ['/Output/ScheduleA/User/' + userId,
                    '/Output/ScheduleB/User/' + userId,
                    '/Output/ScheduleD/User/' + userId,
                    '/Output/ScheduleE/User/' + userId,
                    '/Output/CSA/User/' + userId,
                    '/Output/ChildSupport/User/' + userId];
                default:
                    return [];
            }
        },
    };
    return service;
}]);