<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OneKeyAddSheet.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Sheet.OneKeyAddSheet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>一键发布工单</title>
    <link type="text/css" href="/css/home.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="/js/cient.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#lnkBtnSearch').click(function () {
                if ($("#ddlCustomer").val() == "") {
                    alert("请选择所属客户！");
                    return false;
                }
                if ($("#ddlSheetType").val() == "") {
                    alert("请选择工单类型！");
                    return false;
                }
                if ($('#txtLinkMan').val().trim() == '') {
                    alert('请填写联系人！');
                    return false;
                }
                if ($('#txtLinkTel').val().trim() == '') {
                    alert('请填写联系电话！');
                    return false;
                }
            });
            //取消
            $('#btnCancel').click(function () {
                top.layer.closeAll();
                return false;
            });
        });
    </script>
</head>
<body style="background-color: white;">
    <form id="form1" runat="server">
        <div>
            <div class=" work-center float-r-s margin-top-s margin-left-s">
                <%--<div class="main-title"><span style="color: #1fb5ac">一键报修</span></div>--%>
                <div class="overflow-h">
                    <div class="select float-l-s float-left margin-top-s">
                        所属客户
                       <asp:DropDownList Style="width: 345px; height: 40px" runat="server" ID="ddlCustomer"></asp:DropDownList>
                    </div>
                    <div class="select float-l-s float-left margin-top-s">
                        工单类型
                        <asp:DropDownList Style="width: 345px; height: 40px" runat="server" ID="ddlSheetType"></asp:DropDownList>
                    </div>
                    <div class="select float-l-s float-left margin-top-s">
                        联系人&nbsp;&nbsp;&nbsp;
                      <asp:TextBox ID="txtLinkMan" runat="server"></asp:TextBox>
                    </div>
                    <div class="select float-l-s float-left margin-top-s">
                        联系电话
                        <asp:TextBox ID="txtLinkTel" runat="server"></asp:TextBox>
                    </div>
                    <div class="select float-l-s float-left margin-top-s">
                        <p style="width: 57px;">故障描述</p>
                        <asp:TextBox ID="txtDetail" Style="width: 340px; float: right; margin-top: -18px; margin-right: 17px;" TextMode="MultiLine" runat="server"></asp:TextBox>
                    </div>
                    <br />
                    <div class="button margin-top-s float-right" style="margin-top: 38px; margin-right: 35px;">
                        <div class="button-m background-r float-right ">
                            <asp:LinkButton ID="lnkBtnSearch" Width="100%" Style="color: #fff;" runat="server" OnClick="lnkBtnSearch_Click">保存</asp:LinkButton>
                        </div>
                        <div class="button-m background-cc float-right" style="margin-right: 20px">
                            <a href="#" id="btnCancel" style="width: 100%; color: #fff;">取消</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
