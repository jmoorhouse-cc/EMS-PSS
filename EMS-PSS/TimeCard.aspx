<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="TimeCard.aspx.cs" Inherits="EMS_PSS.TimeCard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <h3>Time Card</h3>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table id="selectEmpToAddTimeCard">
        <tr>
            <td style="height: 100%; width: 100%">
                <table style="height: 100%; width: 100%">
                    <tr><td>Search by: </td></tr>
                    <tr><td style="height: 100%; width: 100px">First Name: </td>
                        <td style="height: 100%; width: 100%"><asp:TextBox ID="fnameSearch" runat="server" Width="100px" /></td></tr>
                    <tr><td style="height: 100%; width: 100px">Last Name: </td>
                        <td style="height: 100%; width: 100%"><asp:TextBox ID="lnameSearch" runat="server" Width="100px" /></td></tr>
                    <tr><td style="height: 100%; width: 100px">SIN: </td>
                        <td style="height: 100%; width: 100%"><asp:TextBox ID="sinSearch" runat="server" Width="100px" /></td></tr>
                    <tr><td>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="searchSubmit_Click" /></td></tr>
                </table>
            </td>
            <td style="height: 100%; width: 100%"><table>
                <tr>
                    <asp:GridView ID="searchResultGrid" runat="server" ShowHeader="True" onrowcommand="GridView_RowCommand" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Actions">
                                <ItemTemplate>
                                    <asp:Button ID="selectBtn" runat="server" CommandName="Select" height="25px" Width="100%" 
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
            <table style="width:100%;">
                <tr>
                    <asp:GridView ID="searchFullResultGrid" runat="server" ShowHeader="True" onrowcommand="GridView_RowCommand" Width="100%" />
                </tr>
            </table>
        </tr>
        <tr>
            <asp:PlaceHolder ID="tcDateInputTable" Visible="false" runat="server">
            <table style="width:100%;">
                <td>Week Of Time Card: </td>
                <td><asp:TextBox ID="tcWeekDate" runat="server"/></td>
            </table>
            </asp:PlaceHolder>
        </tr>
        <tr>
            <asp:PlaceHolder ID="hourInputTable" Visible="false" runat="server">
            <table style="width:100%;">
                <tr>
                    <td></td><td style="color:red">SUN</td><td>MON</td><td>TUE</td><td>WED</td><td>THU</td><td>FRI</td><td style="color:blue">SAT</td>
                </tr>

                <tr>
                    <td>Hours&nbsp;</td>
                    <td><asp:TextBox ID="sunHInput" runat="server"/></td>
                    <td><asp:TextBox ID="monHInput" runat="server"/></td>
                    <td><asp:TextBox ID="tueHInput" runat="server"/></td>
                    <td><asp:TextBox ID="wedHInput" runat="server"/></td>
                    <td><asp:TextBox ID="thuHInput" runat="server"/></td>
                    <td><asp:TextBox ID="friHInput" runat="server"/></td>
                    <td><asp:TextBox ID="satHInput" runat="server"/></td>
                </tr>
            </table>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="pieceInputTable" Visible="false" runat="server">
            <table style="width:100%;">
                <tr>
                    <td>Pieces</td>
                    <td><asp:TextBox ID="sunPInput" runat="server"/></td>
                    <td><asp:TextBox ID="monPInput" runat="server"/></td>
                    <td><asp:TextBox ID="tuePInput" runat="server"/></td>
                    <td><asp:TextBox ID="wedPInput" runat="server"/></td>
                    <td><asp:TextBox ID="thuPInput" runat="server"/></td>
                    <td><asp:TextBox ID="friPInput" runat="server"/></td>
                    <td><asp:TextBox ID="satPInput" runat="server"/></td>
                </tr>
            </table>
            </asp:PlaceHolder>
        </tr>
    </table>

</asp:Content>

