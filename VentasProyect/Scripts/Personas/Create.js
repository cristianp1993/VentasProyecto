window.addEventListener('DOMContentLoaded', function () {
    const perNitInput = document.getElementById('per_nit');


    function updatePlaceholders() {
        const width = window.innerWidth;

        if (width <= 945) {
            perNitInput.placeholder = "# Documento";
            // Actualiza los placeholders de otros elementos según sea necesario
        } else {
            // Restaura los placeholders originales si la resolución es mayor a 1065px
            perNitInput.placeholder = "Numero de documento";
            // Restaura los placeholders de otros elementos según sea necesario
        }
    }

    // Llama a la función para actualizar los placeholders al cargar la página y al redimensionar la ventana
    updatePlaceholders();
    window.addEventListener('resize', updatePlaceholders);
});