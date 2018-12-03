<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FindFriends.aspx.cs" Inherits="TermProjectSolution.FindFriends" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="searchContainer">
                <asp:Label ID="lblSearch" runat="server" Text="Search for a friend:"></asp:Label>
                <br />
                <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
            <br />
            <div id="searchResults">
                <%--use a custom user control here?--%>
                <asp:GridView ID="gvSearchResults" runat="server" AutoGenerateColumns="false" Visible="False">
                    <Columns>
                        <asp:BoundField DataField="name" HeaderText="Name" />
                        <asp:BoundField DataField="email" HeaderText="Email" />
                        <asp:BoundField DataField="state" HeaderText="State" />
                        <asp:BoundField DataField="organization" HeaderText="Organization" />
                        <asp:TemplateField HeaderText="Profile Picture">
                            <ItemTemplate>
                                <asp:Image ID="imgProfilePic" runat="server" />
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
