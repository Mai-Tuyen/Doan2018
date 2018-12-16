$("#formAddImport").validate({
    rules: {
        Code: {
            required: true,
            minlength: 3,
            maxlength: 50
        },
        Detail: {
            required: true
        }
    }
});