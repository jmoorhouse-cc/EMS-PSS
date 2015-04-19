<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="EMS_PSS.Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <h3>Users</h3>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>
                Add new user
            </td>
        </tr>
        <tr>
            <td>
                User Name:
            </td>
            <td>
                <asp:TextBox ID="tbxUserName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                User Password:
            </td>
            <td>
                <asp:TextBox ID="tbxUserPw" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                First Name:
            </td>
            <td>
                <asp:TextBox ID="tbxUserfName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Last Name:
            </td>
            <td>
                <asp:TextBox ID="tbxUserlName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Security Level:
            </td>
            <td>
                <asp:TextBox ID="tbxUsersLevel" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnAddUser" runat="server" Text="Add User" OnClick="btnAddUser_Click" />
</asp:Content>
