<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdatePWD.aspx.cs" Inherits="HF.Cloud.CustomerWeb.LabelWap.UpdatePWD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0;" />
    <title>忘记密码</title>
     <link href="../css/weui.min.css" rel="stylesheet" />
    <script src="../js/jquery-1.11.1.min.js"></script>
    <link href="../css/style.css" rel="stylesheet" />
    <%--<script src="../js/region.js"></script>--%>
    <style type="text/css">
         a {
            cursor: pointer;
        }

        #tips, #pwdtips {
            color: red;
            font-weight: 500;

            margin-top: 10px;
            vertical-align: middle;
            line-height: 12px;
            margin-left:20px;
        }
        #timing {
        text-align:center;
        }

    </style>
      <script type="text/javascript">



          $(document).ready(function () {

              $('#tips').html('');
              $('#pwdtips').html('');
              $('#timing').hide();
              $('#btnSave').attr('disabled', 'disabled');

              $('#btnGetValidCode').click(function () {
                  var ix = ValidatePhone();
                  if (!ix) return false;
                  $.post('/ashx/RegistUserHandler.ashx', { uc: $('#lblSecondLoginCode').val().trim(), Type: 'IsExistUser' }, function (d) {
                      if (d == 'yes') {
                          $.post('/ashx/RegistUserHandler.ashx', { phone: $('#lblSecondLoginCode').val(), Type: 'GetValidCode' }, function (data) {
                              if (data == 'success') {
                                  $('#btnGetValidCode').hide();
                                  $('#timing').show();
                                  ChangeText();
                              }
                          });
                      }
                      else {
                          $('#tips').html('输入的手机号不存在，请重新输入！');
                           }
                  });
              });

              $('#txtValidCode').keyup(function () {
                  if ($(this).val().length == 6) {
                      var ix = ValidatePhone();
                      if (!ix) return false;
                      $.post('/ashx/RegistUserHandler.ashx', { phone: $('#lblSecondLoginCode').val(), code: $('#txtValidCode').val(), Type: 'ValidCodeIsRight' }, function (data) {
                          if (data == 'right') {
                              $('#tips').html('');
                              $.post('/ashx/RegistUserHandler.ashx', { phone: $('#lblSecondLoginCode').val(), code: $('#txtValidCode').val(), Type: 'SBPhoneValidateCode' }, function (d) {
                                  if (d == 'success') {
                                      $('#btnSave').removeAttr('disabled');
                                  }
                                  else {
                                      //layer.alert('验证码无效，请重新获取验证码！');
                                      $('#tips').html('验证码无效，确认输入是否有误，或重新获取验证码！');
                                      $('#btnSave').attr('disabled', 'disabled');
                                  }
                              });
                          }
                          else {
                              $('#btnSave').attr('disabled', 'disabled');
                              $('#tips').html('验证码无效，确认输入是否有误，或重新获取验证码！');
                          }
                      });
                  }
              });

              $('#btnSave').click(function () {
                  if ($('#txtNewPassword').val().length < 6) {
                      $('#pwdtips').html('密码长度位6-12位，不能低于6位！');
                      return;
                  }
                  else if ($('#tips').html()=="") {
                      $('#pwdtips').html('');
                      $.post('/ashx/RegistUserHandler.ashx', { uid: $('#lblSecondLoginCode').val().trim(), pwd: encodeURI($('#txtNewPassword').val()), Type: 'ForgetPwd' }, function (d) {
                          if (d == 'success') {
                              location.href = 'login.aspx';
                          }
                          else if (d == 'NotFind') {
                              $('#pwdtips').html('登陆账号不存在，请重新输入！');
                          }
                          else {
                              $('#pwdtips').html('密码修改失败，请稍后重试！');
                          }
                      });
                  }
              });
              $('#btnCancel').click(function () {
                  document.location.href = '/';
              });
          });

          var time = 60;

          function ChangeText() {
              $('#timing').html("已发送（"+time+"）秒");
              time--;
              if (time >= 0) {
                  setTimeout(ChangeText, 1000);
              }
              else {
                  $('#btnGetValidCode').show();
                  $('#timing').hide().html('');
              }
          }


          function ValidatePhone() {
              var phone = $('#lblSecondLoginCode').val().trim();
              if (phone == "") {
                  $("#tips").html("请输入登陆账号！");
                  return false;
              }
              if (phone.length != 11) {
                  $("#tips").html("手机号输入错误，请重新输入！");
                  return false;
              }
              var myreg = /^(((13[0-9]{1})|(14[0-9]{1})|(17[0,7]{1})|(15[0-3]{1})|(15[5-9]{1})|(18[0-9]{1}))+\d{8})$/;
              if (!myreg.test(phone)) {
                  $("#tips").html("手机号输入错误，请重新输入！");
                  return false;
              }
              return true;
          }


      </script>
</head>
<body>
    <form id="form1" runat="server">
     <div style="margin-top: 20px; text-align: center; font-size: 22px;">
            重置密码
        </div>
            <div class="weui-cells weui-cells_form" id="sign-one">
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">手机号</label>
                    </div>
                    <div class="weui-cell__bd">
                        <asp:TextBox ID="lblSecondLoginCode" placeholder="输入手机号" CssClass="weui-input" runat="server" onfocus="if(value==defaultValue){value='';this.style.color='#000'}$('.sign-error:first').css('display', 'none');" onblur="if(!value){value=defaultValue;this.style.color='#999'}else{ValidatePhone(value);}"></asp:TextBox>
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
                        <asp:TextBox ID="txtValidCode" placeholder="输入验证码" CssClass="weui-input" runat="server" onfocus="if(value==defaultValue){value='';this.style.color='#000'}$('#pValidate').css('display', 'none');" onblur="if(!value){value=defaultValue;this.style.color='#999'}"></asp:TextBox>
                    </div>
                    <div class="weui-cell__ft">
                         <a id="timing" class="weui-vcode-btn" style="width: 150px; line-height: 50px;"></a>
                         <a id="btnGetValidCode" class="weui-vcode-btn" style="width: 100px; line-height: 50px;">获取验证码</a>
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
                           <asp:TextBox ID="txtNewPassword"  class="weui-input"  placeholder="请输入6-12位密码" TextMode="Password" runat="server" Style="background-color: #fff;"></asp:TextBox>
                     </div>
                </div>
                        <p>
                            <span id="pwdtips"></span>
                            <span id="tips"></span>
                        </p>

                <div>
                    <div style="width: 90%; margin: 8px auto 8px auto;">
                        <a class="weui-btn weui-btn_plain-primary" id="btnSave">保存</a>
                    </div>
                </div>
            </div>
    </form>
</body>
</html>
