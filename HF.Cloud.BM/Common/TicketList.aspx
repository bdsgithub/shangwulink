<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TicketList.aspx.cs" Inherits="HF.Cloud.BM.Common.TicketList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>兑换券列表</title>
    <link type="text/css" href="/css/home.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="/js/cient.js"></script>
</head>
<body>
    <form id="form1" runat="server">
     <div>
         <div class=" work-center float-r-s margin-top-s padding-s">
                <div class="main-title">
                    <h2 id="work-select" class="font-color-r">兑换券
                        <a href="/Sheet/CustomerSheetSearch.aspx" target="_self" style="color: #fff;display:none;">
                            <span style="font-size: 14px; font-weight: normal;" class="float-right  button-s background-r button-s-mright">搜索</span>
                        </a>
                        <a href="TicketAdd.aspx" target="_self" style="color: #fff;">
                            <span style="font-size: 14px; font-weight: normal;" class="float-right  button-s background-r button-s-mright">新增</span>
                        </a>
                    </h2>
                </div>
                <div class="tabl border-c margin-top-s">
                    <table cellpadding="0" cellspacing="0">
                        <thead>
                            <tr class="tab-color background-c">
                                <td style="height: 55px">序号</td>
                                <td>激活码</td>
                                <td>金额</td>
                                <td>生成时间</td>
                                <td>过期时间</td>
                                <td>状态</td>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptDataList" runat="server" >
                                <ItemTemplate>
                                    <tr id='<%# Eval("ID") %>' style="cursor: pointer;">
                                        <td style="height: 55px;"><%# Container.ItemIndex +1 %></td>
                                        <td><%# Eval("ActiveCode") %></td>
                                        <td><%# Eval("ActiveMoney") %></td>
                                        <td><%# Eval("StartTime") %></td>
                                        <td><%# Eval("EndTime") %></td>
                                        <td><%# Eval("IsActive").ToString()=="False"?"未激活":"<span style='color:red;'>已激活</span>" %></td>
                                      
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
