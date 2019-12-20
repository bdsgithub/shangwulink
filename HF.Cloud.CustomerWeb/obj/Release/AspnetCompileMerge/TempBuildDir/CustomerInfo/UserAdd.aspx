<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserAdd.aspx.cs" Inherits="HF.Cloud.CustomerWeb.CustomerInfo.UserAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link type="text/css" href="/css/home.css" rel="stylesheet" />
    <script src="/js/jquery-1.8.3.min.js"></script>
    <script src="/js/layer/layer.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#lbtnSave").click(function () {
                if ($("#txtName").val() == "") {
                    top.layer.alert("请输入姓名！");
                    return false;
                }
                if ($("#txtCode").val() == "") {
                    top.layer.alert("请输入手机号！");
                    return false;
                }
                $.post("/ashx/ExecuteUser.ashx", { phone: $("#txtCode").val(), type: "IsExistsCode" }, function (data) {
                    if (data == "Exists") {
                        top.layer.alert("该手机号已存在！");
                        return false;
                    }
                });
            });
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class=" work-center float-r-s margin-top-s padding-s">
                <div class="main-title">
                    <h2 id="work-select" class="font-color-r">成员编辑
                    </h2>
                    <div>
                        <a>添加成员</a>
                    </div>
                </div>
                <div class="tabl border-c margin-top-s">
                    <table>
                        <tr>
                            <td>姓名</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtName"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>手机号</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtCode"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>邮箱</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtEmail"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:LinkButton runat="server" ID="lbtnSave" OnClick="lbtnSave_Click"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
