<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SheetType.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Wechats.SheetType" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent">
    <meta name="format-detection" content="telephone=no">
    <meta name="full-screen" content="yes">
    <meta name="x5-page-mode" content="app">
    <link rel="stylesheet" href="css/style.css">
</head>
<body>
    <form id="form1" runat="server">
        <asp:Repeater runat="server" ID="repData">
            <ItemTemplate>
                <div class="eweic-chunk-white">
                    <div class="eweic-chunk-cont" style="height: 35px; padding-top: 10px; padding-left: 10px;">
                        <a href="OrderSheet.aspx?id=<%#Eval("ID") %>&name=<%#Eval("TypeName") %>"><%#Eval("TypeName") %></a>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </form>
</body>
</html>
