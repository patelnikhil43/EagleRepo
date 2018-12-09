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
        <div>
        <div id="FriendListDiv" class="threediv">
            <asp:GridView ID="FriendListGV" runat="server"  AutoGenerateColumns="False" OnRowCommand="FriendListGV_RowCommand" HorizontalAlign="Center">
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
            <asp:DropDownList ID="ChoosePostTypeDD" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ChoosePostTypeDD_SelectedIndexChanged">
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
           <br />
             <asp:Label ID="TagFriendsLabel" Text="Tag Friends?" runat="server" />
             <asp:DropDownList ID="TagFriendsDD" AutoPostBack="true" runat="server" OnSelectedIndexChanged="TagFriendsDD_SelectedIndexChanged">
                <asp:ListItem Text="Select" Value="default"></asp:ListItem>
                <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
            </asp:DropDownList>

           <asp:GridView runat="server" AutoGenerateColumns="false" ID="TagFriendsGV" HorizontalAlign="Center">
               <Columns>
                   <asp:TemplateField HeaderText="Select">
                       <ItemTemplate>
                           <asp:CheckBox ID="ImagePostTagCheckBox" runat="server" />
                       </ItemTemplate>
                   </asp:TemplateField>

                   <asp:BoundField DataField="name" HeaderText="Name" SortExpression="name" />
                   <asp:BoundField DataField="userEmail" HeaderText="Username" SortExpression="userEmail" />

               </Columns>
               
           </asp:GridView>

            </div>
          
            <br />
            <div id="TypeStatusPostDiv" runat="server" visible="false">
            <asp:Label Text="Caption:" ID="StatusPostCaptionLabel" runat="server" />
           <asp:TextBox ID="StatusPostCaptionTextBox" runat="server" Width="140" Height="80" />
                <br />
                <asp:Label ID="StatusPostTagLabel" Text="Tag Friends?" runat="server" />
             <asp:DropDownList ID="StatusPostTagDD" AutoPostBack="true" runat="server" OnSelectedIndexChanged="StatusPostTagDD_SelectedIndexChanged" >
                <asp:ListItem Text="Select" Value="default"></asp:ListItem>
                <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
            </asp:DropDownList>

           <asp:GridView runat="server" AutoGenerateColumns="false" ID="StatusPostTagGV" HorizontalAlign="Center">
               <Columns>
                   <asp:TemplateField HeaderText="Select">
                       <ItemTemplate>
                           <asp:CheckBox ID="StatusPostTagCheckBox" runat="server" />
                       </ItemTemplate>
                   </asp:TemplateField>

                   <asp:BoundField DataField="name" HeaderText="Name" SortExpression="name" />
                   <asp:BoundField DataField="userEmail" HeaderText="Username" SortExpression="userEmail" />

               </Columns>
               
           </asp:GridView>

            </div>


           <asp:Button Text="Post" ID="PostButton" runat="server" OnClick="PostButton_Click" />
         </div>

        <div  class="threediv">
            <h4>Photo Gallery</h4>
            <h5>Select an image to upload</h5>
            <asp:FileUpload ID="FileUploadImageGallery" runat="server" accept=".png, .jpeg, .jpg" />
            <br />
            <asp:Label Text="Caption" ID="ImageCollectionCaptionLabel" runat="server"></asp:Label>
            <asp:TextBox id="ImageCollectionCaptionTextBox" runat="server" />
            <br />
            <asp:Button Text="Save" ID="UploadImageGalleryButton" runat="server" OnClick="UploadImageGalleryButton_Click" /> 
      
            <br />
            <asp:Label ID="NoImagesLabel" Text="Sorry! No Images Available. Upload some!" runat="server" Visible="false" />

            <asp:GridView runat="server" ID="ImageGalleryGV" AutoGenerateColumns="False" OnRowCommand="ImageGalleryGV_RowCommand">
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
                     <asp:ButtonField Text="DELETE" />
                </Columns>

            </asp:GridView>
            <br />
            </div>
        </div>

        <div id="FeedDiv" style="clear:both">

        </div>
    </form>
</body>
</html>
