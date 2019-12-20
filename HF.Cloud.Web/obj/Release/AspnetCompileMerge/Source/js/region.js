function Init() {
    $("#u25_state0").show(); //文本
    $("#u0_state0").show();//样式
    $("#u11_state0").show();//标题

    $("#u25_state").hide();//文本
    $("#u0_state").hide();//样式
    $("#u11_state").hide();//标题
}
$(function () {
    Init();
    //隐藏显示及背景色的切换
    $("#u9").click(function () {
        Init();
    });
    $("#u230").click(function () {
        $("#u25_state0").hide(); //文本
        $("#u0_state0").hide();//样式
        $("#u11_state0").hide();//标题

        $("#u25_state").show();//文本
        $("#u0_state").show();//样式
        $("#u11_state").show();//标题
    });
    //注册页面跳转登录
    $("#btnLogin").click(function () {
        location.href = "/Login.aspx";
    });
    //登录页面跳转注册
    $("#btnRegion").click(function () {
        location.href = "/Region.aspx";
    });

    //为密码输入框绑定事件，切换密码显示框和密码输入框
    $("#txtPwd").focus(function () {
        var text_value = $(this).val();
        if (text_value == "输入密码6-12位字符") {
            $("#txtPwd").hide();
            $("#txtPwdP").show().focus();
        }
    });
    $("#txtPwdP").blur(function () {
        var text_value = $(this).val();
        if (text_value == "") {
            $("#txtPwd").show();
            $("#txtPwdP").hide();
        }
        else{
            ValidatePwd(text_value);
        }
    });

    var time = 60;
    //重新发送验证码
    $('#anew').click(function () {
        $("#btnSendValidate").click();
    })
    //下一步
    $("#btnNext").click(function () {
        //验证手机格式和密码格式
        if ($(".sign-error:first").css("display") == "none" && ValidatePwd($("#txtPwdP").val())) {
            //验证手机验证码
            if ($("#txtValidateCode").val() != "") {
                $.post("/ashx/RegistUserHandler.ashx", { phone: $("#txtPhone").val(), code: $("#txtValidateCode").val(), Type: "SBPhoneValidateCode" }, function (data) {
                    if (data == "wrong") {
                        $("#pValidate").css("display", "block");
                        $("#pValidate span").html("验证码错误");
                    }
                    else if (data == "success") {
                        $("#sign-one").css("display", "none");
                        $("#sign-next").css("display", "block");
                    }
                });
            }
            else {
                $("#pValidate span").html("请输入验证码");
            }
        }
    });
    //发送验证码
    $("#btnSendValidate").click(function () {
        var phone = $("#txtPhone").val();
        //验证手机格式
        if (ValidatePhone(phone)) {
            $.post("/ashx/RegistUserHandler.ashx", { phone: phone, Type: "SBPhoneValidate" }, function (data) {
                if (data == "success") {
                    $(".sign-error:first").css("display", "none");
                    $("#btnSendValidate").hide();
                    $("#anew").hide();
                    $.six(time)
                }
                else if (data == "had") {
                    $(".sign-error:first").css("display", "block");
                    $(".sign-error:first span").html("此手机号已被注册！");
                }
            });
            return false;//微信端注册页面需要加这句话
        }
    });


    var check = true;
    //注册时验证输入
    function CheckIsNull() {
        if ($("#txtUserName").val().toString().trim() == "") {
            layer.alert("请输入管理员姓名");
            $("#txtUserName").focus();
            check = false;
            return;
        }
        if ($("#txtName").val().toString().trim() == "") {
            layer.alert("请输入企业名称");
            $("#txtName").focus();
            check = false;
            return;
        }
        if ($("#txtPwd").val().toString().trim() == "") {
            layer.alert("请输入密码");
            $("#txtPwd").focus();
            check = false;
            return;
        }
        if ($("#txtPwd").val().length < 6 || $("#txtPwd").val().length > 12) {
            layer.alert("密码6-12位");
            $("#txtPwd").focus();
            check = false;
            return;
        }
        check = true;
        return;
    }
    //注册新服务商
    $("#RegionService").click(function () {
        if ($("#txtName").val().toString().trim() == "企业名称" || $("#txtUserName").val().toString().trim() == "管理员姓名") {
            layer.alert("请输入企业名称或管理员姓名");
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
                        layer.alert("注册成功！");
                        location.href = "/Login.aspx";//注册成功跳转到登录
                    }
                },
                error: function (data) {
                    if (data.toString() == "error") {
                        layer.alert("系统出错，请联系管理员！");
                    }
                }
            });
        }

    });
    //注册客户
    $("#RegionCustomer").click(function () {
        if ($("#txtClientName").val() == "客户名称" || $("#txtClientUserName").val() == "管理员姓名") {
            layer.alert("请输入客户名称或管理员姓名");
            return false;
        }
        else {
            var info = {};
            info["SBPWD"] = $("#txtPwdP").val();
            info["SBTEL"] = $("#txtPhone").val();
            info["CNAME"] = $("#txtClientName").val();
            info["CUserName"] = $("#txtClientUserName").val();
            info["Type"] = "SBCodeApplyCustomer";
            //客户注册
            $.ajax({
                type: "post",
                url: "/ashx/RegistUserHandler.ashx",
                data: info,
                success: function (result) {
                    if (result == "success") {
                        layer.alert("注册成功！");
                        window.location = "http://c.eweic.com";
                    }
                },
                error: function (data) {
                    if (data.toString() == "error") {
                        layer.alert("系统出错，请联系管理员！");
                    }
                }
            });
        }
    });

    //登录
    function Login() {
        var SBCode = $("#userName").val();
        var SBPWD = $("#userPwdP").val();
        var ISRemember = $("#u12_input").attr("checked");//是否记住密码
        if (ISRemember == "checked") {
            ISRemember = "1";
        }
        else {
            ISRemember = "0";
        }
        if (SBCode.length == 0) {
            layer.alert("请输入用户名！");
            return;
        }
        if (SBPWD.length == 0) {
            layer.alert("请输入密码！");
            return;
        }

        $.ajax({
            type: "post",
            url: "/ashx/LoginHandler.ashx",
            data: "Type=QueryLogin&SBCode=" + SBCode + "&SBPWD=" + SBPWD + "&isRemember=" + ISRemember,
            success: function (result) {
                if (result == '1' || result == '2') {
                    if (result == "2") {//首次登陆
                        location.href = '/BasicSet/UserBase/ResetPassword.aspx?ct=firstlogin';
                    }
                    else {
                        location.href = "/Modules/Index.aspx";
                    }
                }
                else {
                    layer.alert("用户名或密码错误！");
                }
            },
            error: function (data) {
                if (data.toString() == "error") {
                    layer.alert("登陆失败，请检查网络是否异常！");
                }
            }
        });
    }
    //服务商登录
    $("#Login").click(function () {
        Login();
    });

    $(document).keydown(function (e) {
        var keyCode = e.which || e.keyCode;
        if (keyCode == 13) {
            Login();
            return false;
        }
    });
    //验证邮箱
    function ValidEmail(val) {
        var reg = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/; //验证邮箱的正则表达式
        if (!reg.test(val)) {
            layer.alert("邮箱格式不对");
            return false;
        }
        else {
            $.post("/ashx/RegistUserHandler.ashx", { Type: "EmailValid", Email: val }, function (data) {
                if (data == "1") {
                    //该邮箱已被注册；
                    return false;
                }
            })
        }
        return true;
    }

    //功能：根据用户输入的Email跳转到相应的电子邮箱首页  
    function gotoEmail($mail) {
        $t = $mail.split('@')[1];
        $t = $t.toLowerCase();
        if ($t == '163.com') {
            return 'mail.163.com';
        } else if ($t == 'vip.163.com') {
            return 'vip.163.com';
        } else if ($t == '126.com') {
            return 'mail.126.com';
        } else if ($t == 'qq.com' || $t == 'vip.qq.com' || $t == 'foxmail.com') {
            return 'mail.qq.com';
        } else if ($t == 'gmail.com') {
            return 'mail.google.com';
        } else if ($t == 'sohu.com') {
            return 'mail.sohu.com';
        } else if ($t == 'tom.com') {
            return 'mail.tom.com';
        } else if ($t == 'vip.sina.com') {
            return 'vip.sina.com';
        } else if ($t == 'sina.com.cn' || $t == 'sina.com') {
            return 'mail.sina.com.cn';
        } else if ($t == 'tom.com') {
            return 'mail.tom.com';
        } else if ($t == 'yahoo.com.cn' || $t == 'yahoo.cn') {
            return 'mail.cn.yahoo.com';
        } else if ($t == 'tom.com') {
            return 'mail.tom.com';
        } else if ($t == 'yeah.net') {
            return 'www.yeah.net';
        } else if ($t == '21cn.com') {
            return 'mail.21cn.com';
        } else if ($t == 'hotmail.com') {
            return 'www.hotmail.com';
        } else if ($t == 'sogou.com') {
            return 'mail.sogou.com';
        } else if ($t == '188.com') {
            return 'www.188.com';
        } else if ($t == '139.com') {
            return 'mail.10086.cn';
        } else if ($t == '189.cn') {
            return 'webmail15.189.cn/webmail';
        } else if ($t == 'wo.com.cn') {
            return 'mail.wo.com.cn/smsmail';
        } else if ($t == '139.com') {
            return 'mail.10086.cn';
        } else {
            return '';
        }
    };

});
//验证密码
function ValidatePwd(val) {
    if (val.length < 6 || val.length > 12) {
        $(".sign-error:last").css("display", "block");
        $(".sign-error:last span").html("密码6-12位");
        return false;
    }
    else {
        $(".sign-error:last").css("display", "none");
        return true;
    }
}
//验证手机号
function ValidatePhone(phone) {
    if (phone == "") {
        $(".sign-error:first").css("display", "block");
        $(".sign-error:first span").html("请输入手机号！");
        return false;
    }
    if (phone.length != 11) {
        $(".sign-error:first").css("display", "block");
        $(".sign-error:first span").html("请输入正确的手机号！");
        return false;
    }
    var myreg = /^(((13[0-9]{1})|(14[0-9]{1})|(17[0,7]{1})|(15[0-3]{1})|(15[5-9]{1})|(18[0-9]{1}))+\d{8})$/;
    if (!myreg.test(phone)) {
        $(".sign-error:first").css("display", "block");
        $(".sign-error:first span").html("请输入正确的手机号！");
        return false;
    }
    $(".sign-error:first").css("display", "none");//通过验证隐藏
    return true;

}
