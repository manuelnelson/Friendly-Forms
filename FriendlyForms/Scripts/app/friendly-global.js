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
                $('html, body').animate({ scrollTop: 0 }, 'fast');
                Friendly.NextForm(nextForm, Friendly.properties.iconSuccess);
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
    if (typeof model === 'undefined') {
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
                $('html, body').animate({ scrollTop: 0 }, 'fast');
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
Friendly.NextForm = function (nextForm, prevIcon) {
    $('#sidebar .active').find('i').attr('class', prevIcon);
    $('#' + nextForm + 'Nav').find('i').attr('class', Friendly.properties.iconEdit);
    $('.wrapper').hide();
    $('#sidebar li').removeClass('active');
    $('#' + nextForm + 'Wrapper').show();
    $('#' + nextForm + 'Nav').addClass('active');
    
    if (nextForm === 'preexistingOther') {
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
    //Form Navigation
    $('.nav-item').click(function () {
        //before we navigate away, we need to check the status of the form
        var currentFormName = $('ul .active').children(':first-child').attr('data-form');
        var nextForm = $(this).children(':first-child').attr('data-form');
        var isGeneric = isGenericForm(currentFormName, nextForm);
        if (isGeneric && $('#' + currentFormName).valid()) {
            //go ahead and save
            var model = Friendly.GetFormInput(currentFormName, nextForm);
            $.ajax({
                url: '/Forms/' + currentFormName + '/',
                type: 'POST',
                data: model,
                success: function () {
                    $('html, body').animate({ scrollTop: 0 }, 'fast');
                    Friendly.NextForm(nextForm, Friendly.properties.iconSuccess);
                    Friendly.EndLoading();
                    return false;
                },
                error: Friendly.GenericErrorMessage
            });
        } else if(isGeneric){
            Friendly.NextForm(nextForm, Friendly.properties.iconError);
        }
    });
    function isGenericForm(formName, nextForm) {
        var genericForms = ['vehicles', 'children', 'decisions', 'holidays'];
        if (genericForms.indexOf(formName) >= 0) {
            switch (formName) {
                case 'vehicles':
                    if ($('#vehicleForm').valid()) {
                        var vehicleFormModel = Friendly.GetFormInput('vehicleForm');
                        $.ajax({
                            url: '/Forms/VehicleForm/',
                            type: 'POST',
                            data: vehicleFormModel,
                            success: function (data) {
                                Friendly.NextForm(nextForm, Friendly.properties.iconSuccess);
                            },
                            error: Friendly.GenericErrorMessage
                        });
                    }else {
                        Friendly.NextForm(nextForm, Friendly.properties.iconError);
                    }
                    break;                                
            }
            return false;
        }

        return true;
    }
});