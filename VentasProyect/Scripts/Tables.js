$(document).ready(function () {
    $('#MyTable').DataTable({
        "lengthMenu": [10, 25, 50, 100], // Opciones de cantidad por p�gina
        "pageLength": 10, // Cantidad por p�gina por defecto
        "language": {
            "zeroRecords": "No se encontraron registros",
            "info": "Mostrando del _START_ al _END_ de _TOTAL_ registros",
            "lengthMenu": "Mostrar _MENU_ registros por p�gina",
            "search": "Buscar:",
            "paginate": {
                "previous": "Anterior",
                "next": "Siguiente"
            }

        }
    });
});