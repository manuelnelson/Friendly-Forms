$(function ($) {
    //House Form
    $('.domestic-part1').click(function () {
        //get values
        var model = Friendly.GetFormInput('house');
        model.Equity = $('#Equity').val().replace(/,/g, "");
        model.MoneyOwed = $('#MoneyOwed').val().replace(/,/g, "");
        model.RetailValue = $('#RetailValue').val().replace(/,/g, ""); 
        Friendly.SubmitForm('house', 'property', model);
    });
    $('#CityState').typeahead({
        source: function (typeahead, query) {
            if (query === "") {
                return typeahead.process([]);
            }
            var url = 'http://ws.geonames.org/searchJSON?country=US&name_startsWith=' + query;
            return $.ajax({
                url: url,
                success: function (data) {                    
                    var cityStates = [];
                    //limit results to 3
                    var maxItems = data.geonames.length < 5 ? data.geonames.length : 5;
                    for (var i = 0; i < maxItems; i++) {
                        cityStates.push({
                            Name: data.geonames[i].name + ', ' + data.geonames[i].adminCode1
                        });
                    }
                    typeahead.process(cityStates);
                },
                error: Friendly.GenericErrorMessage
            });
        },
        property: "Name"
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
        Friendly.SubmitForm('property', 'vehicle');
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
    $('input[id=VehicleViewModel_Refinanced]').change(function () {
        $('#VehicleViewModel_RefinanceDate').val('');
        if ($('#VehicleViewModel_Refinanced:checked').val() === "1") {
            $('.vehicle-refinance').show();
        } else {
            $('.vehicle-refinance').hide();
        }
    });
    $('input[id=VehicleViewModel_VehiclesInvolved]').change(function () {
        $('#vehicle')[0].reset();
        if ($('#VehicleViewModel_VehiclesInvolved:checked').val() === "1") {
            $('.vehicle-info').show();
        } else {
            $('.vehicle-info').hide();
        }
    });
    $('#addVehicle').click(function () {
        Friendly.StartLoading();
        var formName = 'vehicle';
        if ($('#' + formName).valid()) {
            var vehicleFormModel = Friendly.GetFormInput('vehicleForm');
            $.ajax({
                url: '/api/VehicleForm?format=json',
                type: 'POST',
                data: vehicleFormModel,
                success: function (data) {
                    //get values
                    var model = Friendly.GetFormInput(formName);
                    model.VehicleFormId = data.VehicleForm.Id;
                    $.ajax({
                        url: '/api/' + formName + '?format=json',
                        type: 'POST',
                        data: model,
                        success: function (vehicle) {
                            //Add vehicle to list
                            $('.vehicle-table').show();
                            var result = $("#friendly-vehicle-template").tmpl(vehicle.Vehicle);
                            $('.vehicle-table tbody').append(result);
                            Friendly.ClearForm('vehicle');
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
        $('html, body').animate({ scrollTop: 0 }, 'fast');
        if ($('#vehicleForm').valid()) {
            Friendly.NextForm('debt', Friendly.properties.iconSuccess);
        } else {
            Friendly.NextForm('debt', Friendly.properties.iconError);
        }
        Friendly.EndLoading();
        return false;
    });

    //Debt Form
    $('.domestic-part4').click(function () {
        //get values
        Friendly.SubmitForm('debt', 'asset');
    });

    $('input[name=MaritalDebt]').change(function () {
        $('#DebtDivision').val('');
        if ($('#MaritalDebt:checked').val() === "1") {
            $('.debt-details').show();
        } else {
            $('.debt-details').hide();
        }
    });
    //Asset Form
    $('.domestic-part5').click(function () {
        //check if we need to move to next form
        Friendly.SubmitForm('asset', 'healthInsurance');
    });

    $('input[name=Retirement]').change(function () {
        $('#RetirementDescription').val('');
        if ($('#Retirement:checked').val() === "1") {
            $('.retirement-detail').show();
        } else {
            $('.retirement-detail').hide();
        }
    });
    $('input[name=NonRetirement]').change(function () {
        $('#NonRetirementDescription').val('');
        if ($('#NonRetirement:checked').val() === "1") {
            $('.non-retirement-detail').show();
        } else {
            $('.non-retirement-detail').hide();
        }
    });
    $('input[name=Business]').change(function () {
        $('#BusinessDescription').val('');
        if ($('#Business:checked').val() === "1") {
            $('.business-detail').show();
        } else {
            $('.business-detail').hide();
        }
    });

    //Health Insurance
    $('.domestic-part6').click(function () {
        Friendly.SubmitForm('healthInsurance', 'spousal');
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
        Friendly.SubmitForm('spousal', 'tax');
    });
    $('input[name=Spousal]').change(function () {
        $('#SpousalDescription').val('');
        if ($('#Spousal:checked').val() === "1") {
            $('.spousal-detail').show();
        } else {
            $('.spousal-detail').hide();
        }
    });
    //Taxes 
    $('.domestic-part8').click(function () {
        //last form.  Let's try and validate this bi-atch
        Friendly.StartLoading();
        var formName = 'tax';
        var model = Friendly.GetFormInput(formName);
        var formSelector = '#' + formName;
        if ($(formSelector).valid()) {
            $.ajax({
                url: '/api/' + formName + '/?format=json',
                type: 'POST',
                data: model,
                success: function () {
                    var forms = ["house", "property", "vehicleForm", "debt", "asset", "healthInsurance", "spousal", "tax"];
                    var properNames = ["Marital House", "Personal Property", "Vehicles", "Debt", "Assets", "Health Insurance", "Spousal Support", "Taxes"];
                    Friendly.ValidateForms(forms, properNames, '.domestic-part8');
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