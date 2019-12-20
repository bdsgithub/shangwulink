<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AseetInfo.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Wap.AseetInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>设备详情</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent">
    <meta name="format-detection" content="telephone=no">
    <meta name="full-screen" content="yes">
    <meta name="x5-page-mode" content="app">
<%--    <link rel="stylesheet" href="/Wechats/css/style.css">--%>
    <script src="/Wechats/js/zepto.min.js"></script>
        <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.css" rel="stylesheet" >
    <link href="https://cdnjs.cloudflare.com/ajax/libs/weui/1.1.2/style/weui.min.css" rel="stylesheet">
    <style type="text/css">
       *{
            margin: 0;
            padding: 0;
        }
        body{
            background-color: #f5f5f5;
        }
        .weui-cell:before {
            content: " ";
            position: absolute;
            left: 0;
            top: 0;
            right: 0;
            height: 1px;
            border-top: 1px solid #e5e5e5;
            color: #e5e5e5;
            -webkit-transform-origin: 0 0;
            transform-origin: 0 0;
            -webkit-transform: scaleY(0.5);
            transform: scaleY(0.5);
            left: 15px;
            z-index: 2;
        }

       .clearfix:after{
            display: block;
            height: 0;
            content: "1";
            visibility: hidden;
            clear: both;
        }
        .header i{
            display: inline-block;
            margin: 10px;
            float: right;
            color: #1aad19;
            font-size: 24px;
        }
        .weui-cells__title i{
            color:#1aad19 ;
            display: inline-block;
            margin-right: 10px;
        }
        .title{
            clear: both;
            text-align: center;
            line-height: 60px;
            height: 60px;
            font-size: 20px;
            font-weight: bolder;
            color: rgba(8, 6, 13, 0.67);
        }
        .option{
            position: absolute;
            bottom: 10px;
            left: 10px;
            right: 10px;
            z-index: -1;
        }
        #ingeniero_login {
            display:none;
        }
    </style>
      <script type="text/javascript">
        $(function () {
            //工程师登录
            $("#ingeniero_login").click(function () {
                var cid = $("#hf_cid").val();
                var mid = $("#hf_mid").val();
                var sheetId = $("#hidSheetId").val();
                location.href = "Login.aspx?direction=Finish&cid=" + cid + "&mid=" + mid + "&sheetId=" + sheetId;
            });

            $("#WXMark").each(function () {
                    var temp = $(this).text().replace(/\n|\r\n/g, '<br/>');
                    $(this).html(temp);
                });


        });

        function repairBtn()
        {
            var repairBtn = $("#repairBtn").text();
            //alert(repairBtn);
            var cid=$("#hf_cid").val();
            var mid=$("#hf_mid").val();

            var assetId=$("#hidAssetId").val();
            var openid=$("#hf_openid").val();

            var sid=$("#hidSheetId").val();
            if (repairBtn == "一键报修") {
                location.href = "AddNewSheet.aspx?cid=" + cid + "&assetId=" + assetId + "&mid=" + mid + "&openid=" + openid;
            }
            else if (repairBtn == "查看工单状态") {
                location.href = "SheetStateTrack.aspx?sid=" + sid + "&cid=" + cid + "&mid=" + mid;
            }

        }



    </script>
</head>
<body>
    <div class="header">
        <i class="fa fa-sign-in" id="ingeniero_login" runat="server"></i>
        <div class="title"><label id="lbMainName" runat="server"></label>设备管理</div>
    </div>
    <form id="form1" runat="server">
      
       <%-- <div class="ingeniero_login_style" ><p id="ingeniero_login" runat="server">工程师登录</p></div>
        <div class="b-title de_m">易维客设备管理</div>
        <div class="eweic-chunk-white">
            <div class="eweic-chunk-cont">
                <p class="eweic-left">
                    设备编号：<asp:Label ID="lblDeviceCode" runat="server" Text=""></asp:Label>
                </p>
            </div>
        </div>
        <div class="eweic-chunk-white">
            <div class="eweic-chunk-cont">
                <p class="eweic-left">
                    设备类型：
                        <asp:Label ID="lblDeviceName" runat="server" Text=""></asp:Label>
                </p>
            </div>
        </div>
        <div class="eweic-chunk-white">
            <div class="eweic-chunk-cont">
                <p class="eweic-left">
                    设备品牌：<asp:Label ID="lblDeviceBrand" runat="server" Text=""></asp:Label>
                </p>
            </div>
        </div>
        <div class="eweic-chunk-white">
            <div class="eweic-chunk-cont">
                <p class="eweic-left">
                    设备型号：
                        <asp:Label ID="lblDeviceModel" runat="server" Text=""></asp:Label>
                </p>
            </div>
        </div>
        <div class="eweic-bottom">
            <ul>
                <li class="operate-list-blue eweic-bottom-back">
                    <p>
                        <asp:Literal runat="server" ID="ltBut">资产报修</asp:Literal></p>
                        <asp:LinkButton Width="100%" Style="display: block; color: white;" ID="lnkBtnSendRepair" runat="server" OnClick="lnkBtnSendRepair_Click"></asp:LinkButton>
                </li>
            </ul>
        </div>--%>




         <div class="content">
            <div class="weui-cells__title"><i class="fa fa-info" aria-hidden="true"></i>设备详情</div>
                <div class="weui-cells">
                   
                    <div class="weui-cell">
                        <div class="weui-cell__bd">
                            <p>设备编号</p>
                        </div>
                        <div class="weui-cell__ft" ><label id="lbDeviceCode" runat="server"></label></div>
                    </div>
                    <div class="weui-cell">
                        <div class="weui-cell__bd">
                            <p>设备类型</p>
                        </div>
                        <div class="weui-cell__ft"><label id="lbDeviceName" runat="server"></label></div>
                    </div>
                    <div class="weui-cell">
                        <div class="weui-cell__bd">
                            <p>品牌</p>
                        </div>
                        <div class="weui-cell__ft"><label id="lbDeviceBrand" runat="server"></label></div>
                    </div>
                    <div class="weui-cell">
                        <div class="weui-cell__bd">
                            <p>型号</p>
                        </div>
                        <div class="weui-cell__ft"><label id="lbDeviceModel" runat="server"></label></div>
                    </div>
                </div>
              <div class="weui-cell">
                 <p id="WXMark" runat="server" style="font-size:x-small"></p>
              </div>


        </div>
        <div class="option">
            <a href="javascript:" class="weui-btn weui-btn_primary" onclick="repairBtn();return false;"><p id="repairBtn" runat="server">一键报修</p></a>
        </div>








          <asp:HiddenField ID="hidAssetId" runat="server" />

        <asp:HiddenField ID="hidSheetId" runat="server" />
        <asp:HiddenField ID="hf_openid" runat="server" />


         <asp:HiddenField ID="hf_cid" runat="server" />
         <asp:HiddenField ID="hf_mid" runat="server" />


    </form>
</body>
</html>
