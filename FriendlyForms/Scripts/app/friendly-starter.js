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
        var val = $('#DefendantCustodialParent:checked').val() % 2 + 1;
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
                    Friendly.ClearForm('child');
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
        if($('.child-table tr').length > 1) {
            document.location.href = '/Forms/Parenting/';
        } else {
            document.location.href = '/Forms/DomesticMediation/';
        }
    });
});