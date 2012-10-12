$(document).ready(function () {
    $('#printForm').click(function() {
        var html = $('#main-content').html();        
        $.ajax({
            url: '/Output/PrintForm',
            type: 'POST',
            data: {
                html: html
            },
            success: function(data) {
                
            },
            error: Friendly.GenericErrorMessage
        });
    });
});