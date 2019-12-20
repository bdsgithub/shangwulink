<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SheetDetail.aspx.cs" Inherits="HF.Cloud.DingDing.Sheet.SheetDetail" %>

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
             var sheetID = $("#hf_sheetID").val();

             var userNameStr = $("#hf_userNameStr").val();//受理人
             var userName = $("#hf_userName").val();//当前用户名字
             var sheetStateStr = $("#hf_sheetStateStr").val();//工单状态
             //查看受理人是否是本人
             if (userNameStr == userName) {
                 //工单所处阶段为未处理
                 if (sheetStateStr == "已受理") {
                     //显示按钮“开始处理”
                 }
                 //工单所处阶段为处理中
                 if (sheetStateStr == "处理中") {
                     //填写总结后点击按钮“完成”
                     $("sheetSummary_edit").attr("display", "block");//编辑栏显示
                     $("#ltBut").text = "工单完成";

                 }
                 //工单所处阶段为已完成
                 if (sheetStateStr == "已完成") {
                     $("sheetSummary_show").attr("display", "block");//编辑栏显示
                     $("#linkButton").attr("display", "none");//按钮隐藏

                 }
             }
            
             $("#lnkBtnSendSheetState").on("click", function() {
                 if (sheetStateStr == "处理中")
                 {
                     if($("#sheetSummary_edit").val=="")
                     {
                         dd.device.notification.toast({
                             text: '请填写总结...'
                         });
                         return false;
                     }
                 }
              });
             //var mainID = $("#hf_mainID").val();
             //var sendID = $("#hf_sendID").val();
             //var sendTime = $("#hf_sendTime").val();

             //var chatID = $("#hf_chatID").val();

             //导航标题
             dd.biz.navigation.setTitle({
                 title: "工单详细",
                 subTitle: "工单详细...",
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
                 ],
                 onSuccess: function (data) {
                     /*{"id":"1"}*/
                     var menuID = data.id;
                     if (menuID == "1")//如果点击了回复
                     {
                         window.location.href = "SheetReply.aspx?dd_nav_bgcolor=FF5E97F6&sheetID=" + sheetID;
                         //dd.ui.input.plain({
                         //    placeholder: "请输入回复...",
                         //    text: "",
                         //    onSuccess: function (result) {
                         //        var detail = result.text;
                         //        if (detail != "" && detail != " ") {
                         //            $.ajax({
                         //                url: '/DingDing/ashx/sheetReply.ashx?type=Character&sheetID=' + sheetID + '&mainID=' + mainID + '&sendID=' + sendID + '&sendTime=' + sendTime + '&Detail=' + detail,
                         //                type: 'Post',
                         //                success: function (data) {
                         //                    if (data == "success") {
                         //                        //alert(data);
                         //                        window.location.reload();
                         //                    }
                         //                },
                         //                error: function (errorType, error) {
                         //                    alert("回复写入数据库失败！");
                         //                    //alert(errorType + ', ' + error);
                         //                }
                         //            });
                         //        }
                         //    },
                         //    onFail: function (err) {
                         //    }
                         //});
                     }
                 },
                 onFail: function (err) {
                 }
             });

             ////启用下拉刷新
             //dd.ui.pullToRefresh.enable({
             //    onSuccess: function (data) {
             //        // alert(data);
             //        if (data == "\"\"") {
             //            //收起下拉刷新
             //            dd.ui.pullToRefresh.stop({
             //            });

             //            window.location.href = "SheetDetail.aspx?dd_nav_bgcolor=FF5E97F6&sheetID=" + sheetID + "&chatID=" + chatID;
             //        }
             //    },
             //    onFail: function (err) { }

             //});

          
         });
      </script> 


</head>
<body>
    <form id="form1" runat="server">
    <div>
       <div>
            <div style="margin-top:20px;width:100%;margin-bottom:20px;">
                <label style=" float: left;width:20%;margin-left:10px;color:red;font-family:Microsoft yahei;font-size:20px;white-space: nowrap;"><%=sheetIDStr %></label>
                <label style=" float: left;width:60%;margin-left:10px;font-family:Microsoft yahei;font-size:20px;white-space: nowrap;"><%=sheetTypeStr %></label>
                <label style="float:right;margin-right:20px;font-family:Microsoft yahei;font-weight:bold;font-size:20px;white-space: nowrap;"><%=sheetStateStr %></label>
            </div>
           <div style="margin-top:5px;width:100%;margin-bottom:20px;">
                <label style="margin-left:10px;font-family:Microsoft yahei;font-size:15px;white-space: nowrap;"><%=writerNameStr %>创建于</label>
                <label style="font-family:Microsoft yahei;font-size:15px;white-space: nowrap;"><%=writeTimeStr %></label>
           </div>
       </div>

        <div class="sheetType-div">
            <label class="shetDetail_items">客户</label>
            <label class="shetDetail_content"><%=clientNameStr %></label>
        </div>
        <div class="sheetType-div">
            <label class="shetDetail_items">客户地址</label>
            <label class="shetDetail_content"><%=clientAdrStr %></label>
        </div>
        <div class="sheetType-div">
            <label class="shetDetail_items">客户联系人</label>
            <label class="shetDetail_content"><%=contactStr %></label>
        </div>
        <div class="sheetType-div">
            <label class="shetDetail_items">联系人电话</label>
            <label class="shetDetail_content"><%=dianhuaStr %></label>
        </div>
        <div class="sheetType-div" style="height:20px;width:100%;">

        </div>

        <div class="sheetType-div">
            <label class="shetDetail_items">受理人</label>
            <label class="shetDetail_content"><%=userNameStr %></label>
        </div>
        <div class="sheetType-div">
            <label class="shetDetail_items">描述</label>
            <label class="shetDetail_content"><%=sheetDetailStr %></label>
        </div>
        


        <div class="sheetType-div" id="sheetSummary_show" style="display:none">
            <label class="shetDetail_items">工单总结</label>
            <label class="shetDetail_content"><%=sheetSummaryStr %></label>
        </div>
        <div class="sheetType-div" id="sheetSummary_edit" style="display:none">
            <label class="shetDetail_items">工单总结</label>
            <textarea rows="5" placeholder="请输入工单描述" style="border: 0px; margin-top: 19px;" class="eweic-right shetDetail_content" maxlength="80" id="txtSheetSummary"></textarea>
        </div>

      

        <div class="eweic-bottom" id="linkButton">
            <ul>
                <li class="operate-list-blue-dd eweic-bottom-back-dd">
                    <p><asp:Literal runat="server" ID="ltBut">开始处理</asp:Literal></p>
                    <asp:LinkButton Width="100%" Style="display: block; color: white; left: 509px;" ID="lnkBtnSendSheetState" runat="server" OnClick="lnkBtnSendSheetState_Click" ></asp:LinkButton>
                </li>
            </ul>
        </div>


   
    </div>

        <asp:HiddenField ID="hf_sheetID" runat="server" />
        <asp:HiddenField ID="hf_userNameStr" runat="server" />
         <asp:HiddenField ID="hf_userName" runat="server" />
         <asp:HiddenField ID="hf_sheetStateStr" runat="server" />
      <%--  <asp:HiddenField ID="hf_mainID" runat="server" />
        <asp:HiddenField ID="hf_sendID" runat="server" />
        <asp:HiddenField ID="hf_sendTime" runat="server" />

        <asp:HiddenField ID="hf_chatID" runat="server" />--%>

    </form>
</body>
</html>
