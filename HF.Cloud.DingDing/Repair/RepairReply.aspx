<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RepairReply.aspx.cs" Inherits="HF.Cloud.DingDing.Repair.RepairReply" %>

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
             var mainID = $("#hf_mainID").val();
             var sendID = $("#hf_sendID").val();
             var sendTime = $("#hf_sendTime").val();

             var chatID = $("#hf_chatID").val();

             var userName = $("#hf_userName").val();
             //导航标题
             dd.biz.navigation.setTitle({
                 title: "工单回复信息",
                 subTitle: "工单回复信息...",
                 onSuccess: function (result) {
                     /*结构
                     {
                     }*/
                 },
                 onFail: function (err) {
                 }
             });
             //右边菜单
             dd.biz.navigation.setMenu({
                 items: [
                     {
                         "id": "1",//字符串
                         "iconId": "edit",//字符串，图标命名
                         "text": "回复"
                     }
                 ],
                 onSuccess: function (data) {
                     var menuID = data.id;
                     if (menuID == "1")//如果点击了回复
                     {
                         dd.ui.input.plain({
                             placeholder: "请输入回复...",
                             text: "",
                             onSuccess: function (result) {
                                 var detail = result.text;
                                 if (detail != "" && detail != " ") {
                                     $.ajax({
                                         url: '/ashx/repairReply.ashx?type=Character&repairID=' + repairID + '&mainID=' + mainID + '&sendID=' + sendID + '&sendTime=' + sendTime + '&Detail=' + detail,
                                         type: 'Post',
                                         success: function (data) {
                                             if (data == "success") {
                                                 //alert(data);
                                                 window.location.reload();
                                             }
                                         },
                                         error: function (errorType, error) {
                                             alert("回复写入数据库失败！");
                                         }
                                     });
                                 }
                             },
                             onFail: function (err) {
                             }
                         });
                     }
                 },
                 onFail: function (err) {
                 }
             });

             //启用下拉刷新
             dd.ui.pullToRefresh.enable({
                 onSuccess: function (data) {
                     // alert(data);
                     if (data == "\"\"") {
                         //收起下拉刷新
                         dd.ui.pullToRefresh.stop({
                         });
                         $.ajax({
                             url: '/ashx/repairMessage.ashx?repairID=' + repairID + '&chatID=' + chatID,
                             type: 'get',
                             success: function (data) {
                                 if (data != "") {
                                     var htmStr = "";
                                     var jsonData = JSON.parse(data);
                                     for (var i = 0; i < jsonData.result.length; i++) {
                                         if (jsonData.result[i].name == userName) {
                                             htmStr += "<div class=\"SheetReply_bigDIV\">";
                                             htmStr += "<div class=\"SheetReply_replyTitle\">";
                                             htmStr += "<div class=\"SheetReply_imgDIV_right\">";
                                             htmStr += "<img src=\"../img/z.png\"/>";
                                             htmStr += "</div>";
                                             htmStr += "<div class=\"SheetReply_nameDIV_right\">";
                                             htmStr += "<P>" + jsonData.result[i].name + "</P>";
                                             htmStr += "<P class=\"SheetReply_timeDIV\">" + jsonData.result[i].time + "</P>";
                                             htmStr += "</div>";
                                             htmStr += "</div>";
                                             htmStr += "<div class=\"SheetReply_replyDIV_right\">";
                                             htmStr += "<p>" + jsonData.result[i].detail + "</p>";
                                             htmStr += "</div>";
                                             htmStr += "</div>";
                                         }
                                         else {
                                             htmStr += "<div class=\"SheetReply_bigDIV\">";
                                             htmStr += "<div class=\"SheetReply_replyTitle\">";
                                             htmStr += "<div class=\"SheetReply_imgDIV\">";
                                             htmStr += "<img src=\"../img/z.png\"/>";
                                             htmStr += "</div>";
                                             htmStr += "<div class=\"SheetReply_nameDIV\">";
                                             htmStr += "<P>" + jsonData.result[i].name + "</P>";
                                             htmStr += "<P class=\"SheetReply_timeDIV\">" + jsonData.result[i].time + "</P>";
                                             htmStr += "</div>";
                                             htmStr += "</div>";
                                             htmStr += "<div class=\"SheetReply_replyDIV\">";
                                             htmStr += "<p>" + jsonData.result[i].detail + "</p>";
                                             htmStr += "</div>";
                                             htmStr += "</div>";
                                         }
                                     }

                                  
                                     chatID = jsonData.chatID;
                                     $("#SheetReplyDIV").prepend(htmStr);//在被选元素的开头插入内容
                                 }
                                 else //没有数据说明已经没有回复消息内容了，
                                 {
                                     dd.device.notification.toast({
                                         text: '已全部加载完成...'
                                     });
                                 }
                             },
                             error: function (errorType, error) {
                                 alert("刷新工单消息失败！");
                             }
                         });






                     }
                 },
                 onFail: function (err) { }
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
         <div id="SheetReplyDIV">
        <%=htmlStr %>
        </div>

        <asp:HiddenField ID="hf_repairID" runat="server" />
        <asp:HiddenField ID="hf_mainID" runat="server" />
        <asp:HiddenField ID="hf_sendID" runat="server" />
        <asp:HiddenField ID="hf_sendTime" runat="server" />

        <asp:HiddenField ID="hf_chatID" runat="server" />

        <asp:HiddenField ID="hf_userName" runat="server" />


    </div>
    </form>
</body>
</html>
