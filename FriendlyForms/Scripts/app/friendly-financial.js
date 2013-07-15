$(function ($) {
    //#region Child care
    $('#childCareForm #ChildrenInvolved').change(function () {
        if ($('#childCareForm #ChildrenInvolved:checked').val() === "1") {
            $('#childCare-show').show();
        } else {
            $('#childCare-show').hide();
        }
        Friendly.StartLoading();
        var formName = 'childCareForm';
        var model = Friendly.GetFormInput(formName);
        var submitType = 'POST';
        if (typeof model.Id != 'undefined' && model.Id != '0' && model.Id != '')
            submitType = 'PUT';
        else
            model.Id = 0;
        if ($('#' + formName).valid()) {
            $.ajax({
                url: '/api/' + formName + '/?format=json',
                type: submitType,
                data: model,
                success: function () {
                    Friendly.EndLoading();
                },
                error: Friendly.GenericErrorMessage
            });
        }
        Friendly.EndLoading();
    });

    $('.financial-childcare').click(function () {
        //if the form isn't validated, we still need to move on
        if (!Financial.AddChildCare(this)) {
            var child = Friendly.children[Friendly.childNdx - 1];
            Friendly.ChildCareError.push(child.Name);
            if ($(this).hasClass('next') && Friendly.childNdx === Friendly.children.length) {
                Friendly.NextForm('extraExpense', Friendly.properties.iconError);
                return;
            }
            $('html, body').animate({ scrollTop: 0 }, 'fast');
            var nextChild = Friendly.children[Friendly.childNdx];
            Financial.GetChildCare(nextChild);
            Friendly.EndLoading();
        }
    });

    if (window.location.href.indexOf('/Forms/Financial') !== -1) {
        Friendly.NextForm('childCare', 'icon-pencil icon-blue');
    }
    //#endregion 
    //#region Extra Expense
    $('#extraExpenseForm #HasExtraExpenses').change(function () {
        if ($('#extraExpenseForm #HasExtraExpenses:checked').val() === "1") {
            $('#extraExpense-show').show();
        } else {
            $('#extraExpense-show').hide();
        }
        Friendly.StartLoading();
        var formName = 'extraExpenseForm';
        var model = Friendly.GetFormInput(formName);
        var submitType = 'POST';
        if (typeof model.Id != 'undefined' && model.Id != '0' && model.Id != '')
            submitType = 'PUT';
        else
            model.Id = 0;
        if ($('#' + formName).valid()) {
            $.ajax({
                url: '/api/' + formName + '/?format=json',
                type: submitType,
                data: model,
                success: function () {
                    Friendly.EndLoading();
                },
                error: Friendly.GenericErrorMessage
            });
        }
        Friendly.EndLoading();
    });
    $('.financial-extraExpense').click(function () {
        //if the form isn't validated, we still need to move on
        if (!Financial.AddExtraExpense(this)) {
            var child = Friendly.children[Friendly.childNdx - 1];
            Friendly.ExtraExpenseError.push(child.Name);
            if ($(this).hasClass('next') && Friendly.childNdx === Friendly.children.length) {
                Friendly.NextForm('healths', Friendly.properties.iconError);
                return;
            }
            //check if we need to move to previous form
            if ($(this).hasClass('previous') && Friendly.childNdx < 0) {
                Friendly.NextForm('childCare', Friendly.properties.iconError);
                return;
            }
            $('html, body').animate({ scrollTop: 0 }, 'fast');
            var nextChild = Friendly.children[Friendly.childNdx];
            Financial.GetExtraExpense(nextChild);
            Friendly.EndLoading();
        }
    });
    //#endregion 
    //#region Health--------------------------------
    $('.financial-part5').click(function () {
        Friendly.SubmitForm('healths', 'income');
    });
    $('#healths input[name="ProvideHealth"]').change(function () {
        $('#healths #health-provide input[type=text]').val('');
        if ($('#healths #ProvideHealth:checked').val() === "1") {
            $('#healths #health-provide').show();
        } else {
            $('#healths #health-provide').hide();
        }
    });
    $('#healths input[name="Prorate"]').change(function () {
        $('#healths #health-prorate input').val('');
        if ($('#healths #Prorate:checked').val() === "1") {
            $('.health-prorate').show();
        } else {
            $('.health-prorate').hide();
        }
    });
    $('#healths .percent').focusout(function () {
        var percentItems = $.grep($('#healths .percent'), function (element, ndx) {
            return $(element).val() != "";
        });
        //only alter if 
        var remainingVal;
        if (percentItems.length == 2) {
            remainingVal = 100.0 - (parseFloat($(percentItems[0]).val()) + parseFloat($(percentItems[1]).val()));
            percentItems = $.grep($('#healths .percent'), function (element, ndx) {
                return $(element).val() === "";
            });
            $(percentItems).val(remainingVal);
        }
    });
    //#endregion

    //#region Income
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
    //#endregion
    //#region Social Security---------------------
    $('.financial-part2').click(function () {
        Friendly.SubmitForm('socialSecurity', 'preexistingSupportForm');
    });

    $('#socialSecurity input[name="ReceiveSocial"]').change(function () {
        $('#socialSecurity #Amount').val('');
        if ($('#socialSecurity #ReceiveSocial:checked').val() === "1") {
            $('#socialSecurity .social-social').show();
        } else {
            $('#socialSecurity .social-social').hide();
        }
    });
    //#endregion
    //#region Preexisting Support------------------
    $('#preexistingSupportForm input[id="PreexistingSupportFormViewModel_Support"]').change(function () {
        if ($('#preexistingSupportForm input[id="PreexistingSupportFormViewModel_Support"]:checked').val() === "1") {
            $('#supportWrapper').show();
        } else {
            $('#supportWrapper').hide();
        }
        Friendly.StartLoading();
        var formName = 'preexistingSupportForm';
        var model = Friendly.GetFormInput(formName);
        var submitType = 'POST';
        if (typeof model.Id != 'undefined' && model.Id != '0' && model.Id != '')
            submitType = 'PUT';
        else
            model.Id = 0;
        if ($('#' + formName).valid()) {
            $.ajax({
                url: '/api/' + formName + '/?format=json',
                type: submitType,
                data: model,
                success: function () {
                    Friendly.EndLoading();
                },
                error: Friendly.GenericErrorMessage
            });
        }
        Friendly.EndLoading();
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
    //#endregion
    //#region Other Children---------------    
    $('.financial-part4').click(function () {
        if ($('#otherChildWrapper').is(':visible')) {
            submitOtherChildForm(function() {
                Friendly.NextForm('incomeOther', Friendly.properties.iconSuccess);
                return false;
            });
        }
        submitOtherChildForm(function(data) {
            if (data.LegallyResponsible === 1 && data.AtHome === 1 && data.Support === 1 && data.Preexisting === 2 && data.InCourt === 2) {
                //Eligible. Show add children.
                $('#otherChildWrapper #childrenId').val(data.Id);
                $('#otherChildrenWrapper .addOtherChildren').show();
            } else {
                $('#otherChildrenWrapper .addOtherChildren').hide();
                Friendly.NextForm('incomeOther', Friendly.properties.iconSuccess);
            }
            Friendly.EndLoading();
        });
    });
    function submitOtherChildForm(successCallback) {
        Friendly.StartLoading();
        var model = Friendly.GetFormInput('otherChildren');
        var submitType = 'POST';
        if (typeof model.Id != 'undefined' && model.Id != '0' && model.Id != '')
            submitType = 'PUT';
        else
            model.Id = 0;
        if ($('#otherChildren').valid()) {
            $.ajax({
                url: '/api/OtherChildren/?format=json',
                type: submitType,
                data: model,
                success: successCallback,
                error: Friendly.GenericErrorMessage
            });
        }
        Friendly.EndLoading();
    }
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
    //#endregion

    //#region Income Other--------------------------------
    $('#incomeOther input[name="HaveSalary"]').change(function () {
        if ($('#incomeOther #HaveSalary:checked').val() === "1") {
            $('#incomeOther .income-salary').show();
            $('#incomeOther .income-nosalary').hide();
        } else {
            $('#incomeOther .income-salary').hide();
            $('#incomeOther .income-nosalary').show();
        }
    });

    $('#incomeOther input[name="NonW2Income"]').change(function () {
        if ($('#incomeOther #NonW2Income:checked').val() === "1") {
            $('#incomeOther .income-nonW2').show();
        } else {
            $('#incomeOther .income-nonW2').hide();
        }
    });
    $('.financial-part6').click(function () {
        Friendly.SubmitForm('incomeOther', 'socialSecurityOther');
    });
    //#endregion
    //#region Social Security Other--------------------------------
    $('.financial-part7').click(function () {
        Friendly.SubmitForm('socialSecurityOther', 'preexistingSupportFormOther');
    });
    $('#socialSecurityOther input[name="ReceiveSocial"]').change(function () {
        $('#socialSecurityOther #Amount').val('');
        if ($('#socialSecurityOther #ReceiveSocial:checked').val() === "1") {
            $('#socialSecurityOther .social-social').show();
        } else {
            $('#socialSecurityOther .social-social').hide();
        }
    });
    //#endregion
    //#region Preexisting Other--------------------------------
    $('#preexistingSupportFormOther input[id="PreexistingSupportFormViewModel_Support"]').change(function () {
        if ($('#preexistingSupportFormOther input[id="PreexistingSupportFormViewModel_Support"]:checked').val() === "1") {
            $('#supportOtherWrapper').show();
        } else {
            $('#supportOtherWrapper').hide();
        }
        Friendly.StartLoading();
        var formName = 'preexistingSupportFormOther';
        var model = Friendly.GetFormInput(formName);
        var submitType = 'POST';
        if (typeof model.Id != 'undefined' && model.Id != '0' && model.Id != '')
            submitType = 'PUT';
        else
            model.Id = 0;
        if ($('#' + formName).valid()) {
            $.ajax({
                url: '/api/preexistingSupportForm/?format=json',
                type: submitType,
                data: model,
                success: function () {
                    Friendly.EndLoading();
                },
                error: Friendly.GenericErrorMessage
            });
        }
        Friendly.EndLoading();
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
    //#endregion
    //#region Other Children------------------------------    
    $('.financial-part9').click(function () {
        if ($('#otherChildOtherWrapper').is(':visible')) {
            submitOtherChildOtherForm(function () {
                Friendly.NextForm('deviations', Friendly.properties.iconSuccess);
                return false;
            });
        }
        submitOtherChildOtherForm(function (data) {
            if (data.LegallyResponsible === 1 && data.AtHome === 1 && data.Support === 1 && data.Preexisting === 2 && data.InCourt === 2) {
                //Eligible. Show add children.
                $('#otherChildOtherWrapper #childrenId').val(data.Id);
                $('#otherChildrenOtherWrapper .addOtherChildren').show();
            } else {
                $('#otherChildrenOtherWrapper .addOtherChildren').hide();
                Friendly.NextForm('deviations', Friendly.properties.iconSuccess);
            }
            Friendly.EndLoading();
        });
    });

    function submitOtherChildOtherForm(successCallback) {
        Friendly.StartLoading();
        var model = Friendly.GetFormInput('otherChildrenOther');
        var submitType = 'POST';
        if (typeof model.Id != 'undefined' && model.Id != '0' && model.Id != '')
            submitType = 'PUT';
        else
            model.Id = 0;
        if ($('#otherChildrenOther').valid()) {
            $.ajax({
                url: '/api/OtherChildren/?format=json',
                type: submitType,
                data: model,
                success: successCallback,
                error: Friendly.GenericErrorMessage
            });
        }
        Friendly.EndLoading();
    }

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
    //#endregion
    //#region Deviations
    $('.financial-deviations').click(function () {
        //last form.  Let's try and validate this bi-atch
        Friendly.StartLoading();
        var formName = 'deviations';
        var model = Friendly.GetFormInput(formName);
        var submitType = 'POST';

        if (typeof model.Id != 'undefined' && model.Id != '0' && model.Id != '')
            submitType = 'PUT';
        else
            model.Id = 0;

        var formSelector = '#' + formName;
        if ($(formSelector).valid()) {
            $.ajax({
                url: '/api/' + formName + '/?format=json',
                type: submitType,
                data: model,
                success: function () {
                    Friendly.ValidateForms('.financial-deviations');
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
    });
    $('#deviations input[name="Deviation"]').change(function () {
        if ($('#deviations #Deviation:checked').val() === "1") {
            $('#deviationsWrapper #deviations-show').show();
        } else {
            $('#deviationsWrapper #deviations-show').hide();
        }
    });
    $('input[name="HighLow"]').live('change', function () {
        if ($('#HighLow:checked').val() === "1") {
            $('.deviation-high').show();
            $('.deviation-low').hide();
        } else if ($('#HighLow:checked').val() === "2") {
            $('.deviation-low').show();
            $('.deviation-high').hide();
        } else {
            $('.deviation-low').hide();
            $('.deviation-high').hide();
        }
    });
    //#endregion
});