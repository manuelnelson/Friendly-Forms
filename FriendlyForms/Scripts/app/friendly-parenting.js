﻿$(function ($) {
    //let's declare the globals for this form.
    Friendly.ChildDecisionError = [];
    Friendly.ChildHolidayError = [];
    //----------------------Privacy Form-----------------------
    //if on or the other of the supervision checkboxes are checked, then show the rest of the data
    $('input[name=NeedSupervision]').change(function () {
        if ($('#NeedSupervision:checked').val() === "1") {
            $('.supervision-details').show();
        } else {
            $('.supervision-details').hide();
        }
    });

    $('.child-part4').click(function () {
        //check if we need to move to next form
        if ($(this).hasClass('next')) {
            Friendly.SubmitForm('privacy', 'information');
        } 
    });
    //Information form
    $('.child-part5').click(function () {
        Friendly.StartLoading();
        //get values
        var model = Friendly.GetFormInput('information');
        Friendly.LoadChildren('decision');
        var formName = 'information';
        var nextForm = 'decisions';
        var formSelector = '#' + formName;
        if ($(formSelector).valid()) {
            $.ajax({
                url: '/api/' + formName + '?format=json',
                type: 'POST',
                data: model,
                success: function () {
                    Friendly.NextForm(nextForm, Friendly.properties.iconSuccess);
                    Friendly.EndLoading();
                    return false;
                },
                error: Friendly.GenericErrorMessage
            });
        }
        else {
            Friendly.EndLoading();
            return false;
        }
        return false;
    });

    $('#addDecisions').click(function () {
        Friendly.StartLoading();
        var formName = 'extraDecisions';
        var model = Friendly.GetFormInput(formName);
        model.ChildId = $('#childId').val().trim();
        if ($('#' + formName).valid()) {
            $.ajax({
                url: '/api/' + formName + '?format=json',
                type: 'POST',
                data: model,
                success: function (data) {
                    var result = $("#friendly-extraDecisions-template").tmpl(data.ExtraDecision);
                    $('#radio-decisions').append(result);
                    $('input[id=ExtraDecisionsViewModel_DecisionMaker]').removeAttr('checked');
                    $('#ExtraDecisionsViewModel_Description').val('');
                    Friendly.EndLoading();
                    return false;
                },
                error: Friendly.GenericErrorMessage
            });
        }
        else {
            Friendly.EndLoading();
            return false;
        }
        return false;
    });
    $('#decisions input[type=radio]').live('change', Parenting.CheckIfBothIsChecked);
    //Decisions Form
    $('.dropdown-toggle').dropdown();

    $('#copy-decisions').on('click', "li a", function () {
        if (!$('#decisions').valid()) {
            return false;
        }
        Friendly.StartLoading();
        var childId = $(this).attr('data-id');
        if (childId === "0") {
            //all case.  Cycle through all kids
            for (var i = 0; i < Friendly.children.length; i++) {
                
                var currChild = Friendly.children[i];
                //only show message if last child saved completes
                if (i == Friendly.children.length - 1)
                    copyDecision(currChild.Id, true);
                else
                    copyDecision(currChild.Id, false);
            }
        } else {
            //first save current child
            var currChildId = $('#childId').val().trim();
            copyDecision(currChildId, false);
            //then save the copy to child
            copyDecision(childId, true);
        }
        Friendly.EndLoading();
    });
    function copyDecision(childId, showMessage) {
        Parenting.SaveExtraDecisions(childId);
        var formName = 'decisions';
        childId = typeof (childId) === "undefined" ? $('#childId').val() : childId;

        //delete from local storage
        var key = formName + childId;
        $.jStorage.deleteKey(key);

        var model = Friendly.GetFormInput(formName);
        model.ChildId = childId;
        $.ajax({
            url: '/api/Decisions?format=json',
            type: 'POST',
            data: model,
            success: function () {
                if (showMessage)
                    Friendly.ShowMessage('Success!', 'The decisions section have successfully been copied. You can continue to make changes by cycling through the children using the previous and continue buttons.', Friendly.properties.messageType.Success, '#decisionsWrapper .copy-wrapper');
            },
            error: Friendly.GenericErrorMessage
        });
    }
    $('.child-part6').click(function () {        
        //if the form isn't validated, we still need to move on
        if (!Parenting.AddDecision(this)) {
            var child = Friendly.children[Friendly.childNdx - 1];
            Friendly.ChildDecisionError.push(child.Name);
            if ($(this).hasClass('next') && Friendly.childNdx === Friendly.children.length) {
                Friendly.NextForm('responsibility', Friendly.properties.iconError);
                return;
            }
            //check if we need to move to previous form
            if ($(this).hasClass('previous') && Friendly.childNdx < 0) {
                Friendly.NextForm('information', Friendly.properties.iconError);
                return;
            }
            $('html, body').animate({ scrollTop: 0 }, 'fast');
            var nextChild = Friendly.children[Friendly.childNdx];
            Parenting.GetChildDecisions(nextChild);
            Friendly.EndLoading();
        }
    });

    //Responsibility Form
    $('input[name=TransportationCosts]').change(function () {
        var checked = $('#TransportationCosts:checked').val();
        $('.responsibility-percentage').hide();
        $('.responsibility-cost').hide();
        $('.responsibility-percentage input').val('0');
        $('.responsibility-cost input').val('');
        switch (checked) {
            case "3":
                $('.responsibility-percentage').show();
                $('.responsibility-percentage input').val('');
                break;
            case "4":
                $('.responsibility-cost').show();
                break;
        }
    });

    $('.responsibility-percentage input').focusout(function () {
        var percent = $(this).val();
        if ($(this).attr('id') === "FatherPercentage") {
            $('#MotherPercentage').val(parseInt(100 - percent));
        } else {
            $('#FatherPercentage').val(parseInt(100 - percent));
        }
    });

    $('.child-part7').click(function () {
        Friendly.SubmitForm('responsibility', 'communication');
    });


    //Communicate Method
    $('#Other').change(function () {
        if ($(this).is(':checked')) {
            $('.communicate-other').show();
        }
        else {
            $('.communicate-other').hide();
        }
    });

    $('input[name=Limitations]').change(function () {
        var checked = $('#Limitations:checked').val();
        if (checked == "1") {
            $('.communicate-method').show();
        } else {
            $('.communicate-method').hide();
        }
    });
    $('input[name=AccessOfRights]').change(function () {
        var checked = $('#AccessOfRights:checked').val();
        if (checked == "1") {
            $('.communicate-rights').show();
        } else {
            $('.communicate-rights').hide();
        }
    });

    $('.child-part8').click(function () {
        Friendly.SubmitForm('communication', 'schedule');
    });

    //Schedule
    $('input[name=DetermineBeginDate]').change(function () {
        var checked = $('#DetermineBeginDate:checked').val();
        if (checked === '1') {
            $('.schedule-date').hide();
        } else {
            $('.schedule-date').show();
        }
    });
    $('input[name=Weekdays]').change(function () {
        var checked = $('#Weekdays:checked').val();
        if (checked === '1') {
            $('.schedule-weekday').show();
        } else {
            $('.schedule-weekday').hide();
        }
    });
    $('input[name=CustodianWeekend]').change(function () {
        var checked = $('#CustodianWeekend:checked').val();
        $('.schedule-weekend-other').hide();
        switch (checked) {
            case "1":
                $('#NonCustodianWeekend[value="1"]').attr('checked', 'checked');
                break;
            case "2":
                $('#NonCustodianWeekend[value="4"]').attr('checked', 'checked');
                break;
            case "3":
                $('#NonCustodianWeekend[value="4"]').attr('checked', 'checked'); 
                break;
            case "4":
                $('#NonCustodianWeekend[value="3"]').attr('checked', 'checked');
                break;
            case "5":
                $('#NonCustodianWeekend[value="5"]').attr('checked', 'checked');
                $('.schedule-weekend-other').show();
                break;
        }
    });
    $('input[name=NonCustodianWeekend]').change(function () {
        var checked = $('#NonCustodianWeekend:checked').val();
        $('.schedule-weekend-other').hide();
        switch (checked) {
            case "1":
                $('#CustodianWeekend[value="1"]').attr('checked', 'checked');
                break;
            case "2":
                $('#CustodianWeekend[value="4"]').attr('checked', 'checked');
                break;
            case "3":
                $('#CustodianWeekend[value="4"]').attr('checked', 'checked');
                break;
            case "4":
                $('#CustodianWeekend[value="3"]').attr('checked', 'checked');
                break;
            case "5":
                $('#CustodianWeekend[value="5"]').attr('checked', 'checked');
                $('.schedule-weekend-other').show();
                break;
        }
    });

    $('.child-part9').click(function () {
        Friendly.SubmitForm('schedule', 'holiday');
    });
    $('#addHolidays').click(function () {
        Friendly.StartLoading();
        var formName = 'extraHoliday';
        var model = {
            ChildId: $('#holidayChildId').val(),
            HolidayFather: $('#ExtraHolidayViewModel_HolidayFather:checked').val(),
            HolidayMother: $('#ExtraHolidayViewModel_HolidayMother:checked').val(),
            HolidayName: $('#ExtraHolidayViewModel_HolidayName').val(),
        };
        if ($('#' + formName).valid()) {
            $.ajax({
                url: '/api/' + formName + '?format=json',
                type: 'POST',
                data: model,
                success: function (data) {
                    var result = $("#friendly-extraHolidays-template").tmpl(data.ExtraHoliday);
                    $('#holiday').append(result);
                    $('input[id=ExtraHolidayViewModel_HolidayFather]').removeAttr('checked');
                    $('input[id=ExtraHolidayViewModel_HolidayMother]').removeAttr('checked');
                    $('#ExtraHolidayViewModel_HolidayName').val('');
                    Friendly.EndLoading();
                    return false;
                },
                error: Friendly.GenericErrorMessage
            });
        }
        else {
            Friendly.EndLoading();
            return false;
        }
        return false;
    });
    $('.father-all input[type=radio]').change(function () {
        var name = $(this).attr('name');
        var val = $('input[name="' + name + '"]:checked').val();
        $.each($('.holiday-parent input[value=' + val + ']'), function (ndx, item) {
            var holidayName = $(item).attr('name');
            if (typeof (holidayName) === 'undefined')
                return;
            if (holidayName.lastIndexOf("Father") >= 0 && holidayName.lastIndexOf("Father") > holidayName.lastIndexOf("Mother")) {
                $(this).attr('checked', 'checked');
            } else {
                holidayName = holidayName.replace(".", "_");
                holidayCheckOther(holidayName, val);
            }
        });
        //we need to do a validation check after this since red errors don't go away on radio buttons after this
        $('#holiday').valid();
    });
    $('.mother-all input[type=radio]').change(function () {
        var name = $(this).attr('name');
        var val = $('input[name="' + name + '"]:checked').val();
        $.each($('.holiday-parent input[value=' + val + ']'), function (ndx, item) {
            var holidayName = $(item).attr('name');
            if (typeof (holidayName) === 'undefined')
                return;
            if (holidayName.lastIndexOf("Mother") >= 0 && holidayName.lastIndexOf("Mother") > holidayName.lastIndexOf("Father")) {
                $(this).attr('checked', 'checked');
            } else {
                holidayName = holidayName.replace(".", "_");
                holidayCheckOther(holidayName, val);
            }
        });
        $('#holiday').valid();
    });
    function holidayCheckOther(name, val) {
        switch (val) {
            case "1":
                $('#' + name + '[value="2"]').attr('checked', 'checked');
                break;
            case "2":
                $('#' + name + '[value="1"]').attr('checked', 'checked');
                break;
            case "3":
                $('#' + name + '[value="4"]').attr('checked', 'checked');
                break;
            case "4":
                $('#' + name + '[value="3"]').attr('checked', 'checked');
                break;
        }
    }
    $('.holiday-parent input[type=radio]').live('change', function () {
        var name = $(this).attr('name');
        var val = $('input[name="' + name + '"]:checked').val();
        var newName = "", suffix = "";
        if (name.lastIndexOf("Father") >= 0 && name.lastIndexOf("Father") > name.lastIndexOf("Mother")) {
            if (name.length > name.lastIndexOf("Father") + 6)
                suffix = name.substring(name.lastIndexOf("Father") + 6, name.length);
            newName = name.substring(0, name.lastIndexOf("Father")) + "Mother" + suffix;
            newName = newName.replace(".", "_");
        }
        else if (name.lastIndexOf("Mother") >= 0 && name.lastIndexOf("Mother") > name.lastIndexOf("Father")) {
            newName = name.substring(0, name.lastIndexOf("Mother")) + "Father";
            newName = newName.replace(".", "_");
        }
        holidayCheckOther(newName, val);
        return false;
    });

    $('.child-part10').click(function () {
        //if the form isn't validated, we still need to move on
        if (!Parenting.AddHoliday(this)) {
            var child = Friendly.children[Friendly.childNdx - 1];
            Friendly.ChildHolidayError.push(child.Name);
            if ($(this).hasClass('next') && Friendly.childNdx === Friendly.children.length) {
                Friendly.NextForm('addendum', Friendly.properties.iconError);
                return;
            }
            //check if we need to move to previous form
            if ($(this).hasClass('previous') && Friendly.childNdx < 0) {
                Friendly.NextForm('schedule', Friendly.properties.iconError);                
                return;
            }
            $('html, body').animate({ scrollTop: 0 }, 'fast');
            var nextChild = Friendly.children[Friendly.childNdx];
            Parenting.GetChildHoliday(nextChild);
            Friendly.EndLoading();
        }       
    });
    //Addendum
    $('input[name=HasAddendum]').change(function () {
        var checked = $('#HasAddendum:checked').val();
        if (checked == "1") {
            $('.addendum-details').show();
        } else {
            $('.addendum-details').hide();
        }
    });

    $('.child-part11').click(function () {
        var formName = 'addendum';
        if ($('#' + formName).valid()) {
            Friendly.StartLoading();
            var model = Friendly.GetFormInput(formName);
            $.ajax({
                url: '/api/' + formName + "?format=json",
                type: 'POST',
                data: model,
                success: function () {
                    Friendly.ValidateForms('.child-part11');
                    Friendly.EndLoading();
                },
                error: Friendly.GenericErrorMessage
            });
        }
        return false;
    });
    $('input[name="HolidayViewModel.Thanksgiving"]').change(function () {
        var checked = $('#HolidayViewModel_Thanksgiving:checked').val();
        $('.thanksgiving-other').hide();
        $('.thanksgiving-time').hide();
        switch (checked) {
            case '3':
                $('.thanksgiving-other').show();
                break;
            case '1':
                $('.thanksgiving-time').show();
                break;
        
        }
    });
    $('input[name="HolidayViewModel.SpringBreak"]').change(function () {
        var checked = $('#HolidayViewModel_SpringBreak:checked').val();
        if (checked === '2') {
            $('.spring-other').show();
        } else {
            $('.spring-other').hide();
        }
    });
    $('input[name="HolidayViewModel.FallBreak"]').change(function () {
        var checked = $('#HolidayViewModel_FallBreak:checked').val();
        if (checked === '2') {
            $('.fall-other').show();
        } else {
            $('.fall-other').hide();
        }
    });
    $('input[name="HolidayViewModel.Christmas"]').change(function () {
        var checked = $('#HolidayViewModel_Christmas:checked').val();
        $('.christmas-items').hide();
        $('.christmas-other').hide();
        switch (checked) {
            case "2":
                $('.christmas-items').show();
                break;
            case "3":
                $('.christmas-items').show();
                break;
            case "4":
                $('.christmas-items').show();
                break;
            case "5":
                $('.christmas-other').show();
                break;
        }
    });

    $('#copy-holidays').on('click', "li a", function () {
        if (!$('#holiday').valid()) {
            return;
        }
        Friendly.StartLoading();
        var childId = $(this).attr('data-id'); 
        if (childId === "0") {
            //all case.  Cycle through all kids (except current showing)
            for (var i = 0; i < Friendly.children.length; i++) {
                var currChild = Friendly.children[i];
                //only show message if last child saved completes
                if (i == Friendly.children.length - 1)
                    copyHoliday(currChild.Id, true);
                else
                    copyHoliday(currChild.Id, false);
            }
        } else {
            //first save current child
            var currChildId = $('#holidayChildId').val().trim();
            copyHoliday(currChildId, false);
            //then save the copy to child
            copyHoliday(childId, true);
        }
        Friendly.EndLoading();       
    });

    function copyHoliday(childId, showMessage) {
        Parenting.SaveExtraHolidays(childId);
        var formName = 'holiday';
        //do rest of the form
        childId = typeof (childId) === "undefined" ? $('#childId').val() : childId;

        //delete from local storage
        var key = formName + childId;
        $.jStorage.deleteKey(key);

        var model = Friendly.GetFormInput(formName);
        model.ChildId = childId;
        //model.FridayHoliday = $('#HolidayViewModel_FridayHoliday').is(':checked');
        //model.MondayHoliday = $('#HolidayViewModel_MondayHoliday').is(':checked');
        $.ajax({
            url: '/api/' + formName + '?format=json',
            type: 'POST',
            data: model,
            success: function () {
                if (showMessage)
                    Friendly.ShowMessage('Success!', 'The holiday section have successfully been copied. You can continue to make changes by cycling through the children using the previous and continue buttons.', Friendly.properties.messageType.Success, '#holidayWrapper .copy-wrapper');
            },
            error: Friendly.GenericErrorMessage
        });       
    }
});

