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
    $('#registerAdmin').submit(function (e) {
        var formName = $(this).attr('id');
        var action = $(this).attr('action');
        Friendly.StartLoading();
        e.preventDefault();
        var model = Friendly.GetFormInput(formName);
        if ($("#" + formName).valid()) {
            $.ajax({
                url: action + '?format=json',
                type: 'POST',
                data: model,
                success: function () {
                    //Since this is the admin, we need to add the roles to the current user
                    $.ajax({
                        url: '/api/userauths/addrole?format=json',
                        type: 'POST',
                        data: {
                            Roles: ['Admin', 'Attorney']
                        },
                        success: function() {
                            document.location.href = '/';
                        }
                    })
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