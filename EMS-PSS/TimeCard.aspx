<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="TimeCard.aspx.cs" Inherits="EMS_PSS.TimeCard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <h3>Time Card</h3>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table id="selectEmpToAddTimeCard" style="width:auto">
        <tr>
            <td>
                <table>
                    <tr><td>Search by: </td></tr>
                    <tr><td>First Name: </td>
                        <td><asp:TextBox ID="fnameSearch" runat="server" Width="150px" /></td></tr>
                    <tr><td>Last Name: </td>
                        <td><asp:TextBox ID="lnameSearch" runat="server" Width="150px" /></td></tr>
                    <tr><td>SIN: </td>
                        <td><asp:TextBox ID="sinSearch" runat="server" Width="150px" /></td></tr>
                    <tr><td>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="searchSubmit_Click" /></td></tr>
                </table>
            </td>
            <td><table>
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
        </table>
        <table>
        <tr>
            <table>
                <tr>
                    <asp:GridView ID="searchFullResultGrid" runat="server" ShowHeader="True" onrowcommand="GridView_RowCommand" Width="100%" />
                </tr>
            </table>
        </tr>
        </table>
        <hr /><hr /><hr />
        <table>
        <tr>
            <asp:Label ID="errorMsg" runat="server"></asp:Label>
            <asp:PlaceHolder ID="tcDateInputTable" Visible="false" runat="server">
            <table style="border-color:gray;">
                <td>Week Of Time Card: </td>
                <td><asp:TextBox ID="tcWeekDate" runat="server"/></td>
            </table>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="hourInputTable" Visible="false" runat="server">
            <table style="border-color:gray;">
                <tr>
                    <td style="width:50px"></td><td style="color:red">SUN</td><td>MON</td><td>TUE</td><td>WED</td><td>THU</td><td>FRI</td><td style="color:blue">SAT</td>
                </tr>

                <tr>
                    <td style="width:50px">Hours</td>
                    <td><asp:TextBox ID="sunHInput" runat="server" Width="50px"/></td>
                    <td><asp:TextBox ID="monHInput" runat="server" Width="50px"/></td>
                    <td><asp:TextBox ID="tueHInput" runat="server" Width="50px"/></td>
                    <td><asp:TextBox ID="wedHInput" runat="server" Width="50px"/></td>
                    <td><asp:TextBox ID="thuHInput" runat="server" Width="50px"/></td>
                    <td><asp:TextBox ID="friHInput" runat="server" Width="50px"/></td>
                    <td><asp:TextBox ID="satHInput" runat="server" Width="50px"/></td>
                </tr>
            </table>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="pieceInputTable" Visible="false" runat="server">
            <table style="border-color:gray;">
                <tr>
                    <td style="width:50px">Pieces</td>
                    <td><asp:TextBox ID="sunPInput" runat="server" Width="50px"/></td>
                    <td><asp:TextBox ID="monPInput" runat="server" Width="50px"/></td>
                    <td><asp:TextBox ID="tuePInput" runat="server" Width="50px"/></td>
                    <td><asp:TextBox ID="wedPInput" runat="server" Width="50px"/></td>
                    <td><asp:TextBox ID="thuPInput" runat="server" Width="50px"/></td>
                    <td><asp:TextBox ID="friPInput" runat="server" Width="50px"/></td>
                    <td><asp:TextBox ID="satPInput" runat="server" Width="50px"/></td>
                </tr>
            </table>
            </asp:PlaceHolder>
        </tr>
        <tr><asp:Button ID="insertTimeCardBtn" runat="server" Text="Insert Time Card" OnClick="tcInsert_Click" /></tr>
    </table>

</asp:Content>

