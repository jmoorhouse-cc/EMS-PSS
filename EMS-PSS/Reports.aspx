<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="EMS_PSS.Reports" %>
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
                    <asp:RadioButtonList ID="rblReports" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblReports_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Text="Seniority" Value="seniority"></asp:ListItem>
                        <asp:ListItem Text="Weekly Hours Worked" Value="whw"></asp:ListItem>
                        <asp:ListItem Text="Payroll" Value="payroll"></asp:ListItem>
                        <asp:ListItem Text="Active Employee" Value="active"></asp:ListItem>
                        <asp:ListItem Text="Inactive Employee" Value="inactive"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>Company: <asp:DropDownList ID="ftCompany" runat="server" /><asp:Label ID="datein" runat="server" Visible="false">Date of week: </asp:Label><asp:TextBox ID="specifiedWeek" runat="server" Visible="false"></asp:TextBox><asp:Label ID="dateError" runat="server" ForeColor="Red"></asp:Label></td>  
            </tr>
            <tr>    
                <td>
                    <asp:Button ID="btnReport" runat="server" Text="Run Report" OnClick="btnReport_Click" />
                </td>
            </tr>
            <asp:PlaceHolder ID="result" runat="server">
            <tr><td><asp:PlaceHolder ID="ftHeader" runat="server"><b>Full-Time Employee</b></asp:PlaceHolder></td></tr>
            <tr>
                <td>
                    <asp:Label ID="resultLabel1" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="resultGrid1" runat="server" ShowHeader="True"/>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="ptHeader" runat="server"><b>Part-time Employee</b></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="resultLabel2" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="resultGrid2" runat="server" ShowHeader="True"/>
                </td>
            </tr>

            <tr><td><asp:Label ID="slHeader" runat="server"><b>Seasonal Employee</b></asp:Label></td></tr>
            <tr>
                <td>
                    <asp:Label ID="resultLabel3" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>

                    <asp:GridView ID="resultGrid3" runat="server" ShowHeader="True"/>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="genDate" runat="server" Visible="false"></asp:Label><br />
                        &nbsp&nbsp&nbsp&nbsp&nbsp <asp:Label ID="runUser" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
            </asp:PlaceHolder>
        </table>
    </div>
    <div id="divReport" runat="server">
            
    </div>
</asp:Content>
