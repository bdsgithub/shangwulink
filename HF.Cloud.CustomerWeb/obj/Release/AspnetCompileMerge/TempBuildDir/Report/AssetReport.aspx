<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssetReport.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Report.AssetReport" %>

<%@ Register Src="~/Report/leftMenu.ascx" TagPrefix="uc1" TagName="leftMenu" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>设备统计</title>
    <link type="text/css" href="/css/home.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="/js/cient.js"></script>
    <script type="text/javascript" src="/js/echarts.min.js"></script>
    <script type="text/javascript" src="/js/macarons.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var divrepair = document.getElementById('repairs');
            var repairsCount = echarts.init(divrepair, 'macarons');

            repairsCount.setOption({
                title: {
                    text: '使用年限统计',
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

            var divCusCount = document.getElementById('custAssetCount');
            var custAstCount = echarts.init(divCusCount, 'macarons');

            custAstCount.setOption({
                title: {
                    text: '客户设备数量统计',
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
                    data: ['设备数量']
                },
                xAxis: {
                    name: '客户名称',
                    type: 'category',
                    data: [<%= cust_x_axis.ToString()%>]
                },
                yAxis: {
                    name: '设备数量',
                    type: 'value'
                },
                <%= cust_series.ToString()%>
            });
            $('#li4').addClass('background-c').addClass('font-color-r');
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="main-le border-c float-left">
            <uc1:leftMenu runat="server" ID="leftMenu" />
        </div>
        <div class="main-right float-right">
            <div>
                        <asp:DropDownList ID="ddlAssetType" class="float-right" AutoPostBack="true" OnSelectedIndexChanged="ddlAssetType_SelectedIndexChanged" runat="server"></asp:DropDownList>
            </div>
            <div class="border-c padding-top-s">
                <div class="data" style="width: 920px; height: 350px;">

                
                    
                    <div id="repairs" style="width: 920px; height: 350px;">
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
