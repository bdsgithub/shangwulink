<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RepairSearch.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Repair.RepairSearch" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>维修检索</title>
    <link type="text/css" href="/css/home.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="/js/cient.js"></script>
    <script type="text/javascript" src="/js/My97DatePicker/WdatePicker.js"></script>
    <link href="/dropdown/dropdown.css" rel="stylesheet" />
    <script type="text/javascript" src="/dropdown/dropdown.js"></script>
     <script type="text/javascript">
        $(document).ready(function () {
            $('table tbody tr').click(function () {
                document.location.href = '/Repair/RepairDetail.aspx?id=' + $(this).attr('id');
            });
        });
        //获取客户列表
        function GetClient(id, url) {
            //获取所属服务商
            onFocus(id, url);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class=" work-center float-r-s margin-top-s padding-s">
                <div class="main-title"><span style="color: #1fb5ac">维修中心></span>维修搜索</div>
                <div class="border-c padding-top-s margin-top-s overflow-h">
                    <div class="select float-l-s float-left margin-top-s">
                        任务编号：
                        <asp:TextBox ID="txtDeviceCode" runat="server"></asp:TextBox>
                    </div>
                    <div class="select float-l-s float-left margin-top-s">
                        所属客户：
                        <asp:TextBox ID="txtCustomer" runat="server" onfocus="GetClient('txtCustomer','/dropdownfilter/dfClient.aspx')"></asp:TextBox>
                        <asp:TextBox ID="txtCustomer_hidden" Style="display: none;" runat="server"></asp:TextBox>
                    </div>
                    <div class="select float-l-s float-left margin-top-s">
                        设备类型：
                        <asp:DropDownList runat="server" ID="ddlAsset" Style="height: 40px; width: 340px;"></asp:DropDownList>
                    </div>
                    <div class="select float-l-s float-left margin-top-s">
                        任务状态：
                        <asp:DropDownList runat="server" ID="ddlState" Style="height: 40px; width: 340px;"></asp:DropDownList>
                    </div>
                    <div class="select float-l-s float-left margin-top-s">
                        联系人：&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtLinkName" runat="server"></asp:TextBox>
                    </div>
                    <div class="select float-l-s float-left margin-top-s">
                        联系电话：
                        <asp:TextBox ID="txtTel" runat="server"></asp:TextBox>
                    </div>
                    
                    <div class="select float-l-s float-left margin-top-s">
                        发布时间：
                        <asp:TextBox ID="txtBeginTimeS" Width="166px" onfocus="WdatePicker({readOnly:true})" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtBeginTimeE" Width="166px" onfocus="WdatePicker({readOnly:true})" runat="server"></asp:TextBox>
                    </div>
                    
                    <br />
                    <div class="button margin-top-s float-left" style="margin-left: 20px">
                        
                            <asp:LinkButton ID="lnkBtnSearch" OnCommand="lnkBtnSearch_Command" CommandName="Search" runat="server"><div class="button-m background-r float-left ">搜索</div></asp:LinkButton>
                        
                        
                            <asp:LinkButton ID="lnkBtnReset" OnCommand="lnkBtnSearch_Command" CommandName="Reset" runat="server"><div class="button-m background-cc float-left" style="margin-left: 20px">重置</div></asp:LinkButton>
                        
                    </div>

                </div>
                <br />
                <p id="divSearchCount" runat="server" visible="false">
                    共搜索到
                    <span class="font-color-r ma">
                        <asp:Literal ID="ltlAssetCount" runat="server"></asp:Literal></span>
                    条设备
                </p>
                <div class="tabl border-c margin-top-s" runat="server" visible="false" id="divSearchResultList">
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
