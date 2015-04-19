<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="Companies.aspx.cs" Inherits="EMS_PSS.Maintenance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <h3>Companies</h3>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr><td><asp:Label runat="server" Text="Add New Company" Font-Size="30px"></asp:Label></td></tr>
        <tr><td>Company Name:</td>
            <td style="width: 217px"><asp:TextBox ID="tbxCmpyName" runat="server" Width="200px" /></td>
        </tr>
        <tr><asp:Button ID="btnAddCmpy" runat="server" Text="Add Company" OnClick="btnAddCmpy_Click" /></tr>
        <tr><asp:Label ID="cmpyAdditionResultLabel" runat="server" Text="" ForeColor="Red" /></tr>
    </table>
    <table>
        <tr>
            <asp:GridView ID="cmpyDisplayGrid" runat="server" ShowHeader="True" />
        </tr>
    </table>
    <asp:Label ID="cmpyDisplayResultLabel" runat="server" Text="" ForeColor="Red" ></asp:Label>
</asp:Content>
