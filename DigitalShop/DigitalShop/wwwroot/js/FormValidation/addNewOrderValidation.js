$("#frmAddNewOrder").validate({
    shipName2: {
        required: true,
        maxlength: 30
    },
    shipMobile2: {
        required: true,
        minlength: 6,
        maxlength: 12,
        number: true
    },
    shipAddress2: {
        required: true,
        maxlength: 70
    }
});