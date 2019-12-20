<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderDetail.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Wechats.OrderDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent">
    <meta name="format-detection" content="telephone=no">
    <meta name="full-screen" content="yes">
    <meta name="x5-page-mode" content="app">
    <link rel="stylesheet" href="css/style.css">
    <script src="js/zepto.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="operate">
            <div class="eweic-title">
                <p>工单跟踪</p>
            </div>
        </div>

        <div class="eweic-chunk-white">
            <div class="eweic-chunk-cont">
                <p class="eweic-left">
                    <asp:Literal runat="server" ID="lttype"></asp:Literal>类型：
                </p>
                <span class="eweic-p-right">
                    <asp:Literal runat="server" ID="ltSheetType"></asp:Literal></span>
            </div>
        </div>
        <div class="eweic-chunk-white">
            <div class="eweic-chunk-cont">
                <p class="eweic-left">
                    工单描述： 
                </p>
                <span class="eweic-p-right">
                    <asp:Literal runat="server" ID="ltSheetDetail"></asp:Literal></span>
            </div>
        </div>
        <div class="eweic-chunk-white">
            <div class="eweic-chunk-cont">
                <p class="eweic-left">
                    所属客户：
                </p>
                <span class="eweic-p-right">
                    <asp:Literal runat="server" ID="ltClient"></asp:Literal></span>
            </div>
        </div>
        <div class="eweic-chunk-white">
            <div class="eweic-chunk-cont">
                <p class="eweic-left">
                    受理服务商：
                </p>
                <span class="eweic-p-right">
                    <asp:Literal runat="server" ID="ltMain"></asp:Literal></span>
            </div>
        </div>
        <div class="task-details eweic-top">
            <ul>
                <asp:Repeater runat="server" ID="repDetail">
                    <ItemTemplate>
                        <li>
                            <img src="img/l2.png" alt="">
                            <div class="task-details-cont ">
                                <p style="word-break: break-all;">
                                    <%#Eval("SendDetail") %>
                                </p>
                                <p>
                                    <%#Eval("sendtime") %>
                                </p>
                            </div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <div class="chunk"></div>

        <div class="eweic-bottom">
            <ul>
                <li class="operate-list-blue eweic-bottom-back">
                    <p>返回</p>
                    <a href="OrderList.aspx"></a>
                </li>
            </ul>
        </div>
        <script>

            //列表图片
            $(".task-details li").first().find("img").attr("src", "img/l1.png");
            $(".task-details li").last().find("img").attr("src", "img/l3.png");
        </script>
    </form>
</body>
</html>
