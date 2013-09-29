FormsApp.factory('scheduleBService', ['$resource', function($resource) {
    var service = {
        scheduleBs: $resource('/api/output/scheduleB/:userId', { userId: '@userId' },
            {
                get: { method: 'GET', params: { format: 'json' } },
                update: { method: 'PUT', params: { format: 'json' } }
            }),
        showOtherChildren: function(scheduleB) {
            if(scheduleB.OtherChildrenForm.LegallyResponsible == 1 && scheduleB.OtherChildrenForm.AtHome == 1 && scheduleB.OtherChildrenForm.Support == 1 && scheduleB.OtherChildrenForm.Preexisting == 2 && scheduleB.OtherChildrenForm.InCourt == 2 && scheduleB.OtherChildren.length > 0)
                return true;
            return false;
        },
        showPreexistingChildren: function (scheduleB) {
            if (scheduleB.PreexistingSupportForm.Support == 1 && scheduleB.PreexistingSupport.length > 0)
                return true;
            return false;
        }

    };
    return service;
}]);