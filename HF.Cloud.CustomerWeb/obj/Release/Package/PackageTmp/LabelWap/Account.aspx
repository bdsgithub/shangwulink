<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="HF.Cloud.CustomerWeb.LabelWap.Account" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0;" />
    <title>账户管理</title>
    <link href="../css/weui.min.css" rel="stylesheet" />
    <script src="../js/jquery-1.8.3.min.js"></script>
    <script>
        $(function () {
            $('.weui-btn.weui-btn_default').click(function () {
                document.cookie = "eweicuserinfo=-1;path=/";
                document.location.href = '/LabelWap/login.aspx';
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="page__bd">
            <div class="weui-cells">
                <div class="weui-cell">
                    <div class="weui-cell__bd">
                        <p>公司名称</p>
                    </div>
                    <div class="weui-cell__ft">
                        <asp:Label ID="lblCompanyName" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__bd">
                        <p>姓名</p>
                    </div>
                    <div class="weui-cell__ft">
                        <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__bd">
                        <p>账号</p>
                    </div>
                    <div class="weui-cell__ft">
                        <asp:Label ID="lblAccount" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__bd">
                        <p>邮寄地址</p>
                    </div>
                    <div class="weui-cell__ft">
                        <asp:Label ID="lblPostAddress" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
            <div>
                <div style="width: 90%; margin-top: 8px; margin-left: auto; margin-right: auto;">
                     <a class="weui-btn weui-btn_plain-primary" id="btnSave">退出登陆</a>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
