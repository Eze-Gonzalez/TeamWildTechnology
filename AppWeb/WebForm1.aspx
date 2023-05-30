<%@ Page Title="" Language="C#" MasterPageFile="~/TWT.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="AppWeb.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                        <asp:LinkButton ID="btnValidar" CssClass="btnNext" runat="server" ><span class="btnText">VALIDAR</span><span class="bgHover"></span></asp:LinkButton>
                        <asp:LinkButton ID="btnCacelarValidacion" CssClass="btnCancelar" runat="server" ><span class="btnText">CANCELAR</span><span class="bgHover"></span></asp:LinkButton>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>
</asp:Content>
