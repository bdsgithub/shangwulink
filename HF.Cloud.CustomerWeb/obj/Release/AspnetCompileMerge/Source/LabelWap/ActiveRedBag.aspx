<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActiveRedBag.aspx.cs" Inherits="HF.Cloud.CustomerWeb.LabelWap.ActiveRedBag" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0;" />
    <title>激活红包</title>
      <link href="../css/weui.min.css" rel="stylesheet" />
    <script src="../js/jquery-1.8.3.min.js"></script>
     <style type="text/css">
      
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#lnkBtnAdd").click(function () {
                if ($("#txtActiveCode").val().trim() == "" || $("#txtActiveCode").val().trim() == "请输入激活码") {
                    $("#verify").text("请输入激活码！")
                    $("#verify").css("display", "block");
                    return false;
                }
                if ($("#txtActiveCode").val().length !=12) {
                    $("#verify").text("激活码有误！")
                    $("#verify").css("display", "block");
                    return false;
                }

                //提交数据库
                var info = {};
                info["mid"] = $("#hf_mid").val();//服务商ID
                info["activeCode"] = $("#txtActiveCode").val();//激活码
                $.post("ashx/ActiveRedBag.ashx", info, function (Data) {
                    if (Data == "success") {
                        alert("激活成功！");
                        location.href = "Wallet.aspx";
                    }
                    else {
                        alert(Data);
                        location.href = "Wallet.aspx";
                    }
                })
                return false;


            })

        });


    </script>
</head>
<body>
    <form id="form1" runat="server">
      <div class="weui-cells weui-cells_form">
                 <div class="weui-cell">
                    <div class="weui-cell__hd"><label for="" class="weui-label">输入激活码：</label></div>
                    <div class="weui-cell__bd">
                    <input class="weui-input" value="" placeholder="请输入激活码" id="txtActiveCode" runat="server">
                    </div>
                 </div>
                 <div>
                     <label id="verify" style="color:red;margin-top:20px;margin-left:10px;margin-bottom:20px; display:none;"> </label>
                 </div>

            
             <div class="weui-btn-area">
                 <div>
                      <asp:LinkButton CssClass="weui-btn weui-btn_primary" ID="lnkBtnAdd"   runat="server">领 取</asp:LinkButton>
                 </div>
             </div>
            </div>
           <asp:HiddenField ID="hf_mid" runat="server" />

    </form>
</body>
</html>
