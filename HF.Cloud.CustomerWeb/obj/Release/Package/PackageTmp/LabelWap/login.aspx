<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="HF.Cloud.CustomerWeb.LabelWap.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0;" />
    <title>登录</title>
    <link href="../css/weui.min.css" rel="stylesheet" />
    <script src="../js/jquery-1.8.3.min.js"></script>
    <script src="../js/layer/layer.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin-top: 20px; text-align: center; font-size: 22px;">
            登 录
        </div>
        <div class="weui-cells weui-cells_form">
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">账号</label>
                </div>
                <div class="weui-cell__bd">
                    <asp:TextBox ID="txtUserName" CssClass="weui-input" placeholder="请输入用户名" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">密码</label>
                </div>
                <div class="weui-cell__bd">
                    <asp:TextBox ID="txtUserPwd" CssClass="weui-input" TextMode="Password" placeholder="请输入密码" runat="server"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="weui-btn-area">
            <div>
                <asp:LinkButton ID="lnkBtnLogin" CssClass="weui-btn weui-btn_primary" OnClick="lnkBtnLogin_Click" runat="server">登 录</asp:LinkButton>
            </div>
        </div>
        <div class="weui-cells__tips">
            <div style="margin-top: 10px;">
                <a href="UpdatePWD.aspx" style="float: left; margin-left: 10%;">忘记密码</a>
                <a href="Region.aspx" style="float: right; margin-right: 10%;">现在注册</a>
            </div>
        </div>
    </form>
</body>
</html>
