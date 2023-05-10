<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="AppWeb.Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Error</title>
    <link href="Contenido/CSS/Estilos.css" rel="stylesheet" />
    <link href="Contenido/CSS/Modal.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <section class="card">
            <div class="card__tittle">
                <asp:Label ID="lblErrorCode" runat="server" Text="Label"></asp:Label>
            </div>
            <div>
                <asp:Label ID="lblError" runat="server" Text="Label"></asp:Label>
            </div>
            <div class="card__footer">
                <a href="Default.aspx" class="btnNext"><span class="btnText">INICIO</span><span class="bgHover"></span></a>
                <%if (!Validaciones.Validar.sesion(Session["usuario"]))
                    {%>
                <a href="Login.aspx" class="btnNext"><span class="btnText">INICIAR SESIÓN</span><span class="bgHover"></span></a>
                <a href="Login.aspx" class="btnNext"><span class="btnText">REGISTRARSE</span><span class="bgHover"></span></a>
                <%}
                    else
                    {  %>
                <asp:LinkButton ID="lnkCerrarSesion" class="btnNext" runat="server" OnClick="lnkCerrarSesion_Click"><span class="btnText">CERRAR SESIÓN</span><span class="bgHover"></span></asp:LinkButton>
                <%} %>
            </div>
        </section>
    </form>
</body>
</html>
