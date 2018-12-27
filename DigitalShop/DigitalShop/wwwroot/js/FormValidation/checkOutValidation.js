$("#frmSubmitCheckOut").validate({
    shipName: {
        required: true,
        maxlength: 30
    },
    shipMobile: {
        required: true,
        minlength: 6,
        maxlength: 12,
        number: true
    },
    shipAddress: {
        required: true,
        maxlength: 70
    }
});