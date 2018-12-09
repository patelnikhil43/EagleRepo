<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Preferences.aspx.cs" Inherits="TermProjectSolution.Preferences" %>

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
                        <li class="active"><a href="Preferences.aspx">Preferences</a></li>
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
         <div id="PreferencesDiv" runat="server">
             <asp:Label runat="server" text="Preferences" ID="RegisterPreferencesLabel"></asp:Label>
            <br />
            <asp:Label runat="server" Text="Login Preference: " ID="LoginPreferenceLabel" />
            <asp:DropDownList ID="LoginPreferenceDropDown" ForeColor="Black" runat="server">
                  <asp:ListItem Text="None" Selected="True" Value="NONE"></asp:ListItem>
                  <asp:ListItem Text="Auto-Login" Value="Auto-Login"></asp:ListItem>
                  <asp:ListItem Text="Fast-Login"  Value="Fast-Login"></asp:ListItem>
            </asp:DropDownList>
            <br />
             <asp:Label runat="server" Text="Privacy Preference: " ID="PrivacyPreferenceLabel" />
            <asp:DropDownList ID="PrivacyPreferenceDropDown" ForeColor="Black" runat="server">
                  <asp:ListItem Text="Public" Selected="True" Value="Public"></asp:ListItem>
                  <asp:ListItem Text="Friends" Value="Friends"></asp:ListItem>
                  <asp:ListItem Text="Friends-Of-Friends"  Value="FOF"></asp:ListItem>
            </asp:DropDownList>
            <br />
             <asp:Label runat="server" Text="Photo Privacy Preference: " ID="PhotoPrivacyPreferenceLabel" />
            <asp:DropDownList ID="PhotoPrivacyDropDown" ForeColor="Black" runat="server">
                  <asp:ListItem Text="Public" Selected="True" Value="Public"></asp:ListItem>
                  <asp:ListItem Text="Friends" Value="Friends"></asp:ListItem>
                  <asp:ListItem Text="Friends-Of-Friends"  Value="FOF"></asp:ListItem>
            </asp:DropDownList>
             <br />
            <asp:Label runat="server" Text="Feed Privacy Preference: " ID="FeedPrivacyLabel" />
            <asp:DropDownList ID="FeedPrivacyDropDown" ForeColor="Black" runat="server">
                  <asp:ListItem Text="Public" Selected="True" Value="Public"></asp:ListItem>
                  <asp:ListItem Text="Friends" Value="Friends"></asp:ListItem>
                  <asp:ListItem Text="Friends-Of-Friends"  Value="FOF"></asp:ListItem>
            </asp:DropDownList>
            <br />
            <asp:Label runat="server" Text="Theme Preference: " ID="ThemePreferenceLabel" />
            <asp:DropDownList ID="ThemePreferenceDropDown" ForeColor="Black" runat="server">
                  <asp:ListItem Text="Default" Selected="True" Value="Default"></asp:ListItem>
                  <asp:ListItem Text="Light" Value="Light"></asp:ListItem>
                  <asp:ListItem Text="Dark"  Value="Dark"></asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            <asp:Button Text="Submit" CssClass="btnFB" ID="SubmitPreferencesButton" runat="server" OnClick="SubmitPreferencesButton_Click" />
        </div>
    </form>
</body>
</html>
