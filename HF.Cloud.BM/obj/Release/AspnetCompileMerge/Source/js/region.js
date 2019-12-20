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
