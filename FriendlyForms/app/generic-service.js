FormsApp.factory('genericService', function () {
    var service = {
        iconSuccess: 'icon-green icon-ok',
        iconEdit: 'icon-blue icon-pencil',
        iconError: 'icon-red icon-pencil',
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
    };
    return service;
});