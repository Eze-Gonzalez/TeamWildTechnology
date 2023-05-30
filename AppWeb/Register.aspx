<%@ Page Title="" Language="C#" MasterPageFile="~/TWT.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="AppWeb.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="modalLogin">
                <div class="userData">
                    <h2>Registrese</h2>
                    <%if (!StatusV || !StatusP)
                        {  %>
                    <div class="input-box">
                        <label>Email</label>
                        <asp:TextBox ID="txtEmail" CssClass="input" runat="server"></asp:TextBox>
                    </div>
                    <%if (StatusV)
                        {
                    %>
                    <div class="input-box">
                        <label>Nombre de usuario</label>
                        <asp:TextBox ID="txtUsuario" CssClass="input" runat="server"></asp:TextBox>
                    </div>
                    <div class="input-box">
                        <label>Contraseña</label>
                        <div>
                            <asp:TextBox ID="txtPass" ClientIDMode="Static" CssClass="input" TextMode="Password" runat="server" Visible="false"></asp:TextBox>
                            <button id="mostrar" onclick="mostrarPass('txtPass', 'icono')" type="button" class="boton-mostrar"><i id="icono" class="fa fa-eye-slash"></i></button>
                        </div>
                    </div>
                    <div class="input-box">
                        <label for="">Contraseña</label>
                        <div>
                            <asp:TextBox ID="txtRepetir" ClientIDMode="Static" TextMode="Password" CssClass="input" runat="server" Visible="false"></asp:TextBox>
                            <button id="mostrarR" onclick="mostrarPass('txtRepetir', 'iconoR')" type="button" class="boton-mostrar"><i id="iconoR" class="fa fa-eye-slash"></i></button>
                        </div>
                    </div>
                    <%}                                    %>
                    <%} %>
                    <%if (StatusP)
                        { %>
                    <div class="input-box">
                        <asp:TextBox ID="txtNombre" CssClass="input" runat="server"></asp:TextBox>
                        <label for="">Nombre</label>
                    </div>
                    <asp:Label ID="lblErrorNombre" runat="server" Text="" Visible="false" CssClass="danger"></asp:Label>
                    <div class="input-box">
                        <label>Apellido</label>
                        <asp:TextBox ID="txtApellido" CssClass="input" runat="server"></asp:TextBox>
                    </div>
                    <asp:Label ID="lblErrorApellido" runat="server" Text="input" Visible="false" CssClass="danger"></asp:Label>
                    <div class="input-box">
                        <label>Fecha de nacimiento</label>
                        <asp:TextBox ID="txtFecha" TextMode="Date" CssClass="input" runat="server"></asp:TextBox>
                    </div>
                    <asp:Label ID="lblErrorFecha" runat="server" Text="" Visible="false" CssClass="danger"></asp:Label>
                    <%} %>
                    <div class="modal-footer">
                        <%if (!Status || !StatusV || !StatusP)
                            {

                        %>
                        <asp:LinkButton ID="btnSiguiente" runat="server" CssClass="btnNext" OnClick="btnSiguiente_Click" OnClientClick="disableButtonR(this)"><span class="btnText">SIGUIENTE</span><span class="bgHover"></span></asp:LinkButton>
                        <%}
                            else if (Status && StatusV && StatusP)
                            {  %>
                        <asp:LinkButton ID="btnRegistrarse" runat="server" CssClass="btnNext" OnClick="btnRegistrarse_Click" OnClientClick="disableButtonR(this)"><span class="btnText">REGISTRARSE</span><span class="bgHover"></span></asp:LinkButton>
                        <%} %>
                        <a href="Default.aspx" class="btnCancelar"><span class="btnText">CANCELAR</span><span class="bgHover"></span></a>
                    </div>
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
                        <asp:LinkButton ID="btnCacelarValidacion" CssClass="btnCancelar" runat="server" OnClick="btnCancelar_Click"><span class="btnText">CANCELAR</span><span class="bgHover"></span></asp:LinkButton>
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
