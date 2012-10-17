$(document).ready(function () {
    //Court Form
    $('#child-part1').click(function () {
        Friendly.SubmitForm('court', 'participants');
    });

    $('input[name=PlanType]').change(function () {
        $('#PlanDate').val('');
        if ($('#PlanType:checked').val() === "1") {
            $('.court-date').hide();
        } else {
            $('.court-date').show();
        }
    });

    //Participant Form
    $('.child-part2').click(function () {
        //Graham wants these readonly after submittal
        $('#PlaintiffsName').attr('readonly', 'readonly');
        $('#DefendantsName').attr('readonly', 'readonly');

        //check if we need to move to next form
        if ($(this).hasClass('next')) {
            Friendly.SubmitForm('participants', 'children');
        }
        //check if we need to move to previous form
        if ($(this).hasClass('previous')) {
            Friendly.SubmitForm('participants', 'court');
        }
    });

    $('input[name=PlaintiffRelationship]').change(function () {
        var val = $('#PlaintiffRelationship:checked').val();
        switch (val) {
            case "1":
                $('#DefendantRelationship[value="2"]').attr('checked', 'checked');
                break;
            case "2":
                $('#DefendantRelationship[value="1"]').attr('checked', 'checked');
                break;
            case "3":
                $('#DefendantRelationship[value="3"]').attr('checked', 'checked');
                break;
        }
        updatePlaintiffCustodial();
    });
    $('input[name=DefendantRelationship]').change(function () {
        var val = $('#DefendantRelationship:checked').val();
        switch (val) {
            case "1":
                $('#PlaintiffRelationship[value="2"]').attr('checked', 'checked');
                break;
            case "2":
                $('#PlaintiffRelationship[value="1"]').attr('checked', 'checked');
                break;
            case "3":
                $('#PlaintiffRelationship[value="3"]').attr('checked', 'checked');
                break;
        }
        updatePlaintiffCustodial();
    });
    $('input[name=PlaintiffCustodialParent]').change(function () {
        var val = $('#PlaintiffCustodialParent:checked').val() % 2 + 1;
        $('#DefendantCustodialParent[value="' + val + '"]').attr('checked', 'checked');
        updatePlaintiffCustodial();
    });
    $('input[name=DefendantCustodialParent]').change(function () {
        var val = $('#DefendantCustodialParent:checked').val() % 2 + 1;
        $('#PlaintiffCustodialParent[value="' + val + '"]').attr('checked', 'checked');
        updatePlaintiffCustodial();
    });
    //call this function to have it update the first time when page loads
    updatePlaintiffCustodial();
    function updatePlaintiffCustodial() {
        //get values
        var plaintiffCustodial = $('#PlaintiffCustodialParent:checked').val();
        if(typeof (plaintiffCustodial) === 'undefined') {
            return;
        }
        var custodial, nonCustodial;
        if (plaintiffCustodial === "1") {
            //primary custodial
            custodial = $('#PlaintiffRelationship:checked').parent().text().trim();
            nonCustodial = $('#DefendantRelationship:checked').parent().text().trim();
            $('.nonCustodial').text(nonCustodial);
            $('.custodial').text(custodial);
        } else {
            //non-custodial
            custodial = $('#DefendantRelationship:checked').parent().text().trim();
            nonCustodial = $('#PlaintiffRelationship:checked').parent().text().trim();
            $('.nonCustodial').text(nonCustodial);
            $('.custodial').text(custodial);
        }
    }


    //-----------------------------------Child Form----------------------------
    $('#addChild').click(function () {
        Friendly.StartLoading();
        if ($('#child').valid()) {
            //get values
            var model = Friendly.GetFormInput('child');
            $.ajax({
                url: '/Forms/Children/',
                type: 'POST',
                data: model,
                success: function (data) {
                    //Add child to list
                    var result = $("#friendly-child-template").tmpl(data);
                    $('.child-table').show();
                    $('.child-table tbody').append(result);
                    Friendly.EndLoading();
                    Friendly.ClearForm('children');
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

    $('.child-part3').click(function () {
        //get values
        if ($(this).hasClass('next')) {
            Friendly.NextForm('privacy');
        }
        //check if we need to move to previous form
        if ($(this).hasClass('previous')) {
            Friendly.NextForm('participants');
        }
        Friendly.EndLoading();
        return false;
    });

    //----------------------Privacy Form-----------------------
    $('input[name=NeedPrivacy]').change(function () {
        if ($('#NeedPrivacy:checked').val() === "1") {
            $('#detailsWrapper').show();
        } else {
            $('#detailsWrapper').hide();
        }
    });
    $('.child-part4').click(function () {
        //check if we need to move to next form
        if ($(this).hasClass('next')) {
            Friendly.SubmitForm('privacy', 'information');
        }
        //check if we need to move to previous form
        if ($(this).hasClass('previous')) {
            Friendly.SubmitForm('privacy', 'children');
        }
    });
    //Information form
    $('.child-part5').click(function () {
        Friendly.StartLoading();
        //check if we need to move to previous form
        if ($(this).hasClass('previous')) {
            Friendly.SubmitForm('information', 'privacy');
            return false;
        }
        //get values
        var model = Friendly.GetFormInput('information');
        loadChildren('decision');
        var formName = 'information';
        var nextForm = 'decisions';
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
        var model = {
            ChildId: $('#childId').val().trim(),
            DecisionMaker: $('#ExtraDecisionsViewModel_DecisionMaker:checked').val(),
            Description: $('#ExtraDecisionsViewModel_Description').val()
        };
        if ($('#' + formName).valid()) {
            $.ajax({
                url: '/Forms/' + formName + '/',
                type: 'POST',
                data: model,
                success: function (data) {
                    var result = $("#friendly-extraDecisions-template").tmpl(data);
                    $('#decisions').append(result);
                    $('input[id=ExtraDecisionsViewModel_DecisionMaker]').removeAttr('checked');
                    //$('#ExtraDecisionsViewModel_DecisionMaker').removeAttr('checked');
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

    //Children Decisions
    var children = [];
    var childNdx;
    function loadChildren(form) {
        children = [];
        $('.copy-button ul').empty();
        $('.copy-button ul').append('<li><a title="all" data-id="0" href="#">All</a></li>');
        $.each($('.child-table tbody tr'), function (ndx, item) {
            var child = {
                Name: $(item).find('.child-name').text(),
                Gender: $(item).find('.child-gender').text(),
                DateOfBirth: $(item).find('.child-dob').text(),
                Id: $(item).find('.child-id').text().trim(),
            };
            children.push(child);
            //add to decision and holiday dropdown
            $('.copy-button ul').append('<li><a title="all" data-id="' + child.Id + '" href="#">' + child.Name + '</a></li>');
        });

        //get first child's information
        childNdx = 0;
        var firstChild = children[childNdx];
        switch (form) {
            case "decision":
                getChildDecisions(firstChild);
                break;
            case "holiday":
                getChildHoliday(firstChild);
                break;
        }
    }
    $('.decision-item').click(function () {
        loadChildren('decision');
        Friendly.NextForm('decision');
    });

    //Decisions Form
    $('.dropdown-toggle').dropdown();

    $('#copy-decisions').on('click', "li a", function () {
        if (!$('#decisions').valid()) {
            return false;
        }
        var childId = $(this).attr('data-id');
        if (childId === "0") {
            //all case.  Cycle through all kids (except current showing)
            for (var i = 0; i < children.length; i++) {
                var currChild = children[i];
                copyDecision(currChild.Id);
            }
        } else {
            copyDecision(childId);
        }
    });
    function copyDecision(childId) {
        saveExtraDecisions(childId);
        var model = getDecisionModel(childId);
        $.ajax({
            url: '/Forms/Decisions/',
            type: 'POST',
            data: model,
            success: function () {
            },
            error: Friendly.GenericErrorMessage
        });
    }
    function saveExtraDecisions(childId) {
        $.each($('.extra-decision-item'), function (ndx, item) {
            var id = $(item).children('#extra-decision-Id').val();
            var extraModel = {
                Id: typeof (childId) === "undefined" ? id : 0,
                ChildId: typeof (childId) === "undefined" ? $(item).children('#extra-decision-childId').val() : childId,
                DecisionMaker: $(item).find('input[name=ExtraDecisions' + id + ']:checked').val(),
                Description: $(item).children('#extra-decision-description').text().trim(),
            };
            //If we are copying, we need to make sure that the extra decision doesn't already exist for the current child
            //If it does, copy over the Id
            if (typeof (childId) !== "undefined") {
                $.ajax({
                    url: '/Forms/GetChildDecision/' + childId,
                    type: 'GET',
                    success: function (data) {
                        $.each(data.ExtraDecisions, function (ndx, item) {
                            if (item.Description === extraModel.Description) {
                                //We have a match
                                extraModel.Id = item.Id;
                            }
                        });
                        //Now add/update extradecisions
                        $.ajax({
                            url: '/Forms/ExtraDecisions/',
                            type: 'POST',
                            data: extraModel,
                            success: function () {
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
                url: '/Forms/ExtraDecisions/',
                type: 'POST',
                data: extraModel,
                success: function () {
                },
                error: Friendly.GenericErrorMessage
            });
        });
    }
    function getDecisionModel(childId) {
        return {
            Education: $('#DecisionsViewModel_Education:checked').val(),
            HealthCare: $('#DecisionsViewModel_HealthCare:checked').val(),
            Religion: $('#DecisionsViewModel_Religion:checked').val(),
            ExtraCurricular: $('#DecisionsViewModel_ExtraCurricular:checked').val(),
            ChildId: typeof (childId) === "undefined" ? $('#childId').val() : childId,
        };
    }
    $('.child-part6').click(function () {
        if (!$('#decisions').valid()) {
            return false;
        }
        saveExtraDecisions();
        var model = getDecisionModel();
        if ($(this).hasClass('next'))
            childNdx++;
        else {
            childNdx--;
        }
        //check if we need to move to next form
        if ($(this).hasClass('next') && childNdx === children.length) {
            Friendly.SubmitForm('decisions', 'responsibility', model);
            return false;
        }
        //check if we need to move to previous form
        if ($(this).hasClass('previous') && childNdx < 0) {
            Friendly.SubmitForm('decisions', 'information', model);
            return false;
        }

        //save current information
        $.ajax({
            url: '/Forms/Decisions/',
            type: 'POST',
            data: model,
            success: function () {
                $('input[id=DecisionsViewModel_Education]').removeAttr('checked');
                $('input[id=DecisionsViewModel_HealthCare]').removeAttr('checked');
                $('input[id=DecisionsViewModel_Religion]').removeAttr('checked');
                $('input[id=DecisionsViewModel_ExtraCurricular]').removeAttr('checked');
                var nextChild = children[childNdx];
                getChildDecisions(nextChild);
            },
            error: Friendly.GenericErrorMessage
        });
        return false;
    });

    function getChildDecisions(child) {
        $.ajax({
            url: '/Forms/GetChildDecision/' + child.Id,
            type: 'GET',
            success: function (data) {
                $('#childName').text(child.Name);
                $('#childId').val(child.Id);
                $('#DecisionsViewModel_Education[value="' + data.Decisions.Education + '"]').attr('checked', 'checked');
                $('#DecisionsViewModel_HealthCare[value="' + data.Decisions.HealthCare + '"]').attr('checked', 'checked');
                $('#DecisionsViewModel_Religion[value="' + data.Decisions.Religion + '"]').attr('checked', 'checked');
                $('#DecisionsViewModel_ExtraCurricular[value="' + data.Decisions.ExtraCurricular + '"]').attr('checked', 'checked');
                $('.extra-decision-item').remove();
                $.each(data.ExtraDecisions, function (ndx, item) {
                    var result = $("#friendly-extraDecisions-template").tmpl(item);
                    $('#decisions').append(result);
                });
                $('#decisionsWrapper').show();
                $('.decision-item').addClass('active');
            },
            error: Friendly.GenericErrorMessage
        });
    }

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
        var model = {
            BeginningVisitation: $('#BeginningVisitation:checked').val(),
            EndVisitation: $('#EndVisitation:checked').val(),
            TransportationCosts: $('#TransportationCosts:checked').val(),
            FatherPercentage: 0,
            MotherPercentage: 0,
            OtherDetails: ''
        };
        if (model.TransportationCosts === "3") {
            model.FatherPercentage = $('#FatherPercentage').val();
            model.MotherPercentage = $('#MotherPercentage').val();
        }
        if (model.TransportationCosts === "4") {
            model.OtherDetails = $('#OtherDetails').val();
        }
        if ($(this).hasClass('next'))
            Friendly.SubmitForm('responsibility', 'communication', model);
        else {
            //save current information
            $.ajax({
                url: '/Forms/Responsibility/',
                type: 'POST',
                data: model,
                success: function () {
                    $('.wrapper').hide();
                    $('.affix li').removeClass('active');
                    childNdx = children.length - 1;
                    var nextChild = children[childNdx];
                    getChildDecisions(nextChild);
                },
                error: Friendly.GenericErrorMessage
            });
        }
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

    $('.child-part8').click(function () {
        if ($(this).hasClass('next'))
            Friendly.SubmitForm('communication', 'schedule');
        else {
            Friendly.SubmitForm('communication', 'responsibility');
        }
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
    $('input[name=FatherWeekend]').change(function () {
        var checked = $('#FatherWeekend:checked').val();
        $('.schedule-weekend-other').hide();
        switch (checked) {
            case "1":
                $('#MotherWeekend[value="1"]').attr('checked', 'checked');
                break;
            case "2":
                $('#MotherWeekend[value="4"]').attr('checked', 'checked');
                break;
            case "3":
                $('#MotherWeekend[value="4"]').attr('checked', 'checked');
                break;
            case "4":
                $('#MotherWeekend[value="3"]').attr('checked', 'checked');
                break;
            case "5":
                $('#MotherWeekend[value="5"]').attr('checked', 'checked');
                $('.schedule-weekend-other').show();
                break;
        }
    });
    $('input[name=MotherWeekend]').change(function () {
        var checked = $('#MotherWeekend:checked').val();
        $('.schedule-weekend-other').hide();
        switch (checked) {
            case "1":
                $('#FatherWeekend[value="1"]').attr('checked', 'checked');
                break;
            case "2":
                $('#FatherWeekend[value="4"]').attr('checked', 'checked');
                break;
            case "3":
                $('#FatherWeekend[value="4"]').attr('checked', 'checked');
                break;
            case "4":
                $('#FatherWeekend[value="3"]').attr('checked', 'checked');
                break;
            case "5":
                $('#FatherWeekend[value="5"]').attr('checked', 'checked');
                $('.schedule-weekend-other').show();
                break;
        }
    });

    $('.child-part9').click(function () {
        var model = Friendly.GetFormInput('schedule');
        if ($(this).hasClass('previous')) {
            Friendly.SubmitForm('schedule', 'communication', model);
            return false;
        }
        var formName = 'schedule';
        var nextForm = 'holiday';
        var formSelector = '#' + formName;
        if ($(formSelector).valid()) {
            $.ajax({
                url: '/Forms/' + formName + '/',
                type: 'POST',
                data: model,
                success: function () {
                    loadChildren('holiday');
                    Friendly.NextForm(nextForm);
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
    $('#addHolidays').click(function () {
        Friendly.StartLoading();
        var formName = 'extraHolidays';
        var model = {
            ChildId: $('#holidayChildId').val(),
            HolidayFather: $('#ExtraHolidayViewModel_HolidayFather:checked').val(),
            HolidayMother: $('#ExtraHolidayViewModel_HolidayMother:checked').val(),
            HolidayName: $('#ExtraHolidayViewModel_HolidayName').val()
        };
        if ($('#' + formName).valid()) {
            $.ajax({
                url: '/Forms/' + formName + '/',
                type: 'POST',
                data: model,
                success: function (data) {
                    var result = $("#friendly-extraHolidays-template").tmpl(data);
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
    function getChildHoliday(child) {
        $.ajax({
            url: '/Forms/GetChildHoliday/' + child.Id,
            type: 'GET',
            success: function (data) {
                Friendly.ClearForm('holiday');
                $('#holidayChildName').text(child.Name);
                $('#holidayChildId').val(child.Id);
                if (data.Holidays.FridayHoliday)
                    $('#HolidayViewModel_FridayHoliday').attr('checked', 'checked');
                if (data.Holidays.MondayHoliday)
                    $('#HolidayViewModel_MondayHoliday').attr('checked', 'checked');
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
                $.each(data.ExtraHolidays, function (ndx, item) {
                    var result = $("#friendly-extraHolidays-template").tmpl(item);
                    $('#holiday').append(result);
                });
                $('#holidayWrapper').show();
                $('.holiday-item').addClass('active');
            },
            error: Friendly.GenericErrorMessage
        });
    }

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
    $('.holiday-parent input[type=radio]').on('change', function () {
        var name = $(this).attr('name');
        var val = $('input[name="' + name + '"]:checked').val();
        var newName = "";
        if (name.lastIndexOf("Father") >= 0 && name.lastIndexOf("Father") > name.lastIndexOf("Mother")) {
            newName = name.substring(0, name.lastIndexOf("Father")) + "Mother";
            newName = newName.replace(".", "_");
        }
        else if (name.lastIndexOf("Mother") >= 0 && name.lastIndexOf("Mother") > name.lastIndexOf("Father")) {
            newName = name.substring(0, name.lastIndexOf("Mother")) + "Father";
            newName = newName.replace(".", "_");
        }
        holidayCheckOther(newName, val);
        return false;
    });
    function saveExtraHolidays(childId) {
        $.each($('.extra-holiday-item'), function (ndx, item) {
            var id = $(item).children('#extra-holiday-Id').val();
            var extraModel = {
                Id: typeof (childId) === "undefined" ? id : 0,
                ChildId: typeof (childId) === "undefined" ? $(item).children('#extra-holiday-childId').val() : childId,
                HolidayFather: $(item).find('input[name=HolidayFather' + id + ']:checked').val(),
                HolidayMother: $(item).find('input[name=HolidayMother' + id + ']:checked').val(),
                HolidayName: $(item).children('.extra-holiday-name').text(),
            };
            //If we are copying, we need to make sure that the extra holiday doesn't already exist for the current child
            //If it does, copy over the Id
            if (typeof (childId) !== "undefined") {
                $.ajax({
                    url: '/Forms/GetChildHoliday/' + childId,
                    type: 'GET',
                    success: function (data) {
                        $.each(data.ExtraHolidays, function (ndx, item) {
                            if (item.HolidayName === extraModel.HolidayName) {
                                //We have a match
                                extraModel.Id = item.Id;
                            }
                        });
                        //Now add/update extradecisions
                        $.ajax({
                            url: '/Forms/ExtraHolidays/',
                            type: 'POST',
                            data: extraModel,
                            success: function () {
                            },
                            error: Friendly.GenericErrorMessage
                        });
                    },
                    error: Friendly.GenericErrorMessage
                });
                return;
            }
            $.ajax({
                url: '/Forms/ExtraHolidays/',
                type: 'POST',
                data: extraModel,
                success: function () {
                },
                error: Friendly.GenericErrorMessage
            });
        });
    }
    $('.child-part10').click(function () {
        if ($('#holiday').valid()) {
            saveExtraHolidays();
            var model = Friendly.GetFormInput('holiday');
            model.ChildId = $('#holidayChildId').val();
            model.FridayHoliday = $('#HolidayViewModel_FridayHoliday').is(':checked');
            model.MondayHoliday = $('#HolidayViewModel_MondayHoliday').is(':checked');
            if ($(this).hasClass('next'))
                childNdx++;
            else {
                childNdx--;
            }
            //check if we need to move to next form
            if ($(this).hasClass('next') && childNdx === children.length) {
                //Submit form, do final check. 
                return false;
            }
            //check if we need to move to previous form
            if ($(this).hasClass('previous') && childNdx < 0) {
                Friendly.SubmitForm('holiday', 'schedule', model);
                return false;
            }

            //save current information
            $.ajax({
                url: '/Forms/Holidays/',
                type: 'POST',
                data: model,
                success: function () {
                    $('#holiday')[0].reset();
                    var nextChild = children[childNdx];
                    getChildHoliday(nextChild);
                },
                error: Friendly.GenericErrorMessage
            });
            return false;
        }
    });

    $('input[name="HolidayViewModel.Thanksgiving"]').change(function () {
        var checked = $('#HolidayViewModel_Thanksgiving:checked').val();
        if (checked === '3') {
            $('.thanksgiving-other').show();
        } else {
            $('.thanksgiving-other').hide();
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
    $('.holiday-item').click(function () {
        Friendly.NextForm('holiday');
        loadChildren('holiday');
    });
    
    $('#copy-holidays').on('click',"li a", function () {
        if (!$('#holiday').valid()) {
            return;
        }
        var childId = $(this).attr('data-id');
        if (childId === "0") {
            //all case.  Cycle through all kids (except current showing)
            for (var i = 0; i < children.length; i++) {
                var currChild = children[i];
                copyHoliday(currChild.Id);
            }
        } else {
            copyHoliday(childId);
        }
    });
    function copyHoliday(childId) {
        saveExtraHolidays(childId);
        //do rest of the form
        var model = Friendly.GetFormInput('holiday');
        model.ChildId = childId;
        model.FridayHoliday = $('#HolidayViewModel_FridayHoliday').is(':checked');
        model.MondayHoliday = $('#HolidayViewModel_MondayHoliday').is(':checked');
        $.ajax({
            url: '/Forms/Holidays/',
            type: 'POST',
            data: model,
            success: function () {
            },
            error: Friendly.GenericErrorMessage
        });
    }
});