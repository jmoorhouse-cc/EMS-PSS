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
            <td><asp:Label ID="sucessLbl" ForeColor="Red" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>
                <div id="fulltimeInput" runat="server" visible="true">
                    <table>
                        <tr><td>First Name: </td><td><asp:TextBox ID="ftfName" runat="server"/></td>
                            <td><asp:Label ID="ftfNameError" ForeColor="Red" runat="server"></asp:Label></td>
                        </tr>
                        <tr><td>Last Name: </td><td><asp:TextBox ID="ftlName" runat="server"/></td>
                            <td><asp:Label ID="ftlNameError" ForeColor="Red" runat="server"></asp:Label></td>
                        </tr>
                        <tr><td>SIN: </td><td><asp:TextBox ID="ftSin" runat="server"/></td>
                            <td><asp:Label ID="ftSinError" ForeColor="Red" runat="server"></asp:Label></td>
                        </tr>
                        <tr><td>Date of Birth: </td><td><asp:TextBox ID="ftDOB" runat="server"/></td>
                            <td><asp:Label ID="ftDOBError" ForeColor="Red" runat="server"></asp:Label></td>
                        </tr>
                        <tr><td>Works at: </td><td><asp:DropDownList ID="ftCompany" runat="server"/></td>
                            <td><asp:Label ID="ftCompanyError" ForeColor="Red" runat="server"></asp:Label></td>
                        </tr>
                        <tr><td>Date of Hire: </td><td><asp:TextBox ID="ftDateHire" runat="server"/></td>
                            <td><asp:Label ID="ftDateHireError" ForeColor="Red" runat="server"></asp:Label></td>
                        </tr>
                        <tr><td>Date of Termination: </td><td><asp:TextBox ID="ftDateTerm" runat="server"/></td>
                            <td><asp:Label ID="ftDateTermError" ForeColor="Red" runat="server"></asp:Label></td>
                        </tr>
                        <tr><td>Salary: </td><td><asp:TextBox ID="ftSalary" runat="server"/></td>
                            <td><asp:Label ID="ftSalaryError" ForeColor="Red" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </div>

                <div id="parttimeInput" runat="server" visible="false">
                    <table>
                        <tr><td>First Name: </td><td><asp:TextBox ID="ptfName" runat="server"></asp:TextBox></td>
                            <td><asp:Label ID="ptfNameError" ForeColor="Red" runat="server"></asp:Label></td>
                        </tr>
                        <tr><td>Last Name: </td><td><asp:TextBox ID="ptlName" runat="server"></asp:TextBox></td>
                            <td><asp:Label ID="ptlNameError" ForeColor="Red" runat="server"></asp:Label></td>
                        </tr>
                        <tr><td>SIN: </td><td><asp:TextBox ID="ptSin" runat="server"/></td>
                            <td><asp:Label ID="ptSinError" ForeColor="Red" runat="server"></asp:Label></td>
                        </tr>
                        <tr><td>Date of Birth: </td><td><asp:TextBox ID="ptDOB" runat="server"/></td>
                            <td><asp:Label ID="ptDOBError" ForeColor="Red" runat="server"></asp:Label></td>
                            </tr>
                        <tr><td>Date of Hire: </td><td><asp:TextBox ID="ptDateHire" runat="server"/></td>
                            <td><asp:Label ID="ptDateHireError" ForeColor="Red" runat="server"></asp:Label></td>
                        </tr>
                        <tr><td>Works at: </td><td><asp:DropDownList ID="ptCompany" runat="server"/></td>
                            <td><asp:Label ID="ptCompanyError" ForeColor="Red" runat="server"></asp:Label></td>
                            </tr>
                        <tr><td>Date of Termination: </td><td><asp:TextBox ID="ptDateTerm" runat="server"/></td>
                            <td><asp:Label ID="ptDateTermError" ForeColor="Red" runat="server"></asp:Label></td>
                        </tr>
                        <tr><td>Hourly Rate: </td><td><asp:TextBox ID="ptWage" runat="server"/></td>
                            <td><asp:Label ID="ptWageError" ForeColor="Red" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </div>

                <div id="seasonalInput" runat="server" visible="false">
                    <table>
                        <tr><td>First Name: </td><td><asp:TextBox ID="slfName" runat="server"/></td>
                            <td><asp:Label ID="slfNameError" ForeColor="Red" runat="server"></asp:Label></td>
                        </tr>
                        <tr><td>Last Name: </td><td><asp:TextBox ID="sllName" runat="server"/></td>
                            <td><asp:Label ID="sllNameError" ForeColor="Red" runat="server"></asp:Label></td>
                        </tr>
                        <tr><td>SIN: </td><td><asp:TextBox ID="slSin" runat="server"/></td>
                            <td><asp:Label ID="slSinError" ForeColor="Red" runat="server"></asp:Label></td>
                        </tr>
                        <tr><td>Season: </td><td><asp:TextBox ID="slSeason" runat="server"/></td>
                            <td><asp:Label ID="slSeasonError" ForeColor="Red" runat="server"></asp:Label></td>
                        </tr>
                        <tr><td>Season Year: </td><td><asp:TextBox ID="slYear" runat="server"/></td>
                            <td><asp:Label ID="slYearError" ForeColor="Red" runat="server"></asp:Label></td>
                        </tr>
                        <tr><td>Piece Pay: </td><td><asp:TextBox ID="slPcPay" runat="server"/></td>
                            <td><asp:Label ID="slPcPayError" ForeColor="Red" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </div>

                <div id="contractInput" runat="server" visible="false">
                    <table>
                        <tr><td>Contract Company Name: </td><td><asp:DropDownList ID="ctName" runat="server"/></td>
                            <td><asp:Label ID="ctNameError" ForeColor="Red" runat="server"></asp:Label></td>
                        </tr>
                        <tr><td>BIN: </td><td><asp:TextBox ID="ctBin" runat="server"/></td>
                            <td><asp:Label ID="ctBinError" ForeColor="Red" runat="server"></asp:Label></td>
                        </tr>
                        </tr>
                        <tr><td>Date Start: </td><td><asp:TextBox ID="ctStart" runat="server"/></td>
                            <td><asp:Label ID="ctStartError" ForeColor="Red" runat="server"></asp:Label></td>
                        </tr>
                        <tr><td>Date End: </td><td><asp:TextBox ID="ctEnd" runat="server"/></td>
                            <td><asp:Label ID="ctEndError" ForeColor="Red" runat="server"></asp:Label></td>
                        </tr>
                        <tr><td>Contract Amount: </td><td><asp:TextBox ID="ctAmt" runat="server"/></td>
                            <td><asp:Label ID="ctAmtError" ForeColor="Red" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </div>
                <asp:Button ID="btnCreate" runat="server" Text="Create" OnClick="btnCreate_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
