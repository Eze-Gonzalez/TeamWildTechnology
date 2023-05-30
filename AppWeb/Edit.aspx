<%@ Page Title="" Language="C#" MasterPageFile="~/TWT.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="AppWeb.Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="profile">
                <h3>Editar
                    <asp:Label ID="lblEdit" runat="server" Text="Label"></asp:Label></h3>
                <%switch (Request.QueryString["edit"])
                    { %>
                <%case "1":%>
                <div class="input-box vw-100">
                    <label>Ingrese su email actual</label>
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                </div>
                <div class="input-box vw-100">
                    <label>Ingrese su nuevo email</label>
                    <asp:TextBox ID="txtNuevoEmail" runat="server"></asp:TextBox>
                </div>
                <%break;%>
                <%case "2":%>
                <div class="input-box vw-100">
                    <label>Ingrese su nombre de usuario actual</label>
                    <asp:TextBox ID="txtUserActual" runat="server"></asp:TextBox>
                </div>
                <div class="input-box vw-100">
                    <label>Ingrese su nuevo nombre de usuario</label>
                    <asp:TextBox ID="txtUserNuevo" runat="server"></asp:TextBox>
                </div>
                <%break;%>
                <%case "3":%>
                <div class="input-box vw-100">
                    <label>Ingrese su contraseña actual</label>
                    <asp:TextBox ID="txtPassActual" runat="server"></asp:TextBox>
                </div>
                <div class="input-box vw-100">
                    <label>Ingrese su nueva contraseña</label>
                    <asp:TextBox ID="txtPass" runat="server"></asp:TextBox>
                </div>
                <div class="input-box vw-100">
                    <label>Repita su nueva contraseña</label>
                    <asp:TextBox ID="txtRepetir" runat="server"></asp:TextBox>
                </div>
                <%break;%>
                <%case "4":%>
                <div class="input-box vw-100">
                    <label>Ingrese un nombre</label>
                    <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
                </div>
                <div class="input-box vw-100">
                    <label>Ingrese un apellido</label>
                    <asp:TextBox ID="txtApellido" runat="server"></asp:TextBox>
                </div>
                <%break;%>
                <%case "5":%>
                <label>Activar verificación de dos pasos</label>
                <asp:CheckBox ID="chkVerificacion" runat="server" AutoPostBack="true" />
                <%if (chkVerificacion.Checked)
                    { %>
                <label>Seleccione el tipo de verificación en dos pasos que desea activar</label>
                <label>Email</label>
                <asp:CheckBox ID="chkEmail" runat="server" AutoPostBack="true" />
                <label>Aplicación</label>
                <asp:CheckBox ID="chkApp" runat="server" AutoPostBack="true" />
                <%}  %>
                <%break;%>
                <%}  %>
                <div class="modal-footer vw-100 h-8 mt-10">
                    <asp:LinkButton ID="lnkSiguiente" runat="server" CssClass="btnNext buttons" OnClick="lnkSiguiente_Click"><span class="btnText">SIGUIENTE</span><span class="bgHover"></span></asp:LinkButton>
                    <a href="Profile.aspx" class="btnCancelar buttons"><span class="btnText">CANCELAR</span><span class="bgHover"></span></a>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="modalValidacion" CssClass="modalValidacion" runat="server">
        <div class="modalValidacion__Body">
            <p>Se le ha enviado un código de validación al email, ingréselo a continuación</p>
            <asp:TextBox ID="txtVerificar" CssClass="codigo" placeholder="Código de validación" runat="server" onkeypress="return forzarMayus(event)"></asp:TextBox>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div class="mb-3">
                        <asp:Label ID="lblErrorVerificar" CssClass="danger" runat="server" Text="Label" Visible="false"></asp:Label>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnValidar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="modal-footer">
                        <asp:LinkButton ID="btnValidar" CssClass="btnNext" runat="server" OnClick="btnValidar_Click"><span class="btnText">VALIDAR</span><span class="bgHover"></span></asp:LinkButton>
                        <asp:LinkButton ID="btnCacelarValidacion" CssClass="btnCancelar" runat="server" OnClick="btnCacelarValidacion_Click"><span class="btnText">CANCELAR</span><span class="bgHover"></span></asp:LinkButton>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>
    <asp:Button ID="btnMostrarModal" runat="server" Text="Button" Style="display: none" />
    <ajax:ModalPopupExtender runat="server" ID="ajxValidacion" OkControlID="btnCacelarValidacion" CancelControlID="btnCacelarValidacion"
        TargetControlID="btnMostrarModal" PopupControlID="modalValidacion" BackgroundCssClass="ajxBG">
    </ajax:ModalPopupExtender>
</asp:Content>
