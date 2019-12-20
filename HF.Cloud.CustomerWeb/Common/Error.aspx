<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Common.Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>错误页</title>
    <link type="text/css" href="/css/home.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center">
            <br />
            系统出现问题，请联系技术服务商……
            <br />
            <br />
            <a href="/Main.aspx" target="_parent">点我返回主页吧</a>
            <div>
                <asp:Literal ID="ltlError" runat="server"></asp:Literal>
            </div>
        </div>
    </form>
</body>
</html>
