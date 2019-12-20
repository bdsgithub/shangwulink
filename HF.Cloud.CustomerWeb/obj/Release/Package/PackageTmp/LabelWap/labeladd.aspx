<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="labeladd.aspx.cs" Inherits="HF.Cloud.CustomerWeb.LabelWap.labeladd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0;" />
    <title>添加标签</title>
    <link href="../css/weui.min.css" rel="stylesheet" />
    <script src="../js/jquery-1.8.3.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="weui-cells weui-cells_form">
            <div class="weui-cells__title">
                添加标签
            </div>
            <div class="weui-cells weui-cells_form">
                <div class="weui-cell" style="display:none;">
                    <div class="weui-cell__hd">
                        <label class="weui-label">起始编号</label>
                    </div>
                    <div class="weui-cell__bd">
                        <asp:TextBox ID="txtStartNo" placeholder="输入起始编号" CssClass="weui-input" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="weui-cell" style="display:none;">
                    <div class="weui-cell__hd">
                        <label class="weui-label">结束编号</label>
                    </div>
                    <div class="weui-cell__bd">
                        <asp:TextBox ID="txtEndNo" placeholder="输入结束编号" CssClass="weui-input" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">安全码</label>
                    </div>
                    <div class="weui-cell__bd">
                        <asp:TextBox ID="txtSaveNo" placeholder="输入安全码" CssClass="weui-input" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="weui-btn-area">
            <div>
                <asp:LinkButton CssClass="weui-btn weui-btn_primary" ID="lnkBtnAdd" OnClick="lnkBtnAdd_Click" runat="server">添加</asp:LinkButton>
            </div>
        </div>
    </form>
</body>
</html>
