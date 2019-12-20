<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssetDetail.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Inspect.AssetDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link type="text/css" href="/css/home.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="/js/getQueryString.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //获取巡检相关信息给控件赋值
            $.getJSON("/ashx/ExecutePatrol.ashx", { id: getQueryString("id"), op: "GetAssetDetail", mainID: $("#hidMainID").val() }, function (data) {
                $("#id").text(data[0].QBCode);
                $("#mc").text(data[0].AssetName);
                $("#pp").text(data[0].BrandName);
                $("#xh").text(data[0].ModelName);
                $("#xlh").text(data[0].AssetXuLie);
                $("#qysj").text(data[0].BeginTime);
                $("#zt").text(data[0].AssetStatus);
                $("#wg").text(data[0].OutSideStatus);
                $("#ms").text(data[0].AssetDetail);
                $("#pic").html(data[0].AssetPic);
            })
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="main-right float-left">
            <div class="main-title"><span style="color: #1fb5ac">巡检中心></span>设备详情</div>
            <%--<div class="border-c padding-top-s margin-top-s">
                &nbsp;&nbsp;&nbsp;<asp:Label ID="lblAssetName" runat="server" Text=""></asp:Label>
            </div>--%>
            <div class="border-c padding-top-s margin-top-s" style="overflow: hidden;">
                <div class="xun">
                    <p>设备编号：<span id="id"></span></p>
                </div>
                <div class="xun">
                    <p>设备名称：<span id="mc"></span></p>
                    <p>设备品牌：<span id="pp"></span></p>
                    <p>设备型号：<span id="xh"></span></p>
                </div>
                <div class="xun">
                    <p>设备序列号：<span id="xlh"></span></p>
                    <p>启用时间：<span id="qysj"></span></p>
                </div>
                <div class="xun">
                    <p>设备状态：<span id="zt"></span></p>
                    <p>设备外观：<span id="wg"></span></p>
                </div>
                <div class="xun">
                    <p>巡检描述：<span id="ms"></span></p>
                </div>
                <div class="xun">
                    <p>图片：</p>
                </div>
                <div class="xun" id="pic">
                </div>
            </div>
        </div>
        <asp:HiddenField runat="server" ID="hidMainID" />
    </form>
</body>
</html>
