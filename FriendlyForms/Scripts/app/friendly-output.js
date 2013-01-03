$(function ($) {
    var html = $('#main-content').html();
    html = html.replace(/<form.*>/, "");
    html = html.replace(/<input.*>/, "");
    html = html.replace(/<footer[^>]*?>([\s\S]*)<\/footer>/, "");
    //html = html.replace(/<hr.*>/, "");
    $('#html').val(html);
    //$('.printForm').click(function() {
    //    var html = $('#main-content').html();
    //    //remove button at end of form
    //    html = html.replace(/<input.*>/, "");
    //    Friendly.StartLoading();
    //    $.ajax({
    //        url: '/Output/PrintForm',
    //        type: 'POST',
    //        data: {
    //            html: html
    //        },
    //        success: function(data) {
    //            Friendly.EndLoading();
    //        },
    //        error: Friendly.GenericErrorMessage
    //    });
    //});
});