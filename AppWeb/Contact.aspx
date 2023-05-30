<%@ Page Title="" Language="C#" MasterPageFile="~/TWT.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="AppWeb.Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="contact">
                <div class="contact__form">
                    <div class="contact__form-Header"><span></span></div>
                    <div class="contact__form-Icon"><i class="fa-sharp fa-solid fa-envelope"></i></div>
                    <h2>Contactanos</h2>
                    <p>Rellene todos los campos para enviarnos un mensaje.</p>
                    <div class="contact__form-Grid">
                        <p>Nombre</p>
                        <asp:TextBox ID="txtUsuario" ClientIDMode="Static" runat="server"></asp:TextBox>
                        <p>Email</p>
                        <asp:TextBox ID="txtEmail" ClientIDMode="Static" runat="server"></asp:TextBox>
                        <p>Mensaje</p>
                        <asp:TextBox ID="txtMensaje" ClientIDMode="Static" TextMode="MultiLine" runat="server"></asp:TextBox>
                        <div class="contact-footer">
                            <asp:LinkButton ID="lnkEnviar" runat="server" CssClass="btnNext buttons" OnClick="lnkEnviar_Click"><span class="btnText">ENVIAR</span><span class="bgHover"></span></asp:LinkButton>
                            <a href="Default.aspx" class="btnCancelar buttons"><span class="btnText">CANCELAR</span><span class="bgHover"></span></a>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="modalAlerta" runat="server" CssClass="modal-alerta">
        <div class="alerta boxSuccess">
            <div class="swal-icon swal-icon--success">
                <div class="swal-icon--success__ring"></div>
                <div class="swal-icon--success__hide-corners"></div>
                <span class="swal-icon--success__line swal-icon--success__line--long"></span>
                <span class="swal-icon--success__line swal-icon--success__line--tip"></span>
            </div>
            <h2>Mensaje Enviado</h2>
            <p>El mensaje ha sido enviado correctamente, si es necesario, a la brevedad nos pondremos en contacto con usted.</p>
            <label>Gracias por ponerse en contacto con nosotros.</label>
            <p>Team Wild Technology®</p>
            <div class="alerta-footer">
                <asp:LinkButton ID="aceptarAlerta" runat="server" CssClass="btnNext" OnClick="aceptarAlerta_Click" OnClientClick="limpiarCampos()"><span class="btnText">ACEPTAR</span><span class="bgHover"></span></asp:LinkButton>
            </div>
        </div>
    </asp:Panel>
     <asp:Button ID="btnMostrarModal" runat="server" Text="Button" Style="display: none" />
    <ajax:ModalPopupExtender runat="server" ID="ajxAlerta" OkControlID="aceptarAlerta" CancelControlID="aceptarAlerta"
        TargetControlID="btnMostrarModal" PopupControlID="modalAlerta" BackgroundCssClass="ajxBG">
    </ajax:ModalPopupExtender>
</asp:Content>
