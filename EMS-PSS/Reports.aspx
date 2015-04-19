﻿<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="EMS_PSS.Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <h3>Admin</h3>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="options" runat="server">
        <table>
            <tr>
                <td>
                    Select a report to run: 
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButtonList ID="rblReports" runat="server">
                        <asp:ListItem Text="Seniority" Value="seniority"></asp:ListItem>
                        <asp:ListItem Text="WHW" Value="whw"></asp:ListItem>
                        <asp:ListItem Text="Payroll" Value="payroll"></asp:ListItem>
                        <asp:ListItem Text="Active Employee" Value="active"></asp:ListItem>
                        <asp:ListItem Text="Inactive Employee" Value="inactive"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnReport" runat="server" Text="Run Report" OnClick="btnReport_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
