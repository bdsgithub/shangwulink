<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OneKeyAddRepair.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Repair.OneKeyAddRepair" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link type="text/css" href="/css/home.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="/js/cient.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
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
                        top.layer.closeAll();
                    }
                });
            });
            //取消
            $('#btnCancel').click(function () {
                top.layer.closeAll();
                return false;
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class=" work-center float-r-s margin-top-s margin-left-s">
            <div class="overflow-h">
                <div class="select float-l-s float-left margin-top-s">
                    所属客户
                       <asp:DropDownList Style="width: 345px; height: 40px" runat="server" ID="ddlCustomer"></asp:DropDownList>
                </div>
                <div class="select float-l-s float-left margin-top-s">
                    设备类型
                       <asp:DropDownList Style="width: 345px; height: 40px" runat="server" ID="ddlAssetType"></asp:DropDownList>
                </div>
                
                <div class="select float-l-s float-left margin-top-s">
                    联系人&nbsp;&nbsp;&nbsp;
                       <input type="text" runat="server" id="txtLinkName" placeholder="请输入联系人" />
                </div>
                <div class="select float-l-s float-left margin-top-s">
                    电话&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       <input type="text" runat="server" id="txtLinkTel" placeholder="请输入电话" />
                </div>
                <div class="select float-l-s float-left margin-top-s">
                     <p style="width:57px;">故障描述</p>
                       <textarea rows="2" cols="20" placeholder="请输入故障描述" maxlength="80" id="txtSheetDetail" style="width:340px;float:right;margin-top:-18px;margin-right:21px;"></textarea>
                </div>
                <br />
                <div class="button margin-top-s float-right" style="margin-right:39px;margin-top:39px;">
                    <div class="button-m background-r float-right">
                       <a id="lnkBtnSave" href="javascript:void(0)" style="color:#fff;">提交</a>
                    </div>
                    <div class="button-m background-cc float-right" style="margin-right: 20px">
                        <a href="#" id="btnCancel" style="width: 100%;color:#fff;">取消</a>
                    </div>
                </div>
            </div>
        </div>

        <asp:HiddenField runat="server" ID="hidMainID" />
    </form>
</body>
</html>
