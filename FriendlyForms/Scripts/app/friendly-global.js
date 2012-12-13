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
            url: '/api/' + formName + '?format=json',
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
            url: '/api/' + formUrl + '/?format=json',            
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
    switch (nextForm) {
        case 'preexistingOther':
            if ($('#preexistingOther input[id="PreexistingSupportViewModel_Support"]:checked').val() === "1") {
                $('#supportOtherWrapper').show();
            }
            break;
        case 'decisions':
            loadChildren('decision');
            break;
        case 'holiday':
            loadChildren('holiday');
            break;
    }

    $('#sidebar .active').find('i').attr('class', prevIcon);
    $('#' + nextForm + 'Nav').find('i').attr('class', Friendly.properties.iconEdit);
    $('.wrapper').hide();
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
    $(':input', '#' + formName)
 .not(':button, :submit, :reset, :hidden, :radio')
 .val('')
 .removeAttr('selected');
    $(':input', '#' + formName)
 .not(':button, :submit, :reset, :hidden,')
 .removeAttr('checked')
 .removeAttr('selected');
};
Friendly.ValidateForms = function (forms, readableFormNames, btnClassToHide) {
    var allValid = true, invalidForms = [];

    for (var i = 0; i < forms.length; i++) {
        var form = forms[i];
        if (!$('#' + form).valid()) {
            allValid = false;
            invalidForms.push(readableFormNames[i]);
            $('#' + form + 'Nav').find('i').attr('class', Friendly.properties.iconError);
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
Friendly.AddDecision = function (caller) {
    if (!$('#decisions').valid()) {
        return false;
    }
    saveExtraDecisions();
    if (caller != null && $(caller).hasClass('next'))
        Friendly.childNdx++;
    else if (caller != null) {
        Friendly.childNdx--;
    }
    var model = getDecisionModel();
    //check if we need to move to next form
    if (caller != null && $(caller).hasClass('next') && Friendly.childNdx === Friendly.children.length) {
        Friendly.SubmitForm('decisions', 'responsibility', model);
        return false;
    }
    //check if we need to move to previous form
    if (caller != null && $(caller).hasClass('previous') && Friendly.childNdx < 0) {
        Friendly.SubmitForm('decisions', 'information', model);
        return false;
    }

    //save current information
    $.ajax({
        url: '/api/Decisions/?format=json',
        type: 'POST',
        data: model,
        success: function () {
            $('html, body').animate({ scrollTop: 0 }, 'fast');
            $('input[id=DecisionsViewModel_Education]').removeAttr('checked');
            $('input[id=DecisionsViewModel_HealthCare]').removeAttr('checked');
            $('input[id=DecisionsViewModel_Religion]').removeAttr('checked');
            $('input[id=DecisionsViewModel_ExtraCurricular]').removeAttr('checked');
            var nextChild = Friendly.children[Friendly.childNdx];
            getChildDecisions(nextChild);
        },
        error: Friendly.GenericErrorMessage
    });
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
        var isGeneric = isGenericForm(currentFormName, nextForm);
        if (isGeneric && $('#' + currentFormName).valid()) {
            //go ahead and save
            var model = Friendly.GetFormInput(currentFormName, nextForm);
            $.ajax({
                url: '/api/' + currentFormName + '?format=json',
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
        } else if (isGeneric) {
            Friendly.NextForm(nextForm, Friendly.properties.iconError);
            Friendly.EndLoading();
        }
    });
    function isGenericForm(formName, nextForm) {
        var genericForms = ['vehicle', 'children', 'decisions', 'holiday'];
        if (genericForms.indexOf(formName) >= 0) {
            switch (formName) {
                case 'vehicle':
                    if ($('#vehicleForm').valid()) {
                        var vehicleFormModel = Friendly.GetFormInput('vehicleForm');
                        $.ajax({
                            url: '/api/VehicleForm?format=json',
                            type: 'POST',
                            data: vehicleFormModel,
                            success: function () {
                                Friendly.NextForm(nextForm, Friendly.properties.iconSuccess);
                            },
                            error: Friendly.GenericErrorMessage
                        });
                    } else {
                        Friendly.NextForm(nextForm, Friendly.properties.iconError);
                    }
                    break;
                case 'children':
                    if ($('#childForm').valid()) {
                        var childFormModel = Friendly.GetFormInput('childForm');
                        $.ajax({
                            url: '/api/ChildForm?format=json',
                            type: 'POST',
                            data: childFormModel,
                            success: function () {
                                Friendly.NextForm(nextForm, Friendly.properties.iconSuccess);
                            },
                            error: Friendly.GenericErrorMessage
                        });
                    } else {
                        Friendly.NextForm(nextForm, Friendly.properties.iconError);
                    }
                    break;
                case 'decisions':
                    //First, check if current form is valid
                    if (Friendly.AddDecision()) {
                        //cycle through all children and make sure form is valid and saved
                        Friendly.childNdx = 0;
                        var child = Friendly.children[Friendly.childNdx];
                        checkChildDecision(child, nextForm);
                    } else {
                        Friendly.NextForm(nextForm, Friendly.properties.iconError);
                    }
                    break;
                case 'holiday':
                    //First, check if current form is valid
                    if (AddHoliday()) {
                        //cycle through all children and make sure form is valid and saved
                        Friendly.childNdx = 0;
                        var child = Friendly.children[Friendly.childNdx];
                        checkChildHoliday(child, nextForm);
                    } else {
                        Friendly.NextForm(nextForm, Friendly.properties.iconError);
                    }
                    Friendly.EndLoading();
                    break;
            }
            return false;
        }

        return true;
    }
    function checkChildDecision(child, nextForm) {
        $.ajax({
            url: '/api/Decisions?ChildId=' + child.Id + '&format=json',
            type: 'GET',
            success: function (data) {
                $('#childName').text(child.Name);
                $('#childId').val(child.Id);
                $('#DecisionsViewModel_Education[value="' + data.Decisions.Education + '"]').attr('checked', 'checked');
                $('#DecisionsViewModel_HealthCare[value="' + data.Decisions.HealthCare + '"]').attr('checked', 'checked');
                $('#DecisionsViewModel_Religion[value="' + data.Decisions.Religion + '"]').attr('checked', 'checked');
                $('#DecisionsViewModel_ExtraCurricular[value="' + data.Decisions.ExtraCurricular + '"]').attr('checked', 'checked');
                $('.extra-decision-item').remove();
                Friendly.childNdx++;
                if (Friendly.childNdx === Friendly.children.length) {
                    if ($('#decisions').valid()) {
                        Friendly.NextForm(nextForm, Friendly.properties.iconSuccess);
                        return false;
                    } else {
                        Friendly.NextForm(nextForm, Friendly.properties.iconError);
                        return false;
                    }
                }
                if ($('#decisions').valid()) {
                    //recursively go to next child
                    checkChildDecision(Friendly.children[Friendly.childNdx], nextForm);
                    return false;
                } else {
                    Friendly.NextForm(nextForm, Friendly.properties.iconError);
                    return false;
                }
            },
            error: Friendly.GenericErrorMessage
        });
    }
    function checkChildHoliday(child, nextForm) {
        $.ajax({
            url: '/api/Holiday/' + child.Id + '?format=json',
            type: 'GET',
            success: function (data) {
                setChildHolidayForm(data, child);
                Friendly.childNdx++;
                if (Friendly.childNdx === Friendly.children.length) {
                    if ($('#holiday').valid()) {
                        Friendly.NextForm(nextForm, Friendly.properties.iconSuccess);
                        return false;
                    } else {
                        Friendly.NextForm(nextForm, Friendly.properties.iconError);
                        return false;
                    }
                }
                if ($('#holiday').valid()) {
                    //recursively go to next child
                    checkChildHoliday(Friendly.children[Friendly.childNdx], nextForm);
                    return false;
                } else {
                    Friendly.NextForm(nextForm, Friendly.properties.iconError);
                    return false;
                }
            },
            error: Friendly.GenericErrorMessage
        });
    }
    //will go to output forms
    $('#ViewOutput').live('click', function () {
        document.location.href = $(this).attr('data-url');
    });
});