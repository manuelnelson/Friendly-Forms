$(function ($) {
    //Court Form
    $('#child-part1').click(function () {
        Friendly.SubmitForm('court', 'participant');
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
            Friendly.SubmitForm('participant', 'child');
        }
        //check if we need to move to previous form
        if ($(this).hasClass('previous')) {
            Friendly.SubmitForm('participant', 'court');
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
        var val = $('#PlaintiffCustodialParent:checked').val();
        switch (val) {
            case "1":
                $('#DefendantCustodialParent[value="2"]').attr('checked', 'checked');
                break;
            case "2":
                $('#DefendantCustodialParent[value="1"]').attr('checked', 'checked');
                break;
            case "3":
                $('#DefendantCustodialParent[value="3"]').attr('checked', 'checked');
                break;
        }
        updatePlaintiffCustodial();
    });
    $('input[name=DefendantCustodialParent]').change(function () {
        var val = $('#DefendantCustodialParent:checked').val();
        switch (val) {
            case "1":
                $('#PlaintiffCustodialParent[value="2"]').attr('checked', 'checked');
                break;
            case "2":
                $('#PlaintiffCustodialParent[value="1"]').attr('checked', 'checked');
                break;
            case "3":
                $('#PlaintiffCustodialParent[value="3"]').attr('checked', 'checked');
                break;
        }
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
    $('input[id=ChildFormViewModel_ChildrenInvolved]').change(function () {
        Friendly.ClearForm('child');
        if ($('#ChildFormViewModel_ChildrenInvolved:checked').val() === "1") {
            $('.child-info').show();
        } else {
            $('.child-info').hide();
        }
    });
    $('.childEdit').live('click', function() {
        var $row = $(this).parent().parent();
        var name = $row.children('.child-name').html();
        $row.children('.child-name').empty().append("<input data-val='true' class='input-small' data-val-regex='Only alpha-numeric characters and []@$()'!~:#&amp;_,/-?\% are allowed.' data-val-regex-pattern='^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$()'!~:#/&amp;_\-,\%]*$' data-val-required='The Name field is required.' id='Name' name='Name' placeholder='Name' type='text' value='" + name + "'>");
        var dob = $row.children('.child-dob').html();
        $row.children('.child-dob').empty().append("<input class='datepicker' data-date='01/18/2013' data-val='true' data-val-regex='Date must be in mm/dd/yyyy format' data-val-regex-pattern='^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$' data-val-required='The Date of Birth field is required.' id='DateOfBirth' name='DateOfBirth' placeholder='MM/DD/YYYY' type='text' value='" + dob + "'>");
        $row.find('.childEdit, .childDelete').hide();
        $('.datepicker').datepicker();
        $row.find('.childDone').show();        
    });
    $('.childDone').live('click', function () {
        var $row = $(this).parent().parent();
        var name = $row.find('.child-name #Name').val();
        var dob = $row.find('.child-dob #DateOfBirth').val();

        var model = {
            Id: $(this).attr('data-id'),
            UserId: $('#user-id').val(),
            Name: name,
            DateOfBirth: dob,
            ChildFormId: $('#childFormId').val()
        };
        //UPDATE
        $.ajax({
            url: '/api/Child?format=json',
            type: 'PUT',
            data: model,
            success: function () {
                $row.children('.child-name').empty().append(name);
                $row.children('.child-dob').empty().append(dob);
                $row.find('.childEdit, .childDelete').show();
                $row.find('.childDone').hide();
                return false;
            },
            error: Friendly.GenericErrorMessage
        });
    });

    $('.childDelete').live('click', function () {
        var $row = $(this).parent().parent();
        var name = $row.find('.child-name #Name').val();
        var dob = $row.find('.child-dob #DateOfBirth').val();
        var query = 'Id=' + $(this).attr('data-id') + '&UserId=' + $('#user-id').val() + '&Name=' + name + '&DateOfBirth=' + dob + '&ChildFormId=' + $('#childFormId').val();
        //UPDATE
        $.ajax({
            url: '/api/Child?'+ query + '&format=json',
            type: 'DELETE',
            success: function () {
                $row.remove();
                return false;
            },
            error: Friendly.GenericErrorMessage
        });
    });
    $('#addChild').click(function () {
        Friendly.StartLoading();
        if ($('#child').valid()) {
            //get values
            var childFormModel = Friendly.GetFormInput('childForm');
            $.ajax({
                url: '/api/ChildForm?format=json',
                type: 'POST',
                data: childFormModel,
                success: function (data) {
                    //get values
                    var model = Friendly.GetFormInput('child');
                    model.ChildFormId = data.ChildForm.Id;
                    //use this for later when editing child information
                    $('#childFormId').val(model.ChildFormId);
                    $.ajax({
                        url: '/api/Child?format=json',
                        type: 'POST',
                        data: model,
                        success: function (child) {
                            //Add child to list
                            var result = $("#friendly-child-template").tmpl(child.Child);
                            $('.child-table').show();
                            $('.child-table tbody').append(result);
                            Friendly.EndLoading();
                            Friendly.ClearForm('child');
                            return false;
                        },
                        error: Friendly.GenericErrorMessage
                    });
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
        if($('.child-table tr').length > 1) {
            document.location.href = '/Forms/Parenting/';
        } else {
            document.location.href = '/Forms/DomesticMediation/';
        }
    });
});