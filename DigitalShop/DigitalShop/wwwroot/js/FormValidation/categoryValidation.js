$("#frmUpdateCategory").validate({
    rules: {
        Name: {
            required: true,
            minlength: 2,
            maxlength: 50
        }
    }
});