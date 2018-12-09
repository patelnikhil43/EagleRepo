<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Feed.aspx.cs" Inherits="TermProjectSolution.Feed" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Feed</title>
    <link rel="stylesheet" href="fbStyles.css"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link rel="stylesheet" type="text/css" href="FeedStyle.css" />
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
                        <li class="active"><a href="Feed.aspx">Feed</a></li>
                        <li><a href="Profile.aspx">My Profile</a></li>
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
            <asp:Button ID="btnLogOut" CssClass="btnFB" runat="server" Text="Log Out" OnClick="btnLogOut_Click" />
        </div>
       
             <div class="threediv">
            <h4>Post Content</h4>
            <br />
            <asp:DropDownList ID="ChoosePostTypeDD" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ChoosePostTypeDD_SelectedIndexChanged"  >
                <asp:ListItem Text="Choose Type:" Value="default"></asp:ListItem>
                <asp:ListItem Text="Photo Post" Value="PhotoPost"></asp:ListItem>
                <asp:ListItem Text="Status Post" Value="StatusPost"></asp:ListItem>
            </asp:DropDownList>
            <br />

            <div id="TypeImagePostDiv" runat="server" visible="false">
               
           
            <br />
                <h4>Select Image to Upload</h4>
            <asp:FileUpload ID="FileImageUpload" runat="server" accept=".png, .jpeg, .jpg" />
          
            <br />
           <asp:Label Text="Caption:" ID="ImageCaptionLabel" runat="server" />
           <asp:TextBox ID="ImageCaptionTextBox" runat="server" Width="140" Height="80" />
          </div>
           <br />
            <div id="TypeStatusPostDiv" runat="server" visible="false">
            <asp:Label Text="Caption:" ID="StatusPostCaptionLabel" runat="server" />
           <asp:TextBox ID="StatusPostCaptionTextBox" runat="server" Width="140" Height="80" />
                <br />
             </div>
            <asp:Button Text="Post" ID="PostButton" runat="server" OnClick="PostButton_Click" />
         </div>
        
    </form>
</body>
</html>
