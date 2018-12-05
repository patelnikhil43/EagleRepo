<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Messages.aspx.cs" Inherits="TermProjectSolution.Messages" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</head>
<body>
    <nav class="navbar navbar-default">
        <div class="container-fluid">
            <div class="navbar-header">
                <a class="navbar-brand" href="#">WebSiteName</a>
            </div>
            <ul class="nav navbar-nav">
                <li class="active"><a href="#">Home</a></li>
                <li class="dropdown"><a class="dropdown-toggle" data-toggle="dropdown" href="#">Page 1 <span class="caret"></span></a>
                    <ul class="dropdown-menu">
                        <li><a href="#">Page 1-1</a></li>
                        <li><a href="#">Page 1-2</a></li>
                        <li><a href="#">Page 1-3</a></li>
                    </ul>
                </li>
                <li><a href="FindFriends.aspx">Find Friends</a></li>
                <li><a href="Preferences.aspx">Preferences</a></li>
                <li><a href="FriendRequests.aspx">Friend Requests</a></li>
                <li><a href="Messages.aspx">Messages</a></li>
                <li><a href="Profile.aspx">My Profile</a></li>
            </ul>
        </div>
    </nav>
    <form id="form1" runat="server">
        <div id="friendsContainer">
                <asp:GridView ID="gvFriendsOnline" runat="server" AutoGenerateColumns="false" Visible="False">
                    <Columns>
                        <asp:BoundField DataField="name" HeaderText="Name" />
                        <asp:BoundField DataField="friendEmail" HeaderText="Email" />
                        <asp:BoundField DataField="state" HeaderText="State" />
                        <asp:BoundField DataField="organization" HeaderText="Organization" />
                        <asp:TemplateField HeaderText="Profile Picture">
                            <ItemTemplate>
                                <asp:Image ID="imgProfilePic" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Send Message">
                            <ItemTemplate>
                                <asp:Button ID="btnSendMessage" runat="server" Text="Send Message" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="View Profile">
                            <ItemTemplate>
                                <asp:Button ID="btnViewProfile" runat="server" Text="View Profile" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
        </div>
        <div id="messageContainer">
            <asp:Label ID="lblSendMessage" runat="server" Text="Type a friend's email adress and send them a message!"></asp:Label>
            <br />
            <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
            <br />
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="lblMessage" runat="server" Text="Message:"></asp:Label>
            <br />
            <asp:TextBox ID="txtMessage" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="btnSend" runat="server" Text="Send" OnClick="btnSend_Click" />
            <br />
            <asp:Button ID="btnGetMessages" runat="server" Text="Get Messages" OnClick="btnGetMessages_Click" />
            <br />
            <asp:Button ID="btnGetFriendsOnline" runat="server" Text="View Friends Online" OnClick="btnGetFriendsOnline_Click" />
            <br />
        </div>
    </form>
</body>
</html>
