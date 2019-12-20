<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyInfo.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Wechats.MyInfo" %>

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
    <script type="text/javascript">
        $(function () {
            //解除绑定
            $("#UnbindWeiXin").click(function () {
                if (confirm("确定解除绑定？")) {
                    $.post("/ashx/Wechat.ashx", { username: $("#hidOpenID").val(), op: "UnbindWeiXin" }, function () {
                        //设置原有cookie过期，返回登录页面
                        var date = new Date();
                        date.setTime(date.getTime() - 10000);
                        document.cookie = "UserID=-1; expires=" + date.toGMTString();
                        document.cookie = "UserCode=-1; expires=" + date.toGMTString();
                        document.cookie = "OpenID=-1;expires=" + date.toGMTString();
                        location.href = "WeLogin.aspx";
                    });
                }
            });
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <img src="img/b.jpg" alt="" class="login-banner">
        <div class="oneself-list eweic-top">
            <ul>
                <li class="eweic-right-border">
                    <img src="img/cc.png" alt="">
                    <p>查看工单</p>
                    <a href="javascript:void(0)"></a>
                </li>
                <li class="eweic-right-border">
                    <img src="img/s.png" alt="">
                    <p>查看设备</p>
                    <a href="javascript:void(0)"></a>
                </li>
                <li class="eweic-right-border">
                    <img src="img/ss.png" alt="">
                    <p>设备搜索</p>
                    <a href="javascript:void(0)"></a>
                </li>
                <li class="eweic-right-border">
                    <img src="img/z.png" alt="">
                    <p>查看账号</p>
                    <a href="MyAccount.aspx"></a>
                </li>
            </ul>
        </div>
        <div class="oneself-list eweic-bottom-border">
            <ul>
               <%-- <li class="eweic-right-border">
                    <img src="img/c.png" alt="">
                    <p>查看维修单</p>
                    <a href="javascript:void(0)"></a>
                </li>--%>
                <li class="eweic-right-border">
                    <img src="img/c.png" alt="">
                    <p>
                        切换服务商
                    </p>
                    <a href="MainList.aspx"></a>
                </li>
                <li class="eweic-right-border">
                    <img src="img/d.png" alt="">
                    <p>解除绑定</p>
                    <a href="javascript:void(0)" id="UnbindWeiXin"></a>
                </li>
                <li class="eweic-right-border"></li>
                <li class="eweic-right-border"></li>
            </ul>
        </div>
        <asp:HiddenField runat="server" ID="hidOpenID" />
    </form>
</body>
</html>
