$(function ($) {
    getPdfHtml();

    if (window.location.hash.indexOf('#scheduleA') !== -1) {
        $.ajax({
            url: '/api/Output/Financial/ScheduleA?format=json',
            type: 'GET',
            success: function (data) {
                var result = $("#friendly-scheduleA-template").tmpl(data);
                $('#finanicalFormOutput').append(result);
                getPdfHtml();
            },
            error: Friendly.GenericErrorMessage
        });
    }

    $('.output-item').click(function() {
        var form = $(this).find('a').attr('data-form');
        $.ajax({
            url: '/api/Output/Financial/' + form + '/?format=json',
            type: 'GET',
            success: function (data) {
                switch (form) {
                    case 'scheduleB':
                        populateScheduleBForm(data);
                        break;
                    case 'scheduleD':
                        populateScheduleDForm(data);
                        break;
                    case 'scheduleE':
                        populateScheduleEForm(data);
                        break;
                    case 'childSupportWorksheet':
                        populateCswForm(data);
                        break;
                    default:
                        var result = $("#friendly-" + form + "-template").tmpl(data);
                        $('#finanicalFormOutput').empty();
                        $('#finanicalFormOutput').append(result);
                }
                getPdfHtml();
            },
            error: Friendly.GenericErrorMessage
        });
    });

    function populateScheduleBForm(data) {
        var result = $("#friendly-scheduleB-template").tmpl(data);        
        $('#finanicalFormOutput').empty();
        $('#finanicalFormOutput').append(result);
        result = $("#friendly-otherChildren-template").tmpl(data.ScheduleB);
        $('#other-qualified-children').append(result);
        result = $("#friendly-otherChildren-template").tmpl(data.OtherScheduleB);
        $('#other-qualified-children').append(result);
    }
    function populateScheduleDForm(data) {
        var result = $("#friendly-scheduleD-template").tmpl(data);
        $('#finanicalFormOutput').empty();
        $('#finanicalFormOutput').append(result);
        addParentChildCareCosts(data.CustodialParentName, '#friendly-supplementalTableFather-template', data.ChildCare, data.FatherScheduleD);
        addParentChildCareCosts(data.NonCustodialParentName, '#friendly-supplementalTableMother-template', data.ChildCare, data.MotherScheduleD);
        addParentChildCareCosts('Nonparent Custodian', '#friendly-supplementalTableNonParent-template', data.ChildCare, data.NonParentScheduleD);
    }
    function populateCswForm(data) {
        
    }
    function addParentChildCareCosts(parentName, templateName, childCareList, total) {
        var parent = {
            ParentName: parentName
        };
        var result = $("#friendly-supplementalTableHeader-template").tmpl(parent);
        $('#supplemental-table').append(result);
        $.each(childCareList, function (ndx, item) {
            result = $(templateName).tmpl(item);
            $('#supplemental-table').append(result);
        });
        //add total
        var totalObj = {
            Name: "Totals",
            TotalSchool: total.TotalSchool,
            TotalSummer: total.TotalSummer,
            TotalBreaks: total.TotalBreaks,
            TotalOther: total.TotalOther,
            TotalYearly: total.TotalYearly,
            TotalMonthly: total.TotalMonthly,
        };
        result = $('#friendly-supplementalTableTotal-template').tmpl(totalObj);
        $('#supplemental-table').append(result);
    }
    


    function getPdfHtml() {
        var html = $('#main-content').html();
        html = html.replace(/<form.*>/, "");
        html = html.replace(/<input.*>/g, "");
        html = html.replace(/<footer[^>]*?>([\s\S]*)<\/footer>/, "");
        $('.html').val(html);
    }
});