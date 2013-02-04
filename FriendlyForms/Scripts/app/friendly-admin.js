$(function ($) {
    $('#adminEmail').submit(function (e) {
        var formName = $(this).attr('id');
        Friendly.StartLoading();
        e.preventDefault();
        var model = Friendly.GetFormInput(formName);
        if ($("#" + formName).valid()) {
            $.ajax({
                url: '/api/emails?format=json',
                type: 'POST',
                data: model,
                success: function () {
                    $('#' + formName)[0].reset(); 
                    Friendly.ShowMessage("Success!", "The e-mail was successfully sent to the client.", Friendly.properties.messageType.Success, '#' + formName);
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
});