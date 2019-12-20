<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="labelmanage.aspx.cs" Inherits="HF.Cloud.CustomerWeb.LabelWap.labelmanage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0;" />
    <title>标签管理</title>
    <link href="../css/weui.min.css" rel="stylesheet" />
    <script src="../js/jquery-1.8.3.min.js"></script>
    <script>
        $(function () {
            $('.weui-btn.weui-btn_primary').click(function () {
                location.href = 'http://mp.weixin.qq.com/bizmall/mallshelf?id=&t=mall/list&biz=MzI0NTUyNDczNg==&shelf_id=1&showwxpaytitle=1#wechat_redirect';
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="weui-cells">
            <div class="weui-cell">
                <div class="weui-cell__bd">
                    <p>标签数量</p>
                </div>
                <div class="weui-cell__ft">
                    <asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label>个
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__bd">
                    <p>已绑定标签</p>
                </div>
                <div class="weui-cell__ft">
                    <asp:Label ID="lblAlready" runat="server" Text="0"></asp:Label>个
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__bd">
                    <p>可用数量</p>
                </div>
                <div class="weui-cell__ft">
                    <asp:Label ID="lblSurplus" runat="server" Text="0"></asp:Label>个
                </div>
            </div>
        </div>
        <div>
            <div style="width: 90%; margin-top: 8px; margin-left: auto; margin-right: auto;">
                <a class="weui-btn weui-btn_primary">购买标签</a>
            </div>
        </div>
    </form>
</body>
</html>
