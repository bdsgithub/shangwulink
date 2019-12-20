<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RepairDetail.aspx.cs" Inherits="HF.Cloud.DingDing.Repair.RepairDetail" %>

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
                 title: "维修单详细",
                 subTitle: "维修单详细...",
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
                         "text": "查看回复"
                     }
                     ,
                     {
                         "id": "2",//字符串
                         //"iconId": "edit",//字符串，图标命名
                         "text": "完成"
                     }
                     ,
                     {
                         "id": "3",//字符串
                         //"iconId": "edit",//字符串，图标命名
                         "text": "暂停"
                     }
                     ,
                     {
                         "id": "4",//字符串
                         //"iconId": "edit",//字符串，图标命名
                         "text": "转移"
                     }
                 ],
                 onSuccess: function (data) {
                     /*{"id":"1"}*/
                     var menuID = data.id;
                     if (menuID == "1")//如果点击了回复
                     {
                         window.location.href = "RepairReply.aspx?dd_nav_bgcolor=FF5E97F6&repairID=" + repairID;
                     }
                     if (menuID == "2")//如果点击了维修完成
                     {
                         var taskState = 5;//5是完成
                         $.ajax({
                             url: '/ashx/repairStateChange.ashx?repairID=' + repairID + '&taskState=' + taskState + '&updateWriter=' + userID + '&writeAdr=',
                             type: 'get',
                             success: function (data) {
                                 if (data == "success") {
                                     //alert(data);
                                     dd.device.notification.toast({
                                         text: '修改成功...'
                                     });
                                     window.location.reload();
                                 }
                             },
                             error: function (errorType, error) {
                                 alert("repairStateChange点击完成发送错误！"+error);
                             }
                         });
                     }
                     if (menuID == "3")//如果点击了暂停
                     {
                         var taskState = 4;//4是暂停
                         $.ajax({
                             url: '/ashx/repairStateChange.ashx?repairID=' + repairID + '&taskState=' + taskState + '&updateWriter=' + userID + '&writeAdr=',
                             type: 'get',
                             success: function (data) {
                                 if (data == "success") {
                                     //alert(data);
                                     dd.device.notification.toast({
                                         text: '修改成功...'
                                     });
                                     window.location.reload();
                                 }
                             },
                             error: function (errorType, error) {
                                 alert("repairStateChange点击暂停发送错误！" + error);
                             }
                         });
                     }
                     if (menuID == "4")//如果点击了转移工单
                     {
                         window.location.href = "TransferRepair.aspx?dd_nav_bgcolor=FF5E97F6&repairID=" + repairID;
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
       <div>
            <div style="margin-top:20px;width:100%;margin-bottom:20px;">
                <label style=" float: left;width:20%;margin-left:10px;color:red;font-family:Microsoft yahei;font-size:20px;white-space: nowrap;"><%=repairIDStr %></label>
                <label style=" float: left;width:60%;margin-left:10px;font-family:Microsoft yahei;font-size:20px;white-space: nowrap;"><%=typeNameStr %></label>
                <label style="float:right;margin-right:20px;font-family:Microsoft yahei;font-weight:bold;font-size:20px;white-space: nowrap;"><%=taskStatusStr %></label>
            </div>
           <div style="margin-top:5px;width:100%;margin-bottom:10px;">
                <label style="margin-left:10px;font-family:Microsoft yahei;font-size:15px;white-space: nowrap;"><%=realseNameStr %>创建于</label>
                <label style="font-family:Microsoft yahei;font-size:15px;white-space: nowrap;"><%=writeTimeStr %></label>
               
           </div>
           <div style="width:100%;margin-bottom:20px;">
               <label style="float:right;margin-right:20px;font-family:Microsoft yahei;font-size:10px;white-space: nowrap;">已耗时：<%=useTimeStr %></label>
           </div>
       </div>

       
        <div class="sheetType-div">
            <label class="shetDetail_items">客户联系人</label>
            <label class="shetDetail_content"><%=linkNameStr %></label>
        </div>
        <div class="sheetType-div">
            <label class="shetDetail_items">联系人电话</label>
            <label class="shetDetail_content"><%=linkTelStr %></label>
        </div>
        <div class="sheetType-div" style="height:20px;width:100%;">

        </div>

        <div class="sheetType-div">
            <label class="shetDetail_items">受理人</label>
            <label class="shetDetail_content"><%=acceptNameStr %></label>
        </div>
        <div class="sheetType-div">
            <label class="shetDetail_items">描述</label>
            <label class="shetDetail_content"><%=taskDetailStr %></label>
        </div>
        <div class="sheetType-div">
            <label class="shetDetail_items">工单总结</label>
            <label class="shetDetail_content">12</label>
        </div>
   
    </div>

        <asp:HiddenField ID="hf_repairID" runat="server" />
        <asp:HiddenField ID="hf_userID" runat="server" />

    </form>
</body>
</html>
