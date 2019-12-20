<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransferRepair.aspx.cs" Inherits="HF.Cloud.DingDing.Repair.TransferRepair" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>转移工单</title>
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
   <%-- <script type="text/javascript" src="http://g.alicdn.com/ilw/ding/0.9.2/scripts/dingtalk.js">  
    </script>--%>

    <%--requestOperateAuthCode接口需要最新的js  --%>
     <script type="text/javascript" src="http://g.alicdn.com/dingding/open-develop/1.5.1/dingtalk.js">  
        </script> 
        <script type="text/javascript">  
         dd.ready(function () {
             var repairID = $("#hf_repairID").val();
             var userID = $("#hf_userID").val();
             //导航标题
             dd.biz.navigation.setTitle({
                 title: "转移分配",
                 subTitle: "转移分配...",
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
                         "text": "确定"
                     }
                     
                 ],
                 onSuccess: function (data) {
                     /*{"id":"1"}*/
                     var menuID = data.id;
                     if (menuID == "1")//如果点击了确定
                     {
                         if ($("#ddlTeam").val() == "-1") {
                             dd.device.notification.alert({
                                 message: "请选择受理组！",
                                 title: "",//可传空
                                 buttonName: "确定",
                                 onSuccess: function () {
                                 },
                                 onFail: function (err) { }
                             });
                             return;
                         }
                         if ($("#ddlUser").val() == "-1" || $("#ddlUser").val() == "") {
                             dd.device.notification.alert({
                                 message: "请选择受理人！",
                                 title: "",//可传空
                                 buttonName: "确定",
                                 onSuccess: function () {
                                 },
                                 onFail: function (err) { }
                             });
                             return;
                         }
                         var teamID = $("#ddlTeam").val();//受理组
                         var acceptID = $("#ddlUser").val();//受理人
                         $.ajax({
                             url: '/ashx/repairtransfer.ashx?repairID=' + repairID + '&teamID=' + teamID + '&acceptID=' + acceptID + '&userID=' + userID,
                             type: 'get',
                             success: function (data) {
                                 if (data == "success") {
                                
                                     dd.device.notification.toast({
                                         text: '转移成功...'
                                     });
                                     // window.location.reload();
                                     window.location.href = "RepairType.aspx";
                                 }
                             },
                             error: function (errorType, error) {
                                 alert("TransferRepair转移工单发送错误！" + error);
                             }
                         });




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
    
           <div class="eweic-chunk-white">
                <div class="eweic-chunk-cont">
                    <p class="eweic-left">受理组</p>
                    <asp:DropDownList Style="border: 0px; margin-top: 18px;" runat="server" ID="ddlTeam" OnSelectedIndexChanged="DdlTeam_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </div>
            </div>
            <div class="eweic-chunk-white">
                <div class="eweic-chunk-cont">
                    <p class="eweic-left">受理人</p>
                    <asp:DropDownList Style="border: 0px; margin-top: 18px;" runat="server" ID="ddlUser"></asp:DropDownList>
                </div>
            </div>
        <asp:HiddenField ID="hf_repairID" runat="server" />
        <asp:HiddenField ID="hf_userID" runat="server" />
    </div>
    </form>
</body>
</html>
