<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="EMS_PSS.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <h3>Home</h3>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div><asp:Table HorizontalAlign="Center" VerticalAlign="Center" style="text-align:Right;background-color:darkorange;font-size:x-large" runat="server" >
        <asp:TableRow>
            <asp:TableCell><asp:Label runat="server" Text="WELCOME TO EMS-PPS" ForeColor="#ffffff"/></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell><asp:Label runat="server" Text="By Kitvetsu Solution" ForeColor="#cc3300" /></asp:TableCell>
        </asp:TableRow>
    </asp:Table></div>
</asp:Content>
