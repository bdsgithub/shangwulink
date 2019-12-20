<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="HF.Cloud.CustomerWeb.HomePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>主页</title>
    <link type="text/css" href="/css/home.css" rel="stylesheet" />
    <style>
        td {
            height: 55px;
            float: left;
            display: block;
            white-space: nowrap;
            text-overflow: ellipsis;
            -o-text-overflow: ellipsis;
            overflow: hidden;
            line-height: 50px;
        }
    </style>
    <script type="text/javascript" src="/js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="/js/cient.js"></script>
    <script type="text/javascript" src="/js/echarts.min.js"></script>
    <script type="text/javascript" src="/js/macarons.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.main-left table tbody tr').click(function () {
                document.location.href = '/Sheet/CustomerSheetDetails.aspx?id=' + $(this).attr('id');
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class=" main-right float-left" <%--style="width: 100%;margin-bottom:5px;"--%>>
            <div class="quantity" style="margin-top: 20px;">
                <ul>
                    <li class="background-c q-l">
                        <div class="day"></div>
                        <div class="figure">
                            <p class="font-color-r">
                                <asp:Literal ID="ltlTodayCount" runat="server"></asp:Literal>
                            </p>
                            <span>今天日工单数</span>
                        </div>
                    </li>
                    <li class="background-c q-l">
                        <div class="month"></div>
                        <div class="figure">
                            <p class="font-color-r">
                                <asp:Literal ID="ltlMonthCount" runat="server"></asp:Literal>
                            </p>
                            <span>本月工单数</span>
                        </div>
                    </li>
                    <li class="background-c q-l">
                        <div class="num"></div>
                        <div class="figure">
                            <p class="font-color-r">
                                <asp:Literal ID="ltlServiceCount" runat="server"></asp:Literal>
                            </p>
                            <span>服务设备数量</span>
                        </div>
                    </li>
                </ul>
            </div>
            <div class="border-c  margin-top-s" style="overflow: hidden;">
                <div id="sheetTypeTime" style="width: 950px; height: 350px; display: none" class="data"></div>
                <div class="main-left" style="width: 100%; font-size: 14px; margin-top: 0;">
                    <table cellpadding="0" cellspacing="0" style="width: 100%; text-align: center;">
                        <thead>
                            <tr class="tab-color background-c">
                                <td style="height: 55px"></td>
                                <td style="width: 50%; margin-left: 6%; text-align: left;">客户</td>
                                <td style="width: 10%; margin-left: 2%;">类型</td>
                                <td style="width: 10%; margin-left: 2%;">状态</td>
                                <td style="width: 12%; margin-left: 1%;">更新时间</td>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptDynamicSheet" runat="server">
                                <ItemTemplate>
                                    <tr id='<%# Eval("ID") %>' style="cursor: pointer;">
                                        <td style="width: 50%; margin-left: 6%; text-align: left;"><%# HF.Cloud.BLL.ClientBL.GetClientNameByClientID( long.Parse(Eval("ClientID").ToString()))%></td>
                                        <td style="width: 10%; margin-left: 2%;"><%# GetSheetTypeName(long.Parse(Eval("SheetType").ToString()))%></td>
                                        <td style="width: 10%; margin-left: 2%;"><%# HF.Cloud.BLL.DictionaryBL.GetDicName(HF.Cloud.BLL.Common.DictionaryType.SheetStatus, Eval("SheetState").ToString())%></td>
                                        <td style="width: 12%; margin-left: 1%;"><%# GetSheetStatusChangeTime(long.Parse( Eval("ID").ToString()))%></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>
            <div class="border-c padding-top-s margin-top-s" style="display: none;">
                <%--后期修改屏蔽不显示--%>
                <div class="data" style="width: 950px; height: 350px;">
                    <div style="width: 400px; float: right; text-align: right;">
                        <asp:DropDownList ID="ddlDataType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDataType_SelectedIndexChanged">
                            <asp:ListItem Value="customer" Text="客户">
                            </asp:ListItem>
                            <asp:ListItem Value="assetType" Text="设备分类">
                            </asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <br />
                    <div id="repairs" style="width: 950px; height: 350px;">
                    </div>
                </div>
            </div>
        </div>
        <div class="main-l border-c float-right float-r-s" style="margin-top:40px;">
            <ul>
                <li class="background-c font-color-r" style="text-align: left; font-size: 20px;"><span>服务商信息</span></li>
                <li>
                    <span>服务商:</span><asp:Label ID="lblMainName" runat="server"></asp:Label>
                    <br />
                    <span>联系人:</span><asp:Label ID="lblMainUser" runat="server"></asp:Label>
                    <br />
                    <span>电话:</span><asp:Label ID="lblMainTel" runat="server"></asp:Label>
                    <br />
                    <span>投诉电话：</span><asp:Label ID="lblMainTS" runat="server"></asp:Label>
                    <br />

                </li>
            </ul>
        </div>
    </form>

    <%--$(document).ready(function () {
            var sheetTypeTime = echarts.init(document.getElementById('sheetTypeTime'), 'macarons');
            sheetTypeTime.setOption({
                title: {
                    text: '工单统计',
                    //subtext: '指定时间内每种工单类型的工单数据'
                },
                tooltip: {
                    trigger: 'axis'
                },

                legend: {
                    data: ['工单数量']
                },
                xAxis: {
                    name: '日期',
                    type: 'category',
                    data: [<%= x_axis.ToString()%>]
                },
                yAxis: {
                    name: '工单数量',
                    type: 'value'
                },
                <%= series.ToString()%>
            });
            var divrepair = document.getElementById('repairs');
            var repairsCount = echarts.init(divrepair, 'macarons');

            repairsCount.setOption({
                title: {
                    text: '故障统计',
                    //subtext: '每个客户分类的工单数量或客户数量'
                },
                tooltip: {
                    trigger: 'axis'
                },
                toolbox: {
                    show: false,
                    feature: {
                        dataZoom: {},
                        dataView: { readOnly: false },
                        magicType: { type: ['line', 'bar'] },
                        restore: {},
                        saveAsImage: {}
                    }
                },
                legend: {
                    data: ['<%= x_axisName%>']
                },
                xAxis: {
                    name: '<%= x_axisName%>',
                    type: 'category',
                    data: [<%= x_axisRepairs.ToString()%>]
                },
                yAxis: {
                    name: '故障数量',
                    type: 'value'
                },
                <%= seriesRepairs.ToString()%>
            });
        });--%>
</body>
</html>
