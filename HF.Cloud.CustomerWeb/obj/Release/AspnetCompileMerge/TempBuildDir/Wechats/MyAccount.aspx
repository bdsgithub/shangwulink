<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyAccount.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Wechats.MyAccount" %>

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
    <script src="js/zepto.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <img src="img/b.jpg" alt="" class="login-banner">
        <div class="eweic-chunk-white">
            <div class="eweic-chunk-cont">
                <p class="eweic-left">
                    企业名称：<asp:Literal runat="server" ID="ltCustomerName"></asp:Literal>
                </p>

            </div>
        </div>
        <div class="eweic-chunk-white">
            <div class="eweic-chunk-cont">
                <p class="eweic-left">
                    管理员：<asp:Literal runat="server" ID="ltAdminName"></asp:Literal>
                </p>

            </div>
        </div>
        <div class="eweic-chunk-white">
            <div class="eweic-chunk-cont">
                <p class="eweic-left">
                    账号：<asp:Literal runat="server" ID="ltAccount"></asp:Literal>
                </p>

            </div>
        </div>
        <div class="eweic-chunk-white">
            <div class="eweic-chunk-cont">
                <p class="eweic-left">
                    邮箱：
                    <asp:Literal runat="server" ID="ltEmail"></asp:Literal>
                </p>

            </div>
        </div>
        <div class="eweic-chunk-white">
            <div class="eweic-chunk-cont">
                <p class="eweic-left">
                    手机号：
                    <asp:Literal runat="server" ID="ltTel"></asp:Literal>
                </p>

            </div>
        </div>
        <div class="eweic-chunk-white">
            <div class="eweic-chunk-cont">
                <p class="eweic-left">
                    唯一码：
                    <asp:Literal runat="server" ID="ltUniqueCode"></asp:Literal>
                </p>

            </div>
        </div>
        <div class="eweic-bottom">
            <ul>
                <li class="operate-list-blue eweic-bottom-back">
                    <p>返回</p>
                    <a href="MyInfo.aspx"></a>
                </li>
            </ul>
        </div>
      
    </form>
</body>
</html>
