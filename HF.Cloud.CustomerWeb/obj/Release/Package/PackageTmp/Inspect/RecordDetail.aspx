<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecordDetail.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Inspect.RecordDetail" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>执行详情</title>
    <link type="text/css" href="/css/home.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="/js/cient.js"></script>
    <script type="text/javascript" src="/js/getQueryString.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //获取巡检相关信息给控件赋值
            $.getJSON("/ashx/ExecutePatrol.ashx", { id: getQueryString("recordId"), op: "GetPatrolRecord" }, function (data) {
                $("#spClientName").text(data[0].clientName);
                $("#spPlanDate").text(data[0].planDate);
                $("#spPlanUser").text(data[0].planUser);
                $("#spExecuteUser").text(data[0].executeUserName);
                $("#spExecuteDate").text(data[0].executeDate);
                $("#spExecuteDateEnd").text(data[0].executeEndDate);
                $("#spClientContact").text(data[0].clientContact);
                $("#spClientTel").text(data[0].clientTel);
                $("#spAssetCount").text(data[0].assetExecutedCount + "台");
                if (data[0].executeEndDate == "") {
                    $("#spComplete").text("未完成");
                }
                else {
                    $("#spComplete").text("已完成");
                }
            })
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="main-right float-left">
            <div class="main-title"><span style="color: #1fb5ac">巡检中心></span>设备明细</div>
           <%-- <div class="border-c padding-top-s margin-top-s">
                &nbsp;&nbsp;&nbsp;<asp:Label ID="lblAssetName" runat="server" Text=""></asp:Label>
            </div>--%>
            <div class="border-c padding-top-s margin-top-s" style="overflow: hidden">
                <div class="xun">
                    <p>巡检客户：<span id="spClientName"></span></p>
                    <p id="spComplete" class="patrolAsset" style="width: 50px; height: 25px; background-color: #feb322; color: #fff; border-radius: 5px; line-height: 23px; text-align: center;"></p>
                    <p id="spAssetCount" class="patrolAsset" style="width: 50px; height: 25px; background-color: #feb322; color: #fff; border-radius: 5px; line-height: 23px; text-align: center;"></p>
                </div>
                <div class="xun">
                    <p>计划时间：<span id="spPlanDate"></span></p>
                    <p>计划执行人：<span id="spPlanUser"></span></p>
                </div>
                <div class="xun">
                    <p>执行人：<span id="spExecuteUser"></span></p>
                    <p>开始时间：<span id="spExecuteDate"></span></p>
                    <p>结束时间：<span id="spExecuteDateEnd"></span></p>
                </div>
                <div class="xun">
                    <p>客户联系人：<span id="spClientContact"></span></p>
                    <p>联系电话：<span id="spClientTel"></span></p>
                </div>
            </div>
            <div class="border-c padding-top-s margin-top-s">
                <div class="tabl border-c margin-top-s">
                    <table cellpadding="0" cellspacing="0">
                        <thead>
                            <tr class="tab-color background-c">
                                <td style="height: 55px;">序号</td>
                                <td>设备编号</td>
                                <td>设备名称</td>
                                <td>品牌</td>
                                <td>型号</td>
                                <td>设备状态</td>
                                <td>操作</td>
                                <%-- <td>执行时间</td>
                                <td>执行人</td>--%>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptDataList" runat="server">
                                <ItemTemplate>
                                    <tr style="height: 55px;">
                                        <td><%# Container.ItemIndex+1 %></td>
                                        <td><%# Eval("QBCode") %></td>
                                        <%--<td><%# Eval("AssetName") %></td>--%>
                                        <td><%# Eval("TypeName") %></td>
                                        <td><%# Eval("BrandName") %></td>
                                        <td><%# Eval("ModelName") %></td>
                                        <td><%# Eval("AssetStatus").ToString() == "1" ? "正常" : "故障"%></td>
                                        <td><a href="AssetDetail.aspx?id=<%#Eval("paID") %>">查看详情</a></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                    <div style="text-align: center; margin-top: 10px; margin-bottom: 10px;">
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager1_PageChanged" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" AlwaysShowFirstLastPageNumber="True" PagingButtonSpacing="10px"></webdiyer:AspNetPager>
                    </div>
                </div>
            </div>
        </div>
        <div class="main-l border-c float-right float-r-s" style="display: none;">
            <ul>
                <li class="background-c font-color-r" style="text-align: left; font-size: 20px;"><span>执行记录</span></li>
                <li>
                    <span>执行时间：</span><asp:Label ID="lblExecuteTime" runat="server" Text=""></asp:Label>
                    <br />
                    <span>执行人：</span><asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>
                    <br />
                    <span>是否确认：</span><asp:Label ID="lblClientConfirmed" runat="server" Text=""></asp:Label>
                    <br />
                    <span>确认人：</span><asp:Label ID="lblClientConfirmedUser" runat="server" Text=""></asp:Label>
                    <br />
                    <span>确认时间：</span><asp:Label ID="lblClientConfirmTime" runat="server" Text=""></asp:Label>
                    <br />
                </li>

                <li class="background-c font-color-r" style="text-align: left; font-size: 20px;"><span>客户信息</span></li>
                <li>
                    <span>客户名称:</span><asp:Label ID="lblCustomerName" runat="server" Text=""></asp:Label>
                    <br />
                </li>
            </ul>
        </div>
    </form>
</body>
</html>
