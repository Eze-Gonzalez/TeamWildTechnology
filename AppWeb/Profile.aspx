<%@ Page Title="" Language="C#" MasterPageFile="~/TWT.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="AppWeb.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <div class="profile">
                <h3>Mi Perfil</h3>
                <asp:LinkButton ID="lnkEmail" runat="server" CssClass="acceso" OnClick="lnkEmail_Click">Email<i class="fa-solid fa-pen-to-square"></i></asp:LinkButton>
                <asp:LinkButton ID="lnkUser" runat="server" CssClass="acceso" OnClick="lnkUser_Click">Nombre de usuario<i class="fa-solid fa-pen-to-square"></i></asp:LinkButton>
                <asp:LinkButton ID="lnkPass" runat="server" CssClass="acceso" OnClick="lnkPass_Click">Contraseña<i class="fa-solid fa-pen-to-square"></i></asp:LinkButton>
                <asp:LinkButton ID="lnkDatos" runat="server" CssClass="acceso" OnClick="lnkDatos_Click">Datos personales<i class="fa-solid fa-pen-to-square"></i></asp:LinkButton>
                <asp:LinkButton ID="lnkTwoFactor" runat="server" CssClass="acceso" OnClick="lnkTwoFactor_Click">Verificación en dos pasos<i class="fa-solid fa-pen-to-square"></i></asp:LinkButton>
            </div>
</asp:Content>
