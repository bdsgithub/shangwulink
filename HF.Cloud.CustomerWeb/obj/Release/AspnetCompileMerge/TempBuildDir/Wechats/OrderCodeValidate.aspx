<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderCodeValidate.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Wechats.OrderCodeValidate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="/js/jquery-1.8.3.min.js"></script>
    <title></title>
    <script type="text/javascript">
        $(function () {
            //验证微信号是否绑定报修码,已绑定跳转到首页
            $.post("/ashx/Wechat.ashx", { op: "ValidateWeiXin", username: "15903649626" }, function (data) {
                if (data == "success") {
                    location.href = "/Wechats/Home.aspx";
                }
            });
            //验证
            $("#next").click(function () {
                if ($("#txtOrderCode").val() == "") {
                    alert("请输入报修码！");
                    return false;
                }
                //传入微信号和报修码，成功后跳转首页
                var username = "15903649626";
                location.href = "/Wechats/OrderCodeValidateDetail.aspx?username=" + username + "&ordercode=" + $("#txtOrderCode").val();
            });
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            服务码：<input type="text" id="txtOrderCode" />
            <br />
            <a id="next" href="javascript:void(0)">下一步</a>
        </div>
    </form>
</body>
</html>
