<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Preferences.aspx.cs" Inherits="TermProjectSolution.Preferences" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link rel="stylesheet" href="registrationStyles"/>
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
                <li><a href="#">Page 2</a></li>
                <li><a href="#">Preferences</a></li>
            </ul>
        </div>
    </nav>
    <form id="form1" runat="server">
         <div id="PreferencesDiv" runat="server">
             <asp:Label runat="server" text="Preferences" ID="RegisterPreferencesLabel"></asp:Label>
            <br />
            <asp:Label runat="server" Text="Login Preference: " ID="LoginPreferenceLabel" />
            <asp:DropDownList ID="LoginPreferenceDropDown" runat="server">
                  <asp:ListItem Text="None" Selected="True" Value="NONE"></asp:ListItem>
                  <asp:ListItem Text="Auto-Login" Value="Auto-Login"></asp:ListItem>
                  <asp:ListItem Text="Fast-Login"  Value="Fast-Login"></asp:ListItem>
            </asp:DropDownList>
            <br />
             <asp:Label runat="server" Text="Privacy Preference: " ID="PrivacyPreferenceLabel" />
            <asp:DropDownList ID="PrivacyPreferenceDropDown" runat="server">
                  <asp:ListItem Text="Public" Selected="True" Value="Public"></asp:ListItem>
                  <asp:ListItem Text="Friends" Value="Friends"></asp:ListItem>
                  <asp:ListItem Text="Friends-Of-Friends"  Value="FOF"></asp:ListItem>
            </asp:DropDownList>
            <br />
            <asp:Label runat="server" Text="Theme Preference: " ID="ThemePreferenceLabel" />
            <asp:DropDownList ID="ThemePreferenceDropDown" runat="server">
                  <asp:ListItem Text="Default" Selected="True" Value="Default"></asp:ListItem>
                  <asp:ListItem Text="Light" Value="Light"></asp:ListItem>
                  <asp:ListItem Text="Dark"  Value="Dark"></asp:ListItem>
            </asp:DropDownList>
            <br />
            <asp:Button Text="Submit" ID="SubmitPreferencesButton" runat="server" OnClick="SubmitPreferencesButton_Click" />
        </div>
    </form>
</body>
</html>
