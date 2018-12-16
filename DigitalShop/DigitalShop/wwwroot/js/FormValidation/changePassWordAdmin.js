$("#frmChangePassWordAdmin").validate({
    rules: {
        newPassWord: {
            required: true,
            maxlength: 20
        },
        confirmNewPassWord: {
            required: true,
            maxlength: 20
        }
    }
});