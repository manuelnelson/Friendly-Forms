/* File Created: August 18, 2012 */
window.Friendly = window.Friendly || {};

Friendly.properties = {
    messageType: {
        Warning: '',
        Success: 'alert-success',
        Error: 'alert-error'
    },
    iconSuccess: 'icon-green icon-ok',
    iconEdit: 'icon-blue icon-pencil',
    iconError: 'icon-red icon-pencil',
    numberRegEx: /(\d{1,5})/
};
Friendly.children = [];
Friendly.childNdx = 0;
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
    $('html, body').animate({ scrollTop: $(prependTo).offset().top }, 'fast');
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
    //If the data of the form doesn't need massaging or have special requirements, we go ahead and sumbit.  Otherwise, we 
    //perform the special function in the IsGenericForm function
    var isGeneric = Friendly.IsGenericForm(formName, nextForm);
    if (isGeneric && $('#' + formName).valid()) {
        //go ahead and save
        if (typeof model === 'undefined') {
            model = Friendly.GetFormInput(formName);
        }
        if (Friendly.IsOtherForm(formName)) {
            formName = formName.substring(0, formName.lastIndexOf("Other"));
            model.isOtherParent = "true";
        }
        $.ajax({
            url: '/api/' + formName + '?format=json',
            type: 'POST',
            data: model,
            success: function () {
                $('html, body').animate({ scrollTop: 0 }, 'fast');
                Friendly.NextForm(nextForm, Friendly.properties.iconSuccess);
                Friendly.EndLoading();
            },
            error: Friendly.GenericErrorMessage
        });
    } else if (isGeneric) {
        //Friendly.NextForm(nextForm, Friendly.properties.iconError);
        Friendly.EndLoading();
        return false;
    }
    return true;
};
//This is a minor alteration from Submit form. Used for the IsGenericForm function
Friendly.AjaxSubmit = function(formName, nextForm, model) {
    if ($('#' + formName).valid()) {
        if (typeof model === 'undefined') {
            model = Friendly.GetFormInput(formName);
        }
        $.ajax({
            url: '/api/' + formName + '?format=json',
            type: 'POST',
            data: model,
            success: function() {
                $('html, body').animate({ scrollTop: 0 }, 'fast');
                Friendly.NextForm(nextForm, Friendly.properties.iconSuccess);
            },
            error: Friendly.GenericErrorMessage
        });
    } else {
        Friendly.NextForm(nextForm, Friendly.properties.iconError);        
        return false;
    }
};
Friendly.IsOtherForm = function(formName) {
    //If name of form contains other, with the exception of the OtherChildren form, then it is an 'Other' form
    var regex = /Other/g;
    if (formName.search(regex) != -1) {
        return true;
    }
    return false;
};
Friendly.NextForm = function (nextForm, prevIcon) {
    $('#sidebar .active').find('i').attr('class', prevIcon);
    $('#' + nextForm + 'Nav').find('i').attr('class', Friendly.properties.iconEdit);
    $('.wrapper').hide();

    switch (nextForm) {
        case 'preexistingSupport':
            if ($('#preexistingSupport input[id="PreexistingSupportViewModel_Support"]:checked').val() === "1") {
                    $('#supportWrapper').show();
                }
            break;
        case 'preexistingOther':
            if ($('#preexistingOther input[id="PreexistingSupportViewModel_Support"]:checked').val() === "1") {
                $('#supportOtherWrapper').show();
            }
            break;
        case 'decisions':
            Parenting.LoadChildren('decision');
            break;
        case 'holiday':
            Parenting.LoadChildren('holiday');
            break;
    }
    $('#sidebar li').removeClass('active');
    $('#' + nextForm + 'Wrapper').show();
    $('#' + nextForm + 'Nav').addClass('active');
    Friendly.EndLoading();
};
Friendly.GetFormInput = function (formName) {
    var model = {};
    $.each($('#' + formName).serializeArray(), function (i, field) {
        if (field.name.indexOf(".") >= 0) {
            field.name = field.name.substring(field.name.indexOf(".") + 1, field.name.length);
        }
        model[field.name] = field.value;
    });
    model.UserId = $('#user-id').val();
    return model;
};
Friendly.ClearForm = function (formName) {
    $('#' + formName + ' input, #' + formName + ' textarea')
        .not(':button, :submit, :reset, :hidden, :radio, .timepicker')
        .val('')
        .removeAttr('selected');
        
    $(':input', '#' + formName)
     .not(':button, :submit, :reset, :hidden,')
     .removeAttr('checked')
     .removeAttr('selected');
};
Friendly.ValidateForms = function (forms, readableFormNames, btnClassToHide) {
    var allValid = true, invalidForms = [];
    //Cycles through nav tags to see if everything is complete
    var navItems = $('.nav-item');
    for (var i = 0; i < navItems.length; i++) {
        if (!$(navItems[i]).find('i').hasClass('icon-ok') && !$(navItems[i]).find('i').hasClass('icon-blue')) {
            allValid = false;
            invalidForms.push($(navItems[i]).find('a').text());
        }
    }
    if (!allValid) {
        var message = "The following forms are still incomplete or have errors: ";
        for (var j = 0; j < invalidForms.length - 1; j++) {
            message += invalidForms[j] + ", ";
        }
        message += invalidForms[invalidForms.length - 1];
        Friendly.ShowMessage('Almost there!', message, Friendly.properties.messageType.Error);
        $('html, body').animate({ scrollTop: 0 }, 'fast');
        return false;
    }
    $(btnClassToHide).hide();
    $('.viewOutput').show();
};
//Checks if the form needs special attention. 
Friendly.IsGenericForm = function (formName, nextForm) {
    var genericForms = ['house', 'vehicle', 'child', 'decisions', 'holiday', 'preexistingSupport', 'preexistingSupportOther'];
    if (genericForms.indexOf(formName) >= 0) {
        switch (formName) { 
            case 'house':
                //get values
                var model = Friendly.GetFormInput('house');
                model.Equity = $('#Equity').val().replace(/,/g, "");
                model.MoneyOwed = $('#MoneyOwed').val().replace(/,/g, "");
                model.RetailValue = $('#RetailValue').val().replace(/,/g, "");
                Friendly.AjaxSubmit(formName, nextForm, model);
                break;
            case 'vehicle':
                Friendly.AjaxSubmit('vehicleForm', nextForm);
                break;
            case 'child':
                Friendly.AjaxSubmit('childForm', nextForm);
                break;
            case 'decisions':
                //First, check if current form is valid
                if (Parenting.AddDecision()) {
                    //cycle through all children and make sure form is valid and saved
                    Friendly.childNdx = 0;
                    var child = Friendly.children[Friendly.childNdx];
                    Friendly.ChildDecisionError = [];
                    Parenting.CheckChildDecision(child, nextForm);
                } else {
                    Friendly.NextForm(nextForm, Friendly.properties.iconError);
                }
                break;
            case 'holiday':
                //First, check if current form is valid
                if (Parenting.AddHoliday()) {
                    //cycle through all children and make sure form is valid and saved
                    Friendly.childNdx = 0;
                    var child = Friendly.children[Friendly.childNdx];
                    Parenting.CheckChildHoliday(child, nextForm);
                } else {
                    Friendly.NextForm(nextForm, Friendly.properties.iconError);
                }
                Friendly.EndLoading();
                break;                
            case 'preexistingSupport':
                Friendly.NextForm(nextForm, Friendly.properties.iconSuccess);
                break;
            case 'preexistingSupportOther':
                Friendly.NextForm(nextForm, Friendly.properties.iconSuccess);
                break;

        }
        return false;
    }
    return true;
};

$(document).ready(function () {
    // === Sidebar navigation === //

    $('.submenu > a').click(function (e) {
        e.preventDefault();
        var submenu = $(this).siblings('ul');
        var li = $(this).parents('li');
        var submenus = $('#sidebar li.submenu ul');
        var submenusParents = $('#sidebar li.submenu');
        if (li.hasClass('open')) {
            if (($(window).width() > 768) || ($(window).width() < 479)) {
                submenu.slideUp();
            } else {
                submenu.fadeOut(250);
            }
            li.removeClass('open');
        } else {
            if (($(window).width() > 768) || ($(window).width() < 479)) {
                submenus.slideUp();
                submenu.slideDown();
            } else {
                submenus.fadeOut(250);
                submenu.fadeIn(250);
            }
            submenusParents.removeClass('open');
            li.addClass('open');
        }
    });

    var ul = $('#sidebar > ul');

    $('#sidebar > a').click(function (e) {
        e.preventDefault();
        var sidebar = $('#sidebar');
        if (sidebar.hasClass('open')) {
            sidebar.removeClass('open');
            ul.slideUp(250);
        } else {
            sidebar.addClass('open');
            ul.slideDown(250);
        }
    });
    // === Fixes the position of buttons group in content header and top user navigation === //
    function fixPosition() {
        var uwidth = $('#user-nav > ul').width();
        $('#user-nav > ul').css({ width: uwidth, 'margin-left': '-' + uwidth / 2 + 'px' });

        var cwidth = $('#content-header .btn-group').width();
        $('#content-header .btn-group').css({ width: cwidth, 'margin-left': '-' + uwidth / 2 + 'px' });
    }


    // === Resize window related === //
    $(window).resize(function () {
        if ($(window).width() > 479) {
            ul.css({ 'display': 'block' });
            $('#content-header .btn-group').css({ width: 'auto' });
        }
        if ($(window).width() < 479) {
            ul.css({ 'display': 'none' });
            fixPosition();
        }
        if ($(window).width() > 768) {
            $('#user-nav > ul').css({ width: 'auto', margin: '0' });
            $('#content-header .btn-group').css({ width: 'auto' });
        }
    });

    if ($(window).width() < 468) {
        ul.css({ 'display': 'none' });
        fixPosition();
    }
    if ($(window).width() > 479) {
        $('#content-header .btn-group').css({ width: 'auto' });
        ul.css({ 'display': 'block' });
    }

    $(".hoverHelp").popover({
        placement: 'left',
        title: 'Tip',
        trigger: 'hover'
    });

    //I don't understand checkboxes.  This fixes it though
    $('input[type=checkbox]').click(function () {
        var checked = $(this).attr('checked');
        if (checked != undefined)
            $(this).val(true);
        else
            $(this).val(false);
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
    $('#main-content').on('click', '.close', function () {
        $(this).parent().parent().parent().remove();
    });

    $('.currency').mask('000,000,000,000,000', { reverse: true });

    //Form Navigation
    $('.nav-item').click(function () {
        //before we navigate away, we need to check the status of the form
        Friendly.StartLoading();
        var currentFormName = $('ul .active').children(':first-child').attr('data-form');
        var nextForm = $(this).children(':first-child').attr('data-form');
        if (!Friendly.SubmitForm(currentFormName, nextForm)) {
            Friendly.NextForm(nextForm, Friendly.properties.iconError);
        }
    });

    //will go to output forms
    $('#ViewOutput').live('click', function () {
        document.location.href = $(this).attr('data-url');
    });
});