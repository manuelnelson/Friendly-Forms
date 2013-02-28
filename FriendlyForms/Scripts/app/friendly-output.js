$(function ($) {
    var html = $('#main-content').html();
    html = html.replace(/<form.*>/, "");
    html = html.replace(/<input.*>/, "");
    html = html.replace(/<footer[^>]*?>([\s\S]*)<\/footer>/, "");
    $('.html').val(html);
});