<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageBox.ascx.cs" Inherits="TermProjectSolution.MessageBox" %>

<style>
    #messageContainer {
        background-color: dodgerblue;
        color: white;
        border-radius: 5px;
        padding: 5px;
        box-sizing: border-box;
        margin-top: 5px;
    }
    .btnDelete{
        background-color: red;
        color: white;
        border-color: red;
        border-radius: 5px;
    }
</style>

<div id="messageContainer">
    <div style="float: right;">
        <asp:Button ID="btnDelete" CssClass="btnDelete" runat="server" Text="X" OnClick="btnDelete_Click" />
    </div>
    <div style="float: none;"></div>
    <br />
    <asp:Label ID="lblSender" runat="server"></asp:Label>
    <br />
    <div style="float: left; width: 85%;">
        <asp:Label ID="lblMessageBody" runat="server"></asp:Label>
    </div>
    <div style="overflow: auto;"></div>
    <div style="float: right;">
        <asp:Label ID="lblMessageDate" runat="server"></asp:Label>
    </div>
    <div style="clear: left;"></div>
    <asp:Label ID="lblMessageID" runat="server" Visible="false"></asp:Label>
</div>
