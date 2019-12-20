<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddRepair.aspx.cs" Inherits="HF.Cloud.DingDing.Repair.AddRepair" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>添加维修单</title>
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
            var DD_sheetID="";
            //导航标题
            dd.biz.navigation.setTitle({
                title: "添加维修单",
                subTitle: "添加维修单...",
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
                        //"iconId": "add",//字符串，图标命名
                        "text": "发布"
                    }
                ],
                onSuccess: function (data) {
                    var menuID = data.id;
                    if (menuID == "1")//点击了发布
                    {
                        //先判断数据是否合法
                        if ($("#ddlAssetType").val() == "-1") {
                            dd.device.notification.alert({
                                message: "请选择设备类型！",
                                title: "",//可传空
                                buttonName: "确定",
                                onSuccess: function () {
                                },
                                onFail: function (err) { }
                            });
                            return;
                        }
                        if ($("#ddlCustomer").val() == "-1")
                        {
                            dd.device.notification.alert({
                                message: "请选择客户！",
                                title: "",//可传空
                                buttonName: "确定",
                                onSuccess: function () {
                                },
                                onFail: function (err) { }
                            });
                            return;
                        }
                       
                        if ($("#txtLinkName").val() == "") {
                            dd.device.notification.alert({
                                message: "请输入联系人！",
                                title: "",//可传空
                                buttonName: "确定",
                                onSuccess: function () {
                                },
                                onFail: function (err) { }
                            });
                            return;
                        }
                        if ($("#txtLinkTel").val() == "") {
                            dd.device.notification.alert({
                                message: "请输入电话！",
                                title: "",//可传空
                                buttonName: "确定",
                                onSuccess: function () {
                                },
                                onFail: function (err) { }
                            });
                            return;
                        }
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
                            //if ($("#txtSheetDetail").val() == "") {
                            //    dd.device.notification.alert({
                            //        message: "请输入工单描述！",
                            //        title: "",//可传空
                            //        buttonName: "确定",
                            //        onSuccess: function () {
                            //        },
                            //        onFail: function (err) { }
                            //    });
                            //    return;
                            //}
                        else
                        {
                            //提交数据库
                            var info = {};
                            info["MainID"] = $("#hf_mainID").val();//服务商主键
                            info["ClientID"] = $("#ddlCustomer").val();//客户主键
                            info["WriteID"] = $("#hf_WriteID").val();//发布人主键
                            info["AssetTypeID"] = $("#ddlAssetType").val();//设备类型
                            info["TaskDetail"] = $("#txtSheetDetail").val();//描述
                            info["TeamID"] = $("#ddlTeam").val();//受理组
                            info["AcceptID"] = $("#ddlUser").val();//受理人
                            info["LinkName"] = $("#txtLinkName").val();//联系人名字
                            info["LinkTel"] = $("#txtLinkTel").val();//联系人电话
                           
                            $.post("/ashx/addRepair.ashx", info, function (addSheetData) {
                               
                                if (addSheetData>0) {
                                    // alert(addSheetData);
                                    dd.device.notification.toast({
                                        text: '添加成功...'
                                    });
                                    location.href = "RepairType.aspx?dd_nav_bgcolor=FF5E97F6";
                                }
                            });
                        }



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
            <div class="eweic-chunk-white eweic-top">
                <div class="eweic-chunk-cont">
                    <p class="eweic-left">设备类型 </p>
                    <asp:DropDownList Style="border: 0px; margin-top: 18px;" runat="server" ID="ddlAssetType"></asp:DropDownList>
                </div>
            </div>
          <div class="eweic-chunk-white">
                <div class="eweic-chunk-cont">
                    <p class="eweic-left">客户名称</p>
                    <asp:DropDownList Style="border: 0px; margin-top: 18px;" runat="server" ID="ddlCustomer"></asp:DropDownList>
                </div>
            </div>

          <div class="eweic-chunk-white">
                <div class="eweic-chunk-cont">
                    <p class="eweic-left">联系人</p>
                    <input type="text" runat="server" placeholder="请输入联系人" id="txtLinkName" class="eweic-right" />
                </div>
            </div>
            <div class="eweic-chunk-white">
                <div class="eweic-chunk-cont">
                    <p class="eweic-left">电话 </p>
                    <input type="text" runat="server" placeholder="请输入电话" id="txtLinkTel" class="eweic-right" onkeyup="(this.v=function(){this.value=this.value.replace(/[^0-9-]+/,'');}).call(this)" onblur="this.v();" />
                </div>
            </div>


        
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


         <div class="eweic-chunk-white">
                <div class="eweic-chunk-cont">
                    <p class="eweic-left">描述 </p>
                    <textarea rows="5" placeholder="请输入描述" style="border: 0px; margin-top: 19px;" class="eweic-right" maxlength="80" id="txtSheetDetail"></textarea>
                </div>
            </div>
    </div>

        <asp:HiddenField ID="hf_mainID" runat="server" />
        <asp:HiddenField ID="hf_WriteID" runat="server" />
    </form>
</body>
</html>
