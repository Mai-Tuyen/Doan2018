$("#frmRegisterCustomer").validate({
    rules: {
        UserName: {
            email: true,
            required: true,
            maxlength: 30
        },
        DisplayName: {
            required: true,
            maxlength: 30
        },
        Phone: {
            required: true,
            minlength:6,
            maxlength: 12,
            number: true
        },
        Address: {
            required: true,
            maxlength: 70
        },
        password1: {
            required: true,
            minlength: 6,
            maxlength:30
        },
        ConfirmPassword: {
            required: true,
            minlength: 6,
            maxlength: 30
        }
    }
});