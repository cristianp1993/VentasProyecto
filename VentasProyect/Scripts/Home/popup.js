(function () {
    const arrayProducts = [];
    let productTemp;


    function tallCards() {
        const cards = document.querySelectorAll(".product-card");
        let maxHeight = 0;

        //Se busca la altura maxima de las tarjeta
        cards.forEach((card) => {
            const height = card.offsetHeight;
            maxHeight = height > maxHeight ? height : maxHeight;
        });

        //Se actualiza la altura de lamas grande a todas
        cards.forEach((card) => {
            card.style.height = `${maxHeight}px`;
        });
    }
    tallCards()
    function bindEvents() {
        document.querySelectorAll('.increase-quantity').forEach(function (element) {
            element.removeEventListener('click', increaseQuantity);
            element.addEventListener('click', increaseQuantity);
        });

        document.querySelectorAll('.decrease-quantity').forEach(function (element) {
            element.removeEventListener('click', decreaseQuantity);
            element.addEventListener('click', decreaseQuantity);
        });
    }

    function increaseQuantity() {
        let quantity = parseInt(document.getElementById('quantity').value);
        let stock = parseInt(document.getElementById('popup-product-stock').textContent);
        if (quantity < stock) {
            document.getElementById('quantity').value = quantity + 1;
        }
        productTemp.pro_cantidad = document.getElementById('quantity').value;
    }

    function decreaseQuantity() {
        let quantity = parseInt(document.getElementById('quantity').value);
        if (quantity > 1) {
            document.getElementById('quantity').value = quantity - 1;
        }
        productTemp.pro_cantidad = document.getElementById('quantity').value;
    }

    document.querySelectorAll('.open-popup').forEach(function (element) {
        element.addEventListener('click', function () {
            document.body.classList.add('overlay-active');

            const productData = JSON.parse(element.dataset.product);

            document.getElementById('popup-product-id').setAttribute('alt', productData.pro_nombre);
            document.getElementById('popup-product-image').setAttribute('src', productData.pro_url_img);
            document.getElementById('popup-product-image').setAttribute('alt', productData.pro_nombre);
            document.getElementById('popup-product-name').textContent = productData.pro_nombre;
            let price = '$ ' + Number(productData.pro_valor_unitario).toLocaleString('es-CO', { minimumFractionDigits: 0 }) + ' COP';
            document.getElementById('popup-product-price').textContent = price;

            document.getElementById('popup-product-stock').textContent = productData.pro_stock;
            document.getElementById('popup-product-description').textContent = productData.pro_descripcion;

            let stock = productData.pro_stock;
            if (stock === 0) {
                document.getElementById('quantity').value = 0;
                disableElements(['.increase-quantity', '.decrease-quantity', '#quantity', '#buy-now', '#add-to-cart']);
            } else {
                document.getElementById('quantity').value = 1;
                enableElements(['.increase-quantity', '.decrease-quantity', '#quantity', '#buy-now', '#add-to-cart']);
            }

            productTemp = { ...productData, pro_cantidad: 1 }; // Initialize pro_cantidad
            bindEvents();

            document.getElementById('product-popup').classList.remove('d-none');
            document.getElementById('product-popup').style.display = 'block';
        });
    });

    document.querySelectorAll('.close-popup').forEach(function (element) {
        element.addEventListener('click', function () {
            document.body.classList.remove('overlay-active');
            document.getElementById('product-popup').style.display = 'none';
        });
    });

    function disableElements(selectors) {
        selectors.forEach(function (selector) {
            document.querySelectorAll(selector).forEach(function (element) {
                element.disabled = true;
            });
        });
    }

    function enableElements(selectors) {
        selectors.forEach(function (selector) {
            document.querySelectorAll(selector).forEach(function (element) {
                element.disabled = false;
            });
        });
    }

    bindEvents();

    document.getElementById('add-to-cart').addEventListener('click', function () {
        document.getElementById('product-popup').style.display = 'none';

        // Check if the product is already in the array
        const existingProduct = arrayProducts.find(product => product.pro_nombre === productTemp.pro_nombre);
        if (existingProduct) {
            existingProduct.pro_cantidad = parseInt(existingProduct.pro_cantidad) + parseInt(productTemp.pro_cantidad);
        } else {
            const productToAdd = { ...productTemp };
            arrayProducts.push(productToAdd);
        }

        console.log(arrayProducts);

        renderCart();
    });


    document.querySelectorAll('.closePopupButton').forEach(function (element) {
        element.addEventListener('click', function () {
            document.getElementById('popupSale').style.display = 'none';
            document.body.classList.remove('overlay-active');
        });
    });

    function renderCart() {
        const popupSale = document.getElementById('popupSale');
        const popupSaleArticles = document.getElementById('popupSaleArticles');
        popupSaleArticles.innerHTML = '';

        arrayProducts.forEach(function (product) {
            const productItem = document.createElement('div');
            productItem.className = 'product-item';
            productItem.style.display = 'flex'; 
            productItem.style.alignItems = 'center'; 
            productItem.style.marginBottom = '20px';
            productItem.innerHTML = `
                <div style="margin-right:25px">
                    <img src="${product.pro_url_img}" alt="${product.pro_nombre}" style="width: 20vh; height: 25vh;margin-right: 20px;" />
                </div>
                <div>
                    <h4>${product.pro_nombre}</h4>                   
                    <p>Precio: $${Number(product.pro_valor_unitario).toLocaleString('es-CO', { minimumFractionDigits: 0 })} COP</p>
                    <p>Stock: ${product.pro_stock}</p>
                    <p>Cantidad: ${product.pro_cantidad}</p>
                </div>
            `;
            popupSaleArticles.appendChild(productItem);
        });

        popupSale.style.display = 'block';
    }


    document.getElementById("end-Sale-Btn").addEventListener('click', async function () {

        if (arrayProducts.length > 0) {

            const encodedProducts = JSON.stringify(arrayProducts);

            const baseURL = "Ventas/MakeSale?dataProduct=" + encodedProducts;

            window.open(baseURL, "_self");
           
        } else {
            Swal.fire({
                icon: 'warning',
                title: 'No ha elegido producto a comprar',
                text: 'Por favor, seleccione al menos un producto antes de proceder a la compra.'
            });
        }
            
    })

    
})();
