<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Wallet.aspx.cs" Inherits="HF.Cloud.CustomerWeb.LabelWap.Wallet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0;" />
    <title>钱包</title>
      <link href="../css/weui.min.css" rel="stylesheet" />
    <script src="../js/jquery-1.8.3.min.js"></script>
    <style type="text/css">
      
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#Main_Ticket").click(function () {
                location.href = "AddTicket.aspx";//优惠券页面
            });

            $("#lnkBtnAdd").click(function () {
                location.href = "AddMoney.aspx";//充值页面
                return false;
            });

            $("#lnkBtnRedBag").click(function () {
                location.href = "ActiveRedBag.aspx";//激活红包页面
                return false;
            });

        });


    </script>
</head>
<body>
    <form id="form1" runat="server">
     

            <div class="weui-cells weui-cells_form">
                <div class="weui-form-preview__hd">
                    <div class="weui-form-preview__item">
                        <label class="weui-form-preview__label">余额</label>
                        <em class="weui-form-preview__value">¥<label id="lbYue" runat="server"></label></em>
                    </div>
                </div>
                 <div class="weui-form-preview__hd" id="Main_Ticket">
                    <div class="weui-form-preview__item">
                        <label class="weui-form-preview__label">优惠券</label>
                        <em class="weui-form-preview__value"><label id="lbTicket" runat="server" ></label>个   </em>
                    </div>
                </div>

            
             <div class="weui-btn-area">
                 <div>
                      <asp:LinkButton CssClass="weui-btn weui-btn_primary" ID="lnkBtnAdd"   runat="server">充 值</asp:LinkButton>
                 </div>
                 <br />
                  <div>
                      <asp:LinkButton CssClass="weui-btn weui-btn_primary" ID="lnkBtnRedBag"   runat="server">兑换红包</asp:LinkButton>
                 </div>
             </div>
            </div>
       
    </form>
</body>
</html>
