//$(document).ready(function () {
    $("#frmUpdateProduct").validate({
        rules: {
            Name: {
                required: true,
                minlength: 2,
                maxlength:50
            },
            PriceIn: {
                required: true,
                min: 0
            },
            PriceOut: {
                required: true,
                min: 0
            },
            Description: {
                required:true
            }
        }
    });
//});


