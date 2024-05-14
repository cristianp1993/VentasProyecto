$(document).ready(function () {
    $('#MyTable').DataTable({
        "lengthMenu": [10, 25, 50, 100], // Opciones de cantidad por página
        "pageLength": 10, // Cantidad por página por defecto
        "language": {
            "zeroRecords": "No se encontraron registros",
            "info": "Mostrando del _START_ al _END_ de _TOTAL_ registros",
            "lengthMenu": "Mostrar _MENU_ registros por página",
            "search": "Buscar:",
            "paginate": {
                "previous": "Anterior",
                "next": "Siguiente"
            }

        }
    });
});