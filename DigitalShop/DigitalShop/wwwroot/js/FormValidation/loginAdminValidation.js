$("#formAdminLogin").validate({
    rules: {
        userName: {
            required: true,
            maxlength: 30
        },
        password: {
            required: true,
            maxlength: 30
        }
    }
});