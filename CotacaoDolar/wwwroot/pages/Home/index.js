$(document).ready(() => {
    const valorCotacao = $('#input-cotacao').val();
    const $inputReal = $('#input-real');
    const $inputDolar = $('#input-dolar');

    $inputReal.formatCurrency();
    $inputDolar.formatCurrency();

    $inputReal.on('change', function () {
        if ($inputReal.data('currency').isChanging) { return; }
        valorReal = +$inputReal.val().replaceAll(',', '.');;
        valorDolar = valorReal / valorCotacao * 10_000;
        //to ensure things like 1.005 round correctly
        valorDolar += Number.EPSILON;
        valorDolar = Math.round(valorDolar * 100) / 100;

        let currency = $inputDolar.data('currency');
        currency.isChanging = true;
        $inputDolar.data('currency', currency);
        console.log('change input')
        $inputDolar.val(valorDolar);
        currency.isChanging = false;
        $inputDolar.data('currency', currency);
    });
    $inputDolar.on('change', function () {
        if ($inputDolar.data('currency').isChanging) { return; }
        valorDolar = +$inputDolar.val().replaceAll(',', '.');;
        valorReal = valorDolar * valorCotacao / 10_000;
        //to ensure things like 1.005 round correctly
        valorReal += Number.EPSILON;
        valorReal = Math.round(valorReal * 100) / 100;

        let currency = $inputReal.data('currency');
        currency.isChanging = true;
        $inputReal.data('currency', currency);
        console.log('change input')
        $inputReal.val(valorReal);
        currency.isChanging = true;
        $inputReal.data('currency', currency);
    });

    $inputReal.trigger('change');
});