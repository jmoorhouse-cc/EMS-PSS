<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="EMS_PSS.Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <h3>Users</h3>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr><td><asp:Label runat="server" Text="Add New User" Font-Size="30px"></asp:Label></td></tr>
        <tr><td>User Name:</td>
            <td style="width: 217px"><asp:TextBox ID="tbxUserName" runat="server" Width="200px" /></td>
        </tr>
        <tr><td style="height: 29px">User Password:</td>
            <td style="width: 217px; height: 29px;"><asp:TextBox ID="tbxUserPw" runat="server" Width="200px" /></td>
        </tr>
        <tr><td>First Name:</td>
            <td style="width: 217px"><asp:TextBox ID="tbxUserfName" runat="server" Width="200px" /></td>
        </tr>
        <tr><td>Last Name:</td>
            <td style="width: 217px"><asp:TextBox ID="tbxUserlName" runat="server" Width="200px" /></td>
        </tr>
        <tr><td>Security Level:</td>
            <td style="width: 217px"><asp:TextBox ID="tbxUsersLevel" runat="server" Width="200px" /></td>
        </tr>
        <tr><asp:Button ID="btnAddUser" runat="server" Text="Add User" OnClick="btnAddUser_Click" /></tr>
        <tr><asp:Label ID="userAdditionResultLabel" runat="server" Text="" ForeColor="Red" /></tr>
    </table>
</asp:Content>
