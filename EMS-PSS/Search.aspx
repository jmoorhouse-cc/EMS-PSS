<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="EMS_PSS.Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <h3>Search</h3>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    Provide at least one of the following:
    <table>
        <tr>
            <td>
                First Name: 
            </td>
            <td>
                <asp:TextBox ID="tbxSearchfName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Last Name:
            </td>
            <td>
                <asp:TextBox ID="tbxSearchlName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                SIN Num:
            </td>
            <td>
                <asp:TextBox ID="tbxSearchsNum" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="searchSubmit_Click" />
</asp:Content>

