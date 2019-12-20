<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="HF.Cloud.CustomerWeb.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>易维客-客户登录</title>
    <link rel="stylesheet" href="/css/reset.css" />
    <link rel="stylesheet" href="/css/style.css" />
    <script type="text/javascript" src="/js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="/js/region.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="sign-background"></div>
        <div class="sign-content" id="sign-one">
            <p class="sign-title">登录</p>
            <div class="sign-input">
                <input type="text" id="userName" value="请输入用户名" onfocus="if(value==defaultValue){value='';this.style.color='#000'}" onblur="if(!value){value=defaultValue;this.style.color='#999'}" style="color: #999999" />
            </div>
            <div class="sign-input">
                <input type="text" value="密码" id="userPwd" onfocus="if(this.value==defaultValue) {this.value='';this.type='password';this.style.color='#000'}" onblur="if(!value) {value=defaultValue; this.type='text';this.style.color='#999'}" style="color: #999999;" />
            </div>
            <div style="margin-top: 10px;">
                <div style="width: 100px; float: left;">
                    <input id="u12_input" type="checkbox" value="checkbox" />
                    <label for="u12_input">
                        记住密码
                    </label>
                </div>
                <div style="float: right;">
                    <span><a href="/CustomerInfo/ForgetPassword.aspx">忘记密码 ?</a></span>
                </div>
            </div>
            <input type="button" class="sign-button sign-button-w" id="Login" value="登&nbsp;录" />
        </div>
    </form>
</body>
</html>
