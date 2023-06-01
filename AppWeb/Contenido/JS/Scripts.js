const menuButton = document.querySelector('.menu-button');
const menuMobile = document.getElementById('menu-mobile');
const chkBox = document.getElementById('chkOnOff');
const lblOn = document.getElementById('lblOn');
const lblOff = document.getElementById('lblOff');

menuButton.addEventListener('click', (event) => {
    event.preventDefault();
    if (menuMobile.classList.contains('menu-visible')) {
        menuMobile.classList.add('cerrarMenu');
        menuMobile.addEventListener('animationend', () => {
            menuMobile.classList.remove('menu-visible', 'cerrarMenu');
            menuMobile.classList.add('displaynone');
        }, { once: true });
    } else {
        menuMobile.classList.remove('displaynone');
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

function limpiarCampos() {
    document.getElementById('txtUsuario').value = '';
    document.getElementById('txtEmail').value = '';
    document.getElementById('txtMensaje').value = '';
}

if (chkBox.checked) {

}