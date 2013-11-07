FormsApp.factory('genericService', ['menuService', 'headerService', '$location', '$q', function (menuService, headerService, $location, $q) {
    var service = {
        calculateRemainingPercentage: function(val1, val2) {
            if (val2 && val1)
                return 100 - (val1 + val2);
            if (val1)
                return 100 - val1;
            throw "Must provide value";
        },
        iconSuccess: 'icon-green icon-ok',
        iconEdit: 'icon-white icon-pencil',
        iconError: 'icon-red icon-exclamation',
        getFormInput: function(formName) {
            var model = {};
            $.each($(formName).serializeArray(), function (i, field) {
                var fieldName = $("[name='" + field.name + "']").attr('ng-model');
                if (fieldName.indexOf(".") >= 0) {
                    fieldName = fieldName.substring(fieldName.indexOf(".") + 1, fieldName.length);
                }
                model[fieldName] = field.value;
            });
            return model;
        },
        refreshPage: function (callback) {
            if (!menuService.isActive($location.path())) {
                menuService.setActive($location.path());
            }
            headerService.refresh();
            if (callback)
                callback();
        },
    };
    return service;
}]);