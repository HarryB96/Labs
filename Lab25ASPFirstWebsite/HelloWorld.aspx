<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HelloWorld.aspx.cs" Inherits="Lab25ASPFirstWebsite.HelloWorld" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID ="Label1" runat ="server">Label</asp:Label>
    <asp:Button ID="Button" runat ="server" Text="Click Me" OnClick ="Button_Click" />
</asp:Content>
