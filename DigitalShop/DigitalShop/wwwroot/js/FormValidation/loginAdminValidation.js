$("#formAdminLogin").validate({
    rules: {
        userNameAdmin: {
            required: true,
            maxlength: 30
        },
        passWordAdmin: {
            required: true,
            maxlength: 30
        }
    }
});