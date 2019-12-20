<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Wap.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
      <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent">
    <meta name="format-detection" content="telephone=no">
    <meta name="full-screen" content="yes">
    <meta name="x5-page-mode" content="app">
   <%-- <link rel="stylesheet" href="/Wechats/css/style.css">
    
    <script src="../js/jquery-1.8.3.min.js"></script>--%>
     <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/weui/1.1.2/style/weui.min.css" rel="stylesheet"/>
  
    
     <style type="text/css">
        /*.b-title {
            text-align: center;
            font-size: 18px;
        }

        .de_m {
            margin-top: 12%;
            margin-bottom: 12%;
            color: #aed9f5;
            font-size: 24px;
        }
        .sign-input {
            line-height:30px;
            height: 30px;
            border: 0px #dadada solid;
            margin: 50px auto;
            border-radius: 5px;
            width:70%;
           
            vertical-align:central;
        }
        .sign-input input {
            width: 170px;
            font-size: 14px;
            border: none;
            color: #aaa;
            line-height: 46px;
          
           float:right;
            height: 30px;
            background-color: #fff;

        }
        .sign-content {
            width: 100%;
            height: 210px;
            margin: 0 auto;
            }*/


          .weui-input{
            text-align: right;
        }
        body{
            background-color: #f5f5f5;
            padding-top: 20px; 
        }
        .option{
            position: absolute;
            bottom: 10px;
            left: 10px;
            right: 10px;
            z-index: -1;
        }
        .title{
            clear: both;
            text-align: center;
            line-height: 60px;
            height: 60px;
            font-size: 20px;
            font-weight: bolder;
            color: rgba(8, 6, 13, 0.67);
        }
    </style>
    <script type="text/javascript">
        $(function () {

            //登录
            $("#LoginBtn").click(function () {
               
                //var SBCode = $("#userName").val();
                //var SBPWD = $("#userPwd").val();
                //if (SBCode.length == 0 || SBCode == "请输入用户名") {
                //    alert("请输入用户名");
                //    return;
                //}
                //if (SBPWD.length == 0 || SBPWD == "请输入密码") {
                //    alert("请输入密码");
                //    return;
                //}
                var dl_title = $("#dl_title");
                var dl_content = $("#dl_content");
                var SBCode = $("#userName").val();
                var SBPWD = $("#userPwd").val();
                var mid = $("#hf_mid").val();
                if (SBCode.length === 0 || SBCode === "请输入用户名") {
                    dl_title.text("错误！");
                    dl_content.text("请输入用户名");
                    $("#dl").css("display", "block");
                    return false;
                }
                if (SBPWD.length === 0 || SBPWD === "请输入密码") {
                    dl_title.text("错误！");
                    dl_content.text("请输入密码");
                    $("#dl").css("display", "block");
                    return false;
                }


                $.ajax({
                    type: "post",
                    url: "ashx/Region.ashx",
                    data: "Type=QueryLogin&SBCode=" + SBCode + "&SBPWD=" + SBPWD + "&mid=" + mid,
                    success: function (result) {
                        //var strs = result.split(';');
                        if (result!="success") {
                            alert("用户名或密码错误！");
                        }
                        else {
                            //document.cookie = "UserID=" + strs[0] + ";path=/";
                            //document.cookie = "UserCode=" + strs[1] + ";path=/";
                            if ($("#hf_direction").val() == "BinDing")   //登录成功后跳转到绑定页面
                            {
                                //alert($("#hf_cid").val() + "---" + $("#hf_mid").val());
                                location.href = "BinDing.aspx?cid=" + $("#hf_cid").val() + "&mid=" + $("#hf_mid").val();
                            }
                            if ($("#hf_direction").val() == "Finish")
                            {
                                location.href = "Finish.aspx?cid=" + $("#hf_cid").val() + "&mid=" + $("#hf_mid").val() + "&sheetId=" + $("#hf_sheetId").val();
                            }
                        }
                    },
                    error: function (data) {
                        if (data.toString() == "error") {
                            alert("系统出错，请联系管理员！");
                        }
                    }
                });
                return false;
            });

        });
    </script>
</head>
<body>
    <div class="header">
        <!--<i class="fa fa-sign-in"></i>-->
        <div class="title">登录</div>
    </div>
    <form id="form1" runat="server">
   <%-- <div>
       <div class="b-title de_m">标签绑定</div>
         <div class="sign-content" id="sign-one">
         <div class="sign-input">
                用户名<input type="text" id="userName" value="请输入用户名" onfocus="if(value==defaultValue){value='';this.style.color='#000'}" onblur="if(!value){value=defaultValue;this.style.color='#999'}" style="color: #999999" />
            </div>
            <div class="sign-input">
                密 &nbsp;码<input type="text" value="请输入密码" id="userPwd" onfocus="if(this.value==defaultValue) {this.value='';this.type='password';this.style.color='#000'}" onblur="if(!value) {value=defaultValue; this.type='text';this.style.color='#999'}" style="color: #999999;" />
            </div>
         </div>


         <div class="eweic-bottom">
            <ul>
                <li class="operate-list-blue eweic-bottom-back">
                    <p> 
                        <asp:Literal runat="server" ID="ltBut">登 录</asp:Literal></p>
                    <asp:LinkButton Width="100%" Style="display: block; color: white;" ID="LoginBtn" runat="server" ></asp:LinkButton>
                </li>
            </ul>
        </div>
    </div>--%>

          <div class="content">
            <div class="weui-cells">
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label">用户名</label></div>
                    <div class="weui-cell__bd">
                        <input id="userName" class="weui-input" type="text" placeholder="请输入用户名"/>
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label">密码</label></div>
                    <div class="weui-cell__bd">
                        <input id="userPwd" class="weui-input" type="password" placeholder="请输入密码"/>
                    </div>
                </div>
            </div>
        </div>
        <div class="option">
            <a id="LoginBtn" class="weui-btn weui-btn_primary">登 录</a>
        </div>



        <asp:HiddenField ID="hf_direction" runat="server" />
        <asp:HiddenField ID="hf_sheetId" runat="server" />

          <asp:HiddenField ID="hf_cid" runat="server" />
          <asp:HiddenField ID="hf_mid" runat="server" />

    </form>


    <div id="dl" style="display: none;">
        <div class="weui-mask"></div>
        <div class="weui-dialog">
            <div id="dl_title" class="weui-dialog__hd"><strong class="weui-dialog__title">弹窗标题</strong></div>
            <div id="dl_content" class="weui-dialog__bd">弹窗内容，告知当前页面信息等</div>
            <div class="weui-dialog__ft">
                <a href="javascript:" class="weui-dialog__btn weui-dialog__btn_primary" onclick="(function() {
    $('#dl').css('display','none');
                })();">确定</a>
            </div>
        </div>
    </div>


</body>
</html>
