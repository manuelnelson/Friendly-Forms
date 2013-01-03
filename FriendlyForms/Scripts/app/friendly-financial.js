$(function ($) {
    $('.financial-part1').click(function () {
        Friendly.SubmitForm('income', 'socialSecurity');
    });
    $('#income input[name="Employed"]').change(function () {
        $('#income #Salary').val('');
        if ($('#income #Employed:checked').val() === "1") {
            $('#income .income-employed').show();
        } else {
            $('#income .income-employed').hide();
        }
    });

    $('#income input[name="SelfEmployed"]').change(function () {
        $('#income #SelfIncome').val('');
        if ($('#income #SelfEmployed:checked').val() === "1") {
            $('#income .income-self').show();
        } else {
            $('#income .income-self').hide();
        }
    });
    $('#income input[name="SelfTax"]').change(function () {
        $('#income #SelfTaxAmount').val('');
        if ($('#income #SelfTax:checked').val() === "1") {
            $('#income .income-tax').show();
        } else {
            $('#income .income-tax').hide();
        }
    });
    $('#income input[name="OtherSources"]').change(function () {
        if ($('#income #OtherSources:checked').val() === "1") {
            $('#income .income-other').show();
        } else {
            $('#income .income-other').hide();
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
                    Friendly.ClearForm(formName);
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
        var model = Friendly.GetFormInput("child");
        model.PreexistingSupportId = $('#childWrapper #supportId').val();
        if ($('#child').valid()) {
            $.ajax({
                url: '/api/PreexistingSupportChild?format=json',
                type: 'POST',
                data: model,
                success: function (data) {
                    var result = $("#friendly-childsupport-template").tmpl(data.Child);
                    $('#childWrapper .child-table tbody').append(result);
                    $('#childWrapper .child-table').show();
                    Friendly.ClearForm('child');
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
                        Friendly.NextForm('specialCircumstances', Friendly.properties.iconSuccess);
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
                    Friendly.ClearForm(formName);
                    Friendly.EndLoading();
                },
                error: Friendly.GenericErrorMessage
            });
        }
        Friendly.EndLoading();
    });
    //----------------------Special Circumstances---------------    
    $('.financial-part5').click(function () {
        Friendly.SubmitForm('specialCircumstances', 'incomeOther');
    });
    $('input[name="Circumstances"]').change(function () {
        if ($('#Circumstances:checked').val() === "1") {
            $('.circumstance-other').show();
        } else {
            $('.circumstance-other').hide();
        }
    });

    //---------------------------------------Other Parent--------------------------------
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
        Friendly.SubmitFormOther('incomeOther', 'socialSecurityOther');
    });
    //---------------------------------------Social Security--------------------------------
    $('.financial-part7').click(function () {
        Friendly.SubmitFormOther('socialSecurityOther', 'preexistingSupportOther');
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
        var model = Friendly.GetFormInput("supportOther");
        model.IsOtherParent = "true";
        if ($('#supportOther').valid()) {
            $.ajax({
                url: '/api/PreexistingSupport/?format=json',
                type: 'POST',
                data: model,
                success: function (data) {
                    var result = $("#friendly-support-template").tmpl(data.PreexistingSupport);
                    $('#supportOtherWrapper .support-table tbody').append(result);
                    Friendly.ClearForm('supportOther');
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
        var model = Friendly.GetFormInput("childOther");
        model.PreexistingSupportId = $('#childOtherWrapper #supportId').val();
        model.IsOtherParent = "true";
        if ($('#childOther').valid()) {
            $.ajax({
                url: '/api/PreexistingSupportChild?format=json',
                type: 'POST',
                data: model,
                success: function (data) {
                    var result = $("#friendly-childsupport-template").tmpl(data.Child);
                    $('#childOtherWrapper .child-table tbody').append(result);
                    $('#childOtherWrapper .child-table').show();
                    Friendly.ClearForm('childOther');
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
                        Friendly.NextForm('specialCircumstancesOther', Friendly.properties.iconSuccess);
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
        var model = Friendly.GetFormInput("otherchildOther");
        model.OtherChildrenId = $('#otherChildOtherWrapper #childrenId').val();
        if ($('#otherchildOther').valid()) {
            $.ajax({
                url: '/api/OtherChild?format=json',
                type: 'POST',
                data: model,
                success: function (data) {
                    var result = $("#friendly-childsupport-template").tmpl(data.OtherChild);
                    $('#otherChildOtherWrapper .otherChild-table tbody').append(result);
                    $('#otherChildOtherWrapper .otherChild-table').show();
                    Friendly.ClearForm('otherchildOther');
                    Friendly.EndLoading();
                },
                error: Friendly.GenericErrorMessage
            });
        }
        Friendly.EndLoading();
    });

    //----------------------Special Circumstances---------------    
    $('.financial-part10').click(function () {
        //check if we need to move to next form
        Friendly.SubmitForm('specialCircumstancesOther', 'incomeOther');
    });
    $('#circumstanceOther input[name="Circumstances"]').change(function () {
        if ($('#circumstanceOther #Circumstances:checked').val() === "1") {
            $('#circumstanceOther .circumstance-other').show();
        } else {
            $('#circumstanceOther .circumstance-other').hide();
        }
    });
    
});