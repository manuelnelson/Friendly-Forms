$(document).ready(function () {
    //House Form
    $('.domestic-part1').click(function () {
        //get values
        var model = {
            Address: $('#Address').val(),
            Equity: $('#Equity').val().replace(",", ""),
            MaritalHouse: $('#MaritalHouse:checked').val(),
            MoneyOwed: $('#MoneyOwed').val().replace(",", ""),
            MortgageOwner: $('#MortgageOwner').val(),
            RetailValue: $('#RetailValue').val().replace(",", ""),
            Divide: $('#Divide').val()
        };
        Friendly.SubmitForm('maritalHouse', 'property', model);
    });
    $('input[name=MaritalHouse]').change(function () {
        $('.marital-info').val('');
        if ($('#MaritalHouse:checked').val() === "1") {
            $('.marital-house').show();
        } else {
            $('.marital-house').hide();
        }
    });

    //Property Form
    $('.domestic-part2').click(function () {
        Friendly.SubmitForm('property', 'vehicles');
    });
    $('input[name=RealEstate]').change(function () {
        $('#RealEstateDescription').val('');
        if ($('#RealEstate:checked').val() === "1") {
            $('.real-estate-details').show();
        } else {
            $('.real-estate-details').hide();
        }
    });
    $('input[name=PersonalProperty]').change(function () {
        $('#DividingProperty').val('');
        if ($('#PersonalProperty:checked').val() === "3") {
            $('.property-details').show();
        } else {
            $('.property-details').hide();
        }
    });
    
    //Vehicle Form    
    $('input[name=Refinanced]').change(function () {
        $('#RefinanceDate').val('');
        if ($('#Refinanced:checked').val() === "1") {
            $('.vehicle-refinance').show();
        } else {
            $('.vehicle-refinance').hide();
        }
    });
    $('input[id=VehicleFormViewModel_VehiclesInvolved]').change(function () {
        Friendly.ClearForm('vehicles');
        if ($('#VehicleFormViewModel_VehiclesInvolved:checked').val() === "1") {
            $('.vehicle-info').show();
        } else {
            $('.vehicle-info').hide();
        }
    });
    $('#addVehicle').click(function () {
        Friendly.StartLoading();        
        if ($('#vehicles').valid()) {
            var vehicleFormModel = Friendly.GetFormInput('vehicleForm');
            $.ajax({
                url: '/Forms/VehicleForm/',
                type: 'POST',
                data: vehicleFormModel,
                success: function (data) {
                    //get values
                    var model = Friendly.GetFormInput('vehicles');
                    model.VehicleFormId = data;
                    $.ajax({
                        url: '/Forms/Vehicles/',
                        type: 'POST',
                        data: model,
                        success: function (vehicle) {
                            //Add vehicle to list
                            $('.vehicle-table').show();
                            var result = $("#friendly-vehicle-template").tmpl(vehicle);
                            $('.vehicle-table tbody').append(result);
                            Friendly.ClearForm('vehicles');
                            Friendly.EndLoading();
                            return false;
                        },
                        error: Friendly.GenericErrorMessage
                    });
                    //Add vehicle to list
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

    $('.domestic-part3').click(function () {
        //get values
        Friendly.NextForm('debt');
        Friendly.EndLoading();
        return false;
    });

    //Debt Form
    $('.domestic-part4').click(function () {
        //get values
        var model = {
            DebtDivision: $('#DebtDivision').val(),
            MaritalDebt: $('#MaritalDebt:checked').val()
        };
        //check if we need to move to next form
        Friendly.SubmitForm('debt', 'assets', model);
    });

    $('input[name=MaritalDebt]').change(function () {
        $('#DebtDivision').val('');
        if ($('#MaritalDebt:checked').val() === "2") {
            $('.debt-details').show();
        } else {
            $('.debt-details').hide();
        }
    });
    //Asset Form
    $('.domestic-part5').click(function () {
        //check if we need to move to next form
        Friendly.SubmitForm('assets', 'healthInsurance');
    });

    $('input[name=Retirement]').change(function () {
        $('#RetirementDescription').val('');
        if ($('#Retirement:checked').val() === "2") {
            $('.retirement-detail').show();
        } else {
            $('.retirement-detail').hide();
        }
    });
    $('input[name=NonRetirement]').change(function () {
        $('#NonRetirementDescription').val('');
        if ($('#NonRetirement:checked').val() === "2") {
            $('.non-retirement-detail').show();
        } else {
            $('.non-retirement-detail').hide();
        }
    });
    $('input[name=Business]').change(function () {
        $('#BusinessDescription').val('');
        if ($('#Business:checked').val() === "2") {
            $('.business-detail').show();
        } else {
            $('.business-detail').hide();
        }
    });

    //Health Insurance
    $('.domestic-part6').click(function () {
        Friendly.SubmitForm('healthInsurance', 'spousalSupport');
    });
    $('input[name=Health]').change(function () {
        $('#HealthDescription').val('');
        if ($('#Health:checked').val() === "2") {
            $('.health-detail').show();
        } else {
            $('.health-detail').hide();
        }
    });

    //Spousal Support
    $('.domestic-part7').click(function () {
        Friendly.SubmitForm('spousalSupport', 'taxes');
    });
    $('input[name=Spousal]').change(function () {
        $('#SpousalDescription').val('');
        if ($('#Spousal:checked').val() === "2") {
            $('.spousal-detail').show();
        } else {
            $('.spousal-detail').hide();
        }
    });
    //Taxes 
    $('.domestic-part8').click(function () {
        Friendly.SubmitForm('taxes', 'support');
    });
    $('input[name=Taxes]').change(function () {
        $('#TaxDescription').val('');
        if ($('#Taxes:checked').val() === "2") {
            $('.taxes-detail').show();
        } else {
            $('.taxes-detail').hide();
        }
    });

    //Spousal Support
    //$('.domestic-part9').click(function () {
    //    //get values
    //    var model = {
    //        PaidBy: $('#PaidBy').val(),
    //        PaidTo: $('#PaidTo').val(),
    //        MonthlyAmount: $('#MonthlyAmount:checked').val(),
    //        EffectiveDate: $('#EffectiveDate').val(),
    //        TemporaryAgreement: $('#TemporaryAgreement:checked').val(),
    //        Payment: $('#Payment:checked').val(),
    //        PaymentDay: $('#PaymentDay').val()
    //    };
    //    Friendly.SubmitForm('support', 'support', model);
    //});
    //$('input[name=Payment]').change(function () {
    //    $('#PaymentDay').val('');
    //    if ($('#Payment:checked').val() === "2") {
    //        $('.support-payday').show();
    //    } else {
    //        $('.support-payday').hide();
    //    }
    //});
});