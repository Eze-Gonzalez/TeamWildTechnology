<%@ Page Title="" Language="C#" MasterPageFile="~/TWT.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AppWeb.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="panelLogin" runat="server">
                <section>
                    <div class="modalLogin">
                        <div class="userData">
                            <h2>Iniciar Sesión</h2>
                            <asp:Panel ID="panelEmail" runat="server">
                                <div class="input-box">
                                    <label>Email/Nombre de usuario</label>
                                    <asp:TextBox ID="txtEmail" CssClass="input" runat="server"></asp:TextBox>
                                </div>
                            </asp:Panel>
                            <%if (Validaciones.Validar.usuarioRegistrado(txtEmail.Text))
                                {  %>
                            <asp:Panel ID="panelUsuario" runat="server">
                                <div class="panelUsuario">
                                    <asp:Label ID="lblPerfil" runat="server" Text="Nombre"></asp:Label>
                                    <label>¿No es su cuenta?</label>
                                    <asp:LinkButton ID="lnkOtro" runat="server" OnClick="lnkOtro_Click">Cambiar Cuenta</asp:LinkButton>
                                </div>
                            </asp:Panel>
                            <div class="input-box">
                                <label>Contraseña</label>
                                <div>
                                    <asp:TextBox ID="txtPass" ClientIDMode="Static" TextMode="Password" CssClass="input" runat="server"></asp:TextBox>
                                    <button id="mostrar" onclick="mostrarPass('txtPass', 'icono')" type="button" class="boton-mostrar"><i id="icono" class="fa fa-eye-slash"></i></button>
                                </div>
                            </div>
                            <div class="forget">
                                <asp:LinkButton ID="lnkForget" runat="server" OnClick="lnkForget_Click">Olvidé mi contraseña</asp:LinkButton>
                            </div>
                            <%}
                            %>
                            <div class="modal-footer">
                                <%if (Status)
                                    {  %>
                                <asp:LinkButton ID="btnLogin" CssClass="btnNext" runat="server" OnClick="btnLogin_Click"><span class="btnText">INCIAR SESIÓN</span><span class="bgHover"></span></asp:LinkButton>
                                <%}
                                    else
                                    { %>
                                <asp:LinkButton ID="btnSiguiente" CssClass="btnNext" runat="server" OnClick="txtEmail_TextChanged"><span class="btnText">SIGUIENTE</span><span class="bgHover"></span></asp:LinkButton>
                                <%} %>
                                <a href="Default.aspx" class="btnCancelar"><span class="btnText">CANCELAR</span><span class="bgHover"></span></a>
                            </div>
                            <div class="register">
                                <p>¿No tiene cuenta?</p>
                                <a href="Register.aspx" class="ms-2">Registrese aquí</a>
                            </div>
                        </div>
                    </div>
                </section>
            </asp:Panel>
            <asp:Panel ID="panelForget" runat="server" Visible="false">
                <section>
                    <div class="modalLogin">
                        <div class="userData h70">
                            <h2>Cambiar Contraseña</h2>
                            <asp:Panel ID="panelDatos" runat="server">
                                <div class="input-box">
                                    <label for="">Email</label>
                                    <asp:TextBox ID="txtEmailForget" CssClass="input" runat="server"></asp:TextBox>
                                </div>
                                <div class="input-box">
                                    <label for="">Nombre</label>
                                    <asp:TextBox ID="txtNombre" CssClass="input" runat="server"></asp:TextBox>
                                </div>
                                <div class="input-box">
                                    <label for="">Apellido</label>
                                    <asp:TextBox ID="txtApellido" CssClass="input" runat="server"></asp:TextBox>
                                </div>
                                <div class="input-box">
                                    <label for="">Fecha de nacimiento</label>
                                    <asp:TextBox ID="txtFecha" TextMode="Date" CssClass="input" runat="server"></asp:TextBox>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="panelPassF" Visible="false" runat="server">
                                <div class="input-box">
                                    <asp:TextBox ID="txtPassForget" ClientIDMode="Static" TextMode="Password" CssClass="input" runat="server"></asp:TextBox>
                                    <label for="">Contraseña</label>
                                    <button id="mostrarF" onclick="mostrarPass('txtPassForget', 'iconoF')" type="button" class="boton-mostrar"><i id="iconoF" class="fa fa-eye-slash"></i></button>
                                </div>
                                <div class="input-box">
                                    <asp:TextBox ID="txtRepetirForget" ClientIDMode="Static" TextMode="Password" CssClass="input" runat="server"></asp:TextBox>
                                    <label for="">Contraseña</label>
                                    <button id="mostrarRF" onclick="mostrarPass('txtRepetirForget', 'iconoRF')" type="button" class="boton-mostrar"><i id="iconoRF" class="fa fa-eye-slash"></i></button>
                                </div>
                            </asp:Panel>
                            <div class="modal-footer">
                                <%if (StatusF)
                                    {  %>
                                <asp:LinkButton ID="btnCambiar" runat="server" CssClass="btnNext" OnClick="btnCambiar_Click"><span class="btnText">GUARDAR</span><span class="bgHover"></span></asp:LinkButton>
                                <%}
                                    else
                                    { %>
                                <asp:LinkButton ID="btnSiguienteF" runat="server" CssClass="btnNext" OnClick="btnSiguienteF_Click" OnClientClick="disableButtonR(this)"><span class="btnText">SIGUIENTE</span><span class="bgHover"></span></asp:LinkButton>
                                <%} %>
                                <asp:LinkButton ID="btnCancelarF" runat="server" CssClass="btnCancelar" OnClick="btnCancelar_Click"><span class="btnText">CANCELAR</span><span class="bgHover"></span></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </section>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="modalValidacion" CssClass="modalValidacion" runat="server">
        <div class="modalValidacion__Body">
            <p class="form-label">Se le ha enviado un código de validación al email, ingréselo a continuación</p>
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
                    <div class="modal-footer h10">
                        <asp:LinkButton ID="btnValidar" CssClass="btnNext" runat="server" OnClick="btnValidar_Click"><span class="btnText">VALIDAR</span><span class="bgHover"></span></asp:LinkButton>
                        <asp:LinkButton ID="btnCacelarValidacion" CssClass="btnCancelar" runat="server" OnClick="btnCacelarValidacion_Click"><span class="btnText">CANCELAR</span><span class="bgHover"></span></asp:LinkButton>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>
    <asp:Button ID="btnMostrarModal" runat="server" Text="Button" Style="display: none" />
    <ajax:ModalPopupExtender runat="server" ID="ajaxValidacion" OkControlID="btnCacelarValidacion" CancelControlID="btnCacelarValidacion"
        TargetControlID="btnMostrarModal" PopupControlID="modalValidacion" BackgroundCssClass="bg-black bg-opacity-75">
    </ajax:ModalPopupExtender>
</asp:Content>
