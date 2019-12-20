<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Region.aspx.cs" Inherits="HF.Cloud.CustomerWeb.LabelWap.Region" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0;" />
    <title>注册</title>
    <link href="../css/weui.min.css" rel="stylesheet" />
    <script src="../js/jquery-1.11.1.min.js"></script>
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/region.js"></script>
<%--    <script src="../js/layer/layer.js"></script>--%>
    <style type="text/css">
        #MainError {
            display: none;
            margin-top: 10px;
            color: red;
            vertical-align: middle;
            line-height: 12px;
            margin-left:20px;
        }

            #MainError img {
                vertical-align: middle;
            }
       

    </style>
    <script>


        $(function () {
            //$('#regionCplt').hide();
            //$('.weui-btn.weui-btn_plain-primary').click(function () {
            //    $(this).parent().parent().parent().hide(300);
            //    $('#regionCplt').show(300);
            //});
            //$('.weui-btn-area>div a').eq(0).click(function () {
            //    $(this).parent().parent().parent().hide(300).siblings().show(300);

            //});


           


            //注册新服务商
            $("#RegionServiceWeiXin").click(function () {
                if ($("#txtName").val().toString().trim() == "企业名称" || $("#txtUserName").val().toString().trim() == "管理员姓名" || $("#txtName").val().toString().trim() == "" || $("#txtUserName").val().toString().trim() == "") {
                    $("#MainError").css("display", "block");
                    $("#MainError span").html("请输入企业名称或管理员姓名！");
                }
                else {
                    var info = {};
                    info["SBPWD"] = $("#txtPwdP").val();
                    info["SBTEL"] = $("#txtPhone").val();
                    info["CNAME"] = $("#txtName").val();
                    info["CUserName"] = $("#txtUserName").val();
                    info["Type"] = "SBCodeApply";
                    //服务商注册
                    $.ajax({
                        type: "post",
                        url: "/ashx/RegistUserHandler.ashx",
                        data: info,
                        success: function (result) {
                            if (result == "success") {
                                location.href = "login.aspx";//注册成功跳转到登录
                            }
                        },
                        error: function (data) {
                            if (data.toString() == "error") {
                                $("#MainError").css("display", "block");
                                $("#MainError span").html("系统出错，请联系管理员！");
                            }
                        }
                    });
                }
            });



        });


        (function ($) {
            $.six = function (time) {
                $("#send").show()
                $("#send").html('已发送（' + time + "）秒");
                time--;
                if (time <= 0) {
                    $('#send').hide();
                    $('#anew').show();
                    return false;
                }
                set = setTimeout("$.six(" + time + ")", 1000);
            }
        })(jQuery)
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin-top: 20px; text-align: center; font-size: 22px;">
            用户注册
        </div>
        <div>
            <div class="weui-cells weui-cells_form" id="sign-one">
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">手机号</label>
                    </div>
                    <div class="weui-cell__bd">
                        <asp:TextBox ID="txtPhone" placeholder="输入手机号" CssClass="weui-input" runat="server" onfocus="if(value==defaultValue){value='';this.style.color='#000'}$('.sign-error:first').css('display', 'none');" onblur="if(!value){value=defaultValue;this.style.color='#999'}else{ValidatePhone(value);}"></asp:TextBox>
                    </div>
                </div>
                 <p class="sign-error" style="margin-left:20px;">
                 <img src="/image/g.png" />
                 <span>手机号输入有误</span>
                 </p>
                <div class="weui-cell weui-cell_vcode">
                    <div class="weui-cell__hd">
                        <label class="weui-label">验证码</label>
                    </div>
                    <div class="weui-cell__bd">
                        <asp:TextBox ID="txtValidateCode" placeholder="输入验证码" CssClass="weui-input" runat="server" onfocus="if(value==defaultValue){value='';this.style.color='#000'}$('#pValidate').css('display', 'none');" onblur="if(!value){value=defaultValue;this.style.color='#999'}"></asp:TextBox>
                    </div>
                    <div class="weui-cell__ft">
                        <button class="weui-vcode-btn" id="btnSendValidate">获取验证码</button>
                        <button class="weui-vcode-btn" id="send">已发送60</button>
                        <button class="weui-vcode-btn" id="anew">重新获取</button>
                    </div>
                </div>
                    <p class="sign-error" id="pValidate"  style="margin-left:20px;">
                    <img src="/image/g.png" alt="验证码有误" />
                    <span>验证码有误</span>
                    </p>
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">密码</label>
                    </div>
                    <div class="weui-cell__bd">
                        <%--<asp:TextBox ID="txtPwd" placeholder="输入密码6-12位字符" CssClass="weui-input" TextMode="Password" runat="server" onfocus="$('.sign-error:last').css('display','none');" onblur="ValidatePwd(value);"></asp:TextBox>--%>
                      <input type="text" value="输入密码6-12位字符" class="weui-input"  onfocus="$('.sign-error:last').css('display','none');" onblur="ValidatePwd(value);" id="txtPwd" style="color: #999999;" />
                          <input type="password" id="txtPwdP" maxlength="12"   class="weui-input"  style="display: none; color: #000;" onfocus="$('.sign-error:last').css('display','none');" />
                    </div>
                </div>
                <div>
                    <div style="width: 90%; margin: 8px auto 8px auto;">
                        <a class="weui-btn weui-btn_plain-primary" id="btnNext">下一步</a>
                    </div>
                </div>
            </div>


            <div id="sign-next" style="display:none;">
                <div class="weui-cells weui-cells_form">
                    <div class="weui-cell">
                        <div class="weui-cell__hd">
                            <label class="weui-label">企业名称</label>
                        </div>
                        <div class="weui-cell__bd">
                           <%-- <asp:TextBox ID="txtName" placeholder="企业名称" CssClass="weui-input" runat="server" onfocus="if(value==defaultValue){value='';this.style.color='#000';}" onblur="if(!value){value=defaultValue;this.style.color='#999'}" ></asp:TextBox>--%>
                           <input type="text" id="txtName" value="企业名称" class="weui-input" onfocus="if(value==defaultValue){value='';this.style.color='#000';}" onblur="if(!value){value=defaultValue;this.style.color='#999'}" style="color: #999999" />
                        </div>
                    </div>
                    <div class="weui-cell">
                        <div class="weui-cell__hd">
                            <label class="weui-label">姓名</label>
                        </div>
                        <div class="weui-cell__bd">
                            <%--<asp:TextBox ID="txtUserName" placeholder="管理员姓名" CssClass="weui-input" runat="server"  onfocus="if(value==defaultValue){value='';this.style.color='#000';}" onblur="if(!value){value=defaultValue;this.style.color='#999'}" ></asp:TextBox>--%>
                            <input type="text" id="txtUserName" value="管理员姓名" class="weui-input" onfocus="if(value==defaultValue){value='';this.style.color='#000';}" onblur="if(!value){value=defaultValue;this.style.color='#999'}" style="color: #999999" />
                        </div>
                    </div>
                     <p  id="MainError">
                     <img src="/image/g.png" />
                     <span>输入有误！</span>
                     </p>
                </div>
                <div class="weui-btn-area">
                    <div style="text-align: center;">
                       <%-- <a class="weui-btn weui-btn_mini weui-btn_default">上一步</a>&nbsp;&nbsp;--%>
                        <a class="weui-btn weui-btn_plain-primary"  id="RegionServiceWeiXin">完成注册</a>
                    </div>
                </div>
            </div>
         

        </div>
    </form>
</body>
</html>
