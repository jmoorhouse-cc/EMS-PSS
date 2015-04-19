<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="TimeCard.aspx.cs" Inherits="EMS_PSS.TimeCard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <h3>Time Card</h3>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table id="selectEmpToAddTimeCard">
        <tr>
            <td>
                <table style="height: 187px; width: 300px">
                    <tr><td style="width: 94px">Search by: </td></tr>
                    <tr><td style="width: 94px; height: 30px;">First Name: </td>
                        <td style="width: 154px; height: 30px;"><asp:TextBox ID="fnameSearch" runat="server" style="margin-left: 0px" Width="175px" /></td></tr>
                    <tr><td style="width: 94px; height: 30px;">Last Name: </td>
                        <td style="width: 154px"><asp:TextBox ID="lnameSearch" runat="server" style="margin-left: 0px" Width="175px" /></td></tr>
                    <tr><td style="width: 94px; height: 30px;">SIN: </td>
                        <td style="width: 154px"><asp:TextBox ID="sinSearch" runat="server" style="margin-left: 0px" Width="175px" /></td></tr>
                    <tr><td style="width: 94px">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="searchSubmit_Click" /></td></tr>
                </table>
            </td>
            <td><table>
                <tr>
                    <asp:GridView ID="searchResultGrid" runat="server" ShowHeader="True" onrowcommand="GridView_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Actions">
                                <ItemTemplate>
                                    <asp:Button ID="selectBtn" runat="server" CommandName="Select" height="25px" Width="75px" 
                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Select" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Label ID="selectResultLabel" runat="server" Text="" ForeColor="Red" ></asp:Label>
                </tr>
            </table></td>
        </tr>
        <tr>
            <table>
                <tr>
                    <asp:GridView ID="searchFullResultGrid" runat="server" ShowHeader="True" onrowcommand="GridView_RowCommand" Width="616px" />
                </tr>
            </table>
        </tr>
    </table>
</asp:Content>

