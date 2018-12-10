<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MessageConversation.aspx.cs" Inherits="TermProjectSolution.MessageConversation" %>

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
            <div class="col-md-4"></div>
            <div id="messageContainer" class="col-md-4">
                <asp:Label ID="lblSendMessage" runat="server" Font-Size="Large" Text="Type a message below!"></asp:Label>
                <br />
                <asp:Label ID="lblMessage" runat="server" Text="Message:"></asp:Label>
                <br />
                <asp:TextBox ID="txtMessage" CssClass="txtMessage" ForeColor="Black" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
                <div class="row">
                    <div class="col-md-4">
                        <asp:Button ID="btnSend" CssClass="btnFB" runat="server" Text="Send" OnClick="btnSend_Click" />
                    </div>
                    <div class="col-md-4">
                        <asp:Button ID="btnGetMessages" CssClass="btnFB" runat="server" Text="Get Messages" OnClick="btnGetMessages_Click" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
