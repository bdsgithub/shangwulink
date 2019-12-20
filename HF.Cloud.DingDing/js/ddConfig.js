/***************************开始****************************/
/** 
 * _config 这个参数是在前台的H5文件中我定义的，它的值是通过调用步骤6中封装好的参数来获得的 
 */
/* 
我们需要明白的一点是，所有的这些文件都是放在企业应用的服务器后台，和钉钉网站没有半毛钱的关系 
并且钉钉的jsapi中唯一的作用就是提供了对config的验证和获得code值 
对于其他值得获取，如access_token,ticket,sign,username,userid都是自己在后台写java代码通过get或者post方式向 
钉钉开发平台请求得来的，并不是从jsapi中的接口得来的 
*/

//PC客户端使用DingTalkPC.config
//手机端使用dd.config
dd.config({                                                //dd.config方法会对参数进行验证  
//DingTalkPC.config({
   
    agentId: _config.agentId,  //微应用的agentID
    corpId: _config.corpId,    //公司钉钉的id
    timeStamp: _config.timeStamp,//时间戳
    nonceStr: _config.nonceStr,//随机字符串
    signature: _config.signature,//本地生成的签名，传给钉钉服务器用来比较是否一致，一致的话说明成功了
    jsApiList: [                              //需要调用的接口列表 ，必须先声明才可以用   
        'runtime.info',
        'biz.contact.choose',              //选择用户接口  
        'device.notification.confirm',     //confirm,alert,prompt都是弹出小窗口的接口     
        'device.notification.alert',
        'device.notification.prompt',
        'biz.util.openLink',
        'runtime.permission.requestOperateAuthCode'

    ]
});


/* 
*在dd.config()验证通过的情况下，就会执行ready()函数， 
*dd.ready参数为回调函数，在环境准备就绪时触发，jsapi的调用需要保证在 
*该回调函数触发后调用，否则无效,所以你会发现所有对jsapi接口的调用都会在 
*ready的回调函数里面 
*/

dd.ready(function () {
 
    /* 
    *获取容器信息，返回值为ability:版本号，也就是返回容器版本 
    *用来表示这个版本的jsapi的能力，来决定是否使用jsapi 
    */

    /*
    dd.runtime.info({
        onSuccess: function (info) {
            logger.e('runtime info: ' + JSON.stringify(info));
        },
        onFail: function (err) {
            logger.e('fail: ' + JSON.stringify(err));
        }
    });
    */
    /* 
    *获得免登授权码，需要的参数为corpid，也就是企业的ID 
    *成功调用时返回onSuccess,返回值在function的参数info中，具体操作可以在function中实现 
    *返回失败时调用onFail 
    */
    
    dd.runtime.permission.requestAuthCode({
        corpId: _config.corpId,
        onSuccess: function (info) {      //成功获得code值,code值在info中  通过info.code获取
            
            //alert('info.code: ' + info.code);
            //其中success方法和error方法是回调函数，分别表示成功交互后和交互失败情况下处理的方法 
           
            $.ajax({
                url: '/ashx/userinfo.ashx?code=' + info.code,
                type: 'GET',
                /* 
                *ajax中的success为请求得到相应后的回调函数，function(response,status,xhr) 
                *response为响应的数据，status为请求状态，xhr包含XMLHttpRequest对象 
                */
                success: function (data) {
                    //alert("document.cookie1");
                    var userInfo = JSON.parse(data);
                    //alert(userInfo);
                    //alert(userInfo.position);
                    //alert(userInfo.errcode);
                    //  alert("用户" + info.name + "登录成功");
                    //$("#lbl_Name").text(userInfo.name); 
                    //$("#lbl_Mobile").html(userInfo.mobile);
                    //$("#lbl_userid").html(userInfo.userid);
                   //取得手机号，把手机号写入cookie保存，然后在base页面获取cookie保存的手机号搜索SB_User表获取用户信息
                    
                    document.cookie = "Mobile=" + userInfo.mobile + ";path=/";
                    document.cookie = "Userid=" + userInfo.userid + ";path=/";//当前用户钉钉里的ID号


                    //alert(document.cookie.valueOf("Mobile"));
                },
                error: function (errorType, error) {
                    //logger.e("yinyien:" + _config.corpId);
                    alert("获取用户信息发送错误!" + errorType + ', ' + error);
                   // alert(errorType + ', ' + error);
                }
            });


        },
        onFail: function (err) {         //获得code值失败  
            alert('fail: ' + JSON.stringify(err));
        }
    });

    //dd.ui.input.plain({
    //    placeholder:"请输入评论...",
    //    text:"请输入回复..",
    //    onSuccess:function(result)
    //    {
    //        alert(result);
    //    },
    //    onFail: function (err) {
    //    }
    //});


    //导航标题
    dd.biz.navigation.setTitle({
        title: "易维客",
        subTitle: "易维客...",
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


/* 
*在dd.config函数验证没有通过下执行这个函数 
*/
dd.error(function (err) {
    alert('dd error: ' + JSON.stringify(err));
});


/* 
dd中接口的约定： 
所有接口都为异步 
接受一个object类型的参数,function在js中也是一个object 
成功回调 onSuccess(某些异步接口的成功回调，将在事件触发时被调用，具体详情请查看相关onSuccess回调时机，未做描述的即为同步接口) 
失败回调 onFail 
 
模板如下： 
dd.命名空间.功能.方法({ 
    参数1: '', 
    参数2: '', 
    onSuccess: function(result) { 
    //成功回调 
  //{ 
        //所有返回信息都输出在这里 
  //} 
    }, 
    onFail: function(){ 
    //失败回调 
    } 
}) 
*/

/**************************************结束********************************/