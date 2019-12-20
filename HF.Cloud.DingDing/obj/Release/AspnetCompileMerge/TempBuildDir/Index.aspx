<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="HF.Cloud.DingDing.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent">
    <meta name="format-detection" content="telephone=no">
    <meta name="full-screen" content="yes">
    <meta name="x5-page-mode" content="app">
    
    <link rel="stylesheet" href="css/style.css">
    <script src="js/jquery-1.8.3.min.js"></script>

 
    <%--调用jquery需要的库，手机版和电脑版的不同 --%> 
    <script src="js/zepto.min.js"></script>
      <%--手机版钉钉免登引入的jsapi,和电脑版引入的不同  --%>
        <script type="text/javascript" src="http://g.alicdn.com/ilw/ding/0.9.2/scripts/dingtalk.js">  
        </script>  


        <script type="text/javascript">  
            var _config = <%=configStr %>;  
        
             </script> 

    <%--获取code码值的js文件 --%>
    <script src="js/ddConfig.js"></script>
    




</head>
<body>
    <form id="form1" runat="server">
    <img src="img/b.jpg" alt="" class="login-banner">
        <div class="oneself-list eweic-top">
            <ul>
                <li class="eweic-right-border">
                    <img src="img/cc.png" alt="">
                    <p>工单</p>
                    <a href="Sheet/SheetType.aspx?dd_nav_bgcolor=FF5E97F6"></a>
                </li>
                <li class="eweic-right-border">
                    <img src="img/s.png" alt="">
                    <p>维修</p>
                    <a href="Repair/RepairType.aspx?dd_nav_bgcolor=FF5E97F6"></a>
                </li>
                <li class="eweic-right-border">
                    <img src="img/ss.png" alt="">
                    <p>巡检</p>
                    <a href="Patrol/PatrolType.aspx?dd_nav_bgcolor=FF5E97F6"></a>
                </li>
                <li class="eweic-right-border">
                    <img src="img/z.png" alt="">
                    <p>查看</p>
                    <a href="javascript:void(0)"></a>
                </li>
            </ul>
        </div>
        <div class="oneself-list eweic-bottom-border">
            <ul>
                <li class="eweic-right-border">
                    <img src="img/ss.png" alt="">
                    <p>设备</p>
                    <a href="javascript:void(0)"></a>
                </li>
                <li class="eweic-right-border">
                
                    
                </li>
                <li class="eweic-right-border">
                  
                </li>
                <li class="eweic-right-border"></li>
            </ul>
        </div>
    </form>
</body>
</html>
