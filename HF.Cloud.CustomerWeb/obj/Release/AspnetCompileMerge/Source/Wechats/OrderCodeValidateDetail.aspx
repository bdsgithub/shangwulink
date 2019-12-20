<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderCodeValidateDetail.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Wechats.OrderCodeValidateDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="/js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
        $(function () {
            //页面加载获取
            $.getJSON("/ashx/Wechat.ashx", { op: "BindWeiXinValidate", username: $("#hidUserName").val(), ordercode: $("#hidOrderCode").val() }, function (data) {
                $("#client").html(data.clientName);
                $("#main").html(data.mainName);
            });
            //验证
            $("#next").click(function () {
                $.post("/ashx/Wechat.ashx", { op: "BindWeiXin", username: $("#hidUserName").val(), ordercode: $("#hidOrderCode").val() }, function (data) {
                    if (data == "success") {
                        location.href = "/Wechats/Home.aspx";
                    }
                    else {
                        alert("报修码有误！");
                    }
                });

            });
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                客户<span id="client"></span>
            </div>
            <div>
                服务商<span id="main"></span>
            </div>
            <a href="OrderCodeValidate.aspx">取消</a><a id="next" href="javascript:void(0)">下一步</a>
        </div>
        <asp:HiddenField runat="server" ID="hidUserName" />
         <asp:HiddenField runat="server" ID="hidOrderCode" />
    </form>
</body>
</html>
