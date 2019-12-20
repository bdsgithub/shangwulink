<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderList.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Wechats.OrderList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent">
    <meta name="format-detection" content="telephone=no">
    <meta name="full-screen" content="yes">
    <meta name="x5-page-mode" content="app">
    <link rel="stylesheet" href="css/style.css">
    <title>任务跟踪</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="operate">
            <div class="eweic-title">
                <p>今天</p>
            </div>
        </div>
        <asp:Repeater runat="server" ID="repSheet">
            <ItemTemplate>
                <div class="eweic-chunk login-top" onclick="location.href='OrderDetail.aspx?id=<%#Eval("ID") %>&type=sheet'">
                    <div class="task-cont eweic-left">
                        <p>工单：<span><%#Eval("TypeName") %></span> &nbsp; <span class="task-cont-number task-border-o task-o"><%#Eval("ID") %></span></p>
                        <p><%#GetAcceptUser(Eval("SheetStateCN").ToString()) %></p>
                    </div>
                    <div class="task-cont-type task-o eweic-right">
                        <p><%#GetState(Eval("SheetStateCN").ToString()) %></p>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <%--<asp:Repeater runat="server" ID="repRepair">
            <ItemTemplate>
                <div class="eweic-chunk login-top" onclick="location.href='OrderDetail.aspx?id=<%#Eval("ID") %>&type=repair'">
                    <div class="task-cont eweic-left">
                        <p>维修：<span><%#Eval("TypeName") %></span> &nbsp; <span class="task-cont-number task-border-o task-o"><%#Eval("TaskCode") %></span></p>
                        <p><%#GetAcceptUser(Eval("StateCN").ToString()) %></p>
                    </div>
                    <div class="task-cont-type task-o eweic-right">
                        <p><%#GetState(Eval("StateCN").ToString()) %></p>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>--%>
    </form>
</body>
</html>
