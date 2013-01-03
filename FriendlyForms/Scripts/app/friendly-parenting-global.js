window.Parenting = window.Parenting || {};
Parenting.AddDecision = function (caller) {
    var formName = 'decisions';
    if (!$('#' + formName).valid()) {
        return false;
    }
    Parenting.SaveExtraDecisions();
    if (caller != null && $(caller).hasClass('next'))
        Friendly.childNdx++;
    else if (caller != null) {
        Friendly.childNdx--;
    }
    var model = Friendly.GetFormInput(formName);
    model.ChildId = $('#childId').val().trim();
    //check if we need to move to next form
    if (caller != null && $(caller).hasClass('next') && Friendly.childNdx === Friendly.children.length) {
        Friendly.SubmitForm(formName, 'responsibility', model);
        return false;
    }
    //check if we need to move to previous form
    if (caller != null && $(caller).hasClass('previous') && Friendly.childNdx < 0) {
        Friendly.SubmitForm(formName, 'information', model);
        return false;
    }

    //save current information
    $.ajax({
        url: '/api/' + formName + '/?format=json',
        type: 'POST',
        data: model,
        success: function () {
            $('html, body').animate({ scrollTop: 0 }, 'fast');
            $('input[id=DecisionsViewModel_Education]').removeAttr('checked');
            $('input[id=DecisionsViewModel_HealthCare]').removeAttr('checked');
            $('input[id=DecisionsViewModel_Religion]').removeAttr('checked');
            $('input[id=DecisionsViewModel_ExtraCurricular]').removeAttr('checked');
            var nextChild = Friendly.children[Friendly.childNdx];
            Parenting.GetChildDecisions(nextChild);
        },
        error: Friendly.GenericErrorMessage
    });
    return true;
};
Parenting.CheckChildDecision = function (child, nextForm) {
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
            $('#DecisionsViewModel_BothResolve').val(data.Decisions.BothResolve);
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
                Parenting.CheckChildDecision(Friendly.children[Friendly.childNdx], nextForm);
                return false;
            } else {
                Friendly.NextForm(nextForm, Friendly.properties.iconError);
                return false;
            }
        },
        error: Friendly.GenericErrorMessage
    });
};

Parenting.CheckChildHoliday = function (child, nextForm) {
    $.ajax({
        url: '/api/Holiday/' + child.Id + '?format=json',
        type: 'GET',
        success: function (data) {
            Parenting.SetChildHolidayForm(data, child);
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
                Parenting.CheckChildHoliday(Friendly.children[Friendly.childNdx], nextForm);
                return false;
            } else {
                Friendly.NextForm(nextForm, Friendly.properties.iconError);
                return false;
            }
        },
        error: Friendly.GenericErrorMessage
    });
};
Parenting.SaveExtraDecisions = function(childId) {
    var formName = 'extraDecisions';
    $.each($('.extra-decision-item'), function(ndx, item) {
        var id = $(item).children('#extra-decision-Id').val();
        var extraModel = {
            Id: typeof(childId) === "undefined" ? id : 0,
            ChildId: typeof(childId) === "undefined" ? $(item).children('#extra-decision-childId').val() : childId,
            DecisionMaker: $(item).find('input[name=ExtraDecisions' + id + ']:checked').val(),
            Description: $(item).children('#extra-decision-description').text().trim(),
            UserId: $('#user-id').val()
        };
        //If we are copying, we need to make sure that the extra decision doesn't already exist for the current child
        //If it does, copy over the Id
        if (typeof(childId) !== "undefined") {
            $.ajax({
                url: '/api/' + formName + '/' + childId + '?format=json',
                type: 'GET',
                success: function(data) {
                    $.each(data, function(ndx, item) {
                        if (item.Description === extraModel.Description) {
                            //We have a match
                            extraModel.Id = item.Id;
                        }
                    });
                    //Now add/update extradecisions
                    $.ajax({
                        url: '/api/' + formName + '/?format=json',
                        type: 'POST',
                        data: extraModel,
                        success: function() {
                        },
                        error: Friendly.GenericErrorMessage
                    });
                },
                error: Friendly.GenericErrorMessage
            });
            return;
        }
        //else, just update the extradecisions
        $.ajax({
            url: '/api/' + formName + '/?format=json',
            type: 'POST',
            data: extraModel,
            success: function() {
            },
            error: Friendly.GenericErrorMessage
        });
    });
};
Parenting.AddHoliday = function(caller) {
    var formName = 'holiday';
    if ($('#' + formName).valid()) {
        Parenting.SaveExtraHolidays();
        var model = Friendly.GetFormInput(formName);
        model.ChildId = $('#holidayChildId').val();
        if (caller != null && $(caller).hasClass('next'))
            Friendly.childNdx++;
        else if (caller != null) {
            Friendly.childNdx--;
        }
        //check if we need to move to next form
        if (caller != null && $(caller).hasClass('next') && Friendly.childNdx === Friendly.children.length) {
            Friendly.SubmitForm('holiday', 'addendum', model);
        }
        //check if we need to move to previous form
        if (caller != null && $(caller).hasClass('previous') && Friendly.childNdx < 0) {
            Friendly.SubmitForm('holiday', 'schedule', model);
            return false;
        }
        Friendly.StartLoading();
        //save current information
        $.ajax({
            url: '/api/' + formName + "?format=json",
            type: 'POST',
            data: model,
            success: function() {
                $('#' + formName)[0].reset();
                $('html, body').animate({ scrollTop: 0 }, 'fast');
                var nextChild = Friendly.children[Friendly.childNdx];
                Parenting.GetChildHoliday(nextChild);
                Friendly.EndLoading();
            },
            error: Friendly.GenericErrorMessage
        });
        return true;
    }
};
Parenting.GetChildDecisions = function(child) {
    $.ajax({
        url: '/api/Decisions?ChildId=' + child.Id + '&format=json',
        type: 'GET',
        success: function(data) {
            $('#childName').text(child.Name);
            $('#childId').val(child.Id);
            $('#DecisionsViewModel_Education[value="' + data.Decisions.Education + '"]').attr('checked', 'checked');
            $('#DecisionsViewModel_HealthCare[value="' + data.Decisions.HealthCare + '"]').attr('checked', 'checked');
            $('#DecisionsViewModel_Religion[value="' + data.Decisions.Religion + '"]').attr('checked', 'checked');
            $('#DecisionsViewModel_ExtraCurricular[value="' + data.Decisions.ExtraCurricular + '"]').attr('checked', 'checked');
            $('#DecisionsViewModel_BothResolve').val(data.Decisions.BothResolve);
            $('.extra-decision-item').remove();
            $.each(data.ExtraDecisions, function(ndx, item) {
                var result = $("#friendly-extraDecisions-template").tmpl(item);
                $('#radio-decisions').append(result);
            });
            Parenting.CheckIfBothIsChecked();
            //fixes possible error flag if one child isn't complete but another is
            if (data.Decisions.Education !== 0) {
                $('#decisions').valid();
            }
            $('#decisionsWrapper').show();
        },
        error: Friendly.GenericErrorMessage
    });
};
Parenting.CheckIfBothIsChecked = function() {
    var bothChecked = false;
    $.each($('#decisions input[type=radio]'), function (ndx, item) {
        var id = $(item).attr('id');
        if ($('#' + id + ':checked').val() === "3")
            bothChecked = true;
    });
    if (bothChecked) {
        $('.decision-details').show();
    } else {
        $('#DecisionsViewModel_BothResolve').val('');
        $('.decision-details').hide();
    }
}
//Children Decisions
Parenting.LoadChildren = function(form) {
    Friendly.children = [];
    $('.copy-button ul').empty();
    $('.copy-button ul').append('<li><a title="all" data-id="0">All</a></li>');
    $.each($('.child-table tbody tr'), function(ndx, item) {
        var child = {
            Name: $(item).find('.child-name').text(),
            DateOfBirth: $(item).find('.child-dob').text(),
            Id: $(item).find('.child-id').text().trim(),
        };
        Friendly.children.push(child);
        //add to decision and holiday dropdown
        $('.copy-button ul').append('<li><a data-id="' + child.Id + '">' + child.Name + '</a></li>');
    });

    if (Friendly.children.length <= 1) {
        $('.copy-wrapper').hide();
    }

    //get first child's information
    Friendly.childNdx = 0;
    var firstChild = Friendly.children[Friendly.childNdx];
    switch (form) {
    case "decision":
        Parenting.GetChildDecisions(firstChild);
        break;
    case "holiday":
        Parenting.GetChildHoliday(firstChild);
        break;
    }
};
Parenting.GetChildHoliday = function(child) {
    $.ajax({
        url: '/api/Holiday/' + child.Id + '?format=json',
        type: 'GET',
        success: function (data) {
            Parenting.SetChildHolidayForm(data, child);
            //fixes possible error flag if one child isn't complete but another is
            if (typeof (data.Holidays) !== 'undefined' && data.Holidays.SpringBreakFather !== 0) {
                $('#holiday').valid();
            }
            $('#holidayWrapper').show();
            $('.holiday-item').addClass('active');
        },
        error: Friendly.GenericErrorMessage
    });
}

Parenting.SetChildHolidayForm = function(data, child) {
    Friendly.ClearForm('holiday');
    $('#holidayChildName').text(child.Name);
    $('#holidayChildId').val(child.Id);
    //check if child has data already saved
    if (typeof(data.Holidays) === 'undefined')
        return false;
    if (data.Holidays.FridayHoliday) {
        $('#HolidayViewModel_FridayHoliday').attr('checked', 'checked');
        $('#HolidayViewModel_FridayHoliday').val('true');
    } else {
        $('#HolidayViewModel_FridayHoliday').val('false');
    }
    if (data.Holidays.MondayHoliday) {
        $('#HolidayViewModel_MondayHoliday').attr('checked', 'checked');
        $('#HolidayViewModel_MondayHoliday').val('true');
    } else {
        $('#HolidayViewModel_MondayHoliday').val('false');
    }

    $('#HolidayViewModel_Thanksgiving[value="' + data.Holidays.Thanksgiving + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_ThanksgivingOther').val(data.Holidays.ThanksgivingOther);
    $('#HolidayViewModel_Christmas[value="' + data.Holidays.Christmas + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_ChristmasTime').val(data.Holidays.ChristmasTime);
    $('#HolidayViewModel_SpringBreakTime').val(data.Holidays.SpringBreakTime);
    $('#HolidayViewModel_FallBreakTime').val(data.Holidays.FallBreakTime);
    $('#HolidayViewModel_ThanksgivingTime').val(data.Holidays.ThanksgivingTime);
    $('#HolidayViewModel_ChristmasOther').val(data.Holidays.ChristmasOther);
    $('#HolidayViewModel_SpringBreak[value="' + data.Holidays.SpringBreak + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_SpringOther').val(data.Holidays.SpringOther);
    $('#HolidayViewModel_SummerBeginDays').val(data.Holidays.SummerBeginDays);
    $('#HolidayViewModel_SummerBeginTime').val(data.Holidays.SummerBeginTime);
    $('#HolidayViewModel_SummerEndDays').val(data.Holidays.SummerEndDays);
    $('#HolidayViewModel_SummerEndTime').val(data.Holidays.SummerEndTime);
    $('#HolidayViewModel_SummerDetails').val(data.Holidays.SummerDetails);
    $('#HolidayViewModel_FallBreak[value="' + data.Holidays.FallBreak + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_FallOther').val(data.Holidays.FallOther);
    $('#HolidayViewModel_SpringBreakFather[value="' + data.Holidays.SpringBreakFather + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_SpringBreakMother[value="' + data.Holidays.SpringBreakMother + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_FallBreakFather[value="' + data.Holidays.FallBreakFather + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_FallBreakMother[value="' + data.Holidays.FallBreakMother + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_ThanksgivingFather[value="' + data.Holidays.ThanksgivingFather + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_ThanksgivingMother[value="' + data.Holidays.ThanksgivingMother + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_ChristmasFather[value="' + data.Holidays.ChristmasFather + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_ChristmasMother[value="' + data.Holidays.ChristmasMother + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_MlkFather[value="' + data.Holidays.MlkFather + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_MlkMother[value="' + data.Holidays.MlkMother + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_PresidentsFather[value="' + data.Holidays.PresidentsFather + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_PresidentsMother[value="' + data.Holidays.PresidentsMother + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_MothersFather[value="' + data.Holidays.MothersFather + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_MothersMother[value="' + data.Holidays.MothersMother + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_MemorialFather[value="' + data.Holidays.MemorialFather + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_MemorialMother[value="' + data.Holidays.MemorialMother + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_FathersFather[value="' + data.Holidays.FathersFather + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_FathersMother[value="' + data.Holidays.FathersMother + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_IndependenceFather[value="' + data.Holidays.IndependenceFather + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_IndependenceMother[value="' + data.Holidays.IndependenceMother + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_LaborFather[value="' + data.Holidays.LaborFather + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_LaborMother[value="' + data.Holidays.LaborMother + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_HalloweenFather[value="' + data.Holidays.HalloweenFather + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_HalloweenMother[value="' + data.Holidays.HalloweenMother + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_ChildrensFather[value="' + data.Holidays.ChildrensFather + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_ChildrensMother[value="' + data.Holidays.ChildrensMother + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_MothersBdayFather[value="' + data.Holidays.MothersBdayFather + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_MothersBdayMother[value="' + data.Holidays.MothersBdayMother + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_FathersBdayFather[value="' + data.Holidays.FathersBdayFather + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_FathersBdayMother[value="' + data.Holidays.FathersBdayMother + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_ReligiousFather[value="' + data.Holidays.ReligiousFather + '"]').attr('checked', 'checked');
    $('#HolidayViewModel_ReligiousMother[value="' + data.Holidays.ReligiousMother + '"]').attr('checked', 'checked');
    $('.extra-holiday-item').remove();
    $.each(data.ExtraHolidays, function(ndx, item) {
        var result = $("#friendly-extraHolidays-template").tmpl(item);
        $('#holiday').append(result);
    });
};
Parenting.SaveExtraHolidays = function(childId) {
    var formName = 'extraHoliday';
    $.each($('.extra-holiday-item'), function(ndx, item) {
        var id = $(item).children('#extra-holiday-Id').val();
        var extraModel = {
            Id: typeof(childId) === "undefined" ? id : 0,
            ChildId: typeof(childId) === "undefined" ? $(item).children('#extra-holiday-childId').val() : childId,
            HolidayFather: $(item).find('input[name=HolidayFather' + id + ']:checked').val(),
            HolidayMother: $(item).find('input[name=HolidayMother' + id + ']:checked').val(),
            HolidayName: $(item).children('.extra-holiday-name').text(),
            UserId: $('#user-id').val()
        };
        //If we are copying, we need to make sure that the extra holiday doesn't already exist for the current child
        //If it does, copy over the Id
        if (typeof(childId) !== "undefined") {
            $.ajax({
                url: '/api/' + formName + '/' + childId + "?format=json",
                type: 'GET',
                success: function(data) {
                    $.each(data.ExtraHolidays, function(ndx, item) {
                        if (item.HolidayName === extraModel.HolidayName) {
                            //We have a match
                            extraModel.Id = item.Id;
                        }
                    });
                    //Now add/update extradecisions
                    $.ajax({
                        url: '/api/' + formName + "?format=json",
                        type: 'POST',
                        data: extraModel,
                        success: function() {
                        },
                        error: Friendly.GenericErrorMessage
                    });
                },
                error: Friendly.GenericErrorMessage
            });
            return;
        }
        $.ajax({
            url: '/api/' + formName + "?format=json",
            type: 'POST',
            data: extraModel,
            success: function() {
            },
            error: Friendly.GenericErrorMessage
        });
    });
};


