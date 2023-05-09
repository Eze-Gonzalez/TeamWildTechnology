<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="AppWeb.Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Error</title>
    <link href="Contenido/CSS/Estilos.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <section>
            <div>
                <asp:Label ID="lblErrorCode" runat="server" Text="Label"></asp:Label>
            </div>
            <div>
                <asp:Label ID="lblError" runat="server" Text="Label"></asp:Label>
            </div>
        </section>
    </form>
</body>
</html>
