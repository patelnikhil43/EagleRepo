﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FriendRequests.aspx.cs" Inherits="TermProjectSolution.FriendRequests" %>

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
                        <li class="active"><a href="FriendRequests.aspx">Friend Requests</a></li>
                        <li><a href="Messages.aspx">Messages</a></li>
                    </ul>
                </div>
            </div>
        </div>
    </nav>
    <form id="form1" runat="server">
        <div style="position:absolute; top:15px; right:15px;">
            <asp:Button ID="btnLogOut" CssClass="btnFB" runat="server" Text="Log Out" OnClick="btnLogOut_Click"/>
        </div>
        <div class="row" style="padding: 10px;">
            <div class="col-md-3">
                <asp:Label ID="lblTitle" CssClass="lblLoginPrompt" runat="server" Text="Friend Requests"></asp:Label>
                <br />
                <asp:Button ID="btnRefresh" CssClass="btnFB" runat="server" Text="Refresh" OnClick="btnRefresh_Click" />
            </div>
            <div id="friendRequestContainer" class="col-md-9">
                <asp:Label ID="lblMessage" CssClass="lblLoginPrompt" runat="server" Text=""></asp:Label>
                <asp:GridView ID="gvFriendRequests" CssClass="gvFB" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="userEmail" HeaderText="Email" />
                        <asp:BoundField DataField="requestDate" HeaderText="Request Date" />
                        <asp:TemplateField HeaderText="Accept Request">
                            <ItemTemplate>
                                <asp:Button ID="btnAcceptRequest" CssClass="btnFB" runat="server" Text="Accept" OnClick="btnAcceptRequest_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reject Request">
                            <ItemTemplate>
                                <asp:Button ID="btnRejectRequest" CssClass="btnFB" runat="server" Text="Reject" OnClick="btnRejectRequest_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
