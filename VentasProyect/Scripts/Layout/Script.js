document.addEventListener('DOMContentLoaded', function () {
    var menuToggle = document.getElementById('menuToggle');
    var sidebar = document.querySelector('.sidebar');
    var navBar = document.querySelector('.nav-bar');
    var bodyContent = document.querySelector('.body-content');

    function toggleSidebar() {
        sidebar.classList.toggle('collapsed');
        navBar.classList.toggle('collapsed');
        bodyContent.classList.toggle('collapsed-body');
    }

    menuToggle.addEventListener('click', function () {
        toggleSidebar();
    });

    window.addEventListener('resize', function () {
        if (window.innerWidth > 1200) {
            sidebar.classList.remove('collapsed');
            navBar.classList.remove('collapsed');
            bodyContent.classList.remove('collapsed-body');
        }
    });
});
