<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PasswordRecovery.aspx.cs" Inherits="TermProjectSolution.PasswordRecovery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="passwordRecoveryStyles.css"
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="recoveryContainer">
            <asp:Label ID="lblPasswordRecovery" CssClass="lblPasswordRecovery" runat="server" Text="Password Recovery"></asp:Label>
            <br />
            <asp:Label ID="lblPrompt" runat="server" Text="Please enter your email below and click get questions to retrieve your security questions:"></asp:Label>
            <br />
            <br />
            <asp:Label ID="lblMessage" runat="server" Text="Email Address:"></asp:Label>
            <br />
            <asp:TextBox ID="txtEmail" CssClass="txtPasswordRecovery" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btnGetUser" CssClass="btnPasswordRecovery" runat="server" Text="Get Questions" OnClick="btnGetUser_Click" />
            <br />
            <br />
            <div id="securityQuestions">
                <asp:Label ID="lblSecurityQ1" runat="server" Text="Security Question 1: "></asp:Label>
                <br />
                <asp:TextBox ID="txtAnswer1" CssClass="txtPasswordRecovery" runat="server"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="lblSecurityQ2" runat="server" Text="Security Question 2: "></asp:Label>
                <br />
                <asp:TextBox ID="txtAnswer2" CssClass="txtPasswordRecovery" runat="server"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="lblSecurityQ3" runat="server" Text="Security Question 3: "></asp:Label>
                <br />
                <asp:TextBox ID="txtAnswer3" CssClass="txtPasswordRecovery" runat="server"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="btnSubmitQuestions" CssClass="btnPasswordRecovery" runat="server" Text="Submit Questions" OnClick="btnSubmitQuestions_Click" />
                <asp:Button ID="btnBackToLogin" CssClass="btnPasswordRecovery" runat="server" Text="Back to Login" OnClick="btnBackToLogin_Click" />
            </div>
        </div>
    </form>
</body>
</html>
