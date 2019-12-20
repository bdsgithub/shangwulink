<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Client.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Wechats.Client" %>

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
    <script src="/js/jquery-1.8.3.min.js"></script>
    <script src="/js/getQueryString.js"></script>
    <script type="text/javascript">
        function GetClient(id, name) {
            $.post("/ashx/Wechat.ashx", { id: id, name: name, op: "setClient" }, function () {
                //var query = document.location.href.split('?');
                var lo = document.referrer;
                location.href = lo;
                //var type = getQueryString("type");
                //if (type == "sheet") {
                //    loc = "OrderSheet.aspx";
                //}
                //else if (type == "repair") {
                //    loc = "OrderRepair.aspx";
                //}
                //if (query.length > 1) {
                //    location.href = loc + "?" + query[1];
                //}
                //else {
                //    location.href = loc;
                //}
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Repeater runat="server" ID="repData">
            <ItemTemplate>
                <div class="eweic-chunk-white">
                    <div class="eweic-chunk-cont" style="height: 35px; padding-top: 10px; padding-left: 10px;">
                        <a href="javascript:void(0)" onclick="GetClient(<%#Eval("ID") %>,'<%#Eval("ClientName") %>')"><%#Eval("ClientName") %></a>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <div class="eweic-chunk-white" runat="server" id="noData">
            <div class="eweic-chunk-cont" style="height: 35px; padding-top: 10px; padding-left: 10px;">
                <a href="Home.aspx">请联系服务商开通数据！</a>
            </div>
        </div>
    </form>
</body>
</html>
