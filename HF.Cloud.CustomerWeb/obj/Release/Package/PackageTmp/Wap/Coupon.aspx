<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Coupon.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Wap.Coupon" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>领取优惠券</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent">
    <meta name="format-detection" content="telephone=no">
    <meta name="full-screen" content="yes">
    <meta name="x5-page-mode" content="app">
     <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/weui/1.1.2/style/weui.min.css" rel="stylesheet"/>
      <style type="text/css">
        /*.weui-input{
            text-align: right;
        }
        body{
            background-color: #f5f5f5;
            padding-top: 20px; 
        }
        .option{
            position: absolute;
            bottom: 10px;
            left: 10px;
            right: 10px;
            z-index: -1;
        }
        .title{
            clear: both;
            text-align: center;
            line-height: 60px;
            height: 60px;
            font-size: 20px;
            font-weight: bolder;
            color: rgba(8, 6, 13, 0.67);
        }
       */
    ::-webkit-input-placeholder { /* WebKit browsers */
        color:    #A9A9A9;
    }
    :-moz-placeholder { /* Mozilla Firefox 4 to 18 */
       color:    #A9A9A9;
       opacity:  1;
    }
    ::-moz-placeholder { /* Mozilla Firefox 19+ */
       color:    #A9A9A9;
       opacity:  1;
    }
    :-ms-input-placeholder { /* Internet Explorer 10+ */
       color:    #A9A9A9;
    }
        *{
            margin: 0;
            padding: 0;
        }
        .main{
            position: relative;
        }
        #tel {
            display: inline-block;
            background-color: #ffffff;
            border: none;
            text-align: center;
            width: 70%;
            height: 6%;
            position: absolute;
            left: 15%;
            top: 40%;
            font-size: 1em;
            outline: none;
            color: #A9A9A9;

        }
        .back{
            max-width: 100%;

        }
        .sub img{
            max-width: 100%;
        }
        .sub{
            width: 50%;
            text-align: center;
            position: absolute;
            top: 50%;
            left: 50%;
            margin-left: -25%;
        }


        
       
        .clearfix:after{
            display: inline-block;
            height: 0;
            content: '1';
            visibility: hidden;
            clear: both;
        }
      
        #money img{
            max-width: 100%;
        }
        #money{
            text-align: center;
            width: 40%;
            position: absolute;
            top: 15%;
            left: 50%;
            margin-left: -20%;
        }
        #val{
            float: right;
            font-size: 2em;
            color: #ee1d27;
        }
        #content{
            width: 100%;
            position: absolute;
            top: 22%;
            left: 50%;
            margin-left: -50%;
            font-size: 1.5em;
            font-family: '微软雅黑 Bold', '微软雅黑';
            color: #E15655;

        }
        #ticket{
            font-size: 1.6em;
            color: #FFE347;
        }

         #result
        {
            display:none;
        }
      </style>
      <script type="text/javascript">
      
        $(function () {
            var m = document.getElementById("tel");
            m.onkeyup = function () {
                var demovalue = this.value;
                this.value = demovalue.replace(/\D/g, "");
            };

            //确认
            $("#ltBut").click(function () {
                var userTel = $("#tel").val();
                if (userTel.length != 11)
                {
                    alert("输入的电话号码格式错误！");
                    return;
                }
               
                //领取优惠券
                var info = {};
                info["usertel"] = $("#tel").val();//电话
                info["ticket"] = $("#ticket").text();//优惠券金额
                $.post("ashx/Coupon.ashx", info, function (data) {
                    if (data == "success") {
                        //alert("领取成功！");
                        $("#addTicket").css("display", "none");
                        $("#result").css("display", "block");
                    }
                    else if (data == "ishad")
                    {
                       alert("不可重复领取！");
                    }
                });
            });
        })
         </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="addTicket">
      <%--    <div class="content" >
                <div class="weui-cells">
              
                    <div style="text-align:center;margin-top:35px;">输入电话领取！</div>
                   
                    <div class="weui-cell" style="margin-left:30px;margin-right:30px;margin-top:50px;">
                        <div class="weui-cell__hd"><label class="weui-label">电话</label></div>
                        <div class="weui-cell__bd">
                            <input id="userTel" class="weui-input" type="text"/>
                        </div>
                    </div>
                </div>
        </div>

         <div class="option">
            <a id="ltBut" class="weui-btn weui-btn_primary">领 取</a>
        </div>--%>
          <div class="main" style="text-align: center">
        <img class="back" src="image/back.png">
        <input id="tel" placeholder="请输入手机号">
        <a class="sub" id="ltBut"><img src="image/马上领券.png"></a>
    </div>
    </div>





        <div id="result">
           <%-- <div style="text-align:center;margin-top:50px;">恭喜你！获得 <label id="ticket" runat ="server" style="color:red;font-size:40px;"></label> 元优惠券</div> 
       
             <div class="option">
               <a id="btnSkip" class="weui-btn weui-btn_primary" href="http://demo1.eweic.com/LabelWap/login.aspx">登 录</a>
             </div>--%>
            <div class="main" style="text-align: center">
        <span id="content">恭喜获得 <label id="ticket" runat ="server"></label> 元现金券</span>
        <img class="back" src="image/优惠券使用背景.png">
        <a class="sub" href="http://www.eweic.com/LabelWap/manage.aspx"><img src="image/立即使用.png"></a>
    </div>
        </div>

    </form>
</body>
</html>
