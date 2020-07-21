// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

String.prototype.replaceAll = function (charOut, charIn) {
    return this.split(charOut).join(charIn);
};

$.fn.formatCurrency = function () {
    const changeValue = function () {
        let value = $(this).val().replaceAll('.', ',');
        $(this).data('currency', { isChanging: true, isCurrencyChanging: true });
        $(this).val(value);
        $(this).data('currency', { isChanging: false, isCurrencyChanging: false });
    };

    $(this).data('currency', { isChanging: false, isCurrencyChanging: false });

    $(this).on('keyup', changeValue);
    $(this).on('change', function () {
        console.log('change');
        if ($(this).data('currency').isCurrencyChanging) { return; }
        changeValue.apply(this);
    });
};