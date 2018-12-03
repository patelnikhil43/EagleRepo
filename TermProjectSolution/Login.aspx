<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TermProjectSolution.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="loginStyles.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="SignInDiv">
            <%--<asp:Label runat="server" ID="UserNameLabel">User Name:</asp:Label>
            <asp:TextBox runat="server" placeholder="User Name" ID="UserNameTxtBox"></asp:TextBox>
            <br />
            <asp:Label runat="server" ID="UserPassword">Password:</asp:Label>
            <asp:TextBox runat="server" placeholder="Password" ID="UserPasswordTxtBox"></asp:TextBox>
            <br />--%>
            
            <asp:Label ID="lblLoginPrompt" CssClass="lblLoginPrompt" runat="server" Text="Fakebook Login"></asp:Label>
            <br />
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="lblEmail" runat="server" Text="Email Address"></asp:Label>
            <br />
            <asp:TextBox ID="txtEmail" CssClass="txtLogin" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
            <br />
            <asp:TextBox ID="txtPassword" CssClass="txtLogin" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btnLogin" CssClass="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
            <br />
            <hr />
            <br />
            <asp:HyperLink ID="ForgetPasswordHyperLink" NavigateUrl="PasswordRecovery.aspx" Text="Forgot your password?" runat="server"></asp:HyperLink>
            <br />
            <br />
            <asp:HyperLink ID="RegisterHyperLink" NavigateUrl="Registration.aspx" Text="Create an account" runat="server"></asp:HyperLink>
            <br />
            <br />
            <asp:Label ID="lblLoginMode" runat="server" Text="Login Mode"></asp:Label>
            <br />
            <asp:RadioButton ID="rdoNormalLogin" GroupName="loginMode" Text="Normal" runat="server" />
            <asp:RadioButton ID="rdoFastLogin" GroupName="loginMode" Text="Remember Username" runat="server" />
            <asp:RadioButton ID="rdoAutoLogin" GroupName="loginMode" Text="Auto Login" runat="server" />
        </div>
    </form>

</body>
</html>
