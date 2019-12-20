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
        location.href = "Login.aspx";
    });
    //登录页面跳转注册
    $("#btnRegion").click(function () {
        location.href = "Region.aspx";
    });
    var check = true;
    function CheckIsNull() {
        if ($("#regionEmail").val().toString().trim() == "") {
            alert("请输入用户名");
            $("#regionEmail").focus(); check = false;
            return;
        } else if ($("#regionPwd").val().toString().trim() == "") {
            alert("请输入密码");
            $("#regionPwd").focus(); check = false;
            return;
        } else { check = true; }
    }
    //注册新服务商
    $("#Region").click(function () {
        var SBCode = $("#regionEmail").val();
        var SBPWD = $("#regionPwd").val();
        CheckIsNull();
        if (check) {
            $.ajax({
                type: "post",
                url: "/ashx/RegistUserHandler.ashx",
                data: "Type=SBCodeApply&SBCode=" + SBCode + "&SBPWD=" + SBPWD,
                success: function (result) {
                    var strs = result.split(';');

                    if (strs[0] == 0) {
                        alert("用户已存在！");
                        $("#regionEmail").focus();
                    }
                    else {
                        document.cookie = "MainID=" + strs[1] + ";IsFounder=" + strs[2] + ";path=/";
                        document.cookie = "UserID=" + strs[0] + ";IsFounder=" + strs[2] + ";path=/";
                        location.href = "/Modules/";

                        //location.href = "Main.aspx";
                    }
                },
                error: function (data) {
                    if (data.toString() == "error") {
                        alert("系统出错，请联系管理员！");
                    }
                }
            });
        }
    });




  /*----------------------------微信服务商平台使用开始------------------------------------------*/
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
        else {
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
        }
        return false;//微信端注册页面需要加这句话
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
    
    /*----------------------------微信服务商平台使用结束------------------------------------------*/





 




    function Login() {
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
                    location.href = "/Main.aspx";
                }
            },
            error: function (data) {
                if (data.toString() == "error") {
                    alert("系统出错，请联系管理员！");
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

    $("#userPwd").keydown(function (e) {
        var keyCode = e.which || e.keyCode;
        if (keyCode == 13) {
            Login();
            return false;
        }
    });

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
