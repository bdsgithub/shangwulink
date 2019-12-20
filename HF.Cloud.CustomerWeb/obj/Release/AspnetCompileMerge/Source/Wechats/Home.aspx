<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Wechats.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent">
    <meta name="format-detection" content="telephone=no">
    <meta name="full-screen" content="yes">
    <meta name="x5-page-mode" content="app">
    <script src="js/zepto.min.js"></script>
    <link rel="stylesheet" href="css/style.css">
    <title>易维客在线操作</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="operate">
            <div class="eweic-title">
                <p class="tab-operate-c" style="cursor:pointer;">工单统计&nbsp;&nbsp;</p>
                <p class="tab-operate-w" style="cursor:pointer;">维修统计</p>
                <div class="tab-operate">
                    <ul class="eweic-top">
                        <li class="operate-list-b">
                            <p>今天</p>
                        </li>
                        <li class="operate-list-blue">
                            <p class="operate-list-title">未受理</p>
                            <p class="operate-list-t">
                                <asp:Literal runat="server" ID="ltNoAccept1"></asp:Literal>
                            </p>
                        </li>
                        <li class="operate-list-blue">
                            <p class="operate-list-title">已完成</p>
                            <p class="operate-list-t">
                                <asp:Literal runat="server" ID="ltComplete1"></asp:Literal>
                            </p>
                        </li>
                        <li class="operate-list-blue">
                            <p class="operate-list-title">全部</p>
                            <p class="operate-list-t">
                                <asp:Literal runat="server" ID="ltAll1"></asp:Literal>
                            </p>
                        </li>
                    </ul>

                    <ul class="eweic-top">
                        <li class="operate-list-o">
                            <p>昨天</p>
                        </li>
                        <li class="operate-list-orange">
                            <p class="operate-list-title">未受理</p>
                            <p class="operate-list-t">
                                <asp:Literal runat="server" ID="ltNoAccept2"></asp:Literal>
                            </p>
                        </li>
                        <li class="operate-list-orange">
                            <p class="operate-list-title">已完成</p>
                            <p class="operate-list-t">
                                <asp:Literal runat="server" ID="ltComplete2"></asp:Literal>
                            </p>
                        </li>
                        <li class="operate-list-orange">
                            <p class="operate-list-title">全部</p>
                            <p class="operate-list-t">
                                <asp:Literal runat="server" ID="ltAll2"></asp:Literal>
                            </p>
                        </li>
                    </ul>
                    <ul class="eweic-top">
                        <li class="operate-list-g">
                            <p>本月</p>
                        </li>
                        <li class="operate-list-green">
                            <p class="operate-list-title">未受理</p>
                            <p class="operate-list-t">
                                <asp:Literal runat="server" ID="ltNoAccept3"></asp:Literal>
                            </p>
                        </li>
                        <li class="operate-list-green">
                            <p class="operate-list-title">已完成</p>
                            <p class="operate-list-t">
                                <asp:Literal runat="server" ID="ltComplete3"></asp:Literal>
                            </p>
                        </li>
                        <li class="operate-list-green">
                            <p class="operate-list-title">全部</p>
                            <p class="operate-list-t">
                                <asp:Literal runat="server" ID="ltAll3"></asp:Literal>
                            </p>
                        </li>
                    </ul>
                </div>
                <div class="tab-work">
                    <ul class="eweic-top">
                        <li class="operate-list-b">
                            <p>今天</p>
                        </li>
                        <li class="operate-list-blue">
                            <p class="operate-list-title">未受理</p>
                            <p class="operate-list-t">
                                <asp:Literal runat="server" ID="ltRNoAccept1"></asp:Literal>
                            </p>
                        </li>
                        <li class="operate-list-blue">
                            <p class="operate-list-title">已完成</p>
                            <p class="operate-list-t">
                                <asp:Literal runat="server" ID="ltRComplete1"></asp:Literal>
                            </p>
                        </li>
                        <li class="operate-list-blue">
                            <p class="operate-list-title">全部</p>
                            <p class="operate-list-t">
                                <asp:Literal runat="server" ID="ltRAll1"></asp:Literal>
                            </p>
                        </li>
                    </ul>
                    <ul class="eweic-top">
                        <li class="operate-list-o">
                            <p>昨天</p>
                        </li>
                        <li class="operate-list-orange">
                            <p class="operate-list-title">未受理</p>
                            <p class="operate-list-t">
                                <asp:Literal runat="server" ID="ltRNoAccept2"></asp:Literal>
                            </p>
                        </li>
                        <li class="operate-list-orange">
                            <p class="operate-list-title">已完成</p>
                            <p class="operate-list-t">
                                <asp:Literal runat="server" ID="ltRComplete2"></asp:Literal>
                            </p>
                        </li>
                        <li class="operate-list-orange">
                            <p class="operate-list-title">全部</p>
                            <p class="operate-list-t">
                                <asp:Literal runat="server" ID="ltRAll2"></asp:Literal>
                            </p>
                        </li>
                    </ul>
                    <ul class="eweic-top">
                        <li class="operate-list-g">
                            <p>本月</p>
                        </li>
                        <li class="operate-list-green">
                            <p class="operate-list-title">未受理</p>
                            <p class="operate-list-t">
                                <asp:Literal runat="server" ID="ltRNoAccept3"></asp:Literal>
                            </p>
                        </li>
                        <li class="operate-list-green">
                            <p class="operate-list-title">已完成</p>
                            <p class="operate-list-t">
                                <asp:Literal runat="server" ID="ltRComplete3"></asp:Literal>
                            </p>
                        </li>
                        <li class="operate-list-green">
                            <p class="operate-list-title">全部</p>
                            <p class="operate-list-t">
                                <asp:Literal runat="server" ID="ltRAll3"></asp:Literal>
                            </p>
                        </li>
                    </ul>
                </div>
            </div>

        </div>
        <div class="operate-list eweic-top">
            <ul>
                <li class="eweic-right-border">
                    <img src="img/yij.png" alt="">
                    <div class="operate-cont">
                        <p>一键下单</p>
                        <span>先看单再下单</span>
                    </div>
                    <a href="OrderSheet.aspx"></a>
                </li>
                <li>
                    <img src="img/baox.png" alt="">
                    <div class="operate-cont">
                        <p>一键报修</p>
                        <span>一键服务到家</span>
                    </div>
                    <a href="OrderRepair.aspx"></a>
                </li>
            </ul>
        </div>
        <div class="operate-list eweic-bottom-border">
            <ul>
                <li class="eweic-right-border">
                    <img src="img/renw.png" alt="">
                    <div class="operate-cont">
                        <p>任务跟踪</p>
                        <span>为你解决麻烦</span>
                    </div>
                    <a href="OrderList.aspx"></a>
                </li>
                <li>
                    <img src="img/fuw.png" alt="">
                    <div class="operate-cont">
                        <p>更多服务</p>
                        <span>欢迎来电</span>
                    </div>
                    <a href="MyInfo.aspx"></a>
                </li>
            </ul>
        </div>
        <script>
        $('.tab-operate-c').on('click',function(){
            $(this).css('color','#3f9cff')
            $('.tab-operate-w').css('color','#000')
            $('.tab-operate').show()
            $('.tab-work').hide()
        })
        $('.tab-operate-w').on('click',function(){
            $(this).css('color','#3f9cff')
            $('.tab-operate-c').css('color','#000')
            $('.tab-work').show()
            $('.tab-operate').hide()
        })
    </script>
    </form>
</body>
</html>
