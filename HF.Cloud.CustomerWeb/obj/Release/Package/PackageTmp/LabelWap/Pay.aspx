<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pay.aspx.cs" Inherits="HF.Cloud.CustomerWeb.LabelWap.Pay" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0;" />
    <title>支付</title>
      <link href="../css/weui.min.css" rel="stylesheet" />
    <script src="../js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript"> 
            var appId = "<%=appId%>";
            var timeStamp = "<%=timeStamp %>";
            var nonceStr = "<%=nonceStr %>";
            var package_pay = "<%=package %>";
            var signType = "<%=signType %>";
            var paySign = "<%=paySign %>";

        function onBridgeReady() {
            WeixinJSBridge.invoke(
                   'getBrandWCPayRequest', {
                       "appId": appId,     //公众号名称，由商户传入     
                       "timeStamp": timeStamp,         //时间戳，自1970年以来的秒数     
                       "nonceStr": nonceStr, //随机串     
                       "package": package_pay,//"prepay_id=u802345jgfjsdfgsdg888",     
                       "signType": "MD5",         //微信签名方式：     
                       "paySign": paySign //微信签名 
                   },
                function (res) {
                    if (res.err_msg == "get_brand_wcpay_request:ok") { // 使用以上方式判断前端返回,微信团队郑重提示：res.err_msg将在用户支付成功后返回    ok，但并不保证它绝对可靠。 
                        alert("充值成功！");
                    }    
                }
            );
        }


        function callpay(){
            if (typeof WeixinJSBridge == "undefined") {
                if (document.addEventListener) {
                    document.addEventListener('WeixinJSBridgeReady', onBridgeReady, false);
                } else if (document.attachEvent) {
                    document.attachEvent('WeixinJSBridgeReady', onBridgeReady);
                    document.attachEvent('onWeixinJSBridgeReady', onBridgeReady);
                }
            } else {
                onBridgeReady();
            }
        }
      
        
      
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <br/><br/>
            <asp:Button ID="submit" runat="server" Text="立即支付" OnClientClick="callpay()" style="width:210px; height:50px; border-radius: 15px;background-color:#00CD00; border:0px #FE6714 solid; cursor: pointer;  color:white;  font-size:16px;" />
	  
      </div>


        <div class="weui-btn-area">
                 <div>
                      <asp:LinkButton CssClass="weui-btn weui-btn_primary" ID="lnkBtnAdd"   runat="server">充 值</asp:LinkButton>
                 </div>
        </div>


    </form>
</body>
</html>
