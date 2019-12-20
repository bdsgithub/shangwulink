<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderRepair.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Wechats.OrderRepair" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="/js/jquery-1.8.3.min.js"></script>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent">
    <meta name="format-detection" content="telephone=no">
    <meta name="full-screen" content="yes">
    <meta name="x5-page-mode" content="app">
    <link rel="stylesheet" href="css/style.css">
    <script src="js/zepto.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#ddlAssetType").click(function () {
                if ($("#ddlCustomer").val() == "") {
                    alert("请先选择所属客户！");
                    return false;
                }
            });
            //保存验证提交
            $("#lnkBtnSave").click(function () {
                if ($("#ddlCustomer").val() == "") {
                    alert("请选择所属客户！");
                    return false;
                }
                if ($("#ddlAssetType").val() == "") {
                    alert("请选择设备！");
                    return false;
                }
                if ($("#txtLinkName").val() == "") {
                    alert("请输入联系人！");
                    return false;
                }
                if ($("#txtLinkTel").val() == "") {
                    alert("请输入联系电话！");
                    return false;
                }
                if ($("#txtSheetDetail").val() == "") {
                    alert("请输入故障描述！");
                    return false;
                }
                var info = {};
                info["assetTypeID"] = $("#ddlAssetType").val();
                info["linkName"] = $("#txtLinkName").val();
                info["linkTel"] = $("#txtLinkTel").val();
                info["task"] = $("#txtSheetDetail").val();
                info["mainID"] = $("#hidMainID").val();
                info["clientID"] = $("#ddlCustomer").val();
                info["op"] = "OrderRepair";
                $.post("/ashx/Wechat.ashx", info, function (data) {
                    if (data == "success") {
                        alert("发布成功！");
                        location.href = "/Wechats/Home.aspx";
                    }
                });
            });
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="operate">
            <div class="eweic-title">
                <p>一键报修</p>
            </div>
        </div>
        <div class="eweic-chunk-white eweic-top">
            <div class="eweic-chunk-cont">
                <p class="eweic-left">所属客户 </p>
                <asp:DropDownList runat="server" Style="border: 0px; margin-top: 18px;" ID="ddlCustomer"></asp:DropDownList>
            </div>
        </div>
        <div class="eweic-chunk-white">
            <div class="eweic-chunk-cont">
                <p class="eweic-left">设备类型 </p>
                <asp:DropDownList runat="server" Style="border: 0px; margin-top: 18px;" ID="ddlAssetType"></asp:DropDownList>
            </div>
        </div>
        <div class="eweic-chunk-white">
            <div class="eweic-chunk-cont">
                <p class="eweic-left">联系人 </p>
                <input type="text" runat="server" id="txtLinkName" placeholder="请输入联系人" class="eweic-right" />
            </div>
        </div>
        <div class="eweic-chunk-white">
            <div class="eweic-chunk-cont">
                <p class="eweic-left">电话 </p>
                <input type="text" runat="server" id="txtLinkTel" placeholder="请输入电话" class="eweic-right" />
            </div>
        </div>
        <div class="eweic-chunk-white">
            <div class="eweic-chunk-cont">
                <p class="eweic-left">故障描述 </p>
                <textarea rows="5" class="eweic-right" placeholder="请输入故障描述" style="border: 0px; margin-top: 19px;" maxlength="80" id="txtSheetDetail"></textarea>
            </div>
        </div>

        <div class="eweic-bottom eweic-top">
            <ul>
                <li class="operate-list-gray eweic-bottom-repairs">
                    <p>取消</p>
                    <a href="Home.aspx"></a>
                </li>
                <li class="operate-list-blue eweic-bottom-repairs">
                    <p>报修</p>
                    <a href="#" id="lnkBtnSave"></a>
                </li>
            </ul>
        </div>


        <asp:HiddenField runat="server" ID="hidMainID" />
    </form>
</body>
</html>
