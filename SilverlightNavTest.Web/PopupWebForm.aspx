<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupWebForm.aspx.cs" Inherits="SilverlightNavTest.Web.PopupWebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>this is pop up web form</title>
    <style type="text/css">
        .auto-style1 {
            width: 507px;
        }
        .auto-style2 {
            width: 494px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Hello<br />
        This is popup web form<br />
        <br />
        <table style="width:100%;">
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="Label1" runat="server" Text="this is a very long label" Width="100%"></asp:Label>
                </td>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Button ID="Button1" runat="server" Text="Button" Width="100%" />
                </td>
                <td class="auto-style2">
                    <asp:Label ID="Label2" runat="server" BackColor="Fuchsia" Text="Label" Width="100%"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <br />
        <br />
        </div>
    </form>
</body>
</html>
