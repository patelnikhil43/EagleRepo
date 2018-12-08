<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FindFriends.aspx.cs" Inherits="TermProjectSolution.FindFriends" %>

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
                        <li class="active"><a href="FindFriends.aspx">Find Friends</a></li>
                        <li><a href="FriendRequests.aspx">Friend Requests</a></li>
                        <li><a href="Messages.aspx">Messages</a></li>
                    </ul>
                </div>
            </div>
        </div>
    </nav>
    <form id="form1" runat="server">
        <div style="position:absolute; top:15px; right:15px; width: 8px;">
            <asp:Button ID="btnLogOut" CssClass="btnFB" runat="server" Text="Log Out" OnClick="btnLogOut_Click"/>
        </div>
        <div>
            <div id="searchContainer">
                <div class="row" style="margin-left: 2px;">
                    <div class="col-md-4">
                        <asp:Label ID="lblSearch" runat="server" Text="Search for a friend by name:"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <div class="col-md-4">
                            <asp:Button ID="btnSearch" CssClass="btnFB" runat="server" Text="Search" OnClick="btnSearch_Click" />
                        </div>
                        <div class="col-md-4">
                            <asp:Button ID="btnCancel" CssClass="btnFB" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                        </div>
                        <div class="col-md-4">
                            <asp:Button ID="btnAdvancedSearch" CssClass="btnFB" runat="server" Text="Advanced Search" OnClick="btnAdvancedSearch_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div id="advancedSearchContainer" runat="server" visible="false">
                <div class="row" style="margin-left: 2px;">
                    <div class="col-md-4">
                        <asp:Label ID="lblAdvancedSearch" runat="server" Text="Search for a friend using the following catergories:"></asp:Label>
                        <br />
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblState" runat="server" Text="State:"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtState" runat="server"></asp:TextBox>
                                <br />
                                <asp:Label ID="lblCity" runat="server" Text="City:"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                                <asp:Button ID="btnStateSearch" CssClass="btnFB" runat="server" Text="Search" OnClick="btnStateSearch_Click" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblOrganization" runat="server" Text="Organization:"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtOrganization" runat="server"></asp:TextBox>
                                <asp:Button ID="btnOrganizationSearch" CssClass="btnFB" runat="server" Text="Search" OnClick="btnOrganizationSearch_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div id="searchResults" style="margin-left: 15px;">
                <%--use a custom user control here?--%>
                <asp:GridView ID="gvSearchResults" runat="server" AutoGenerateColumns="false" Visible="False">
                    <Columns>
                        <asp:BoundField DataField="name" HeaderText="Name" />
                        <asp:BoundField DataField="email" HeaderText="Email" />
                        <asp:BoundField DataField="state" HeaderText="State" />
                        <asp:BoundField DataField="organization" HeaderText="Organization" />
                        <asp:TemplateField HeaderText="Profile Picture">
                            <ItemTemplate>
                                <asp:Image ID="imgProfilePic" CssClass="imgProfilePic" runat="server" src="https://upload.wikimedia.org/wikipedia/commons/9/93/Default_profile_picture_%28male%29_on_Facebook.jpg" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Send Request">
                            <ItemTemplate>
                                <asp:Button ID="btnSendRequest" runat="server" Text="Send Request" OnClick="btnSendRequest_Click" />
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
        </div>
    </form>
</body>
</html>
