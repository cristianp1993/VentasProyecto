window.addEventListener('DOMContentLoaded', function () {
    const perNitInput = document.getElementById('per_nit');


    function updatePlaceholders() {
        const width = window.innerWidth;

        if (width <= 945) {
            perNitInput.placeholder = "# Documento";
            // Actualiza los placeholders de otros elementos seg�n sea necesario
        } else {
            // Restaura los placeholders originales si la resoluci�n es mayor a 1065px
            perNitInput.placeholder = "Numero de documento";
            // Restaura los placeholders de otros elementos seg�n sea necesario
        }
    }

    // Llama a la funci�n para actualizar los placeholders al cargar la p�gina y al redimensionar la ventana
    updatePlaceholders();
    window.addEventListener('resize', updatePlaceholders);
});