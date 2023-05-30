//<div class="modal-alerta">
//    <div class="alerta">
//        <div class="swal-icon swal-icon--success">
//            <div class="swal-icon--success__ring"></div>
//            <div class="swal-icon--success__hide-corners"></div>
//            <span class="swal-icon--success__line swal-icon--success__line--long"></span>
//            <span class="swal-icon--success__line swal-icon--success__line--tip"></span>
//        </div>
//        <h2>Titulo</h2>
//        <p>Mensaje</p>
//        <div class="alerta-footer">
//            <a href="#" class="btnNext"><span class="btnText">ACEPTAR</span><span class="bgHover"></span></a>
//            <a href="#" class="btnNext"><span class="btnText">INICIAR SESIÓN</span><span class="bgHover"></span></a>
//        </div>
//    </div>
//</div>
function crearAlerta(status, titulo, mensaje) {
    const modalalerta = document.createElement("div");
    modalalerta.className = "modal-alerta";
    const alerta = document.createElement("div");
    alerta.className = "alerta";
    if (status) {
        const swalsuccess = document.createElement("div");
        swalsuccess.className = "swal-icon swal-icon--success";
        const swalring = document.createElement("div");
        swalring.className = "swal-icon--success__ring";
        const hidecorners = document.createElement("div");
        hidecorners.className = "swal-icon--success__hide-corners";
        const linelong = document.createElement("span");
        linelong.className = "swal-icon--success__line swal-icon--success__line--long"
        const linetip = document.createElement("span");
        linetip.className = "swal-icon--success__line swal-icon--success__line--tip";
        swalsuccess.appendChild(linetip);
        swalsuccess.appendChild(linelong);
        swalsuccess.appendChild(hidecorners);
        swalsuccess.appendChild(swalring);
        alerta.appendChild(swalsuccess);
        alerta.classList.add("boxSuccess");
    } else {
        var iconoError = document.createElement("div");
        var iconoX = document.createElement("div");
        var iconoLeft = document.createElement("span");
        var iconoRight = document.createElement("span");
        iconoError.className = "swal-icon swal-icon--error";
        iconoX.className = "swal-icon--error__x-mark";
        iconoLeft.className = "swal-icon--error__line swal-icon--error__line--left";
        iconoRight.className = "swal-icon--error__line swal-icon--error__line--right";
        iconoError.appendChild(iconoX);
        iconoX.appendChild(iconoLeft);
        iconoX.appendChild(iconoRight);
        alerta.appendChild(iconoError);
        alerta.classList.add("boxError");
    }
    var closeBtn = document.createElement("span");
    closeBtn.className = "displaynone";
    closeBtn.innerHTML = "&times;";
    closeBtn.onclick = function () {
        modalalerta.style.display = "none";
    };
    const title = document.createElement("h2");
    const message = document.createElement("p");
    title.innerHTML = titulo;
    message.innerText = mensaje;
    alerta.appendChild(title);
    alerta.appendChild(message);
    const alertafooter = document.createElement("div");
    alertafooter.className = "alerta-footer";
    const linkAceptar = document.createElement("a");
    linkAceptar.className = "btnNext";
    const spanAceptar = document.createElement("span");
    spanAceptar.className = "btnText";
    spanAceptar.innerHTML = "ACEPTAR";
    const spanAceptarHover = document.createElement("span");
    spanAceptarHover.className = "bgHover";
    linkAceptar.onclick = function () {
        modalalerta.style.display = "none";
    };
    linkAceptar.appendChild(spanAceptar);
    linkAceptar.appendChild(spanAceptarHover);
    alertafooter.appendChild(linkAceptar);
    if (titulo === "Email ya registrado" || titulo === "Contraseña existente") {
        const linkLogin = document.createElement("a");
        linkLogin.className = "btnNext";
        const spanLogin = document.createElement("span");
        spanLogin.className = "btnText";
        spanLogin.innerHTML = "INICIAR SESIÓN";
        const spanLoginHover = document.createElement("span");
        spanLoginHover.className = "bgHover";
        linkLogin.appendChild(spanLogin);
        linkLogin.appendChild(spanLoginHover);
        alertafooter.appendChild(linkLogin);
        linkLogin.onclick = function () {
            modalalerta.style.display = "none";
            var xhr = new XMLHttpRequest();
            xhr.open("POST", "Support.aspx", true);
            xhr.setRequestHeader("X-Requested-With", "XMLHttpRequest");
            xhr.send();
            window.location.href = "Login.aspx";
        };
    }
    if (titulo === "Email no registrado") {
        const linkRegister = document.createElement("a");
        linkRegister.className = "btnNext";
        const spanRegister = document.createElement("span");
        spanRegister.className = "btnText";
        spanRegister.innerHTML = "REGISTRARSE";
        const spanRegisterHover = document.createElement("span");
        spanRegisterHover.className = "bgHover";
        linkRegister.appendChild(spanRegister);
        linkRegister.appendChild(spanRegisterHover);
        alertafooter.appendChild(linkRegister);
        linkRegister.onclick = function () {
            modalalerta.style.display = "none";
            var xhr = new XMLHttpRequest();
            xhr.open("POST", "Support.aspx", true);
            xhr.setRequestHeader("X-Requested-With", "XMLHttpRequest");
            xhr.send();
            window.location.href = "Register.aspx";
        };
    }
    alerta.appendChild(alertafooter);
    modalalerta.appendChild(alerta);
    document.body.appendChild(modalalerta);
}