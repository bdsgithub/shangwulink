<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddMoney.aspx.cs" Inherits="HF.Cloud.CustomerWeb.LabelWap.AddMoney" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0;" />
    <title>充值</title>
    <link href="../css/weui.min.css" rel="stylesheet" />
    <script src="../js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">

        var appId = "";
        var timeStamp = "";
        var nonceStr = "";
        var package_pay = "";
        var paySign = "";

        $(function () {
            var m = document.getElementById("txtPayMoney");
            m.onkeyup = function () {
                var demovalue = this.value;
                this.value = demovalue.replace(/\D/g, "");
            };

            $("#lnkBtnAdd").click(function () {
                if ($("#txtPayMoney").val().trim() == "" || $("#txtPayMoney").val().trim() == "请输入金额") {
                    $("#verify").text("请输入金额！")
                    $("#verify").css("display", "block");
                    return false;
                }
                if ($("#txtPayMoney").val() == 0) {
                    $("#verify").text("金额不可为0！")
                    $("#verify").css("display", "block");
                    return false;
                }

                //提交数据库
                var info = {};
                info["openid"] = $("#hf_openid").val();//openid
                info["paymoney"] = $("#txtPayMoney").val();//充值金额
                $.post("ashx/GetPrePayid.ashx", info, function (Data) {
                    //alert(Data);
                    if (Data != "") {
                        var jsonData = JSON.parse(Data);//转换成json
                        if (jsonData.data == "success") {
                            //"appId": "wx7c4f0b7254a7ee13",     //公众号名称，由商户传入     
                            //    "timeStamp": "1395712654",         //时间戳，自1970年以来的秒数     
                            //    "nonceStr": "e61463f8efa94090b1f366cccfbbb444", //随机串     
                            //    "package": "prepay_id=u802345jgfjsdfgsdg888",
                            //    "signType": "MD5",         //微信签名方式：     
                            //    "paySign": "70EA570631E4BB79628FBCA90534C63FF7FADD89" //微信签名 
                            appId = jsonData.appId;
                            timeStamp = jsonData.timeStamp
                            nonceStr = jsonData.nonceStr;
                            package_pay = jsonData.package;
                            paySign = jsonData.paySign;
                            //alert(appId);

                            callpay();//调用维修支付jsapi
                            //location.href = "Pay.aspx?appId=" + Data.appId + "&timeStamp=" + Data.timeStamp + "&nonceStr=" + Data.nonceStr + "&package=" + Data.package + "&signType=" + Data.signType + "&paySign="+Data.paySign;
                        }
                    }
                  
                });
                return false;

            });
        })




        //参考钉钉的接口做
        //点击充值后ashx执行代码，不用click后台执行，得到的数据后执行下面的语句

        function onBridgeReady() {
            WeixinJSBridge.invoke(
                   'getBrandWCPayRequest', {
                       "appId":appId,     //公众号名称，由商户传入     
                       "timeStamp": timeStamp,         //时间戳，自1970年以来的秒数     
                       "nonceStr": nonceStr, //随机串     
                       "package": package_pay,//"prepay_id=u802345jgfjsdfgsdg888",     
                        "signType": "MD5",         //微信签名方式：     
                        "paySign": paySign //微信签名 
                },
                function (res) {
                    //alert(res.err_msg);
                    if (res.err_msg == "get_brand_wcpay_request:ok") { // 使用以上方式判断前端返回,微信团队郑重提示：res.err_msg将在用户支付成功后返回    ok，但并不保证它绝对可靠。 
                        //支付成功，修改服务商余额，添加充值记录
                        //提交数据库
                        var info = {};
                        info["mid"] = $("#hf_mid").val();//服务商ID
                        info["openid"] = $("#hf_openid").val();//openid
                        info["paymoney"] = $("#txtPayMoney").val();//充值金额
                        $.post("ashx/UpdateMoney.ashx", info, function (Data) {
                            if (Data == "success") {
                                alert("充值成功！");
                                location.href = "Wallet.aspx";
                            }
                            else
                            {
                                alert("充值失败！");
                                location.href = "Wallet.aspx";
                            }
                        })
                    }
                    else
                    {
                        alert("充值失败！");
                        location.href = "Wallet.aspx";
                    }
                }
            );
        }

        function callpay() {
            //alert(paySign);
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
    <div>
    
            <div class="weui-cells weui-cells_form">
               
                
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label for="" class="weui-label">金额：</label></div>
                    <div class="weui-cell__bd">
                    <input class="weui-input" value="" placeholder="请输入金额" id="txtPayMoney" runat="server">
                    </div>
                </div>
                <div>
                     <label id="verify" style="color:red;margin-top:20px;margin-left:10px;margin-bottom:20px; display:none;"> </label>
                </div>
            
             <div class="weui-btn-area">
                 <div>
                      <asp:LinkButton CssClass="weui-btn weui-btn_primary" ID="lnkBtnAdd"   runat="server" OnClick="lnkBtnAdd_Click">确 定</asp:LinkButton>
                 </div>
             </div>
            </div>
    </div>
        <asp:HiddenField ID="hf_openid" runat="server" />
         <asp:HiddenField ID="hf_mid" runat="server" />

    </form>
</body>
</html>
