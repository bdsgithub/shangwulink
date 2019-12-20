<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PatrolDetail.aspx.cs" Inherits="HF.Cloud.DingDing.Patrol.PatrolDetail" %>

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
                 title: "设备巡检",
                 subTitle: "设备巡检...",
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
                         "text": "保存"
                     }
                   
                 ],
                 onSuccess: function (data) {
              
                     var menuID = data.id;
                     if (menuID == "1")//如果点击了巡检计划
                     {
                         //提交数据库
                         var info = {}; 
                         info["AssetRecordId"] = $("#hf_AssetRecordId").val();//设备记录Id
                         if ($("#sate01").attr("checked"))
                         {
                             info["AssetStatus"] = "1";//设备状态
                         }
                         else
                         {
                             info["AssetStatus"] = "2";//设备状态
                         }
                         if ($("#outside01").attr("checked")) {
                             info["OutSideStatus"] = "1";//外观描述
                         }
                         else
                         {
                             info["OutSideStatus"] = "2";//外观描述
                         }
                        
                         info["AssetDetail"] = $("#txtAssetDetail").val();//巡检描述
                         info["Pic"] = "";//图片名称
                         info["Base64"] = "";//Base64图片
                         info["PagerOrderCode"] = $("#txtPagerOrderCode").val();//巡检编号 纸质编号
                        
                         $.post("/ashx/saveAssetPatrol.ashx", info, function (addSheetData) {
                             alert(addSheetData);
                          

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
                    <p class="eweic-left">设备名称</p>
                     <label class="shetDetail_content"><%=assetName %></label>
                </div>
        </div>
        <div class="eweic-chunk-white">
                <div class="eweic-chunk-cont">
                    <p class="eweic-left">品牌</p>
                     <label class="shetDetail_content"><%=brandName %></label>
                </div>
        </div>
        <div class="eweic-chunk-white">
                <div class="eweic-chunk-cont">
                    <p class="eweic-left">型号</p>
                     <label class="shetDetail_content"><%=modelName %></label>
                </div>
        </div>
        <div class="eweic-chunk-white">
                <div class="eweic-chunk-cont">
                    <p class="eweic-left">序列号</p>
                     <label class="shetDetail_content"><%=assetXuLie %></label>
                </div>
        </div>
        <div class="sheetType-div" style="height:20px;">    
        </div>
        
        <div class="eweic-chunk-white">
                <div class="eweic-chunk-cont">
                    <p class="eweic-left">设备状态</p>
                   <asp:RadioButton style="float:right;white-space: nowrap;width:100px;" TextAlign="Right" ID="sate02" runat="server" Text="故障" GroupName="assetState"/>
                         <asp:RadioButton style="float:right;white-space: nowrap;width:100px;height:30px;"   ID="sate01" runat="server" Text="正常" GroupName="assetState"/>
                         
                     
                </div>
        </div>
         <div class="eweic-chunk-white">
                <div class="eweic-chunk-cont">
                    <p class="eweic-left">外观描述</p>
                    <asp:RadioButton style="float:right;white-space: nowrap;width:100px;height:10px;" ID="outside02" runat="server" Text="破旧" GroupName="outside"/>
                         <asp:RadioButton style="float:right;white-space: nowrap;width:100px;height:10px;" ID="outside01" runat="server" Text="正常" GroupName="outside"/>
                </div>
        </div>
   
        <div class="eweic-chunk-white">
                <div class="eweic-chunk-cont">
                    <p class="eweic-left">巡检编号</p>
                    <input type="text" runat="server"  id="txtPagerOrderCode" class="eweic-right"/>
                </div>
            </div>
         <div class="eweic-chunk-white">
                <div class="eweic-chunk-cont">
                    <p class="eweic-left">巡检描述</p>
                    <textarea rows="5"  style="border: 0px; margin-top: 19px;" class="eweic-right" maxlength="80" id="txtAssetDetail" ></textarea>
                </div>
            </div>
        <asp:HiddenField ID="hf_AssetRecordId" runat="server" />

    </div>
    </form>
    
</body>
</html>
