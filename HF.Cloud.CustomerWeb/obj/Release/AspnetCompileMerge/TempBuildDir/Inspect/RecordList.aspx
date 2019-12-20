<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecordList.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Inspect.RecordList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>巡检中心</title>
    <link type="text/css" href="/css/home.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('table tbody tr').click(function () {
                document.location.href = 'RecordDetail.aspx?recordId=' + $(this).attr('id');
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
                                <th>服务商</th>
                                <th>巡检数量</th>
                                <th>当前状态</th>
                                <th>完成时间</th>
                                <th>操作</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptDataList" runat="server">
                                <ItemTemplate>
                                    <tr id='<%# Eval("recordId") %>' style="cursor: pointer;">
                                        <td style="height: 55px;"><%# Container.ItemIndex+1 %></td>
                                        <td><%# Eval("mainName") %></td>
                                        <td><%# Eval("AssetCount") %></td>
                                        <td><%# Eval("executed") %></td>
                                        <td><%# Eval("executeEndDate") %></td>
                                        <td><a>查看报告</a>
                                            <a href="RecordDetail.aspx?recordId=<%#Eval("recordId") %>">查看设备</a>
                                        </td>
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
