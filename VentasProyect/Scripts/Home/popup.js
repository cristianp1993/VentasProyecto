$(document).ready(function () {
    function bindEvents() {
        $('.increase-quantity').off('click').on('click', function () {
            var quantity = parseInt($('#quantity').val());
            var stock = parseInt($('#popup-product-stock').text());
            if (quantity < stock) {
                $('#quantity').val(quantity + 1);
            }
        });

        $('.decrease-quantity').off('click').on('click', function () {
            var quantity = parseInt($('#quantity').val());
            if (quantity > 1) {
                $('#quantity').val(quantity - 1);
            }
        });
    }

    $('.open-popup').on('click', function () {
        $('body').addClass('overlay-active');

        var productData = $(this).data('product');

        $('#popup-product-image').attr('src', productData.pro_url_img);
        $('#popup-product-image').attr('alt', productData.pro_nombre);
        $('#popup-product-name').text(productData.pro_nombre);
        var price = '$ ' + productData.pro_valor_unitario.toLocaleString('es-CO', { minimumFractionDigits: 0 }) + ' COP';
        $('#popup-product-price').text(price);

        $('#popup-product-stock').text(productData.pro_stock);
        $('#popup-product-description').text(productData.pro_descripcion);

        var stock = productData.pro_stock;
        if (stock === 0) {
            $('#quantity').val(0);
            $('.increase-quantity, .decrease-quantity, #quantity').prop('disabled', true);
            $('#buy-now, #add-to-cart').prop('disabled', true);
        } else {
            $('#quantity').val(1);
            $('.increase-quantity, .decrease-quantity, #quantity').prop('disabled', false);
            $('#buy-now, #add-to-cart').prop('disabled', false);
        }

        bindEvents(); // Bind the events when opening the popup

        $('#product-popup').removeClass('d-none').show();
    });

    $('.close-popup').on('click', function () {
        $('body').removeClass('overlay-active');
        $('#product-popup').hide();
    });

    // Initial bind for events outside the popup
    bindEvents();
});
