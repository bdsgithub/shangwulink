<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QBModelList.aspx.cs" Inherits="HF.Cloud.BM.Labels.QBModelList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>标签管理</title>
    <%--<link rel="stylesheet" href="/css/eweicCont.css" />--%>
    <link href="/css/style.css" rel="stylesheet" />
    <link href="/css/reset.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="/js/jquery.autocomplete.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //删除模板
            $.DeleteModel = function (id) {
                top.layer.confirm("确认删除？", function (index) {
                    top.layer.close(index);
                    $.post("/ashx/ExecuteQBCode.ashx", { id: id, op: "deleteModel" }, function (data) {
                        if (data == "success") {
                            top.layer.alert("删除成功！");
                            location.reload();
                        }
                    });
                });
            }
        });
    </script>
    <style type="text/css">
        html, body {
            height: auto;
        }

        table {
            border-collapse: collapse;
            border: 0.5px gainsboro solid;
        }

        html {
        }

        .cont-lef a {
            color: #fff;
        }
    </style>
</head>
<body class="eweic-manage">
    <form id="form1" runat="server">
        <div class="manage-title" style="margin-top: 20px; height: 30px;">
            <h2>
                <p class="m-title" style="color: #1fb5ac; width: 200px; float: left; margin-left: 10px; display: inline; font-family: Microsoft Yahei,Helvetica Neue,Helvetica,Arial,sans-serif">
                    标签管理
                </p>
                <p class="cut-off" style="font-size: 14px; font-weight: normal; float: right; margin-right: 80px; display: inline-block;">
                    服务商：<asp:DropDownList ID="ddlSPList" Width="120px" AutoPostBack="true" OnSelectedIndexChanged="ddlSPList_SelectedIndexChanged" runat="server"></asp:DropDownList>
                </p>
            </h2>
        </div>
        <div class="ewwic-content-box">
            <div class="tabl border-c margin-top-s">
                <div class="cont-workorder">
                    <div class="label">
                        <%=ModelHtml%>
                    </div>
                    <div class="blank"></div>
                    <div class="blank"></div>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
