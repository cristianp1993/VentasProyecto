document.addEventListener('DOMContentLoaded', function () {
    var menuToggle = document.getElementById('menuToggle');
    var sidebar = document.querySelector('.sidebar');
    var navBar = document.querySelector('.nav-bar');

    function toggleSidebar() {
        sidebar.classList.toggle('collapsed');
        navBar.classList.toggle('collapsed');
    }

    menuToggle.addEventListener('click', function () {
        toggleSidebar();
    });

    // Controla la apertura o cierre del sidebar al cambiar el tamaño de la pantalla
    window.addEventListener('resize', function () {
        if (window.innerWidth > 1200) {
            sidebar.classList.remove('collapsed');
            navBar.classList.remove('collapsed');
        }
    });
});
