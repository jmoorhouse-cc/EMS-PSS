<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="EMS_PSS.Create" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <h3>Create</h3>
</asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <table runat="server">
        <tr>
            <td>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem Text="Fulltime" Value="fulltime" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Parttime" Value="parttime"></asp:ListItem>
                    <asp:ListItem Text="Seasonal" Value="seasonal"></asp:ListItem>
                    <asp:ListItem Text="Contract" Value="contract"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                <div id="fulltimeInput" runat="server" visible="true">
                    <table>
                        <tr>
                            <td>First Name: </td>
                            <td><asp:TextBox ID="ftfName" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Last Name: </td>
                            <td><asp:TextBox ID="ftlName" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>SIN: </td>
                            <td><asp:TextBox ID="ftSin" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Date of Hire: </td>
                            <td><asp:TextBox ID="ftDateHire" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Date of Termination: </td>
                            <td><asp:TextBox ID="ftDateTerm" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Salary: </td>
                            <td><asp:TextBox ID="ftSalary" runat="server"></asp:TextBox></td>
                        </tr>
                    </table>
                </div>

                <div id="parttimeInput" runat="server" visible="false">
                    <table>
                        <tr>
                            <td>First Name: </td>
                            <td><asp:TextBox ID="ptfName" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Last Name: </td>
                            <td><asp:TextBox ID="ptLname" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>SIN: </td>
                            <td><asp:TextBox ID="ptSin" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Date of Hire: </td>
                            <td><asp:TextBox ID="ptDateHire" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Date of Termination: </td>
                            <td><asp:TextBox ID="ptDateTerm" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Wage: </td>
                            <td><asp:TextBox ID="ptWage" runat="server"></asp:TextBox></td>
                        </tr>
                    </table>
                </div>

                <div id="seasonalInput" runat="server" visible="false">
                    <table>
                        <tr>
                            <td>First Name: </td>
                            <td><asp:TextBox ID="slfName" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Last Name: </td>
                            <td><asp:TextBox ID="slLname" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>SIN: </td>
                            <td><asp:TextBox ID="slSin" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Season: </td>
                            <td><asp:TextBox ID="slSeason" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Season Year: </td>
                            <td><asp:TextBox ID="slYear" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Piece Pay: </td>
                            <td><asp:TextBox ID="slPcPay" runat="server"></asp:TextBox></td>
                        </tr>
                    </table>
                </div>

                <div id="contractInput" runat="server" visible="false">
                    <table>
                        <tr>
                            <td>Contract Company Name: </td>
                            <td><asp:TextBox ID="ctName" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>BIN: </td>
                            <td><asp:TextBox ID="ctBin" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Date Start: </td>
                            <td><asp:TextBox ID="ctStart" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Date End: </td>
                            <td><asp:TextBox ID="ctEnd" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Contract Amount: </td>
                            <td><asp:TextBox ID="ctAmt" runat="server"></asp:TextBox></td>
                        </tr>
                    </table>
                </div>
                <asp:Button ID="btnCreate" runat="server" Text="Create" OnClick="btnCreate_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
