window.Financial = window.Financial|| {};
Financial.GetChildCare = function(child) {
    var formName = 'childCare';
    $.ajax({
        url: '/api/' + formName + '?ChildId=' + child.Id + '&format=json',
        type: 'GET',
        success: function(data) {
            var $form = $('#' + formName);
            $form.empty();
            if (data === "")
                data = {};
            data.Name = child.Name;
            data.Id = child.Id;
            var result = $("#friendly-childCare-template").tmpl(data);
            $form.append(result);
            //check if valid
            $form.valid();
        },
        error: Friendly.GenericErrorMessage
    });
};
Financial.AddChildCare = function (caller) {
    var formName = 'childCare';
    if (caller != null && $(caller).hasClass('next'))
        Friendly.childNdx++;
    else if (caller != null) {
        Friendly.childNdx--;
    }
    var model = Friendly.GetFormInput(formName);
    model.ChildId = $('#ChildId').val().trim();
    //key is for local storage
    var key = formName + model.ChildId;
    if (!$('#' + formName).valid()) {
        //if validation fails, save data to local storage for later retrieval
        $.jStorage.set(key, model);
        return false;
    }
    //delete key since form is valid (doesn't matter if it exists or not
    $.jStorage.deleteKey(key);
    //check if we need to move to next form
    if (caller != null && $(caller).hasClass('next') && Friendly.childNdx === Friendly.children.length) {
        Friendly.SubmitForm(formName, 'income', model);
        return false;
    }
    //check if we need to move to previous form
    if (caller != null && $(caller).hasClass('previous') && Friendly.childNdx < 0) {
        Friendly.SubmitForm(formName, 'deviations', model);
        return false;
    }
    if ($('#' + formName + 'Id').length > 0)
        model.Id = $('#' + formName + 'Id').val();
    var submitType = 'POST';
    if (typeof model.Id != 'undefined' && model.Id != '0')
        submitType = 'PUT';

    //save current information
    $.ajax({
        url: '/api/' + formName + '/?format=json',
        type: submitType,
        data: model,
        success: function () {
            $('html, body').animate({ scrollTop: 0 }, 'fast');
            var nextChild = Friendly.children[Friendly.childNdx];
            Financial.GetChildCare(nextChild);
        },
        error: Friendly.GenericErrorMessage
    });
    return true;
};
