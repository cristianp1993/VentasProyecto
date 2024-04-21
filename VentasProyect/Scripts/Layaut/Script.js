document.getElementById('btnCloseSesion').addEventListener('click', function () {
    // Realizar la solicitud Fetch al método CloseSesion en LoginController
    fetch('/Login/CloseSesion', {
        method: 'POST', 
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.text())
        .then(data => {
            // Manejar la respuesta del servidor
            if (data == "False") {
                
                window.location.href = '/Home/Index';
            } else {
                alert('Hubo un error al intentar cerrar sesión.');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Hubo un error al intentar cerrar sesión.');
        });
});