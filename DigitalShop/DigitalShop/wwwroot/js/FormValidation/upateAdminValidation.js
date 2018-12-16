$("#formUpdateAdmin").validate({
    rules: {
        UserName: {
            required: true,
            maxlength: 30
        },
        PassWord: {
            required: true,
            maxlength: 30
        }
    }
});