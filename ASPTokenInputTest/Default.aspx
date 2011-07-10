<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ASPTokenInputTest.Default" %>

<%@ Register Assembly="ASPTokenInputLib" Namespace="ASPTokenInputLib" TagPrefix="ati" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ASPTokenInput Sample Project</title>
    <script src="Scripts/jquery-1.5.1-vsdoc.js" type="text/javascript"></script>
    <script src="Scripts/jquery-1.5.1.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.tokeninput.js" type="text/javascript"></script>
    <script src="Scripts/json2.js" type="text/javascript"></script>

    <link href="Styles/token-input.css" rel="stylesheet" type="text/css" />
    <link href="Styles/token-input-mac.css" rel="stylesheet" type="text/css" />
    <link href="Styles/token-input-facebook.css" rel="stylesheet" type="text/css" />
</head>

<body>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div>
        <table>
            <tr>
                <td style="vertical-align: top">
                    <ati:asptokeninput id="tiTest1" runat="server" requesthandlerpath="~/ItemList.aspx"
                        onlistchanged="tiTest1_ListChanged" postbackonitemadded="True" postbackonitemremoved="True" />
                    <br />
                    <asp:Button ID="btnFullPostback" runat="server" Text="Full Postback" OnClick="btnFullPostback_Click" />
                    <br />
                    <br />
                    <asp:ListBox ID="lbList1" runat="server" Style="width: 100%" />
                </td>
                <td>
                    <asp:UpdatePanel ID="upPanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <ati:asptokeninput id="tiTest2" runat="server" requesthandlerpath="~/ItemList.aspx"
                                onlistchanged="tiTest2_ListChanged" postbackonitemadded="True" postbackonitemremoved="True" />
                            <br />
                            <asp:Button ID="btnPartialPostback" runat="server" Text="Partial Postback" OnClick="btnPartialPostback_Click" />
                            <br />
                            <br />
                            <asp:ListBox ID="lbList2" runat="server" Style="width: 100%" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
