<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainList.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Wechats.MainList" %>

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
    <script type="text/javascript">
        function GoHome(id) {
            document.cookie = "mainID=" + id + ";path=/";
            location.href = "Home.aspx?mainID=" + id;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="height: 35px; padding-top: 10px; text-align: center; background-color: #8fd7fb; color: #fff;">请选择您的服务商</div>
        <asp:Repeater runat="server" ID="repData">
            <ItemTemplate>
                <div class="eweic-chunk-white">
                    <div class="eweic-chunk-cont" onclick="GoHome(<%#Eval("ID") %>)" style="height: 35px; padding-top: 10px; padding-left: 10px;">
                        <a  href="javascript:void(0)"><%#Eval("SBName") %></a>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <div class="eweic-chunk-white" runat="server" id="noData">
            <div class="eweic-chunk-cont" style="height: 35px; padding-top: 10px; padding-left: 10px;">
            请联系服务商开通数据！
            </div>
        </div>
    </form>
</body>
</html>
