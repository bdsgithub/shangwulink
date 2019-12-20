<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PatrolType.aspx.cs" Inherits="HF.Cloud.DingDing.Patrol.PatrolType" %>

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
             var repairID = $("#hf_repairID").val();
             var userID = $("#hf_userID").val();
             //导航标题
             dd.biz.navigation.setTitle({
                 title: "巡检记录",
                 subTitle: "巡检记录...",
                 onSuccess: function (result) {
                     /*结构
                     {
                     }*/
                 },
                 onFail: function (err) { }


             });
             ////右边菜单
             dd.biz.navigation.setMenu({
                 items: [
                     {
                         "id": "1",//字符串
                         //"iconId": "edit",//字符串，图标命名
                         "text": "巡检计划"
                     }
                   
                 ],
                 onSuccess: function (data) {
              
                     var menuID = data.id;
                     if (menuID == "1")//如果点击了巡检计划
                     {
                         window.location.href = "RepairReply.aspx?dd_nav_bgcolor=FF5E97F6&repairID=" + repairID;
                     }
                   
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
