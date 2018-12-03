<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="TermProjectSolution.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="RegisterUserDetails" runat="server">
            <asp:Label runat="server" text="Name*" ID="RegisterNameLabel"></asp:Label>
            <asp:TextBox runat="server" placeholder="Nikhil" id="RegisterNameTxtBox"></asp:TextBox>
            <br />
            <asp:Label runat="server" text="Address*" ID="ResgisterAddressLabel"></asp:Label>
            <asp:TextBox runat="server" placeholder="123 Lit Street" id="RegisterAddressTxtBox"></asp:TextBox>
            <br />
            <asp:Label runat="server" text="City*" ID="CityLabel"></asp:Label>
            <asp:TextBox runat="server" placeholder="New York" id="CityTxtBox"></asp:TextBox>
            <br />
            <asp:Label runat="server" text="Zip*" ID="ZipLabel"></asp:Label>
            <asp:TextBox runat="server" placeholder="19122" id="ZipTxtBox"></asp:TextBox>
            <br />
            <asp:Label runat="server" text="Email*" ID="RegisterEmailLabel"></asp:Label>
            <asp:TextBox runat="server" placeholder="example@server.com" id="RegisterEmailTxtBox"></asp:TextBox>
            <br />
             <asp:Label runat="server" text="Password*" ID="RegisterPasswordLabel"></asp:Label>
            <asp:TextBox runat="server" TextMode="Password" id="RegisterPasswordTxtBox"></asp:TextBox>
            <br />
             <asp:Label runat="server" text="Confirm Password*" ID="RegisterCPasswordLabel"></asp:Label>
            <asp:TextBox runat="server" TextMode="Password" id="RegisterCPasswordTxtBox"></asp:TextBox>
            <br />
            <asp:Button Text="Register" ID="RegisterUserButton" runat="server" OnClick="RegisterUserButton_Click" />
        </div>

        <div runat="server" visible="false" id="PrivacyQuestionsDiv">
            <asp:Label runat="server" Text="Security Question 1: " ID="PrivacyLabel1"></asp:Label>
            <asp:TextBox runat="server" ID="PrivacyQ1TxtBox" placeholder="What is my pet name?"></asp:TextBox>
            <br />
            <asp:Label runat="server" Text="Answer: " ID="PrivacyA1Label"></asp:Label>
            <asp:TextBox runat="server" ID="PrivacyA1TxtBox" placeholder="Tommy"></asp:TextBox>
            <br />
            <asp:Label runat="server" Text="Security Question 2: " ID="PrivacyLabel2"></asp:Label>
            <asp:TextBox runat="server" ID="PrivacyQ2TxtBox" placeholder="What is the street you grew up on?"></asp:TextBox>
            <br />
            <asp:Label runat="server" Text="Answer: " ID="PrivacyA2Label"></asp:Label>
            <asp:TextBox runat="server" ID="PrivacyA2TxtBox" placeholder="Cecil B Moore"></asp:TextBox>
            <br />
            <asp:Label runat="server" Text="Security Question 3: " ID="PrivacyLabel3"></asp:Label>
            <asp:TextBox runat="server" ID="PrivacyQ3TxtBox" placeholder="Name of your best friend?"></asp:TextBox>
            <br />
            <asp:Label runat="server" Text="Answer: " ID="PrivacyA3Label"></asp:Label>
            <asp:TextBox runat="server" ID="PrivacyA3TxtBox" placeholder="Nick"></asp:TextBox>
            <br />
            <asp:Button runat="server" ID="SecurityButton" Text="Submit" OnClick="SecurityButton_Click" />
        </div>

        <div id="PreferencesDiv" runat="server" visible="false">
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
