<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ASPTokenInputTest.Default" %>

<%@ Register Assembly="ASPTokenInputLib" Namespace="ASPTokenInputLib" TagPrefix="ati" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ASPTokenInput Demo</title>
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
        <h2>ASPTokenInput Demo</h2>
        |&nbsp;<a href="https://github.com/harindaka/ASPTokenInput" target="_blank">Project on Github</a>&nbsp;|&nbsp;<a href="https://github.com/harindaka/ASPTokenInput/wiki" target="_blank">Howto and Wiki</a>&nbsp;|&nbsp;<a href="http://harindaka.blogspot.com" target="_blank">Author's Blog</a>&nbsp;|
        <br />
        <br />
        <table style="border-style: solid; border-color: Black; border-width: 1px; width: 100%;">
            <tr>
                <td style="width:50%; vertical-align: top; border-style: solid; border-color: Black; border-width: 1px; padding:10px">
                    <span style="font-weight: bold">ASPTokenInput Outside UpdatePanel</span><br/>
                    <ati:ASPTokenInput ID="tiTest1" runat="server" RequestHandlerPath="~/ItemList.aspx"
                        OnListChanged="tiTest1_ListChanged" PostbackOnItemAdded="True" PostbackOnItemRemoved="True" HintText="Start Typing Country Names..."/>
                    <br />                                       
                    <asp:ListBox ID="lbList1" runat="server" Width="100%" Height="150px" />
                    <br /><br />
                    <asp:Button ID="btnFullPostback" runat="server" Text="Trigger Full Postback" OnClick="btnFullPostback_Click" /> 
                </td>
                <td style="width:50%; vertical-align: top; border-style: solid; border-color: Black; border-width: 1px; padding:10px">
                    <asp:UpdatePanel ID="upPanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <span style="font-weight: bold">ASPTokenInput Inside UpdatePanel</span><br/>
                            <ati:ASPTokenInput ID="tiTest2" runat="server" RequestHandlerPath="~/ItemList.aspx"
                                OnListChanged="tiTest2_ListChanged" PostbackOnItemAdded="True" PostbackOnItemRemoved="True" Theme="facebook" HintText="Start Typing Country Names..."/>                            
                            <br />
                            <asp:ListBox ID="lbList2" runat="server" Width="100%" Height="150px" />
                            <br /><br/>
                            <asp:Button ID="btnPartialPostback" runat="server" Text="Trigger Partial Postback" OnClick="btnPartialPostback_Click" />
                            <br /><br />
                            <span>Some of ASPTokenInput properties are as follows. Set valid values and hit 
                            the &#39;Set Properties&#39; button. (Most of these properties directly map to the 
                            jquery tokeninput plugin properties)</span>
                            <table>
                            <tr>
                            <td><b>RequestHandlerPath</b></td>
                            <td><asp:Label ID="lblRequestHandlerPath" runat="server" /></td>
                            </tr>
                            <tr>
                            <td><b>PostbackOnItemAdded</b></td>
                            <td><asp:CheckBox ID="chkPostbackOnItemAdded" runat="server" /></td>
                            </tr>
                            <tr>
                            <td><b>PostbackOnItemRemoved</b></td>
                            <td><asp:CheckBox ID="chkPostbackOnItemRemoved" runat="server" /></td>
                            </tr>
                            <tr>
                            <td><b>HintText</b></td>
                            <td><asp:TextBox ID="txtHintText" runat="server" /></td>
                            </tr>
                            <tr>
                            <td><b>NoResultsText</b></td>
                            <td><asp:TextBox ID="txtNoResultsText" runat="server" /></td>
                            </tr>
                            <tr>
                            <td><b>SearchingText</b></td>
                            <td><asp:TextBox ID="txtSearchingText" runat="server" /></td>
                            </tr>
                            <tr>
                            <td><b>DeleteText</b></td>
                            <td><asp:TextBox ID="txtDeleteText" runat="server" /></td>
                            </tr>
                            <tr>
                            <td><b>Theme</b></td>
                            <td><asp:DropDownList ID="ddlTheme" runat="server" Width="150px"/></td>
                            </tr>
                            <tr>
                            <td><b>AnimateDropdown</b></td>
                            <td><asp:CheckBox ID="chkAnimateDropdown" runat="server" /></td>
                            </tr>
                            <tr>
                            <td><b>SearchDelay</b></td>
                            <td><asp:TextBox ID="txtSearchDelay" runat="server" /></td>
                            </tr>
                            <tr>
                            <td><b>MinChars</b></td>
                            <td><asp:TextBox ID="txtMinChars" runat="server" /></td>
                            </tr>
                            <tr>
                            <td><b>TokenLimit</b></td>
                            <td><asp:TextBox ID="txtTokenLimit" runat="server" /></td>
                            </tr>
                            <tr>
                            <td><b>PreventDuplicates</b></td>
                            <td><asp:CheckBox ID="chkPreventDuplicates" runat="server" /></td>
                            </tr>
                            </table>
                            <br /><asp:Button ID="btnSetProperties" runat="server" Text="Set Properties" 
                                onclick="btnSetProperties_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
