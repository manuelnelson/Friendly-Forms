/* File Created: August 18, 2012 */
window.Friendly = window.Friendly || {};

Friendly.properties = {
    messageType: {
        Warning: '',
        Success: 'alert-success',
        Error: 'alert-error'
    },
    numberRegEx: /(\d{1,5})/
};
Friendly.StartLoading = function () {
    $('#loading').show();
    $('body').css('cursor', 'wait');
};
Friendly.EndLoading = function () {
    $('#loading').hide(); 
    $('body').css('cursor', 'default');
};
Friendly.ShowMessage = function (title, message, type, prependTo) {
    Friendly.EndLoading();
    type = (typeof type === "undefined") ? Friendly.properties.messageType.Warning : type;
    var messageOptions = {
        Title: title,
        Message: message,
        AlertType: type
    };
    var result = $("#friendly-message-template").tmpl(messageOptions);
    if (typeof prependTo === "undefined") {
        prependTo = '#main-content';
    }
    $(prependTo).prepend(result);
};
Friendly.GenericErrorMessage = function (jqXHR, textStatus, errorThrown) {
    Friendly.EndLoading();
    if (jqXHR.responseText != "" && jqXHR.responseText[0] != '<') {
        //Should have a response status. Parse into json, display message
        var responseStatus = JSON.parse(jqXHR.responseText);
        if (errorThrown === "Bad Request") {
            //Bad input error typically
            Friendly.ShowMessage("Input Error", responseStatus.ResponseStatus.Message, Friendly.properties.messageType.Error);
            return false;
        }
    }
    Friendly.ShowMessage("Sorry!", "We could not process your request.  The error has been logged and we will do our best to correct the error asap.", Friendly.properties.messageType.Error);
    return false;
};
Friendly.SubmitForm = function (formName, nextForm, model) {
    Friendly.StartLoading();
    if (typeof model === 'undefined') {
        model = Friendly.GetFormInput(formName);
    }
    var formSelector = '#' + formName;
    if ($(formSelector).valid()) {
        $.ajax({
            url: '/Forms/' + formName + '/',
            type: 'POST',
            data: model,
            success: function () {
                Friendly.NextForm(nextForm);
                Friendly.EndLoading();
                return false;
            },
            error: Friendly.GenericErrorMessage
        });
    } else {
        Friendly.EndLoading();
        return false;
    }
    return false;
};
Friendly.SubmitFormOther = function (formName, nextForm, model) {
    Friendly.StartLoading();
    if(typeof model === 'undefined') {
        model = Friendly.GetFormInput(formName);
    }
    model.isOtherParent = "true";
    var formSelector = '#' + formName;
    var formUrl = formName.substring(0, formName.indexOf("Other"));
    if ($(formSelector).valid()) {
        $.ajax({
            url: '/Forms/' + formUrl + '/',
            type: 'POST',
            data: model,
            success: function () {
                Friendly.NextForm(nextForm);
                Friendly.EndLoading();
                return false;
            },
            error: Friendly.GenericErrorMessage
        });
    } else {
        Friendly.EndLoading();
        return false;
    }
    return false;
};
Friendly.NextForm = function (nextForm) {
    $('#sidebar .active').find('i').attr('class', 'icon-ok');
    $('#' + nextForm + 'Nav').find('i').attr('class', 'icon-pencil');
    $('.wrapper').hide();
    $('#sidebar li').removeClass('active');
    $('#' + nextForm + 'Wrapper').show();
    $('#' + nextForm + 'Nav').addClass('active');
    if(nextForm === 'preexistingOther') {
        if ($('#preexistingOther input[id="PreexistingSupportViewModel_Support"]:checked').val() === "1") {
            $('#supportOtherWrapper').show();
        }
    }
};
Friendly.GetFormInput = function (formName) {
    var model = {};
    $.each($('#' + formName).serializeArray(), function (i, field) {
        if (field.name.indexOf(".") >= 0) {
            field.name = field.name.substring(field.name.indexOf(".") + 1, field.name.length);
        }
        model[field.name] = field.value;
    });
    return model;
};
Friendly.ClearForm = function (formName) {
    $(':input', '#' + formName)
 .not(':button, :submit, :reset, :hidden, :radio')
 .val('')
 .removeAttr('selected');
    $(':input', '#' + formName)
 .not(':button, :submit, :reset, :hidden,')
 .removeAttr('checked')
 .removeAttr('selected');
};
$(document).ready(function () {
    $(".hoverHelp").popover({
        placement: 'left',
        title: 'Tip',
        trigger: 'hover'
    });

    $.validator.addMethod("textlineinput", function (val, el, params) {
        if (this.optional(el)) return true;
        return val.split('\\')[0] === params;
    });

    // register the validator
    $.validator.unobtrusive.adapters.add("textlineinput", ["name"], function (options) {
        options.rules["textlineinput"] = options.params.name;
        if (options.message) options.messages["textlineinput"] = options.message;
    });

    //Modify Validation on forms to cross-over to twitter bootstrap
    var $forms = $('form');
    var oldErrorFunction = [], oldSucessFunction = [], oldInvalidHandler = [];
    for (var f = 0; f < $forms.length; f++) {
        var settings = $.data($forms[f], 'validator').settings;
        oldErrorFunction[f] = settings.errorPlacement;
        oldSucessFunction[f] = settings.success;
        oldInvalidHandler[f] = settings.invalidHandler;
        settings.formNdx = f;
        settings.errorPlacement = function (error, inputElement) {
            $(inputElement).closest('.control-group').addClass('error');
            if (!$.data($forms[this.formNdx], 'validator').valid()) {
            }
            oldErrorFunction[this.formNdx](error, inputElement);
        };
        settings.success = function (error) {
            $(error).closest('.control-group').removeClass('error');
            oldSucessFunction[this.formNdx](error);
        };
        settings.invalidHandler = function (error) {
            oldInvalidHandler[this.formNdx]();
        };
    }
    //Datepickers
    $(".datepicker").datepicker();

    //Timepickers
    $(".timepicker").timepicker({
        minuteStep: 15,
        showInputs: false,
        disableFocus: true
    });
    $('#main-content').on('click', '.close', function() {
        $(this).parent().parent().parent().remove();
    });
    //Form Navigation
    $('.nav-item').click(function () {
        //if($(this).find('i').hasClass('icon-lock')) {
        //    Friendly.ShowMessage("Sorry.  This page is currently locked. Please finish previous pages prior to opening this page.");
        //    return false;
        //}
        var formToShow = $(this).children(':first-child').attr('data-form');
        Friendly.NextForm(formToShow);
    });
});