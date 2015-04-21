<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="Modify.aspx.cs" Inherits="EMS_PSS.Modify" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <h3>Modify</h3>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="height: 187px; width: 800px">
        <tr><td style="width: 104px">Search by: </td></tr>
        <tr><td style="width: 104px; height: 30px;">First Name: </td>
            <td style="width: 400px"><asp:TextBox ID="fnameSearch" runat="server" style="margin-left: 0px" Width="390px" /></td></tr>
        <tr><td style="width: 104px; height: 30px;">Last Name: </td>
            <td style="width: 400px"><asp:TextBox ID="lnameSearch" runat="server" style="margin-left: 0px" Width="390px" /></td></tr>
        <tr><td style="width: 104px; height: 30px;">SIN: </td>
            <td style="width: 400px"><asp:TextBox ID="sinSearch" runat="server" style="margin-left: 0px" Width="390px" /></td></tr>
        <tr><td style="width: 104px">
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="searchSubmit_Click" /></td></tr>
    </table>
    <table>
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
        </tr>
    </table>
    <asp:Label ID="selectResultLabel" runat="server" Text="" ForeColor="Red" ></asp:Label>
    <table>
            <asp:GridView ID="searchFullResultGrid" runat="server" ShowHeader="True" onrowcommand="GridView_RowCommand" />
    </table>

    <table runat="server">
        
        <tr>
            <td>
                <div id="fulltimeInput" runat="server" visible="false">
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
                        <tr><td>Date of Birth: </td><td><asp:TextBox ID="slDOB" runat="server"/></td>
                            <td><asp:Label ID="slDOBError" ForeColor="Red" runat="server"></asp:Label></td>
                            </tr>
                        <tr><td>Season: </td><td><asp:DropDownList ID="slSeason" runat="server">
                            <asp:ListItem Value="FALL">Fall</asp:ListItem>
                            <asp:ListItem Value="WINTER">Winter</asp:ListItem>
                            <asp:ListItem Value="SPRING">Spring</asp:ListItem>
                            <asp:ListItem Value="SUMMER">Summer</asp:ListItem>
                            </asp:DropDownList>
                            </td>
                            <td><asp:Label ID="slSeasonError" ForeColor="Red" runat="server"></asp:Label></td>
                        </tr>
                            
                        <tr><td>Works at: </td><td><asp:DropDownList ID="slCompany" runat="server"/></td>
                            <td><asp:Label ID="slCompanyError" ForeColor="Red" runat="server"></asp:Label></td>
                            </tr>
                        <tr><td>Date Start: </td><td><asp:TextBox ID="sldateStart" runat="server"/></td>
                            <td><asp:Label ID="slDateStartError" ForeColor="Red" runat="server"></asp:Label></td>
                            
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
                        <tr><td>Contract Company Name: </td><td><asp:TextBox ID="ctName" runat="server"/></td>
                            <td><asp:Label ID="ctNameError" ForeColor="Red" runat="server"></asp:Label></td>
                        </tr>
                        <tr><td>BIN: </td><td><asp:TextBox ID="ctBin" runat="server"/></td>
                            <td><asp:Label ID="ctBinError" ForeColor="Red" runat="server"></asp:Label></td>
                        </tr>
                        <tr><td>Works at: </td><td><asp:DropDownList ID="ctCompany" runat="server"/></td>
                            <td><asp:Label ID="ctCompanyError" ForeColor="Red" runat="server"></asp:Label></td>
                            </tr>
                        <tr><td>Date of incorporation: </td><td><asp:TextBox ID="ctDOI" runat="server"/></td>
                           <td><asp:Label ID="ctDOIError" ForeColor="Red" runat="server"></asp:Label></td>
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
                <asp:Button ID="btnModify" runat="server" Text="Create" OnClick="btnModify_Click" visible="false"/>
            </td>
        </tr>
    </table>
</asp:Content>
