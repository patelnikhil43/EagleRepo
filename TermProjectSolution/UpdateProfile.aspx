<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateProfile.aspx.cs" Inherits="TermProjectSolution.UpdateProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link rel="stylesheet" href="fbStyles.css"/>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</head>
<body>
    <nav class="navbar navbar-default">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-11">
                    <div class="navbar-header">
                        <a class="navbar-brand" href="#">Fakebook</a>
                    </div>
                    <ul class="nav navbar-nav">
                        <li><a href="Feed.aspx">Feed</a></li>
                        <li><a href="Profile.aspx">My Profile</a></li>
                        <li><a href="Preferences.aspx">Preferences</a></li>
                        <li><a href="FindFriends.aspx">Find Friends</a></li>
                        <li><a href="FriendRequests.aspx">Friend Requests</a></li>
                        <li class="active"><a href="Messages.aspx">Messages</a></li>
                    </ul>
                </div>
            </div>
        </div>
    </nav>
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-md-4">
                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
                <br />
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
                <br />
                <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="lblCity" runat="server" Text="City"></asp:Label>
                <br />
                <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="lblZip" runat="server" Text="Zip"></asp:Label>
                <br />
                <asp:TextBox ID="txtZip" runat="server"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="btnUpdate" CssClass="btnFB" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                <br />
                <asp:Button ID="btnCancel" CssClass="btnFB" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>
    </form>
</body>
</html>
