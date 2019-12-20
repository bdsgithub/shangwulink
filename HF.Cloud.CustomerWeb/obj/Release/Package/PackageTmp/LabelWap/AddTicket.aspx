<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddTicket.aspx.cs" Inherits="HF.Cloud.CustomerWeb.LabelWap.AddTicket" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0;" />
    <title>优惠券</title>
        <link href="../css/weui.min.css" rel="stylesheet" />
        <script src="../js/jquery-1.8.3.min.js"></script>

    <script type="text/javascript">

     
                function addAccount(MID,sum,tel)
                {
                    //提交数据库
                    var info = {};
                    info["mid"] = MID;//服务商id
                    info["sum"] = sum;//金额总数
                    info["tel"] = tel;//电话

                    $.post("ashx/addTicket.ashx", info, function (addData) {
                        if (addData == "success") {
                            alert("激活成功！");
                        }
                        else
                        {
                            alert("激活失败！");
                        }
                        location.href = "Wallet.aspx";
                    });
                }

               

    </script>
</head>
<body>
     
    <form id="form1" runat="server">
    <div>
      
      <div class="weui-cells__title">点击领取优惠券</div>
        <%=htmlStr %>
            <%--<div class="weui-cells" >
                <a class="weui-cell weui-cell_access" href="javascript:addAccount('187876545768','12.34');">
                    <div class="weui-cell__bd">
                        <p>首次优惠券</p>
                    </div>
                    <div class="weui-cell__ft">¥ 10.00</div>
                </a>
            </div>--%>
       
    </div>
    </form>
</body>
</html>
