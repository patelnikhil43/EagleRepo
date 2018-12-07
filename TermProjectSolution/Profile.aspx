<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="TermProjectSolution.Profile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link rel="stylesheet" href="fbStyles.css"/>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="Profile.css" type="text/css" />
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
                        <li class="active"><a href="Profile.aspx">My Profile</a></li>
                        <li><a href="Preferences.aspx">Preferences</a></li>
                        <li><a href="FindFriends.aspx">Find Friends</a></li>
                        <li><a href="FriendRequests.aspx">Friend Requests</a></li>
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
         <div id="UserNameDiv">
            <asp:Label ID="UserNameLabel" runat="server"></asp:Label>
        </div>
        <div id="UserProfileImageDiv">
            
            <asp:Image ID="UserProfileImage" runat="server" />
            <br />
            <asp:FileUpload ID="ProfileImageUpload" runat="server" accept=".png, .jpeg, .jpg" />
            <asp:Button Text="Upload" ID="ChangeUserProfileImageButton" runat="server" OnClick="ChangeUserProfileImageButton_Click" /> 
        </div>
        <div id="UserProfileInformation">
            <asp:Table ID="UserProfileTable" runat="server"> 
               
             </asp:Table>  
        </div>
        <div id="FriendListDiv">
            <asp:GridView ID="FriendListGV" runat="server"  AutoGenerateColumns="False" OnRowCommand="FriendListGV_RowCommand">
                <Columns>
                  <%--  <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Image ID="UserFriendImage" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                     <asp:TemplateField visible="false">
                        <ItemTemplate>
                            <asp:Label ID="friendEmailID" runat="server" Text='<%# Eval("userEmail") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:BoundField DataField="name" HeaderText="Friends List" SortExpression="name" />
                  
                  
                    <asp:ButtonField Text="View" />
                  
                  
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
