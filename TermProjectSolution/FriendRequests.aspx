<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FriendRequests.aspx.cs" Inherits="TermProjectSolution.FriendRequests" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="friendRequestContainer">
                <%--use a custom user control here?--%>
                <asp:Label ID="lblMessage" CssClass="lblLoginPrompt" runat="server" Text=""></asp:Label>
                <asp:GridView ID="gvFriendRequests" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="userEmail" HeaderText="Email" />
                        <asp:BoundField DataField="requestDate" HeaderText="Request Date" />
                        <asp:TemplateField HeaderText="Accept Request">
                            <ItemTemplate>
                                <asp:Button ID="btnAcceptRequest" runat="server" Text="Accept" OnClick="btnAcceptRequest_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reject Request">
                            <ItemTemplate>
                                <asp:Button ID="btnRejectRequest" runat="server" Text="Reject" OnClick="btnRejectRequest_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
