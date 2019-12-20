<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QBModePrintSlt.aspx.cs" Inherits="HF.Cloud.BM.SaleLabels.QBModePrintSlt" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>打印选择</title>
    <link type="text/css" href="/css/home.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#lnkBtnSave').click(function () {
                if ($('#rblPrintSelect').val() == '2') {
                    var iv = $('#txtPrintNum').val();
                    var av = $('#lblNotPrintNum').val();

                    if (iv != '') {
                        if (parseInt(iv) && !isNaN(parseInt(iv))) {
                            if (parseInt(iv) > parseInt(av)) {
                                alert('输入的打印数量不能大于未打印数量！');
                                return false;
                            }
                        }
                        else {
                            alert('输入打印数量的格式不正确！\n请重新输入！');
                            return false;
                        }
                    }
                    else {
                        alert('请输入打印数量！');
                        return false;
                    }
                }
                else {
                    var iv = $('#txtPrintNum').val();
                    if (!iv || iv == '' || iv == '0') { alert('请输入打印数量！'); return false; }
                    if (isNaN(parseInt(iv))) { alert('输入打印数量格式不正确！\n\n请重新输入！'); return false; }
                }
            });
        });
    </script>
    <style>
        .select {
            width: 440px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class=" work-center float-r-s margin-top-s padding-s">
                <div class="main-title"><span style="color: #1fb5ac">标签打印></span>打印</div>
                <div class="border-c padding-top-s margin-top-s overflow-h">
                    <div class="select float-l-s float-left margin-top-s">
                        未打印数量：
                        <asp:Label ID="lblNotPrintNum" runat="server" ForeColor="Green"></asp:Label>
                    </div>
                    <div class="select float-l-s float-left margin-top-s">
                    </div>
                    <div class="select float-l-s float-left margin-top-s">
                        <asp:RadioButtonList ID="rblPrintSelect" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="True" OnSelectedIndexChanged="rblPrintSelect_SelectedIndexChanged">
                            <asp:ListItem Value="1" Selected="True">新生成标签打印</asp:ListItem>
                            <asp:ListItem Value="2">打印已有标签</asp:ListItem>
                        </asp:RadioButtonList>

                    </div>
                    <div class="select float-l-s float-left margin-top-s">
                        &nbsp;批号：
                        <asp:Label ID="lblBatchNo" runat="server"></asp:Label>
                    </div>
                    <div class="select float-l-s float-left margin-top-s">
                        打印数量：<asp:TextBox ID="txtPrintNum" runat="server"></asp:TextBox>
                    </div>
                    <br />
                    <div class="button margin-top-s float-left" style="margin-left: 20px">
                        <asp:LinkButton ID="lnkBtnSave" runat="server" OnClick="btnSubmit_Click"><div class="button-m background-r float-left ">确定</div></asp:LinkButton>
                        <a href='QBModelList.aspx?mainId=<%= GetQueryString("mainId") %>'>
                            <div class="button-m background-cc float-left" id="reset" style="margin-left: 20px">取消</div>
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
