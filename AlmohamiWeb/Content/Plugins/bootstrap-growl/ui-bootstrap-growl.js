// by dharmesh
var toaster = function () {

    return {

        success: function (msg) {
            $.bootstrapGrowl(msg, {
                ele: 'body', // which element to append to
                type: 'success', // (null, 'info', 'danger', 'success', 'warning')
                offset: {
                    from: 'bottom',
                    amount: '10'
                }, // ' top', or 'bottom'
                align: 'right', // ('left', 'right', or 'center')
                width: 'auto', // (integer, or 'auto')
                delay: '2500', // Time while the message will be displayed. It's not equivalent to the *demo* timeOut!
                allow_dismiss: 'false', // If true then will display a cross to close the popup.
                stackup_spacing: 10 // spacing between consecutively stacked growls.
            });
        },

        danger: function (msg) {
            $.bootstrapGrowl(msg, {
                ele: 'body', // which element to append to
                type: 'danger', // (null, 'info', 'danger', 'success', 'warning')
                offset: {
                    from: 'bottom',
                    amount: '10'
                }, // ' top', or 'bottom'
                align: 'right', // ('left', 'right', or 'center')
                width: 'auto', // (integer, or 'auto')
                delay: '2500', // Time while the message will be displayed. It's not equivalent to the *demo* timeOut!
                allow_dismiss: 'false', // If true then will display a cross to close the popup.
                stackup_spacing: 10 // spacing between consecutively stacked growls.
            });
        },
        warning: function (msg) {
            $.bootstrapGrowl(msg, {
                ele: 'body', // which element to append to
                type: 'warning', // (null, 'info', 'danger', 'success', 'warning')
                offset: {
                    from: 'bottom',
                    amount: '10'
                }, // ' top', or 'bottom'
                align: 'right', // ('left', 'right', or 'center')
                width: 'auto', // (integer, or 'auto')
                delay: '2500', // Time while the message will be displayed. It's not equivalent to the *demo* timeOut!
                allow_dismiss: 'false', // If true then will display a cross to close the popup.
                stackup_spacing: 10 // spacing between consecutively stacked growls.
            });
        }, info: function (msg) {
            $.bootstrapGrowl(msg, {
                ele: 'body', // which element to append to
                type: 'info', // (null, 'info', 'danger', 'success', 'warning')
                offset: {
                    from: 'bottom',
                    amount: '10'
                }, // ' top', or 'bottom'
                align: 'right', // ('left', 'right', or 'center')
                width: 'auto', // (integer, or 'auto')
                delay: '2500', // Time while the message will be displayed. It's not equivalent to the *demo* timeOut!
                allow_dismiss: 'false', // If true then will display a cross to close the popup.
                stackup_spacing: 10 // spacing between consecutively stacked growls.
            });
        },
    };

}();

