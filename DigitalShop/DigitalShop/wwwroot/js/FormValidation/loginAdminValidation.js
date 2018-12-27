$("#formAdminLogin").validate({
    rules: {
        userName: {
            required: true,
            maxlength: 30
        },
        passWord: {
            required: true,
            maxlength: 30
        }
    }
});