<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="Audit.aspx.cs" Inherits="EMS_PSS.Audit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <h3>Audit</h3>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>
            Audit Information:
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="selectResultLabel" runat="server" Text="Label" Visible="false"></asp:Label>
                <asp:GridView ID="searchFullResultGrid" runat="server" ShowHeader="True" Visible="False" />
            </td>
        </tr>
    </table>
</asp:Content>
