$("#frmLoginCustomer").validate({
    rules: {
        userNameCustomerLogin: {
            email :true,
            required: true,
            maxlength: 30
        },
        passWordCustomerLogin: {
            required: true,
            maxlength: 30
        }
    }
});