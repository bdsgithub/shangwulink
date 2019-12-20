<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="HF.Cloud.BM.ServiceProvider.Add" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>新增服务商</title>
    <link type="text/css" href="/css/home.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="/js/cient.js"></script>
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
                <div class="main-title"><span style="color: #1fb5ac">服务商></span>新增</div>
                <div class="border-c padding-top-s margin-top-s overflow-h">
                    <div class="select float-l-s float-left margin-top-s">
                        企业名称&nbsp;&nbsp;：&nbsp;
                        <asp:TextBox ID="txtSBName" runat="server"></asp:TextBox>
                    </div>
                    <div class="select float-l-s float-left margin-top-s">
                        手机号：&nbsp;&nbsp;
                        <asp:TextBox ID="txtUserCode" runat="server"></asp:TextBox>
                    </div>
                    <div class="select float-l-s float-left margin-top-s">
                        管理员姓名：
                        <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                    </div>
                    <br />
                    <div class="button margin-top-s float-left" style="margin-left: 20px">
                        <asp:LinkButton ID="lnkBtnSearch" runat="server" OnClick="lnkBtnSearch_Click"><div class="button-m background-r float-left ">保存</div></asp:LinkButton>
                        <asp:LinkButton ID="lnkBtnReset" runat="server" OnClick="lnkBtnReset_Click"> <div class="button-m background-cc float-left" id="reset" style="margin-left: 20px">取消</div></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
