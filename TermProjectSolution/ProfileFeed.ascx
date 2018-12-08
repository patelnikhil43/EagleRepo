<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfileFeed.ascx.cs" Inherits="TermProjectSolution.ProfileFeed" %>

<style>
    #messageBox {
        background-color: dodgerblue;
        color: white;
        border-radius: 5px;
        padding: 5px;
        box-sizing: border-box;
        width: 80%;
        margin: auto;
        border: 1px solid grey;
    }
    .btnDelete{
        background-color: red;
        color: white;
        border-color: red;
        border-radius: 5px;
    }
</style>

<div id="postBox">
    <asp:Label ID="lblPoster" Text="" runat="server"></asp:Label>
    <br />
    <br />
    <asp:Image ID="imgPicture" runat="server" />
    <br />
    <div style="float: left; width: 85%;">
        <asp:Label ID="lblPostCaption" runat="server"></asp:Label>
    </div>
    <div style="overflow: auto;"></div>
    <div style="float: right;">
        <asp:Label ID="lblPostDate" runat="server"></asp:Label>
    </div>
    <div style="clear: left;"></div>
    <asp:Label ID="lblPostID" runat="server" Visible="false"></asp:Label>
</div>