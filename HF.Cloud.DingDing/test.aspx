<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="HF.Cloud.DingDing.test" %>

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
    
    <link rel="stylesheet" href="css/style.css">
    <script src="js/jquery-1.8.3.min.js"></script>
    <%--调用jquery需要的库，手机版和电脑版的不同 --%> 
    <script src="js/zepto.min.js"></script>
      <%--手机版钉钉免登引入的jsapi,和电脑版引入的不同  --%>
        <script type="text/javascript" src="http://g.alicdn.com/ilw/ding/0.9.2/scripts/dingtalk.js">  
        </script>  
      <script type="text/javascript">  
        //  var ws;
        //  if(typeof(WebSocket) == "undefined") {
        //      alert("您的浏览器不支持WebSocket");
        //  }
        //function ToggleConnectionClicked() {  
        //    try {  
        //        //var SOCKECT_ADDR = "ws://localhost:9192/chat";  //直接写端口后，后面的就不需要了
        //        //var SOCKECT_ADDR = "ws://192.168.1.41:8124";// 如果是发布的网站，就需要用这个Ip
        //        var SOCKECT_ADDR = "ws://localhost:8124";//在本机就用localhost，端口要用应用程序里设置的端口
        //        ws = new WebSocket(SOCKECT_ADDR);   
        //        ws.onopen = function (event) { alert("已经与服务器建立了连接\r\n当前连接状态：" + this.readyState); };  
        //        ws.onmessage = function (event) { alert("接收到服务器发送的数据：\r\n" + event.data); };  
        //        ws.onclose = function (event) { alert("已经与服务器断开连接\r\n当前连接状态：" + this.readyState); };  
        //        ws.onerror = function (event) {alert("WebSocket异常！" + event.data);};  
        //    } catch (ex) {  
        //        alert(ex.message);  
        //    }  
        //};  
  
        //function SendData() {  
        //    try {  
        //        ws.send("success");  
        //    } catch (ex) {  
        //        alert(ex.message);  
        //    }  
        //};  
  
        //function seestate() {  
        //    alert(ws.readyState);  //判断websocket状态，1是链接状态
        //}  
         
    </script>  


  <%--  <script type="text/javascript">
        alert("hello");
        var jjj = "{\"result\":[{\"name\":\"张三\",\"time\":\"2017-2-3\",\"detail\":\"付电话费舒服的书\"},{\"name\":\"李四\",\"time\":\"2012-2-2\",\"detail\":\"还没见过图\"}],\"chatID\":\"232\"}";
        //alert(jjj);
        //var json02 = eval("(" + jjj + ")");
        //alert(json02.chatID);
        //alert(json02.result);
        var json01 = JSON.parse(jjj);
        alert("chatID:"+json01.chatID);
        for (var i = 0; i < json01.result.length; i++) {
            alert(json01.result[i].name + " ---" + json01.result[i].time + " ---" + json01.result[i].detail);
        }
        alert(json01.result.length);
    </script>--%>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    <%--  <button id='ToggleConnection' type="button" onclick='ToggleConnectionClicked();'>  
        连接服务器</button><br />  
    <br />  
    <button id='ToggleConnection01' type="button" onclick='SendData();'>  
        发送我的名字：beston</button><br />  
    <br />  
    <button id='ToggleConnection02' type="button" onclick='seestate();'>  
        查看状态</button><br />  
    <br />  --%>
    </div>
    </form>
</body>
</html>
