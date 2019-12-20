<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RepairList.aspx.cs" Inherits="HF.Cloud.DingDing.Repair.RepairList" %>

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
    
    <link rel="stylesheet" href="../css/style.css">
    <script src="../js/jquery-1.8.3.min.js"></script>
  
    <%--调用jquery需要的库，手机版和电脑版的不同 --%> 
    <script src="../js/zepto.min.js"></script>
      <%--手机版钉钉免登引入的jsapi,和电脑版引入的不同  --%>
        <script type="text/javascript" src="http://g.alicdn.com/ilw/ding/0.9.2/scripts/dingtalk.js">  
        </script>  
    
     <script type="text/javascript">  
         dd.ready(function () {
             //导航标题
             dd.biz.navigation.setTitle({
                 title: "维修列表",
                 subTitle: "维修列表...",
                 onSuccess: function (result) {
                     /*结构
                     {
                     }*/
                 },
                 onFail: function (err) { }


             });
             ////右边菜单
             dd.biz.navigation.setMenu({
                 //backgroundColor: "#ADD8E6",
                 //textColor: "#ADD8E611",
                 items: [
                     {
                         "id": "1",//字符串
                         "iconId": "more",//字符串，图标命名
                         "text": "占位图标"
                     }
                 ],
                 onSuccess: function (data) {
                     /*{"id":"1"}*/

                 },
                 onFail: function (err) {
                 }
             });


         });
        </script> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <%=htmlstr %>
    </div>
    </form>
</body>
</html>
