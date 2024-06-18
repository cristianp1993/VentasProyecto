(function () {
    const arrayProducts = [];
    let productTemp;
    let addCartListenerAdded = false;
    let quantityEventListenersAdded = false;
    let flagIncreasing = false;
    function tallCards() {
        const cards = document.querySelectorAll(".product-card");
        let maxHeight = 0;

        cards.forEach((card) => {
            const height = card.offsetHeight;
            maxHeight = height > maxHeight ? height : maxHeight;
        });

        cards.forEach((card) => {
            card.style.height = `${maxHeight}px`;
        });
    }
    tallCards();

    function bindEvents() {
        if (!quantityEventListenersAdded) {
            document.querySelectorAll('.increase-quantity').forEach(function (element) {
                element.addEventListener('click', increaseQuantity, { capture: false }); // Add listener with capture: false
            });

            document.querySelectorAll('.decrease-quantity').forEach(function (element) {
                element.addEventListener('click', decreaseQuantity, { capture: false }); // Add listener with capture: false
            });

            quantityEventListenersAdded = true;
        }
    }

    function increaseQuantity() {
        let quantityElement = document.getElementById('quantity');
        let quantity = parseInt(quantityElement.value);
        let stock = parseInt(document.getElementById('popup-product-stock').textContent);
        if (quantity < stock) {
            quantityElement.value = quantity + 1;
        }
        productTemp.pro_cantidad = quantityElement.value;
        console.log(`Increased quantity to ${productTemp.pro_cantidad}`);
    }

    function decreaseQuantity() {
        let quantityElement = document.getElementById('quantity');
        let quantity = parseInt(quantityElement.value);
        if (quantity > 1) {
            quantityElement.value = quantity - 1;
        }
        productTemp.pro_cantidad = quantityElement.value;
        console.log(`Decreased quantity to ${productTemp.pro_cantidad}`);
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

    document.getElementById("buy-now").addEventListener('click', function () {

        arrayProducts.push(productTemp);
        console.log(arrayProducts)
        sendDataToSale()
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

    if (!addCartListenerAdded) {
        document.getElementById('add-to-cart').addEventListener('click', function () {
            document.getElementById('product-popup').style.display = 'none';

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

        addCartListenerAdded = true; // Set the flag to true after adding the listener
    }

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
            
            const productDetails = document.createElement('div');
            productDetails.style.display = 'flex';
            productDetails.style.width = '100%';
            
            const productImage = document.createElement('img');
            productImage.src = product.pro_url_img;
            productImage.alt = product.pro_nombre;
            productImage.style.width = '20vh';
            productImage.style.height = '25vh';
            productImage.style.marginRight = '20px';

            const productInfo = document.createElement('div');            
                productInfo.innerHTML = `
                  <h4>${product.pro_nombre}</h4>
                  <p>Precio: $${Number(product.pro_valor_unitario).toLocaleString('es-CO', { minimumFractionDigits: 0 })} COP</p>
                  <p>Stock: ${product.pro_stock}</p>
                  <p>Cantidad:
                  <input style="width: 7vh;" type="number" min="1" value="${product.pro_cantidad}" class="quantity-input">
                  </p>`;

            const deleteButton = document.createElement('span');
                deleteButton.style.cssText = `
                  flex: 0 0 auto;
                  margin-left: auto;
                `;
                deleteButton.classList.add('material-symbols-outlined', 'icon-actions-table');
                deleteButton.textContent = 'delete';
                deleteButton.style.cursor = "pointer";
                deleteButton.addEventListener('click', function () {

                const productIndex = arrayProducts.indexOf(product);
                arrayProducts.splice(productIndex, 1);
                console.log(arrayProducts)

                renderCart();
            });

            
            const quantityInput = productInfo.querySelector('.quantity-input');
            
            quantityInput.addEventListener('change', function () {
                
                let newQuantity = parseInt(this.value);
                
                if (isNaN(newQuantity) || newQuantity <= 0) {
                    newQuantity = product.pro_cantidad; 
                    this.value = newQuantity;
                    
                } else {                   
                    product.pro_cantidad = newQuantity;                   
                    this.value = newQuantity;
                    console.log("Quantity updated for", product.pro_nombre, "to", newQuantity);
                }
            });

            productDetails.appendChild(productInfo);
            productItem.appendChild(productImage);
            productItem.appendChild(productDetails);
            productDetails.appendChild(deleteButton);

            
            popupSaleArticles.appendChild(productItem);
        });

        popupSale.style.display = 'block';
    }


    let isRedirected = false;

    document.getElementById("end-Sale-Btn").addEventListener('click', async function () {
        if (arrayProducts.length > 0) {
            sendDataToSale();
        } else {
            Swal.fire({
                icon: 'warning',
                title: 'No ha elegido producto a comprar',
                text: 'Por favor, seleccione al menos un producto antes de proceder a la compra.'
            });
        }
    });

    async function sendDataToSale() {

        const filteredProducts = arrayProducts.map(product => ({
            pro_id: product.pro_id,
            pro_cantidad: product.pro_cantidad
        }));

        const encodedProducts = JSON.stringify(filteredProducts);

        try {
            const response = await fetch('/Ventas/MakeSale', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ dataProduct: encodedProducts })
            });

            if (response.ok) {
                const result = await response.json();
                if (result.success) {
                    const redirectUrl = result.redirectUrl + '?productos=' + encodeURIComponent(result.productos);
                    if (!isRedirected) {
                        isRedirected = true;
                        window.location.href = redirectUrl;
                    }
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'No se recibió una URL de redirección válida.'
                    });
                }
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: 'Ocurrió un error al procesar la venta.'
                });
            }
        } catch (error) {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Ocurrió un error al procesar la venta.'
            });
        }
    }
})();
