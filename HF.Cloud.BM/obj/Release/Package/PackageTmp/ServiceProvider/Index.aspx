<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="HF.Cloud.BM.ServiceProvider.Index" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>服务商管理</title>
    <link type="text/css" href="/css/home.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="/js/cient.js"></script>
    <script>
        $(function () {
            $('table tbody td a[id*=LnkBtnStopUse]').click(function () {
                if ($(this).html() == '启用') {
                    if (!confirm('确认启用吗？')) return false;
                }
                else if ($(this).html() == '停用') {
                    if (!confirm('确认停用吗？')) return false;
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class=" work-center float-r-s margin-top-s padding-s">
                <div class="main-title">
                    <h2 id="work-select" class="font-color-r">服务商
                        <a href="/Sheet/CustomerSheetSearch.aspx" target="_self" style="color: #fff;display:none;">
                            <span style="font-size: 14px; font-weight: normal;" class="float-right  button-s background-r button-s-mright">搜索</span>
                        </a>
                        <a href="add.aspx" target="_self" style="color: #fff;">
                            <span style="font-size: 14px; font-weight: normal;" class="float-right  button-s background-r button-s-mright">新增</span>
                        </a>
                    </h2>
                </div>
                <div class="tabl border-c margin-top-s">
                    <table cellpadding="0" cellspacing="0">
                        <thead>
                            <tr class="tab-color background-c">
                                <td style="height: 55px">序号</td>
                                <td>名称</td>
                                <td>账户</td>
                                <td>联系人</td>
                                <td>联系电话</td>
                                <td>投诉电话</td>
                                <td>余额</td>
                                <td>注册时间</td>
                                <td>状态</td>
                                <td>&nbsp;</td>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptDataList" runat="server" OnItemDataBound="rptDataList_ItemDataBound">
                                <ItemTemplate>
                                    <tr id='<%# Eval("ID") %>' style="cursor: pointer;">
                                        <td style="height: 55px;"><%# Container.ItemIndex +1 %></td>
                                        <td><%# Eval("SBName") %></td>
                                        <td><%# Eval("SBMobile") %></td>
                                        <td><%# Eval("SBContact") %></td>
                                        <td><%# Eval("SBTel") %></td>
                                        <td><%# Eval("ComplainTel") %></td>
                                        <td><%# Eval("AccountMoney") %></td>
                                        <td><%# Eval("ApplyTime") %></td>
                                        <td><%# Eval("Valid").ToString()=="1"?"正常":"<span style='color:red;'>已停用</span>" %></td>
                                        <td>
                                            <asp:LinkButton ID="LnkBtnStopUse" CommandName="Stop" CommandArgument='<%# Eval("ID") %>' OnCommand="LnkBtnDelete_Command" runat="server">停用</asp:LinkButton>
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
