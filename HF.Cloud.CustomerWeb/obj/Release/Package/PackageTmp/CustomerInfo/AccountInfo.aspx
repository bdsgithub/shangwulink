<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountInfo.aspx.cs" Inherits="HF.Cloud.CustomerWeb.CustomerInfo.AccountInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link type="text/css" href="/css/home.css" rel="stylesheet" />
    <title></title>
    <script src="/js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#spCustomer").click(function () {
                top.layer.open({
                    type: 1,
                    title: '企业名称编辑',
                    shadeClose: true,
                    shade: 0.5,
                    area: ['300px', '150px'],
                    content: "<div style='margin-top:20px;margin-left:20px;'>企业名称：<input type='text' id='txtName' /></div>",
                    btn: ["确定", "取消"],
                    yes: function (index, layero) {
                        if (layero.find('#txtName').val() == "") {
                            top.layer.msg("请输入企业名称");
                            return false;
                        }
                        $.post("/ashx/ExecuteAccount.ashx", { op: "UpdateCustomerName", id: $("#hidID").val(), name: layero.find('#txtName').val() }, function (data) {
                            if (data == "success") {
                                top.layer.msg("保存成功！");
                                $("#spCustomer").prev().html("企业名称：" + layero.find('#txtName').val());
                                top.layer.closeAll();
                            }
                        });
                    }
                }
                    );
            });
            $("#spAdmin").click(function () {
                top.layer.open({
                    type: 1,
                    title: '管理员名称编辑',
                    shadeClose: true,
                    shade: 0.5,
                    area: ['300px', '150px'],
                    content: "<div style='margin-top:20px;margin-left:20px;'>管理员：<input type='text' id='txtName' /></div>",
                    btn: ["确定", "取消"],
                    yes: function (index, layero) {
                        if (layero.find('#txtName').val() == "") {
                            top.layer.msg("请输入管理员名称");
                            return false;
                        }
                        $.post("/ashx/ExecuteAccount.ashx", { op: "UpdateAdminName", id: $("#hidID").val(), name: layero.find('#txtName').val() }, function (data) {
                            if (data == "success") {
                                top.layer.msg("保存成功！");
                                $("#spAdmin").prev().html("管理员：" + layero.find('#txtName').val());;
                                top.layer.closeAll();
                            }
                        });
                    }
                }
                    );
            });
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class=" work-center float-r-s margin-top-s padding-s">
                <div class="main-title">
                    <h2 id="work-select" class="font-color-r">账户信息
                    </h2>
                </div>
                <div class="border-c padding-top-s margin-top-s" style="overflow: hidden;">
                    <div class="xun">
                        <span>企业名称：<asp:Literal runat="server" ID="ltCustomerName"></asp:Literal></span>
                        <span id="spCustomer" class="font-color-r" style="cursor:pointer; margin-left:10px;">编辑</span>
                    </div>
                    <div class="xun">
                        <span>管理员：<asp:Literal runat="server" ID="ltAdminName"></asp:Literal></span>
                        <span id="spAdmin" class="font-color-r" style="cursor:pointer; margin-left:10px;">编辑</span>
                    </div>
                    <div class="xun">
                        <span>账号：<asp:Literal runat="server" ID="ltAccount"></asp:Literal></span>
                    </div>
                    <div class="xun">
                        <span>邮箱：<asp:Literal runat="server" ID="ltEmail"></asp:Literal></span>
                    </div>
                    <div class="xun">
                        <span>手机号：<asp:Literal runat="server" ID="ltTel"></asp:Literal></span>
                    </div>
                    <div class="xun">
                        <span>唯一码：<asp:Literal runat="server" ID="ltUniqueCode"></asp:Literal></span>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField runat="server" ID="hidID" />
    </form>
</body>
</html>
