document.addEventListener("DOMContentLoaded", function () {
    const sidebar = document.querySelector("#sidebar");

    function adjustSidebar() {
        if (window.innerWidth < 992) {
            sidebar.classList.remove("expand");
            sidebar.classList.add("collapsed");
        } else {
            sidebar.classList.remove("collapsed");
            sidebar.classList.add("expand");
        }
    }

    // Ajustar el sidebar al cargar la página
    adjustSidebar();

    // Ajustar el sidebar cuando se cambia el tamaño de la ventana
    window.addEventListener("resize", adjustSidebar);

    // Alternar la clase expand/collapsed al hacer clic en el botón de toggle
    const hamBurger = document.querySelector(".toggle-btn");
    hamBurger.addEventListener("click", function () {
        sidebar.classList.toggle("expand");
        sidebar.classList.toggle("collapsed");
    });
});