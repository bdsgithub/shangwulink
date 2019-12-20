<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerAssetList.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Asset.CustomerAssetList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>设备列表</title>
    <link type="text/css" href="/css/home.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('table tbody tr').click(function () {
                document.location.href = 'CustomerAssetDetails.aspx?id=' + $(this).attr('id');
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class=" work-center float-r-s margin-top-s padding-s">
                <div class="main-title">
                    <h2 id="work-select" class="font-color-r">设备中心
                            <a href="CustomerAssetSearch.aspx" target="_self"><span style="font-size: 14px; font-weight: normal;" class="float-right  button-s background-r">设备搜索</span></a>
                    </h2>
                </div>
                <div class="tabl border-c margin-top-s">
                    <table cellpadding="0" cellspacing="0">
                        <thead>
                            <tr class="tab-color background-c">
                                <th>设备类型</th>
                                <th>品牌</th>
                                <th>型号</th>
                                <th style="height: 55px;">设备标签</th>
                                <th>所属客户</th>
                                <th>启用时间</th>
                                <%-- <th>过保时间</th>
                                <th>使用时长</th>
                                <th>维修次数</th>--%>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptDataList" runat="server">
                                <ItemTemplate>
                                    <tr id='<%# Eval("ID") %>' style="cursor: pointer;">

                                        <td><%# Eval("TypeName") %></td>
                                        <td><%# Eval("BrandName") %></td>
                                        <td><%# Eval("ModelName") %></td>
                                        <td style="height: 55px;"><%# Eval("QBCode") %></td>
                                        <td><%# Eval("ClientName") %></td>
                                        <td><%# Eval("BeginTime") %></td>
                                        <%--<td><%# Eval("ExcessTime") %></td>
                                        <td><%# Eval("useTime") %></td>
                                        <td><%# GetRepairTimes(long.Parse(Eval("ID").ToString())) %></td>--%>
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
