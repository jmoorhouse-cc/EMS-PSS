<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="EMS_PSS.Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <h3>Search</h3>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>
                <asp:RadioButtonList ID="rblSearch" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="First Name" Value="fName"></asp:ListItem>
                    <asp:ListItem Text="Last Name" Value="lName"></asp:ListItem>
                    <asp:ListItem Text="SIN" Value="sin"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
</asp:Content>
