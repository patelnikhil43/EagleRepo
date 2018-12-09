<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfileFeed.ascx.cs" Inherits="TermProjectSolution.ProfileFeed" %>

<style>
    #postBox{
        text-align: center;
        background-color: cornflowerblue;
        margin-top: 10px;
        width: auto;
    }
</style>


<div id="postBox">
    <asp:Label ID="lblPoster" Text="" runat="server"></asp:Label>

    <br />
    <asp:Label ID="lblUserName" Text="" runat="server"></asp:Label>

    <br />
    <asp:Image ID="imgPicture" runat="server" style="width: 250px; height: 250px;"/>
    <br />

    <asp:Label ID="lblPostCaption" runat="server"></asp:Label>
    <br />


    <asp:Label ID="lblPostDate" runat="server"></asp:Label>
    <br />
    <asp:Label ID="lblPostID" runat="server"></asp:Label>
</div>
