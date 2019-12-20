<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SheetStateTrack.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Wap.SheetStateTrack" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>工单状态跟踪</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent">
    <meta name="format-detection" content="telephone=no">
    <meta name="full-screen" content="yes">
    <meta name="x5-page-mode" content="app">
   <%-- <link rel="stylesheet" href="/Wechats/css/style.css"/>--%>
    <script src="/Wechats/js/zepto.min.js"></script>


    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/weui/1.1.2/style/weui.min.css" rel="stylesheet"/>


     <style type="text/css">
          body{
            background-color: #f5f5f5;
            padding-top: 20px; 
              }
        .option{
            margin-top:10px;
            margin-bottom:10px;
            margin-left:10px;
            margin-right:10px;
        }
        .title{
            clear: both;
            text-align: center;
            line-height: 60px;
            height: 60px;
            font-size: 20px;
            font-weight: bolder;
            color: rgba(8, 6, 13, 0.67);
        }
/*-------------/Wechats/css/style.css中的样式-------------------------------------*/

ul {
    list-style: none;
}

    ul li {
        float: left;
    }

        .task-details {
    padding: 2% 0;
    background-color: #fff;
    border-top: 1px solid #ccc;
    border-bottom: 1px solid #ccc;
    overflow: hidden;
}

    .task-details img {
        width: 16%;
        float: left;
    }

.task-details-cont {
    width: 80%;
    float: left;
    /*white-space: nowrap;*/   /*规定段落中的文本不进行换行*/
    border-bottom: 1px solid #ccc;
    overflow: hidden;
    line-height: 29px;
}

    .task-details-cont p {
        font-size: 14px;
    }

.task-details li {
    width: 100%;
}
.task-details-cont {
    width: 80%;
    float: left;
    /*white-space: nowrap;*/   /*规定段落中的文本不进行换行*/
    border-bottom: 1px solid #ccc;
    overflow: hidden;
    line-height: 29px;
}

    .task-details-cont p {
        font-size: 14px;
    }
     </style>
</head>
<body>
        <div class="header">
             <div class="title">工单跟踪</div>
        </div>
    <form id="form1" runat="server">
       <%-- <div class="operate">
            <div class="eweic-title">
                <p>工单跟踪</p>
            </div>
        </div>

        <div class="eweic-chunk-white">
            <div class="eweic-chunk-cont">
                <p class="eweic-left">
                    工单类型：
                </p>
                <span class="eweic-p-right">
                    <asp:Literal runat="server" ID="ltSheetType"></asp:Literal></span>
            </div>
        </div>
        <div class="eweic-chunk-white">
            <div class="eweic-chunk-cont">
                <p class="eweic-left">
                    工单描述： 
                </p>
                <span class="eweic-p-right">
                    <asp:Literal runat="server" ID="ltSheetDetail"></asp:Literal></span>
            </div>
        </div>
        <div class="eweic-chunk-white">
            <div class="eweic-chunk-cont">
                <p class="eweic-left">
                    所属客户：
                </p>
                <span class="eweic-p-right">
                    <asp:Literal runat="server" ID="ltClient"></asp:Literal></span>
            </div>
        </div>
        <div class="eweic-chunk-white">
            <div class="eweic-chunk-cont">
                <p class="eweic-left">
                    受理服务商：
                </p>
                <span class="eweic-p-right">
                    <asp:Literal runat="server" ID="ltMain"></asp:Literal></span>
            </div>
        </div>--%>
        
          <div class="weui-cells">
            <div class="weui-cell">
                <div class="weui-cell__bd">
                    <p>工单类型：</p>
                </div>
                <div class="weui-cell__ft" ><label id="ltSheetType" runat="server"></label></div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__bd">
                    <p>所属客户</p>
                </div>
                <div class="weui-cell__ft" ><label id="ltClient" runat="server"></label></div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__bd">
                    <p>工单描述</p>
                </div>
                <div class="weui-cell__ft" ><label id="ltSheetDetail" runat="server"></label></div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__bd">
                    <p>受理服务商</p>
                </div>
                <div class="weui-cell__ft" ><label id="ltMain" runat="server"></label></div>
            </div>
        </div>





        <div class="task-details eweic-top ">
            <ul>
                <asp:Repeater runat="server" ID="repDetail">
                    <ItemTemplate>
                        <li>
                            <img src="/Wechats/img/l2.png" alt="">
                            <div class="task-details-cont ">
                               <p style="word-break: break-all;">
                                    <%#Eval("SendDetail") %>
                                </p>
                                <p>
                                    <%#Eval("sendtime") %>
                                </p>
                            </div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
            
   <%--     <div class="chunk"></div>--%>

       <%-- <div class="eweic-bottom">
            <ul>
                <li class="operate-list-blue eweic-bottom-back">
                    <p>查看设备详情</p>
                    <a href='AseetInfo.aspx?cid=<%= GetCid() %>&mid=<%= GetMid() %>'></a>
                </li>
            </ul>
        </div>--%>
        <div class="option ">
            <a href='AseetInfo.aspx?cid=<%= GetCid() %>&mid=<%= GetMid() %>' id="ltBut" class="weui-btn weui-btn_primary" style="color:white;">查看设备详情</a>
        </div>



        <script>
            //列表图片
            $(".task-details li").first().find("img").attr("src", "/Wechats/img/l1.png");
            $(".task-details li").last().find("img").attr("src", "/Wechats/img/l3.png");
        </script>

    </form>
</body>
</html>
