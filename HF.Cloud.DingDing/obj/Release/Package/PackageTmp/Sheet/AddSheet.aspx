<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddSheet.aspx.cs" Inherits="HF.Cloud.DingDing.Sheet.AddSheet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>添加工单</title>
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
        var _config = <%=configStr %>; 
        
    </script>


    <script type="text/javascript">  
       
        //手机端使用dd.config
        dd.config({          //dd.config方法会对参数进行验证  
            agentId: _config.agentId,  //微应用的agentID
            corpId: _config.corpId,    //公司钉钉的id
            timeStamp: _config.timeStamp,//时间戳
            nonceStr: _config.nonceStr,//随机字符串
            signature: _config.signature,//本地生成的签名，传给钉钉服务器用来比较是否一致，一致的话说明成功了
            //type:"0",   
            jsApiList: [                              //需要调用的接口列表 ，必须先声明才可以用   
                'runtime.info',
                'biz.contact.choose',              //选择用户接口  
                'device.notification.confirm',     //confirm,alert,prompt都是弹出小窗口的接口     
                'device.notification.alert',
                'device.notification.prompt',
                'biz.util.openLink',     
                'biz.chat.pickConversation',
                'runtime.permission.requestOperateAuthCode',
                'biz.ding.post'
            ]
        });

        dd.ready(function () {
            var DD_sheetID="";
            //导航标题
            dd.biz.navigation.setTitle({
                title: "添加工单",
                subTitle: "添加工单...",
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
                    //,
                    //{
                    //    "id": "2",//字符串
                    //    //"iconId": "add",//字符串，图标命名
                    //    "text": "发消息"
                    //}
                ],
                onSuccess: function (data) {
                    var menuID = data.id;
                    if (menuID == "1")//点击了发布
                    {
                        //alert($("#ddlCustomer").val());
                        //先判断数据是否合法
                        //window.location.href = "AddSheet.aspx?dd_nav_bgcolor=FF5E97F6";
                        if($("#ddlCustomer").val()=="-1")
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
                        if ($("#ddlSheetType").val() == "-1") {
                            dd.device.notification.alert({
                                message: "请选择工单类型！",
                                title: "",//可传空
                                buttonName: "确定",
                                onSuccess: function (){
                                },
                                onFail: function (err){ }
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
                            info["SheetType"] = $("#ddlSheetType").val();//工单类型
                            info["SheetDetail"] = $("#txtSheetDetail").val();//工单描述
                            info["TeamID"] = $("#ddlTeam").val();//受理组
                            info["AcceptID"] = $("#ddlUser").val();//受理人
                            info["LinkName"] = $("#txtLinkName").val();//联系人名字
                            info["LinkTel"] = $("#txtLinkTel").val();//联系人电话
                            $.post("/ashx/addSheet.ashx", info, function (addSheetData) {
                                alert(addSheetData);
                                 
                                if (addSheetData>0) {
                                    DD_sheetID=addSheetData;
                                    //dd.device.notification.toast({
                                    //    text: '发布成功...'
                                    //});
//////////////////////////////////////////////////////////////////
                                    ////选择联系人
                                    //dd.biz.contact.choose({
                                    //    startWithDepartmentId: 0, //-1表示打开的通讯录从自己所在部门开始展示, 0表示从企业最上层开始，(其他数字表示从该部门开始:暂时不支持)
                                    //    multiple: true, //是否多选： true多选 false单选； 默认true
                                    //    users: [], //默认选中的用户列表，userid；成功回调中应包含该信息
                                    //    disabledUsers:[],// 不能选中的用户列表，员工userid
                                    //    corpId: "ding8fd03d105d14827d", //企业id
                                    //    max: 30, //人数限制，当multiple为true才生效，可选范围1-1500
                                    //    limitTips:"人数过多", //超过人数限制的提示语可以用这个字段自定义
                                    //    isNeedSearch:true, // 是否需要搜索功能
                                    //    title : "选择要通知的人员", // 如果你需要修改选人页面的title，可以在这里赋值 
                                    //    local:"false", // 是否显示本地联系人，默认false
                                    //    onSuccess: function(data) {
                                    //        var touser="";
                                    //        for(var person in data)
                                    //        {
                                    //            //alert("name:"+data[person].name+"id:"+data[person].emplId);
                                    //            touser+=data[person].emplId;//获取所选人员的userid
                                    //            touser+="|";
                                    //        }
                                    //        touser=touser.substring(0,touser.length-1);
                                    //        alert("touser:"+touser);

                                    //        //var content="客户："+$("#ddlCustomer").val()+" --- 工单类型："+$("#ddlSheetType").val();
                                    //        var content="您有新的工单，请注意查收！"
                                    //        var info = {};
                                    //        info["touser"] = touser;//员工列表id
                                    //        info["toparty"] ="";//部门列表Id
                                    //        info["agentid"] =_config.agentId;//企业应用id
                               
                                    //        info["msgtype"] ="oa";//消息类型
                                    //        info["content"] =content;//消息内容
                                    //        info["message_url"] = "http://192.168.1.42:8123/Sheet/SheetDetail.aspx?dd_nav_bgcolor=FF5E97F6&sheetID="+DD_sheetID;//
                                    //        info["customer"] =$("#ddlCustomer").find("option:selected").text();//客户
                                    //        info["sheetType"] =$("#ddlSheetType").find("option:selected").text();//工单类型
                                            
                                    //        $.post(
                                    //            "/ashx/sendCorpConversation.ashx", 
                                    //            info, 
                                    //            function (data) {
                                    //                if (data == "success") {
                                    //                    dd.device.notification.toast({
                                    //                        text: '发送会话成功...'
                                    //                    });
                                    //                location.href = "SheetType.aspx?dd_nav_bgcolor=FF5E97F6";
                                    //                }
                                    //            });

                                    //    },
                                    //    onFail : function(err) {
                                    //        alert('choose_fail: ' + JSON.stringify(err));
                                    //    }
                                    //});

                                    //////////////////////////////////////////////////////////////////////



////////////////----------------------企业会话消息开始
//                                    //选择联系人
//                                    dd.biz.contact.choose({
//                                        startWithDepartmentId: 0, //-1表示打开的通讯录从自己所在部门开始展示, 0表示从企业最上层开始，(其他数字表示从该部门开始:暂时不支持)
//                                        multiple: true, //是否多选： true多选 false单选； 默认true
//                                        users: [], //默认选中的用户列表，userid；成功回调中应包含该信息
//                                        disabledUsers:[],// 不能选中的用户列表，员工userid
//                                        corpId: "ding8fd03d105d14827d", //企业id
//                                        max: 30, //人数限制，当multiple为true才生效，可选范围1-1500
//                                        limitTips:"人数过多", //超过人数限制的提示语可以用这个字段自定义
//                                        isNeedSearch:true, // 是否需要搜索功能
//                                        title : "请选择要通知的人员", // 如果你需要修改选人页面的title，可以在这里赋值 
//                                        local:"false", // 是否显示本地联系人，默认false
//                                        onSuccess: function(data) {
//                                            var touser="";
//                                            for(var person in data)
//                                            {
//                                                //alert("name:"+data[person].name+"id:"+data[person].emplId);
//                                                touser+=data[person].emplId;//获取所选人员的userid
//                                                touser+="|";
//                                            }
//                                            touser=touser.substring(0,touser.length-1);
//                                            alert("touser:"+touser);

//                                            ////////////////////////////反馈式会话消息开始///////////////////////////////////////////
//                                            //获取微应用反馈式操作的临时授权码code
//                                            dd.runtime.permission.requestOperateAuthCode({
//                                                corpId:_config.corpId,
//                                                agentId:_config.agentId,
//                                                onSuccess:function(result){
//                                                    var code=result.code;
//                                                    //alert("code:"+code);
//                                                    //var content="客户："+$("#ddlCustomer").val()+" --- 工单类型："+$("#ddlSheetType").val();
//                                                    var content="您有新工单，请注意查收！"
//                                                    var info = {};
//                                                    info["touser"] = touser;//员工列表id
//                                                    info["toparty"] ="";//部门列表Id
//                                                    info["agentid"] =_config.agentId;//企业应用id
                               
//                                                    info["msgtype"] ="oa";//消息类型
//                                                    info["content"] =content;//消息内容
//                                                    info["message_url"] = "http://192.168.1.41:8123/Sheet/SheetDetail.aspx?dd_nav_bgcolor=FF5E97F6&sheetID="+DD_sheetID;//
//                                                    info["customer"] =$("#ddlCustomer").find("option:selected").text();//客户
//                                                    info["sheetType"] =$("#ddlSheetType").find("option:selected").text();//工单类型
//                                                    info["code"] =code;//临时授权码code
//                                                    $.post(
//                                                        "/ashx/sendCorpConversation.ashx", 
//                                                        info, 
//                                                        function (send_data) {
//                                                            if (send_data == "success") {
//                                                                dd.device.notification.toast({
//                                                                    text: '发送会话成功...'
//                                                                });
//                                                                location.href = "SheetType.aspx?dd_nav_bgcolor=FF5E97F6";
//                                                            }
//                                                        });


//                                                },
//                                                onFail : function(err) {
//                                                    alert('requestOperateAuthCode_fail: ' + JSON.stringify(err));
//                                                }
//                                            });

//                                            ////////////////////////////反馈式会话消息结束///////////////////////////////////////////
//                                        },
//                                        onFail : function(err) {
//                                            alert('choose_fail: ' + JSON.stringify(err));
//                                        }
//                                    });
//                                    //////////////----------------------企业会话消息结束





                                    ////////////--------------------发钉消息开始
                                    dd.biz.ding.post({
                                        users : [],//用户列表，工号
                                        corpId:"ding8fd03d105d14827d", //企业id
                                        type: 2, //钉类型 1：image  2：link
                                        alertType: 2,//钉提醒类型 0:电话, 1:短信, 2:应用内
                                        //alertDate: {"format":"yyyy-MM-dd HH:mm","value":"2015-05-09 08:00"},
                                        attachment: {
                                            title:"新工单通知",
                                            url:"http://192.168.1.41:8123/Sheet/SheetDetail.aspx?dd_nav_bgcolor=FF5E97F6&sheetID="+DD_sheetID,
                                           // image:"",//
                                            text:"客户："+$("#ddlCustomer").find("option:selected").text()+" 工单类型:"+$("#ddlSheetType").find("option:selected").text()
                                        },
                                        text:"您有新的工单消息，请注意查收！", //消息
                                        onSuccess : function(dingResult) {
                                            if(dingResult.dingCreateResult=true)
                                            {
                                                dd.device.notification.toast({
                                                text: '发送钉消息成功...'
                                                });
                                                location.href = "SheetType.aspx?dd_nav_bgcolor=FF5E97F6";
                                            }
                                        },
                                        onFail : function(err) {
                                            alert('ding.post_fail: ' + JSON.stringify(err));
                                        }
                                    })
                                    ////////////--------------------发钉消息结束
                                }
                            });
                        }



                    }
                 

                    //if (menuID == "2")//点击了发消息
                    //{
                    //    //选择联系人
                    //    dd.biz.contact.choose({
                    //        startWithDepartmentId: 0, //-1表示打开的通讯录从自己所在部门开始展示, 0表示从企业最上层开始，(其他数字表示从该部门开始:暂时不支持)
                    //        multiple: true, //是否多选： true多选 false单选； 默认true
                    //        users: [], //默认选中的用户列表，userid；成功回调中应包含该信息
                    //        disabledUsers:[],// 不能选中的用户列表，员工userid
                    //        corpId: "ding8fd03d105d14827d", //企业id
                    //        max: 30, //人数限制，当multiple为true才生效，可选范围1-1500
                    //        limitTips:"人数过多", //超过人数限制的提示语可以用这个字段自定义
                    //        isNeedSearch:true, // 是否需要搜索功能
                    //        title : "选择人员", // 如果你需要修改选人页面的title，可以在这里赋值 
                    //        local:"false", // 是否显示本地联系人，默认false
                    //        onSuccess: function(data) {
                    //            var touser="";
                    //            for(var person in data)
                    //            {
                    //                //alert("name:"+data[person].name+"id:"+data[person].emplId);
                    //                touser+=data[person].emplId;//获取所选人员的userid
                    //                touser+="|";
                    //            }
                    //            touser=touser.substring(0,touser.length-1);
                    //            alert("touser:"+touser);

                    //            ////////////////////////////反馈式会话消息开始///////////////////////////////////////////
                    //            //获取微应用反馈式操作的临时授权码code
                    //            dd.runtime.permission.requestOperateAuthCode({
                    //                corpId:_config.corpId,
                    //                agentId:_config.agentId,
                    //                onSuccess:function(result){
                    //                    //alert("requestOperateAuthCode进来了")
                    //                    var code=result.code;
                    //                    //alert("code:"+code);
                    //                    //var content="客户："+$("#ddlCustomer").val()+" --- 工单类型："+$("#ddlSheetType").val();
                    //                    var content="您有新工单，请注意查收！"
                    //                    var info = {};
                    //                    info["touser"] = touser;//员工列表id
                    //                    info["toparty"] ="";//部门列表Id
                    //                    info["agentid"] =_config.agentId;//企业应用id
                               
                    //                    info["msgtype"] ="oa";//消息类型
                    //                    info["content"] =content;//消息内容
                    //                    info["message_url"] = "http://192.168.1.42:8123/Sheet/SheetDetail.aspx?dd_nav_bgcolor=FF5E97F6&sheetID="+DD_sheetID;//
                    //                    info["customer"] =$("#ddlCustomer").find("option:selected").text();//客户
                    //                    info["sheetType"] =$("#ddlSheetType").find("option:selected").text();//工单类型
                    //                    info["code"] =code;//临时授权码code
                    //                    $.post(
                    //                        "/ashx/sendCorpConversation.ashx", 
                    //                        info, 
                    //                        function (data) {
                    //                            if (data == "success") {
                    //                                dd.device.notification.toast({
                    //                                    text: '发送会话成功...'
                    //                                });
                    //                                //location.href = "SheetType.aspx?dd_nav_bgcolor=FF5E97F6";
                    //                            }
                    //                        });


                    //                },
                    //                onFail : function(err) {
                    //                    alert('requestOperateAuthCode_fail: ' + JSON.stringify(err));
                    //                }
                    //            });
                    //            ////////////////////////////反馈式会话消息结束///////////////////////////////////////////
                    //        },
                    //        onFail : function(err) {
                    //            alert('choose_fail: ' + JSON.stringify(err));
                    //        }
                    //    });
                    //}



                    if(menuID == "2")//发钉
                    {
                            //选择联系人
                            dd.biz.contact.choose({
                                startWithDepartmentId: 0, //-1表示打开的通讯录从自己所在部门开始展示, 0表示从企业最上层开始，(其他数字表示从该部门开始:暂时不支持)
                                multiple: true, //是否多选： true多选 false单选； 默认true
                                users: [], //默认选中的用户列表，userid；成功回调中应包含该信息
                                disabledUsers:[],// 不能选中的用户列表，员工userid
                                corpId: "ding8fd03d105d14827d", //企业id
                                max: 30, //人数限制，当multiple为true才生效，可选范围1-1500
                                limitTips:"人数过多", //超过人数限制的提示语可以用这个字段自定义
                                isNeedSearch:true, // 是否需要搜索功能
                                title : "选择人员", // 如果你需要修改选人页面的title，可以在这里赋值 
                                local:"false", // 是否显示本地联系人，默认false
                                onSuccess: function(data) {
                                    var touser="";
                                    for(var person in data)
                                    {
                                        //alert("name:"+data[person].name+"id:"+data[person].emplId);
                                        touser+=data[person].emplId;//获取所选人员的userid
                                        touser+="|";
                                    }
                                    touser=touser.substring(0,touser.length-1);
                                    alert("touser:"+touser);
 
                                    dd.biz.ding.post({
                                        users : [],//用户列表，工号
                                        corpId:"ding8fd03d105d14827d", //企业id
                                        type: 2, //钉类型 1：image  2：link
                                        alertType: 2,
                                        alertDate: {"format":"yyyy-MM-dd HH:mm","value":"2015-05-09 08:00"},
                                        attachment: {
                                            title: '',
                                            url: '',
                                            image: '',
                                            text: ''
                                        },
                                        text: '', //消息
                                    onSuccess : function() {},
                                    onFail : function() {}
                                })

                                
                                
                                },
                                onFail : function(err) {
                                    alert('choose_fail: ' + JSON.stringify(err));
                                }
                            });
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
            <div class="eweic-chunk-white">
                <div class="eweic-chunk-cont">
                    <p class="eweic-left">客户名称</p>
                    <asp:DropDownList Style="border: 0px; margin-top: 18px;" runat="server" ID="ddlCustomer"></asp:DropDownList>
                </div>
            </div>
            <div class="eweic-chunk-white eweic-top">
                <div class="eweic-chunk-cont">
                    <p class="eweic-left">工单类型 </p>
                    <asp:DropDownList Style="border: 0px; margin-top: 18px;" runat="server" ID="ddlSheetType"></asp:DropDownList>
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
                    <p class="eweic-left">工单描述 </p>
                    <textarea rows="5" placeholder="请输入工单描述" style="border: 0px; margin-top: 19px;" class="eweic-right" maxlength="80" id="txtSheetDetail"></textarea>
                </div>
            </div>





        </div>


        <asp:HiddenField ID="hf_mainID" runat="server" />
        <asp:HiddenField ID="hf_WriteID" runat="server" />
        <asp:HiddenField ID="hf_dd_userid" runat="server" />
        <asp:HiddenField ID="hf_config" runat="server" />


    </form>
</body>
</html>
