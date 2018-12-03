<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="TermProjectSolution.Profile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="Profile.css" type="text/css" />
</head>
<body>
    <nav class="navbar navbar-default">
        <div class="container-fluid">
            <div class="navbar-header">
                <a class="navbar-brand" href="#">WebSiteName</a>
            </div>
            <ul class="nav navbar-nav">
                <li ><a href="#">Home</a></li>
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
                <li class="active"><a href="Profile.aspx">My Profile</a></li>
            </ul>
        </div>
    </nav>

    <form id="form1" runat="server">
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
            <asp:GridView ID="FriendListGV" runat="server">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Email" SortExpression="Name" Visible="false" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="Button1" runat="server" Text="Button" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="Button2" runat="server" Text="Button" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
