<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SheetReport.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Report.SheetReport" %>

<%@ Register Src="~/Report/leftMenu.ascx" TagPrefix="uc1" TagName="leftMenu" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>工单报表</title>
    <link type="text/css" href="/css/home.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="/js/cient.js"></script>
    <script type="text/javascript" src="/js/echarts.min.js"></script>
    <script type="text/javascript" src="/js/macarons.js"></script>
    <script type="text/javascript" src="/js/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var divrepair = document.getElementById('repairs');
            var repairsCount = echarts.init(divrepair, 'macarons');

            repairsCount.setOption({
                title: {
                    text: '工单统计',
                    //subtext: '每个客户分类的工单数量或客户数量'
                },
                tooltip: {
                    trigger: 'axis'
                },
                toolbox: {
                    show: false,
                    feature: {
                        dataZoom: {},
                        dataView: { readOnly: false },
                        magicType: { type: ['line', 'bar'] },
                        restore: {},
                        saveAsImage: {}
                    }
                },
                legend: {
                    data: ['工单数量']
                },
                xAxis: {
                    name: '日期',
                    type: 'category',
                    data: [<%= x_axis.ToString()%>]
                },
                yAxis: {
                    name: '工单数量',
                    type: 'value'
                },
                <%= series.ToString()%>
            });

            var divSheetType = document.getElementById('sheetTypeCount');
            var SheetTypeCount = echarts.init(divSheetType, 'macarons');
            SheetTypeCount.setOption({
                title: {
                    text: '客户工单统计',
                    //subtext: '每个客户分类的工单数量或客户数量'
                },
                tooltip: {
                    trigger: 'axis'
                },
                toolbox: {
                    show: false,
                    feature: {
                        dataZoom: {},
                        dataView: { readOnly: false },
                        magicType: { type: ['line', 'bar'] },
                        restore: {},
                        saveAsImage: {}
                    }
                },
                legend: {
                    data: ['工单数量']
                },
                xAxis: {
                    name: '客户名称',
                    type: 'category',
                    data: [<%= SheetType_x_axis.ToString()%>]
                },
                yAxis: {
                    name: '工单数量',
                    type: 'value'
                },
                <%= sheetType_series.ToString()%>
            });
            $('#li3').addClass('background-c').addClass('font-color-r');
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="main-le border-c float-left">
            <uc1:leftMenu runat="server" ID="leftMenu" />
        </div>
        <div class="main-right float-right">
             <div class="float-right">
                        <div class="float-right">
                            <asp:TextBox ID="txtStartTime" onfocus="WdatePicker({readOnly:true})" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtEndTime" onfocus="WdatePicker({readOnly:true})" runat="server"></asp:TextBox>
                        </div>
                        <asp:DropDownList ID="ddlDataType" runat="server">
                            <asp:ListItem Value="-1">请选择</asp:ListItem>
                            <asp:ListItem Value="today" Text="今天"></asp:ListItem>
                            <asp:ListItem Value="last7" Text="最近7天"></asp:ListItem>
                            <asp:ListItem Value="last30" Text="最近30天"></asp:ListItem>
                            <asp:ListItem Value="definition" Text="自定义日期"></asp:ListItem>
                        </asp:DropDownList>
             </div>
            <div class="border-c padding-top-s">
                <div class="data" style="width: 920px; height: 350px;">
                   
                    <div id="repairs" style="width: 920px; height: 350px;">
                    </div>
                    
                   
                </div>
            </div>
            <br />
            <div class="float-right" >
                        <asp:DropDownList ID="ddlSheetType" AutoPostBack="true" OnSelectedIndexChanged="ddlSheetType_SelectedIndexChanged" runat="server"></asp:DropDownList>
            </div>
            <div class="border-c padding-top-s">
                <div class="data" style="width: 920px; height: 350px;">
                     
                    <div id="sheetTypeCount" style="width: 920px; height: 350px;">
                    </div>
                </div>
            </div>
            <br />
        </div>
    </form>
</body>
</html>
