$(function ($) {
    //----------------------Special Circumstances---------------    
    $('.financial-part0').click(function () {
        Friendly.SubmitForm('deviations', 'childcare');
    });
    $('input[name="Deviations"]').change(function () {
        if ($('#Deviations:checked').val() === "1") {
            $('.deviation-other').show();
        } else {
            $('.deviation-other').hide();
        }
    });
    $('input[name="HighLow"]').change(function () {
        if ($('#HighLow:checked').val() === "1") {
            $('.deviation-high').show();
            $('.deviation-low').hide();
        } else {
            $('.deviation-low').show();
            $('.deviation-high').hide();
        }
    });
    $('.financial-part1').click(function () {
        Friendly.SubmitForm('income', 'socialSecurity');
    });
    $('#income input[name="HaveSalary"]').change(function () {
        if ($('#income #HaveSalary:checked').val() === "1") {
            $('#income .income-salary').show();
            $('#income .income-nosalary').hide();
        } else {
            $('#income .income-salary').hide();
            $('#income .income-nosalary').show();
        }
    });

    $('#income input[name="NonW2Income"]').change(function () {
        if ($('#income #NonW2Income:checked').val() === "1") {
            $('#income .income-nonW2').show();
        } else {
            $('#income .income-nonW2').hide();
        }
    });
    //-----------------Child care---------------------
    $('#childCareForm #ChildrenInvolved').change(function() {
        if ($('#childCareForm #ChildrenInvolved:checked').val() === "1") {
            $('#childCare-show').show();
        } else {
            $('#childCare-show').hide();
        }
    });
    
    $('.financial-childcare').click(function () {
        //if the form isn't validated, we still need to move on
        if (!Financial.AddChildCare(this)) {
            var child = Friendly.children[Friendly.childNdx - 1];
            Friendly.ChildCareError.push(child.Name);
            if ($(this).hasClass('next') && Friendly.childNdx === Friendly.children.length) {
                Friendly.NextForm('income', Friendly.properties.iconError);
                return;
            }
            //check if we need to move to previous form
            if ($(this).hasClass('previous') && Friendly.childNdx < 0) {
                Friendly.NextForm('deviations', Friendly.properties.iconError);
                return;
            }
            $('html, body').animate({ scrollTop: 0 }, 'fast');
            var nextChild = Friendly.children[Friendly.childNdx];
            Parenting.GetChildCare(nextChild);
            Friendly.EndLoading();
        }
        
    });
    //-----------------Social Security---------------------
    $('.financial-part2').click(function () {
        Friendly.SubmitForm('socialSecurity', 'preexistingSupport');
    });

    $('#socialSecurity input[name="ReceiveSocial"]').change(function () {
        $('#socialSecurity #Amount').val('');
        if ($('#socialSecurity #ReceiveSocial:checked').val() === "1") {
            $('#socialSecurity .social-social').show();
        } else {
            $('#socialSecurity .social-social').hide();
        }
    });
    //-----------------Preexisting Support------------------
    $('#preexistingSupport input[id="PreexistingSupportViewModel_Support"]').change(function () {
        if ($('#preexistingSupport input[id="PreexistingSupportViewModel_Support"]:checked').val() === "1") {
            $('#supportWrapper').show();
        } else {
            $('#supportWrapper').hide();
        }
    });

    $('#addSupport').click(function () {
        Friendly.StartLoading();
        var formName = 'support';
        var model = Friendly.GetFormInput(formName);
        if ($('#' + formName).valid()) {
            $.ajax({
                url: '/api/PreexistingSupport/?format=json',
                type: 'POST',
                data: model,
                success: function (data) {
                    var result = $("#friendly-support-template").tmpl(data.PreexistingSupport);
                    $('#supportWrapper .support-table tbody').append(result);
                    $('#supportWrapper .support-table').show();
                    $('#' + formName)[0].reset();
                    Friendly.EndLoading();
                },
                error: Friendly.GenericErrorMessage
            });
        }
        Friendly.EndLoading();
    });

    $('#supportWrapper table').on('click', '.view-children', function () {
        var id = $(this).attr('data-id');
        $.ajax({
            url: '/api/PreexistingSupportChild/' + id + '/?format=json',
            type: 'GET',
            success: function (data) {
                if (data.Children.length !== 0) {
                    $('#childWrapper .child-table tbody').empty();
                    var result = $("#friendly-childsupport-template").tmpl(data.Children);
                    $('#childWrapper .child-table tbody').append(result);
                    $('#childWrapper .child-table').show();
                } else {
                    $('#childWrapper .child-table').hide();
                }
                $('#childWrapper #supportId').val(id);
                $('#childWrapper').show();
            },
            error: Friendly.GenericErrorMessage
        });
    });
    $('#addChildSupport').click(function () {
        Friendly.StartLoading();
        var formName = 'child';
        var model = Friendly.GetFormInput(formName);
        model.PreexistingSupportId = $('#childWrapper #supportId').val();
        if ($('#' + formName).valid()) {
            $.ajax({
                url: '/api/PreexistingSupportChild?format=json',
                type: 'POST',
                data: model,
                success: function (data) {
                    var result = $("#friendly-childsupport-template").tmpl(data.Child);
                    $('#childWrapper .child-table tbody').append(result);
                    $('#childWrapper .child-table').show();
                    $('#' + formName)[0].reset();
                    Friendly.EndLoading();
                },
                error: Friendly.GenericErrorMessage
            });
        }
        Friendly.EndLoading();
    });
    $('.financial-part3').click(function () {
        Friendly.NextForm('otherChildren', Friendly.properties.iconSuccess);
    });

    //----------------------Other Children---------------    
    $('.financial-part4').click(function () {
        if ($('#otherChildWrapper').is(':visible')) {
            Friendly.NextForm('specialCircumstances', Friendly.properties.iconSuccess);
            return false;
        }
        Friendly.StartLoading();
        var model = Friendly.GetFormInput('otherChildren');
        if ($('#otherChildren').valid()) {
            $.ajax({
                url: '/api/OtherChildren/?format=json',
                type: 'POST',
                data: model,
                success: function (data) {
                    if (model.LegallyResponsible === "1" && model.AtHome === "1" && model.Support === "1" && model.Preexisting === "2" && model.InCourt === "2") {
                        //Eligible. Show add children.
                        $('#otherChildWrapper #childrenId').val(data.OtherChildren.Id);
                        $('#otherChildWrapper').show();
                    } else {
                        Friendly.NextForm('incomeOther', Friendly.properties.iconSuccess);
                    }
                    Friendly.EndLoading();
                },
                error: Friendly.GenericErrorMessage
            });
        }
        Friendly.EndLoading();
    });
    $('#addOtherChild').click(function () {
        Friendly.StartLoading();
        var formName = 'otherchild';
        var model = Friendly.GetFormInput(formName);
        model.OtherChildrenId = $('#otherChildWrapper #childrenId').val();
        if ($('#' + formName).valid()) {
            $.ajax({
                url: '/api/' + formName + '?format=json',
                type: 'POST',
                data: model,
                success: function (data) {
                    var result = $("#friendly-childsupport-template").tmpl(data.OtherChild);
                    $('#otherChildWrapper .otherChild-table tbody').append(result);
                    $('#otherChildWrapper .otherChild-table').show();
                    $('#' + formName)[0].reset();
                    Friendly.EndLoading();
                },
                error: Friendly.GenericErrorMessage
            });
        }
        Friendly.EndLoading();
    });
    //---------------------------------------Health--------------------------------
    $('.financial-part5').click(function () {
        Friendly.SubmitForm('healths', 'incomeOther');
    });
    $('#healths input[name="ProvideHealth"]').change(function () {
        $('#healths #health-provide input').val('');
        if ($('#healths #ProvideHealth:checked').val() === "1") {
            $('#healths #health-provide').show();
        } else {
            $('#healths #health-provide').hide();
        }
    });
    $('#healths .percent').focusout(function () {
        var percentItems = $.grep($('#healths .percent'), function(element, ndx) {
            return $(element).val() != "";
        });
        //only alter if 
        var remainingVal = 0;
        if (percentItems.length == 2) {
            remainingVal = 100 - (parseFloat($(percentItems[0]).val()) + parseFloat($(percentItems[1]).val()));
            percentItems = $.grep($('#healths .percent'), function (element, ndx) {
                return $(element).val() === "";
            });
            $(percentItems).val(remainingVal);
        }
    });
    //---------------------------------------Income--------------------------------
    $('#incomeOther input[name="Employed"]').change(function () {
        $('#incomeOther #Salary').val('');
        if ($('#incomeOther #Employed:checked').val() === "1") {
            $('#incomeOther .income-employed').show();
        } else {
            $('#incomeOther .income-employed').hide();
        }
    });

    $('#incomeOther input[name="SelfEmployed"]').change(function () {
        $('#incomeOther #SelfIncome').val('');
        if ($('#incomeOther #SelfEmployed:checked').val() === "1") {
            $('#incomeOther .income-self').show();
        } else {
            $('#incomeOther .income-self').hide();
        }
    });
    $('#incomeOther input[name="SelfTax"]').change(function () {
        $('#incomeOther #SelfTaxAmount').val('');
        if ($('#incomeOther #SelfTax:checked').val() === "1") {
            $('#incomeOther .income-tax').show();
        } else {
            $('#incomeOther .income-tax').hide();
        }
    });
    $('#incomeOther input[name="OtherSources"]').change(function () {
        if ($('#incomeOther #OtherSources:checked').val() === "1") {
            $('#incomeOther .income-other').show();
        } else {
            $('#incomeOther .income-other').hide();
        }
    });
    $('.financial-part6').click(function () {
        Friendly.SubmitForm('incomeOther', 'socialSecurityOther');
    });
    //---------------------------------------Social Security--------------------------------
    $('.financial-part7').click(function () {
        Friendly.SubmitForm('socialSecurityOther', 'preexistingSupportOther');
    });

    $('#socialSecurityOther input[name="ReceiveSocial"]').change(function () {
        $('#socialSecurityOther #Amount').val('');
        if ($('#socialSecurityOther #ReceiveSocial:checked').val() === "1") {
            $('#socialSecurityOther .social-social').show();
        } else {
            $('#socialSecurityOther .social-social').hide();
        }
    });
    //---------------------------------------Preexisting Other--------------------------------
    $('#preexistingSupportOther input[id="PreexistingSupportViewModel_Support"]').change(function () {
        if ($('#preexistingSupportOther input[id="PreexistingSupportViewModel_Support"]:checked').val() === "1") {
            $('#supportOtherWrapper').show();
        } else {
            $('#supportOtherWrapper').hide();
        }
    });

    $('#addSupportOther').click(function () {
        Friendly.StartLoading();
        var formName = "supportOther";
        var model = Friendly.GetFormInput(formName);
        model.IsOtherParent = "true";
        if ($('#' + formName).valid()) {
            $.ajax({
                url: '/api/PreexistingSupport/?format=json',
                type: 'POST',
                data: model,
                success: function (data) {
                    var result = $("#friendly-support-template").tmpl(data.PreexistingSupport);
                    $('#supportOtherWrapper .support-table tbody').append(result);
                    $('#' + formName)[0].reset();                    
                    Friendly.EndLoading();
                },
                error: Friendly.GenericErrorMessage
            });
        }
        Friendly.EndLoading();
    });

    $('#supportOtherWrapper table').on('click', '.view-children', function () {
        var id = $(this).attr('data-id');
        $.ajax({
            url: '/api/PreexistingSupportChild/' + id + '/?format=json',
            type: 'GET',
            success: function (data) {
                if (data.length !== 0) {                    
                    $('#childOtherWrapper .child-table tbody').empty();
                    var result = $("#friendly-childsupport-template").tmpl(data.Children);
                    $('#childOtherWrapper .child-table tbody').append(result);
                    $('#childOtherWrapper .child-table').show();
                } else {
                    $('#childOtherWrapper .child-table').hide();
                }
                $('#childOtherWrapper #supportId').val(id);
                $('#childOtherWrapper').show();
            },
            error: Friendly.GenericErrorMessage
        });
    });
    $('#addChildSupportOther').click(function () {
        Friendly.StartLoading();
        var formName = "childOther";
        var model = Friendly.GetFormInput();
        model.PreexistingSupportId = $('#childOtherWrapper #supportId').val();
        model.IsOtherParent = "true";
        if ($('#' + formName).valid()) {
            $.ajax({
                url: '/api/PreexistingSupportChild?format=json',
                type: 'POST',
                data: model,
                success: function (data) {
                    var result = $("#friendly-childsupport-template").tmpl(data.Child);
                    $('#childOtherWrapper .child-table tbody').append(result);
                    $('#childOtherWrapper .child-table').show();
                    $('#' + formName)[0].reset();
                    Friendly.EndLoading();
                },
                error: Friendly.GenericErrorMessage
            });
        }
        Friendly.EndLoading();
    });
    $('.financial-part8').click(function () {               
        Friendly.NextForm('otherChildrenOther', Friendly.properties.iconSuccess);
    });
    //----------------------Other Children---------------    
    $('.financial-part9').click(function () {
        if ($('#otherChildOtherWrapper').is(':visible')) {
            Friendly.NextForm('specialCircumstancesOther', Friendly.properties.iconSuccess);
            return false;
        }
        Friendly.StartLoading();
        var model = Friendly.GetFormInput('otherChildrenOther');
        model.IsOtherParent = "true"; 
        if ($('#otherChildrenOther').valid()) {
            $.ajax({
                url: '/api/OtherChildren/?format=json',
                type: 'POST',
                data: model,
                success: function (data) {
                    if (model.LegallyResponsible === "1" && model.AtHome === "1" && model.Support === "1" && model.Preexisting === "2" && model.InCourt === "2") {                        
                        //Eligible. Show add children.
                        $('#otherChildOtherWrapper #childrenId').val(data.OtherChildren.Id);
                        $('#otherChildOtherWrapper').show();
                    } else {
                        Friendly.NextForm('healthsOther', Friendly.properties.iconSuccess);
                    }
                    Friendly.EndLoading();
                },
                error: Friendly.GenericErrorMessage
            });
        }
        Friendly.EndLoading();
    });
    $('#addOtherChildOther').click(function () {
        Friendly.StartLoading();
        var formName = "otherchildOther";
        var model = Friendly.GetFormInput(formName);
        model.OtherChildrenId = $('#otherChildOtherWrapper #childrenId').val();
        if ($('#' + formName).valid()) {
            $.ajax({
                url: '/api/OtherChild?format=json',
                type: 'POST',
                data: model,
                success: function (data) {
                    var result = $("#friendly-childsupport-template").tmpl(data.OtherChild);
                    $('#otherChildOtherWrapper .otherChild-table tbody').append(result);
                    $('#otherChildOtherWrapper .otherChild-table').show();
                    $('#' + formName)[0].reset();
                    Friendly.EndLoading();
                },
                error: Friendly.GenericErrorMessage
            });
        }
        Friendly.EndLoading();
    });
    //----------------------Health Other---------------    

    $('.financial-part10').click(function () {
        Friendly.SubmitForm('healthsOther', 'incomeOther');
    });
    $('#healthsOther input[name="ProvideHealth"]').change(function () {
        $('#healthsOther #health-provide input').val('');
        if ($('#healthsOther #ProvideHealth:checked').val() === "1") {
            $('#healthsOther #health-provide').show();
        } else {
            $('#healthsOther #health-provide').hide();
        }
    });
    $('#healthsOther .percent').focusout(function () {
        var percentItems = $.grep($('#healthsOther .percent'), function (element, ndx) {
            return $(element).val() != "";
        });
        //only alter if 
        var remainingVal = 0;
        if (percentItems.length == 2) {
            remainingVal = 100 - (parseFloat($(percentItems[0]).val()) + parseFloat($(percentItems[1]).val()));
            percentItems = $.grep($('#healthsOther .percent'), function (element, ndx) {
                return $(element).val() === "";
            });
            $(percentItems).val(remainingVal);
        }
    });
});