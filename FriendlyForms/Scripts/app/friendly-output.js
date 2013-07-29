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
    var populateScheduleEForm = function (data) {
        var result = $("#friendly-scheduleE-template").tmpl(data);
        $('#finanicalFormOutput').empty();
        $('#finanicalFormOutput').append(result);
        scheduleETemplate(data.Extraordinaries.Education, data.Extraordinaries.EducationTotal, '#education');
        scheduleETemplate(data.Extraordinaries.Tuition, data.Extraordinaries.TuitionTotal, '#other-education');
        scheduleETotalTemplate(data.Extraordinaries.YearlyEducation, data.Extraordinaries.MonthlyEducation,data.Extraordinaries.YearlyEducationTotal, data.Extraordinaries.MonthlyEducationTotal, 'education');
        scheduleETemplate(data.Extraordinaries.Medical, data.Extraordinaries.MedicalTotal, '#medical');
        scheduleETotalTemplate(data.Extraordinaries.YearlyMedical, data.Extraordinaries.MonthlyMedical, data.Extraordinaries.YearlyMedicalTotal, data.Extraordinaries.MonthlyMedicalTotal, 'medical');
        scheduleETemplate(data.Extraordinaries.Rearing, data.Extraordinaries.RearingTotal, '#rearing');
        scheduleETotalTemplate(data.Extraordinaries.YearlyRearing, data.Extraordinaries.MonthlyRearing, data.Extraordinaries.YearlyRearingTotal, data.Extraordinaries.MonthlyRearingTotal, 'rearing');
    };
    function scheduleETemplate(listModel, sumModel, name) {
        var $template = $('#friendly-supplementalTableChild-template');
        var $totalTemplate = $('#friendly-supplementalTableChildSum-template');
        var result;
        result = $totalTemplate.tmpl(sumModel);
        $(result).insertAfter(name);
        $.each(listModel, function (ndx, item) {
            result = $template.tmpl(item);
            $(result).insertAfter(name);
        });
    }
    function scheduleETotalTemplate(yearlyModel, monthlyModel, yearlySum, monthlySum, name) {
        var $template = $('#friendly-supplementalTableChildTotalAll-template');
        var $totalTemplate = $('#friendly-supplementalTableChildCombinedTotal-template');
        var result;
        result = $totalTemplate.tmpl(yearlySum);
        $(result).insertAfter('#yearly-' + name);
        result = $template.tmpl(yearlyModel);
        $(result).insertAfter('#yearly-' + name);
        result = $totalTemplate.tmpl(monthlySum);
        $(result).insertAfter('#monthly-' + name);
        result = $template.tmpl(monthlyModel);
        $(result).insertAfter('#monthly-' + name);
    }
    function populateCswForm(data) {
        var result = $("#friendly-childSupportWorksheet-template").tmpl(data);
        $('#finanicalFormOutput').empty();
        $('#finanicalFormOutput').append(result);
        result = $("#friendly-CSWChildren-template").tmpl(data);
        $('#children').append(result);

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