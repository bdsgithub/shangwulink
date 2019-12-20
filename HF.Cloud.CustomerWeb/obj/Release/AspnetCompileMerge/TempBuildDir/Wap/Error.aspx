<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Wap.Error" %>

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
    <%--  <link rel="stylesheet" href="/Wechats/css/style.css">--%>
      <script src="/Wechats/js/zepto.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/weui/1.1.2/style/weui.min.css" rel="stylesheet">
    <style type="text/css">
            body{
            background-color: #f5f5f5;
        }
        .b-title {
            text-align: center;
            font-size: 16px;
        }
        .de_m {
            margin-top: 35%;
            margin-bottom: 25%;
            color:#6F6F6F;
        }
        .option{
            margin-top: 20%;
            margin-left: 10px;
            margin-right: 10px;
        }
    </style>
    <script type="text/javascript">
        //$(function () {
            function lnkBtnLogin_Click() {
                var cid = $("#hf_cid").val();
                var mid = $("#hf_mid").val();
                if (cid != "" && mid != "")
                {
                    location.href = "Login.aspx?direction=BinDing&cid=" + cid + "&mid=" + mid;
                }
                else
                {
                    alert("此标签无效！");
                }
            }
        //});
    </script>
</head>
<body>
    <form id="form1" runat="server">
       <%-- <div class="b-title de_m">
            <img src="/image/sad.png" />
            <div style="margin-top:30px;margin-bottom:30px;">
                非常抱歉，该标签未绑定或无效。<br /><br />
                请联系技术人员维护。
            </div>
             
            <div class="eweic-bottom" >
                <ul>
                    <li class="operate-list-blue eweic-bottom-back">
                        <p><asp:Literal runat="server" ID="ltBut">绑定标签</asp:Literal></p>
                        <asp:LinkButton Width="100%" Style="display: block; color: white; left:0px;" ID="lnkBtnLogin" runat="server" OnClick="lnkBtnLogin_Click"></asp:LinkButton>
                    </li>
                </ul>
            </div>

          
        </div>--%>







          <div class="b-title de_m">
            <div class="icon-box">
            <i class="weui-icon-warn weui-icon_msg-primary"></i>
            <div class="icon-box__ctn">
                <h3 class="icon-box__title">标签未绑定或无效</h3>
                <p class="icon-box__desc">请联系技术人员绑定标签</p>
            </div>
            </div>

            <div class="option">
                <a href="javascript:void(0)" onclick="lnkBtnLogin_Click();return false;" class="weui-btn weui-btn_primary">绑定标签</a>
            </div>

        </div>
          <asp:HiddenField ID="hf_cid" runat="server" />
             <asp:HiddenField ID="hf_mid" runat="server" />


    </form>
</body>
</html>
