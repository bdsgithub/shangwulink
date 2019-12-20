<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgetPassword.aspx.cs" Inherits="HF.Cloud.CustomerWeb.CustomerInfo.ForgetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>忘记密码</title>

    <link type="text/css" href="/css/reset.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <style type="text/css">
        a {
            cursor: pointer;
        }

        #tips, #pwdtips {
            color: red;
            font-weight: 800;
        }
    </style>
    <script src="../js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $('#tips').html('');
            $('#pwdtips').html('');
            $('#timing').hide();

            $('#btnSave').attr('disabled', 'disabled');

            $('#btnGetValidCode').click(function () {

                var ix = ValidatePhone();
                if (!ix) return false;
                $.post('/Asyn/RegistUserHandler.ashx', { uc: $('#lblSecondLoginCode').val().trim(), Type: 'IsExistUser' }, function (d) {
                    if (d == 'yes') {
                        $.post('/Asyn/RegistUserHandler.ashx', { phone: $('#lblSecondLoginCode').val(), Type: 'GetValidCode' }, function (data) {
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


                    $.post('/Asyn/RegistUserHandler.ashx', { phone: $('#lblSecondLoginCode').val(), code: $('#txtValidCode').val(), Type: 'ValidCodeIsRight' }, function (data) {
                        if (data == 'right') {

                            $('#tips').html('');

                            $.post('/Asyn/RegistUserHandler.ashx', { phone: $('#lblSecondLoginCode').val(), code: $('#txtValidCode').val(), Type: 'SBPhoneValidateCode' }, function (d) {
                                if (d == 'success') {
                                    $('#btnSave').removeAttr('disabled');
                                }
                                else {
                                    alert('验证码无效，请重新获取验证码！');
                                    $('#btnSave').attr('disabled', 'disabled');
                                }
                            });
                        }
                        else {
                            $('#btnSave').attr('disabled', 'disabled');
                            $('#tips').html('验证码无效，确认输入无误后，请重新获取验证码！');
                        }
                    });
                }
            });

            $('#btnSave').click(function () {
                //var reg = /^(?=.{6,12}$)(?![0-9]+$)(?!.*(.).*\1)[0-9a-zA-Z@#$%^&*(){},._]+$/;
                if ($('#txtNewPassword').val().length < 6) {
                    $('#pwdtips').html('密码长度位6-12位，不能低于6位！');
                }
                else {
                    $('#pwdtips').html('');
                    $.post('/Asyn/RegistUserHandler.ashx', { uid: $('#lblSecondLoginCode').val().trim(), pwd: encodeURI($('#txtNewPassword').val()), Type: 'ForgetPwd' }, function (d) {
                        if (d == 'success') {
                            alert('密码重置成功，页面即将跳转登陆页面，请重新登陆！');
                            //msg('密码重置成功，页面即将跳转登陆页面，请重新登陆！');
                            location.href = '/';
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

        var time = 90;

        function ChangeText() {
            $('#timing').html(time);
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
        <div id="secondStep" style="width: 800px; height: 474px; margin: 50px auto; border: 1px #ccc solid; border-radius: 10px;">
            <div style="height: 50px; background-color: #00a9ff; border-radius: 10px 10px 0 0;">
                <p style="font-size: 16px; color: #fff; text-align: center; line-height: 50px;">重置密码</p>
            </div>
            <div style="width: 350px; margin: 50px  auto 20px;">
                <div class="sign-input">
                    <asp:TextBox Style="background-color: #fff;" ID="lblSecondLoginCode" placeholder="请输入登陆账号" runat="server" Text=""></asp:TextBox>
                </div>
                <div class="sign-s">
                    <div class="sign-input sign-content-m">
                        <asp:TextBox ID="txtValidCode" MaxLength="6" placeholder="请输入6位验证码" runat="server" Style="width: 200px; background-color: #fff;"></asp:TextBox>
                    </div>
                    <a id="timing" class="sign-button sign-button-m" style="width: 100px; line-height: 50px;"></a>
                    <a id="btnGetValidCode" class="sign-button sign-button-m" style="width: 100px; line-height: 50px;">获取验证码</a>
                </div>
                <div class="sign-input">
                    <asp:TextBox ID="txtNewPassword" placeholder="请输入6-12位密码" TextMode="Password" runat="server" Style="background-color: #fff;"></asp:TextBox>
                    <br />
                    <span id="pwdtips"></span>
                    <span id="tips"></span>
                </div>
                <div>
                    <input class="sign-button sign-button-w" style="width: 49%;" id="btnSave" type="button" value="保存" />
                    <input class="sign-button sign-button-w" id="btnCancel" style="width: 49%;" type="button" value="取消" />
                </div>
            </div>

        </div>
        <asp:HiddenField ID="hidUid" runat="server" />
    </form>
</body>
</html>
