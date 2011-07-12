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
    <asp:ScriptManager runat="server">
    </asp:ScriptManager>
    <div>
        <table style="border-style: solid; border-color: Black; border-width: 1px; width: 100%;">
            <tr>
                <td style="vertical-align: top; border-style: solid; border-color: Black; border-width: 1px; padding:10px">
                    <span style="font-weight: bold">ASPTokenInput Outside UpdatePanel</span><br/>
                    <ati:ASPTokenInput ID="tiTest1" runat="server" RequestHandlerPath="~/ItemList.aspx"
                        OnListChanged="tiTest1_ListChanged" PostbackOnItemAdded="True" PostbackOnItemRemoved="True"/>
                    <br />                                       
                    <asp:ListBox ID="lbList1" runat="server" Width="100%" Height="150px" />
                    <br /><br />
                    <asp:Button ID="btnFullPostback" runat="server" Text="Trigger Full Postback" OnClick="btnFullPostback_Click" /> 
                </td>
                <td style="vertical-align: top; border-style: solid; border-color: Black; border-width: 1px; padding:10px">
                    <asp:UpdatePanel ID="upPanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <span style="font-weight: bold">ASPTokenInput Inside UpdatePanel</span><br/>
                            <ati:ASPTokenInput ID="tiTest2" runat="server" RequestHandlerPath="~/ItemList.aspx"
                                OnListChanged="tiTest2_ListChanged" PostbackOnItemAdded="True" PostbackOnItemRemoved="True" Theme="facebook"/>                            
                            <br />
                            <asp:ListBox ID="lbList2" runat="server" Width="100%" Height="150px" />
                            <br /><br/>
                            <asp:Button ID="btnPartialPostback" runat="server" Text="Trigger Partial Postback" OnClick="btnPartialPostback_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
