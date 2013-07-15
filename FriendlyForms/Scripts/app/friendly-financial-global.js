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
            data.ChildId = child.Id;
            var result = $("#friendly-childCare-template").tmpl(data);
            $form.append(result);
            //check if valid
            $form.valid();
        },
        error: Friendly.GenericErrorMessage
    });
};
Financial.AddChildCare = function (caller, stop) {
    var formName = 'childCare';
    if (caller != null && $(caller).hasClass('next'))
        Friendly.childNdx++;
    else if (caller != null) {
        Friendly.childNdx--;
    }
    $.each($('#' + formName + ' input[type=text]'), function(ndx, item) {
        if ($(item).val() === "") {
            $(item).val(0);
        }
    });
    var model = Friendly.GetFormInput(formName);
    if ($('#'+ formName +' #ChildId').val()) {
        model.ChildId = $('#' + formName + ' #ChildId').val().trim();
    }
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
        Friendly.SubmitForm(formName, 'extraExpense', model);
        return true;
    }
    //check if we need to move to previous form
    //if (caller != null && $(caller).hasClass('previous') && Friendly.childNdx < 0) {
    //    Friendly.SubmitForm(formName, 'deviations', model);
    //    return true;
    //}
    var submitType = 'POST';
    if (typeof model.Id != 'undefined' && model.Id != '0' && model.Id != '')
        submitType = 'PUT';
    else 
        model.Id = 0;
    
    //save current information
    $.ajax({
        url: '/api/' + formName + '/?format=json',
        type: submitType,
        data: model,
        success: function () {
            if (stop == null || !stop) {
                $('html, body').animate({ scrollTop: 0 }, 'fast');
                var nextChild = Friendly.children[Friendly.childNdx];
                Financial.GetChildCare(nextChild);
            }
        },
        error: Friendly.GenericErrorMessage
    });
    return true;
};
Financial.CheckChildCare = function (child, nextForm) {
    var formName = 'childCare';
    $.ajax({
        url: '/api/' + formName + '?ChildId=' + child.Id + '&format=json',
        type: 'GET',
        success: function (data) {
            Friendly.ClearForm(formName);
            Friendly.childNdx++;
            data.Name = child.Name;
            var result = $("#friendly-childCare-template").tmpl(data);
            var $form = $('#' + formName);
            $form.append(result);
            //Check if we've gone through all children. If not, continue on
            if (Friendly.childNdx !== Friendly.children.length) {
                if ($form.valid()) {
                    //recursively go to next child
                    Financial.CheckChildCare(Friendly.children[Friendly.childNdx], nextForm);
                    return;
                }
                //Record Error and recursively go to next child
                Friendly.ChildCareError.push(child.Name);
                Financial.CheckChildCare(Friendly.children[Friendly.childNdx], nextForm);
                return;
            }
            //At last child.  Advance to next form - show error if there are errors
            if (!$form.valid()) {
                Friendly.ChildCareError.push(child.Name);
            }
            if (Friendly.ChildCareError.length > 0) {
                Friendly.NextForm(nextForm, Friendly.properties.iconError);
            } else {
                Friendly.NextForm(nextForm, Friendly.properties.iconSuccess);
            }
        },
        error: Friendly.GenericErrorMessage
    });
};

Financial.GetExtraExpense = function (child) {
    var formName = 'extraExpense';
    $.ajax({
        url: '/api/' + formName + '?ChildId=' + child.Id + '&format=json',
        type: 'GET',
        success: function (data) {
            var $form = $('#' + formName);
            $form.empty();
            if (data === "")
                data = {};
            data.Name = child.Name;
            data.ChildId = child.Id;
            var result = $("#friendly-" + formName + "-template").tmpl(data);
            $form.append(result);
            //check if valid
            $form.valid();
        },
        error: Friendly.GenericErrorMessage
    });
};
Financial.AddExtraExpense = function (caller, stop) {
    var formName = 'extraExpense';
    if (caller != null && $(caller).hasClass('next'))
        Friendly.childNdx++;
    else if (caller != null) {
        Friendly.childNdx--;
    }
    $.each($('#' + formName + ' input[type=text]'), function (ndx, item) {
        if ($(item).val() === "") {
            $(item).val(0);
        }
    });
    var model = Friendly.GetFormInput(formName);
    if ($('#' + formName + ' #ChildId').val()) {
        model.ChildId = $('#' + formName + ' #ChildId').val().trim();
    }
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
        Friendly.SubmitForm(formName, 'healths', model);
        return true;
    }
    //check if we need to move to previous form
    if (caller != null && $(caller).hasClass('previous') && Friendly.childNdx < 0) {
        Friendly.SubmitForm(formName, 'childCare', model);
        return true;
    }
    var submitType = 'POST';
    if (typeof model.Id != 'undefined' && model.Id != '0' && model.Id != '')
        submitType = 'PUT';
    else
        model.Id = 0;

    //save current information
    $.ajax({
        url: '/api/' + formName + '/?format=json',
        type: submitType,
        data: model,
        success: function () {
            if (stop == null || !stop) {
                $('html, body').animate({ scrollTop: 0 }, 'fast');
                var nextChild = Friendly.children[Friendly.childNdx];
                Financial.GetExtraExpense(nextChild);
            }
        },
        error: Friendly.GenericErrorMessage
    });
    return true;
};
Financial.CheckExtraExpense = function (child, nextForm) {
    var formName = 'extraExpense';
    $.ajax({
        url: '/api/' + formName + '?ChildId=' + child.Id + '&format=json',
        type: 'GET',
        success: function (data) {
            Friendly.ClearForm(formName);
            Friendly.childNdx++;
            data.Name = child.Name;
            var result = $("#friendly-" + formName +"-template").tmpl(data);
            var $form = $('#' + formName);
            $form.append(result);
            //Check if we've gone through all children. If not, continue on
            if (Friendly.childNdx !== Friendly.children.length) {
                if ($form.valid()) {
                    //recursively go to next child
                    Financial.CheckExtraExpense(Friendly.children[Friendly.childNdx], nextForm);
                    return;
                }
                //Record Error and recursively go to next child
                Friendly.ExtraExpenseError.push(child.Name);
                Financial.CheckExtraExpense(Friendly.children[Friendly.childNdx], nextForm);
                return;
            }
            //At last child.  Advance to next form - show error if there are errors
            if (!$form.valid()) {
                Friendly.ExtraExpenseError.push(child.Name);
            }
            if (Friendly.ExtraExpenseError.length > 0) {
                Friendly.NextForm(nextForm, Friendly.properties.iconError);
            } else {
                Friendly.NextForm(nextForm, Friendly.properties.iconSuccess);
            }
        },
        error: Friendly.GenericErrorMessage
    });
};

Financial.GetIncomeHighAmount = function (nextForm) {
    var formName = '/Output/Financial/scheduleB/';
    $.ajax({
        url: '/api' + formName + '?format=json',
        type: 'GET',
        success: function (data) {
            var incomeHigherAmount = parseInt(data.ScheduleB.AdjustedSupport) - 30000;
            $('.IncomeHigherAmount').text(Friendly.addCommas(incomeHigherAmount));
            $('#sidebar li').removeClass('active');
            $('#' + nextForm + 'Wrapper').show();
            $('#' + nextForm + 'Nav').addClass('active');
            Friendly.EndLoading();
        },
        error: Friendly.GenericErrorMessage
    });
};
