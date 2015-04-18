<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EMS_PSS.WebForm1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        #outPopUp{
             position:absolute;
             width:260px;
             height:84px;
             z-index:15;
             top:50%;
             left:50%;
             margin:-100px 0 0 -150px;
             background:gray;
             border:ridge;
         }
        #errorLabel{
            position:absolute;
            width:260px;
            height:20px;
            top:46%;
            left:50%;
            margin:-100px 0 0 -150px;
            background:white;
            }
    </style>
</head>
<body>
    <div id="errorLabel">
        <asp:Label ID="lblErrorMsg" runat="server" ForeColor="#CC0000"></asp:Label>
    </div>
    <div id="outPopUp">
        <form id="form1" runat="server">   
            <table>
                <tr>
                    <td align="right">
                        Username:
                    </td>
                    <td>
                        <input id="tbxUsername" runat="server" type="text" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Password:
                    </td>
                    <td>
                        <input id="tbxPassword" runat="server" type="password" /><br />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td align="right">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="SubmitLogin" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
