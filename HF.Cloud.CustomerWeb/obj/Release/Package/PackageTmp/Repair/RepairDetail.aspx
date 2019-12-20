<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RepairDetail.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Repair.RepairDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>任务查看</title>
    <link type="text/css" href="/css/home.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="/js/cient.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tbasset tbody tr').click(function () {
                location.href = '/Asset/CustomerAssetDetails.aspx?id=' + $(this).attr('id');
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="main-right float-left">
            <div class="main-title">
                <span style="color: #1fb5ac">维修中心></span>任务查看
            </div>
            <div class="border-c padding-top-s margin-top-s">
                <div class="work-title">
                    <h2>
                        <asp:Label ID="lblSheetTitle" runat="server"></asp:Label></h2>
                    <br />
                    <p>
                        <asp:Label ID="lblSheetDetail" runat="server"></asp:Label>
                    </p>
                    <br />
                    <p class="border-bom"></p>
                    <h2 class="margin-top-s">维修跟踪</h2>
                    <div class="work-record">
                        <ul>
                            <asp:Repeater runat="server" ID="repDetail">
                                <ItemTemplate>
                                    <li>
                                        <img src="/Wechats/img/l2.png" alt="">
                                        <div class="task-details-cont ">
                                            <p>
                                                <%#Eval("SendDetail") %>
                                            </p>
                                            <p><%#Eval("sendTime") %></p>
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>

                        </ul>
                    </div>
                    <p class="border-bom"></p>
                    <h2 class="margin-top-s">维修设备</h2>
                    <div class="tabl border-c margin-top-s">
                        <table cellpadding="0" cellspacing="0" id="tbasset">
                            <thead>
                                <tr class="tab-color background-c">
                                    <td>编号</td>
                                    <td style="height: 55px;">设备类型</td>
                                    <td>品牌</td>
                                    <td>型号</td>
                                    <td>维修进度</td>
                                    <td>更换零件</td>
                                    <td>总结</td>
                                    <td>图片</td>
                                    <td>记录时间</td>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptDataList" runat="server">
                                    <ItemTemplate>
                                        <tr id='<%# Eval("assetID") %>' style="cursor: pointer;">
                                            <td style="height: 55px;">
                                                <a href='/Asset/CustomerAssetDetails.aspx?id=<%# Eval("assetID") %>'>
                                                    <%# Eval("assetID") %>
                                                </a></td>
                                            <td><%# Eval("TypeName") %></td>
                                            <td><%# Eval("BrandName") %></td>
                                            <td><%# Eval("ModelName") %></td>
                                            <td><%# Eval("SpdName") %></td>
                                            <td><%# GetChangePart()%></td>
                                            <td><%# Eval("RepairSummary") %></td>
                                            <td><%#GetPic() %></td>
                                            <td><%# Eval("WriteDate") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>

                    </div>
                </div>
            </div>
        </div>

        <div class="main-l border-c float-right float-r-s">
            <ul>
                <li class="background-c font-color-r" style="text-align: left; font-size: 20px;"><span>工单信息</span></li>
                <li>
                    <span>任务编号:</span><asp:Label ID="lblSheetCode" runat="server"></asp:Label>
                    <br />
                    <span>发布时间:</span><asp:Label ID="lblWriteTime" runat="server"></asp:Label>
                    <br />
                    <span>任务状态:</span><asp:Label ID="lblSheetState" runat="server"></asp:Label>
                    <br />
                    <span>任务类型：</span><asp:Label ID="lblSheetType" runat="server"></asp:Label>
                    <br />
                    <span>优先级:</span><asp:Label ID="lblSheetPriority" runat="server"></asp:Label>
                    <br />
                    <span>受理人:</span><asp:Label ID="lblAccept" runat="server"></asp:Label>
                    <br />
                </li>
                <li class="background-c font-color-r" style="text-align: left; font-size: 20px;">
                    <span>客户信息</span>
                </li>
                <li>
                    <div>
                        <table>
                            <tr>
                                <td style="width: 50px; vertical-align: top;"><span>客户:</span></td>
                                <td>
                                    <asp:Label ID="lblCustomer" runat="server" Text=""></asp:Label></td>
                            </tr>
                        </table>
                    </div>
                    <span>联系人:</span><asp:Label ID="lblContact" runat="server"></asp:Label>
                </li>
            </ul>
        </div>

        <script>

            //列表图片
            $(".work-record li").first().find("img").attr("src", "/Wechats/img/l1.png");
            $(".work-record li").last().find("img").attr("src", "/Wechats/img/l3.png");
        </script>
    </form>
</body>
</html>
