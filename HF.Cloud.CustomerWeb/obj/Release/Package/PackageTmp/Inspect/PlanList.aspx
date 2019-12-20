<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlanList.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Inspect.PlanList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>巡检计划列表</title>
    <link type="text/css" href="/css/home.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('table tbody tr').click(function () {
                document.location.href = 'RecordList.aspx?pid=' + $(this).attr('id');
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class=" work-center float-r-s margin-top-s padding-s">
                <div class="main-title">
                    <h2 id="work-select" class="font-color-r">巡检中心
                        <span style="font-size: 14px; font-weight: normal; display: none;" class="float-right  button-s background-r">
                            <a href="" target="_self">设备搜索</a>
                        </span>
                    </h2>
                </div>
                <div class="tabl border-c margin-top-s">
                    <table cellpadding="0" cellspacing="0">
                        <thead>
                            <tr class="tab-color background-c">
                                <th style="height: 55px;">序号</th>
                                <th>客户名称</th>
                                <th>计划执行时间</th>
                                <th>计划执行人</th>
                                <th>巡检周期</th>
                                <th>周期单位</th>
                                <th>是否执行</th>
                                <th>执行人</th>
                                <th>执行时间</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptDataList" runat="server">
                                <ItemTemplate>
                                    <tr id='<%# Eval("ID") %>' style="cursor: pointer;">
                                        <td style="height: 55px;"><%# Container.ItemIndex+1 %></td>
                                        <td><%# HF.Cloud.BLL.ClientBL.GetClientNameByClientID( long.Parse(Eval("ClientID").ToString())) %></td>
                                        <td><%# ForamteDate(Eval("PlanDate").ToString()) %></td>
                                        <td><%# HF.Cloud.BLL.UserBLL.GetUserNameByUserId(long.Parse( Eval("UserID").ToString())) %></td>
                                        <td><%# Eval("PatrolCycle") %></td>
                                        <td><%# GetPatrolType( Eval("PatrolCycleType").ToString()) %></td>
                                        <td><%# Eval("Executed").ToString() == "False" ? "否" : "是" %></td>
                                        <td><%# HF.Cloud.BLL.UserBLL.GetUserNameByUserId(long.Parse(Eval("ExecuteUserID").ToString() == "" ? "0" : Eval("ExecuteUserID").ToString())) %></td>
                                        <td><%# ForamteDate(Eval("ExecuteDate").ToString()) %></td>
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
    </form>
</body>
</html>
