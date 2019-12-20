<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerAssetDetails.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Asset.CustomerAssetDetails" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>设备详情</title>
    <link type="text/css" href="/css/home.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="/js/cient.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="main-right float-left">
            <div class="main-title"><span style="color: #1fb5ac">设备中心></span>设备查看</div>
            <div class="border-c padding-top-s margin-top-s" style="padding-top: 0px;">
                <div class="work-title">
                    <h2>
                        <asp:Label ID="lblAssetName" runat="server" Text=""></asp:Label></h2>
                </div>
            </div>
            <div class="border-c padding-top-s margin-top-s">
                工单记录
                <div class="tabl border-c margin-top-s">
                    <table cellpadding="0" cellspacing="0">
                        <thead>
                            <tr class="tab-color background-c">
                                <td style="height: 55px;">工单编号</td>
                                <td>工单类型</td>
                                <td>受理人</td>
                                <td>工单状态</td>
                                <td>发布时间</td>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptDataList" runat="server">
                                <ItemTemplate>
                                    <tr style="height: 55px;">
                                        <td><%# Eval("ID") %></td>
                                        <td><%# HF.Cloud.BLL.SheetBL.GetSheetTypeNameBySheetTypeId(long.Parse( Eval("SheetType").ToString())) %></td>
                                        <td><%# GetAcceptsInfo(long.Parse( Eval("ID").ToString())) %></td>
                                        <td><%# HF.Cloud.BLL.DictionaryBL.GetDicName(HF.Cloud.BLL.Common.DictionaryType.SheetStatus,( Eval("SheetState").ToString())) %></td>
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

            <div class="border-c padding-top-s margin-top-s" id="drepair">
                维修记录
                <div class="tabl border-c margin-top-s">
                    <table cellpadding="0" cellspacing="0">
                        <thead>
                            <tr class="tab-color background-c">
                                <td style="height: 55px;">任务编号</td>
                                <td>设备类型</td>
                                <td>受理人</td>
                                <td>任务状态</td>
                                <td>发布时间</td>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="repRepair" runat="server">
                                <ItemTemplate>
                                    <tr style="height: 55px;">
                                        <td><%# Eval("ID") %></td>
                                        <td><%# HF.Cloud.BLL.AssetBL.GetAssetTypeNameByTypeId(long.Parse( Eval("AssetTypeID").ToString())) %></td>
                                        <td><%# GetAcceptsInfo(long.Parse( Eval("ID").ToString())) %></td>
                                        <td><%# HF.Cloud.BLL.DictionaryBL.GetDicName(HF.Cloud.BLL.Common.DictionaryType.RepairTaskStatus,( Eval("TaskState").ToString())) %></td>
                                        <td><%# Eval("WriteTime") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                    <div style="text-align: center; margin-top: 10px; margin-bottom: 10px;">
                        <webdiyer:AspNetPager ID="AspNetPager2" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager2_PageChanged" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" AlwaysShowFirstLastPageNumber="True" PagingButtonSpacing="10px"></webdiyer:AspNetPager>
                    </div>
                </div>
            </div>
        </div>
        <div class="main-l border-c float-right float-r-s">
            <ul>
                <li class="background-c font-color-r" style="text-align: left; font-size: 20px;"><span>设备信息</span></li>
                <li>
                    <span>创建时间：</span><asp:Label ID="lblCreateTime" runat="server" Text=""></asp:Label>
                    <br />
                    <span>设备类型：</span><asp:Label ID="lblAssetTypeName" runat="server" Text=""></asp:Label>
                    <br />
                    <span>品牌：</span><asp:Label ID="lblBrandName" runat="server" Text=""></asp:Label>
                    <br />
                    <span>型号：</span><asp:Label ID="lblModelName" runat="server" Text=""></asp:Label>
                    <br />
                    <span>启用时间：</span><asp:Label ID="lblBeginTime" runat="server" Text=""></asp:Label>
                    <br />
                    <span>过保时间：</span><asp:Label ID="lblExcessTime" runat="server" Text=""></asp:Label>
                    <br />
                </li>

                <li class="background-c font-color-r" style="text-align: left; font-size: 20px;"><span>客户信息</span></li>
                <li>
                    <div style="padding-left:5px;">
                        <table>
                            <tr>
                                <td style="width:80px; vertical-align:top;">客户名称:</td>
                                <td><asp:Label ID="lblCustomerName" runat="server" Text=""></asp:Label></td>
                            </tr>
                        </table>
                    </div>
                    <div>
                        <span>地址:</span><asp:Label ID="lblCustomerLink" runat="server" Text=""></asp:Label>
                    </div>
                </li>
            </ul>
        </div>
    </form>
</body>
</html>
