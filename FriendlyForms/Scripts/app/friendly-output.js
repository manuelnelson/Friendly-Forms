$(function ($) {
    getPdfHtml();

    if (window.location.hash.indexOf('#scheduleA') !== -1) {
        var userId = $('#user-id').val();
        $.ajax({
            url: '/api/Output/Financial/ScheduleA/?UserId=' + userId + '&format=json',
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
        var userId = $('#user-id').val();
        $.ajax({
            url: '/api/Output/Financial/' + form + '/?UserId=' + userId + '&format=json',
            type: 'GET',
            success: function (data) {
                var result = $("#friendly-" + form + "-template").tmpl(data);
                $('#finanicalFormOutput').empty();
                $('#finanicalFormOutput').append(result);
                getPdfHtml();
            },
            error: Friendly.GenericErrorMessage
        });
    });

    function getPdfHtml() {
        var html = $('#main-content').html();
        html = html.replace(/<form.*>/, "");
        html = html.replace(/<input.*>/g, "");
        html = html.replace(/<footer[^>]*?>([\s\S]*)<\/footer>/, "");
        $('.html').val(html);
    }
});