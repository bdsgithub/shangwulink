<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeLogin.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Wechats.WeLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent">
    <meta name="format-detection" content="telephone=no">
    <meta name="full-screen" content="yes">
    <meta name="x5-page-mode" content="app">
    <link rel="stylesheet" href="css/style.css">
    <script type="text/javascript" src="/js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
        $(function () {
            var username = $("#hidUserName").val();//微信用户唯一标识
            //页面根据username判断是否已绑定客户，已绑定直接返回用户信息
            $.post("/ashx/Wechat.ashx", { op: "GetUser", username: username }, function (data) {
                var strs = data.split(';');
                if (strs[0] != 0) {
                    document.cookie = "UserID=" + strs[0] + ";path=/";
                    document.cookie = "UserCode=" + strs[1] + ";path=/";
                    document.cookie = "OpenID=" + username + ";path=/";
                    location.href = "/Wechats/MainList.aspx";
                }
            })
            //登录
            $("#login").click(function () {
                var SBCode = $("#userName").val();
                var SBPWD = $("#userPwd").val();

                if (SBCode.length == 0) {
                    alert("请输入登录邮箱");
                    return;
                }
                if (SBPWD.length == 0) {
                    alert("请输入用户名");
                    return;
                }

                $.ajax({
                    type: "post",
                    url: "/Asyn/Region.ashx",
                    data: "Type=QueryLogin&SBCode=" + SBCode + "&SBPWD=" + SBPWD,
                    success: function (result) {
                        var strs = result.split(';');
                        if (strs[0] == 0) {
                            alert("用户名或密码错误！");
                        }
                        else {
                            document.cookie = "UserID=" + strs[0] + ";path=/";
                            document.cookie = "UserCode=" + strs[1] + ";path=/";
                            document.cookie = "OpenID=" + username + ";path=/";
                            //登录成功验证微信是否绑定
                            $.post("/ashx/Wechat.ashx", { username: username, op: "ValidateWeiXin", customerID: strs[0] });
                            location.href = "/Wechats/MainList.aspx";
                        }
                    },
                    error: function (data) {
                        if (data.toString() == "error") {
                            alert("系统出错，请联系管理员！");
                        }
                    }
                });
            });
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <img src="img/b.jpg" alt="" class="login-banner" />
        <div class="login-input login-top">
            <img src="img/username.png" alt="" />
            <input type="text" id="userName" placeholder="请输入您的账号" />
        </div>
        <div class="login-input login-input-border">
            <img src="img/password.png" alt="" />
            <input type="password" id="userPwd" placeholder="请输入您的密码" />
        </div>
        <div class="login-button">
            <p>登录并绑定</p>
            <a href="javascript:void(0)" id="login"></a>
        </div>
        <asp:HiddenField runat="server" ID="hidCode" />
        <asp:HiddenField runat="server" ID="hidUserName" />
    </form>
</body>
</html>
