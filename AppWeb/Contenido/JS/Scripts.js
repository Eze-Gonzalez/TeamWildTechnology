const menuButton = document.querySelector('.menu-button');
const menuMobile = document.getElementById('menu-mobile');

menuButton.addEventListener('click', (event) => {
    event.preventDefault();
    if (menuMobile.classList.contains('menu-visible')) {
        menuMobile.classList.add('cerrarMenu');
        menuMobile.addEventListener('animationend', () => {
            menuMobile.classList.remove('menu-visible', 'cerrarMenu');
        }, { once: true });
    } else {
        menuMobile.classList.add('menu-visible');
    }
});

function mostrarPass(txt, ico) {
    var txtPass = document.getElementById(txt);
    var icono = document.getElementById(ico);
    if (icono.classList.contains('fa-eye-slash')) {
        icono.classList.replace('fa-eye-slash', 'fa-eye');
        txtPass.value = txtPass.value;
        txtPass.type = 'text';
    } else {
        icono.classList.replace('fa-eye', 'fa-eye-slash');
        txtPass.value = txtPass.value;
        txtPass.type = 'password';
    }
}



