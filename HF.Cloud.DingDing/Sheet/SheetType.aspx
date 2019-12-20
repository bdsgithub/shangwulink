<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SheetType.aspx.cs" Inherits="HF.Cloud.DingDing.Sheet.SheetType" %>

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
    <style type="text/css">
        .div_height {
        height:40px;
        }
        .innerdiv{
            margin-top:10px;
        }
        .img_style{
            width:20px;height:20px;margin-left:20px;margin-right:10px;
        }
        .label_style{
            font-family:Microsoft yahei;font-weight:bold;font-size:18px;
            margin-left:10px;
        }
        


    </style>
     <script type="text/javascript">  
         dd.ready(function () {
             //导航标题
             dd.biz.navigation.setTitle({
                 title: "工单",
                 subTitle: "工单...",
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
                         "iconId": "add",//字符串，图标命名
                         "text": "添加工单"
                     }
                 ],
                 onSuccess: function (data) {
                     var menuID = data.id;
                     if (menuID == "1")//如果点击了添加工单，跳转到工单类
                     {
                         window.location.href = "AddSheet.aspx?dd_nav_bgcolor=FF5E97F6";
                     }
                 },
                 onFail: function (err) {
                     alert('setMenu_fail: ' + JSON.stringify(err));
                 }
             });


         });
        </script> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <%=htmlStr %>
       <%-- <div class="oneself-list eweic-bottom-border div_height"><a href=""><div class="innerdiv" onclick=""><img class="img_style"  src="../img/c.png" /> <label class="label_style" >未受理工单</label><label class="label_style">23</label></div></a></div>
      <div class="oneself-list eweic-bottom-border div_height"><a href=""><div class="innerdiv" onclick=""><img class="img_style"  src="../img/c.png" /> <label class="label_style" >未受理工单</label><label class="label_style">23</label></div></a></div>--%>
    </div>
          
         </form>
</body>
</html>
