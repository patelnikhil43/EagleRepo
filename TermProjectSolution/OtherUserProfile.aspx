<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OtherUserProfile.aspx.cs" Inherits="TermProjectSolution.OtherUserProfile" %>

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
                <li ><a href="Feed.aspx">Home</a></li>
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
                </div>
        <div id="UserProfileInformation">
            <asp:Table ID="UserProfileTable" runat="server"> 
               
             </asp:Table>  
        </div>

         <div id="FriendListDiv" class="threediv">

             <asp:Label Text="No Friends Found, Make Friends!" ID="NoFriendsLabel" runat="server" visible="false"/>

            <asp:GridView ID="FriendListGV" runat="server"  AutoGenerateColumns="False" OnRowCommand="FriendListGV_RowCommand" >
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

        <div class="threediv">
            <h4>Post Content</h4>
            <br />
            <asp:DropDownList ID="ChoosePostTypeDD" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ChoosePostTypeDD_SelectedIndexChanged" >
                <asp:ListItem Text="Choose Type:" Value="default"></asp:ListItem>
                <asp:ListItem Text="Photo Post" Value="PhotoPost"></asp:ListItem>
                <asp:ListItem Text="Status Post" Value="StatusPost"></asp:ListItem>
            </asp:DropDownList>
            <br />

            <div id="TypeImagePostDiv" runat="server" visible="false">
               
            <asp:Image ID="UserPostImage" runat="server" />
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
            <asp:Button Text="Post" ID="PostButton" runat="server" OnClick="PostButton_Click"  />
         </div>

        <div class="threediv">
            <asp:Label Text="No Images Available" ID="NoImagesLabel" runat="server" Visible="false"/>
             <asp:GridView runat="server" ID="ImageGalleryGV" AutoGenerateColumns="False">
                <Columns>
                   
                     <asp:TemplateField HeaderText="Your Images">
                                    <ItemTemplate>
                                        <asp:Image ID="GalleryCollectionImages" runat="server" Height="150px" Width="150px"/>
                                    </ItemTemplate>
                     </asp:TemplateField>

                    <asp:BoundField DataField="caption" HeaderText="Caption" SortExpression="caption" />
                     <asp:TemplateField visible="false">
                        <ItemTemplate>
                            <asp:Label ID="GalleryImageID" runat="server" Text='<%# Eval("ImageID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField visible="false">
                        <ItemTemplate>
                            <asp:Label ID="GalleryImageURL" runat="server" Text='<%# Eval("ImageURL") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                     
                </Columns>

            </asp:GridView>
        </div>
         <div id="FeedDiv" style="clear: both">

        </div>
        <div>
            <asp:Label id="NoFeedLabel" Text="No Feed Available" runat="server" Visible="false" />
        </div>
    </form>
</body>
</html>
