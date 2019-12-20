<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerSheetSearch.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Sheet.CustomerSheetSearch" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>工单搜索</title>
    <link type="text/css" href="/css/home.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="/js/cient.js"></script>
    <link href="/dropdown/dropdown.css" rel="stylesheet" />
    <script type="text/javascript" src="/dropdown/dropdown.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('table tbody tr').click(function () {
                document.location.href = '/Sheet/CustomerSheetDetails.aspx?id=' + $(this).attr('id');
            });
        });
        //获取客户列表
        function GetClient(id, url) {
            //获取所属服务商
            onFocus(id, url);
        }
    </script>
    <style>
        .select {
            width: 428px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class=" work-center float-r-s margin-top-s padding-s">
                <div class="main-title"><span style="color: #1fb5ac">工单中心></span>工单搜索</div>
                <div class="border-c padding-top-s margin-top-s overflow-h">
                    <div class="select float-l-s float-left margin-top-s">
                        工单编号：&nbsp;&nbsp;
                    <asp:TextBox ID="txtSheetCode" runat="server"></asp:TextBox>
                    </div>

                    <div class="select float-l-s float-left margin-top-s">
                        客户名称：&nbsp;&nbsp;
                    <asp:TextBox ID="txtCustomer" runat="server" onfocus="GetClient('txtCustomer','/dropdownfilter/dfClient.aspx')"></asp:TextBox>
                        <asp:TextBox ID="txtCustomer_hidden" Style="display: none;" runat="server"></asp:TextBox>
                    </div>

                    <div class="select float-l-s float-left margin-top-s">
                        工单类型：&nbsp;&nbsp;
                    <asp:DropDownList runat="server" ID="ddlSheetType" Style="height: 40px; width: 340px;"></asp:DropDownList>
                    </div>
                    <div class="select float-l-s float-left margin-top-s">
                        工单状态：&nbsp;&nbsp;
                        <asp:DropDownList runat="server" ID="ddlSheetState" Style="height: 40px; width: 340px;"></asp:DropDownList>
                        <%--<asp:TextBox ID="txtSheetState" runat="server"></asp:TextBox>--%>
                    </div>
                    <div class="select float-l-s float-left margin-top-s">
                        工单联系人：<asp:TextBox ID="txtLinkName" runat="server"></asp:TextBox>
                    </div>
                    <div class="select float-l-s float-left margin-top-s">
                        联系人电话：<asp:TextBox ID="txtLinkTel" runat="server"></asp:TextBox>
                    </div>
                    <div class="select float-l-s float-left margin-top-s">
                        发布时间：&nbsp;&nbsp;
                    <asp:TextBox ID="txtWriteTimeBegin" Width="166px" onfocus="WdatePicker({readOnly:true})" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtWriteTimeEnd" Width="166px" onfocus="WdatePicker({readOnly:true})" runat="server"></asp:TextBox>
                    </div>

                    <br />
                    <div class="button margin-top-s float-left" style="margin-left: 20px">

                        <asp:LinkButton ID="lnkBtnSearch" runat="server" OnClick="lnkBtnSearch_Click"><div class="button-m background-r float-left ">搜索</div></asp:LinkButton>


                        <asp:LinkButton ID="lnkBtnReset" runat="server" OnClick="lnkBtnReset_Click"> <div class="button-m background-cc float-left" id="reset" style="margin-left: 20px">重置</div></asp:LinkButton>

                    </div>
                </div>
                <br />
                <p id="divSearchCount">
                    共搜索到
                <span class="font-color-r ma">
                    <asp:Literal ID="ltlAssetCount" runat="server"></asp:Literal>
                </span>
                    条工单
                </p>
                <div class="tabl border-c margin-top-s" id="divSearchResultList">
                    <table cellpadding="0" cellspacing="0">
                        <thead>
                            <tr class="tab-color background-c">
                                <td style="height: 55px;">工单编号</td>
                                <td>类型</td>
                                <td>客户</td>
                                <td>受理人</td>
                                <td>工单状态</td>
                                <td>发布时间</td>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptDataList" runat="server">
                                <ItemTemplate>
                                    <tr id='<%# Eval("ID") %>' style="cursor: pointer;">
                                        <td style="height: 55px;">
                                            <a href='/Sheet/CustomerSheetDetails.aspx?id=<%# Eval("ID") %>'>
                                                <%# Eval("ID") %>
                                            </a></td>
                                        <td><%# Eval("TypeName") %></td>
                                        <td><%# Eval("ClientName") %></td>
                                        <td><%# Eval("acceptName") %></td>
                                        <td><%# Eval("sheetStatusName") %></td>
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
