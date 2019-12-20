<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="manage.aspx.cs" Inherits="HF.Cloud.CustomerWeb.LabelWap.manage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0;" />
    <title>管理页面</title>
    <link href="../css/weui.min.css" rel="stylesheet" />
    <link href="../css/slider.css" rel="stylesheet" />
    <link href="../css/font-awesome.min.css" rel="stylesheet" />
    <script src="../js/jquery-1.8.3.min.js"></script>
    <script src="../js/jquery.themepunch.plugins.min.js"></script>
    <script src="../js/jquery.themepunch.revolution.min.js"></script>
    <style type="text/css">
        .weui-grid__icon
        {
            color:dodgerblue;    /*图标的颜色*/
        }
    </style>
    <script>
        $(document).ready(function () {
            $('.tp-banner').revolution({
                delay: 5000,
                startwidth: 1200,
                startheight: 500,
                hideThumbs: 10,
                fullWidth: "on",
                forceFullWidth: "on",
                onHoverStop: "on"
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="tp-banner-container">
                <div class="tp-banner">
                    <ul>
                        <li data-transition="slideleft" data-slotamount="14">
                            <img src="images/banner01.jpg" />
                        </li>
                        <li data-transition="boxslide" data-slotamount="7" data-masterspeed="300">
                            <img src="images/banner02.jpg" alt="darkblurbg" data-fullwidthcentering="on" />
                        </li>
                    </ul>
                    <div class="tp-bannertimer"></div>
                </div>
            </div>
            <div>
                <div class="weui-cells__title">
                    <div style="width: 50%; float: left;"><%= MainName %></div>
                    <div style="display: inline; margin-left: 35%;">
                    </div>
                </div>
            </div>
            <div style="height: auto; overflow: hidden; display: inline;">
                <div class="weui-grids">
                    <a href="/LabelWap/Account.aspx" class="weui-grid">
                        <div class="weui-grid__icon">
                            <i class="fa fa-user-o fa-2x"></i>
                        </div>
                        <p class="weui-grid__label">账户</p>
                    </a>
                    <a href="/LabelWap/labelmanage.aspx" class="weui-grid">
                        <div class="weui-grid__icon">
                            <i class="fa fa-wpforms fa-2x"></i>
                        </div>
                        <p class="weui-grid__label">标签</p>
                    </a>
                    <a href="/LabelWap/labeladd.aspx" class="weui-grid">
                        <div class="weui-grid__icon">
                            <span class="fa fa-plus-square-o fa-2x"></span>
                        </div>
                        <p class="weui-grid__label">添加标签</p>
                    </a>
                    <a href="/LabelWap/Wallet.aspx" class="weui-grid">
                        <div class="weui-grid__icon"> 
                            <span class="fa fa-credit-card fa-2x"></span>
                        </div>
                        <p class="weui-grid__label">钱包</p>
                    </a>
                    <a href="javascript:;" class="weui-grid">
                        <div class="weui-grid__icon">
                            <span class="fa fa-spinner fa-2x"></span>
                        </div>
                        <p class="weui-grid__label">敬请期待</p>
                    </a>
                    <a href="javascript:;" class="weui-grid">
                        <div class="weui-grid__icon">
                            <span class="fa fa-spinner fa-2x"></span>
                        </div>
                        <p class="weui-grid__label">敬请期待</p>
                    </a>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
