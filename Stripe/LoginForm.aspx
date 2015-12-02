<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginForm.aspx.cs" Inherits="Stripe.LoginForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Username<asp:TextBox runat="server" ID="Username"></asp:TextBox>
            Password<asp:TextBox runat="server" ID="Password"></asp:TextBox>
            <asp:Button Text="Login" OnClick="Unnamed_Click" runat="server" />
        </div>
    </form>
</body>
</html>
