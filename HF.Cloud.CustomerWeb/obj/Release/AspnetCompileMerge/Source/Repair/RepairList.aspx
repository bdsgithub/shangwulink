<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RepairList.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Repair.RepairList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>报修中心</title>
    <link type="text/css" href="/css/home.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="/js/cient.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('table tbody tr').click(function () {
                document.location.href = '/Repair/RepairDetail.aspx?id=' + $(this).attr('id');
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class=" work-center float-r-s margin-top-s padding-s">
                <div class="main-title">
                    <h2 id="work-select" class="font-color-r">维修中心
                            <a href="/Repair/RepairSearch.aspx" target="_self"><span style="font-size: 14px; font-weight: normal;" class="float-right  button-s background-r">维修搜索</span></a>
                    </h2>
                </div>
                <div class="tabl border-c margin-top-s">
                    <table cellpadding="0" cellspacing="0">
                        <thead>
                            <tr class="tab-color background-c">
                                <td style="height: 55px">任务编号</td>
                                <td>设备类型</td>
                                <td>客户</td>
                                <td>受理人</td>
                                <td>任务状态</td>
                                <td>发布时间</td>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptDataList" runat="server">
                                <ItemTemplate>
                                    <tr id='<%# Eval("ID") %>' style="cursor: pointer;">
                                        <td style="height: 55px;"><%# Eval("ID") %></td>
                                        <td><%# Eval("TypeName") %></td>
                                        <td><%# Eval("ClientName") %></td>
                                        <td><%# Eval("UserName") %></td>
                                        <td><%# Eval("StateCN") %></td>
                                        <td><%# Eval("WriteTime") %></td>
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
