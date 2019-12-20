<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HallDistribute.aspx.cs" Inherits="HF.Cloud.CustomerWeb.map.HallDistribute" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script src="../js/jquery-1.8.3.min.js"></script>
    <title>大厅业务分布图</title>
    <link href="../css/home.css" rel="stylesheet" />
    <style>
   /*.main{ width:800px; height:520px; margin-left:20px; float:left; background:#c9e8fd; position:relative;}
.xianjinqu{ width:458px; height:103px; position:absolute; left:0; top:40px; cursor: pointer;background-color:#66D3EE;border:2px solid #D5D5D5;text-align:center;line-height: 80px;}
.shebeiqu{ width:158px; height:103px; position:absolute; left:462px; top:40px; cursor:pointer;background-color:#66D3EE;border:2px solid #D5D5D5;text-align:center;line-height: 80px;}
.bangongshi{width:172px; height:168px; position:absolute; right:0; top:40px; cursor:pointer;background-color:#66D3EE;border:2px solid #D5D5D5;text-align:center;line-height: 80px;}
.licaiqu{ width:95px; height:259px; position: absolute; left:0; top:147px; cursor:pointer;background-color:#66D3EE;border:2px solid #D5D5D5;text-align:center;line-height: 80px;}
.VIP{width:172px; height:166px; position:absolute; right:0; top:212px; cursor: pointer;background-color:#66D3EE;border:2px solid #D5D5D5;text-align:center;line-height: 80px;}
.ATM{ width:172px; height:134px; position:absolute; right:0; bottom:0; cursor:pointer;background-color:#66D3EE;border:2px solid #D5D5D5;text-align:center;line-height: 80px;}
.zidongfuwuqu{ width:184px; height:106px; position:absolute; left:0; bottom:0; cursor:pointer;background-color:#66D3EE;border:2px solid #D5D5D5;text-align:center;line-height: 80px;}
.jiaohaoji{ width:100px; height:60px; position:absolute; right:220px; bottom:90px; cursor:pointer;background-color:#66D3EE;border:2px solid #D5D5D5;text-align:center;line-height: 50px;}
.damenkou{  width:153px; height:38px; position:absolute; right:300px; bottom:0; cursor:pointer;background-color:#66D3EE;border:2px solid #D5D5D5;text-align:center;line-height: 30px;}
.SpanFontStyle  {
   font-size:25px;
   font-family:Microsoft YaHei; 
    color:#FFFFFF;
}*/

.main{ width:700px; height:450px; float:left;  background: url(image/bg.jpg) no-repeat;background-size:700px 450px;  position:relative;}
.xianjinqu{ width:418px; height:103px; position:absolute; left:0; top:0px; cursor: pointer;}
.shebeiqu{ width:138px; height:103px; position:absolute; left:412px; top:0px; cursor:pointer;}
.bangongshi{width:142px; height:168px; position:absolute; right:0; top:0px; cursor:pointer;}
.licaiqu{ width:95px; height:229px; position: absolute; left:0; top:107px; cursor:pointer;}
.VIP{width:142px; height:166px; position:absolute; right:0; top:172px; cursor: pointer;}
.ATM{ width:142px; height:134px; position:absolute; right:0; bottom:0; cursor:pointer;}
.zidongfuwuqu{ width:184px; height:106px; position:absolute; left:0; bottom:0; cursor:pointer;}
.jiaohaoji{ width:100px; height:60px; position:absolute; right:200px; bottom:70px; cursor:pointer;}
.damenkou{  width:153px; height:40px; position:absolute; right:270px; bottom:0; cursor:pointer;}



  </style>
    <script type="text/javascript">
        $(document).ready(function () {
           
            $(".xianjinqu").click(function () {
                $.post('GetAssetByArea.ashx',
                    {MainID:<%=mainID%>, AreaID: '1', ClientID: <%=clientID %> },
                    function (data) {
                            //layer.open({
                            //    type: 1,
                            //    title: '设备信息',
                            //    area: ['450px', '250px'],
                            //    shadeClose: true, //点击遮罩关闭
                            //    content: '\<\div style="padding:20px;">'+data+'\<\/div>'
                            //});
                            $("#assetList").html(data);
                    }
                    );
            });
         
            ClickFunction(".shebeiqu","2");
            ClickFunction(".bangongshi","3");
            ClickFunction(".licaiqu","4");
            ClickFunction(".VIP","5");
            ClickFunction(".ATM","6");
            ClickFunction(".zidongfuwuqu","7");
            ClickFunction(".jiaohaoji","8");
            ClickFunction(".damenkou","9");
        })

        function ClickFunction(ClassName,areaID){
             $(ClassName).click(function () {
                 $.post('GetAssetByArea.ashx',
                    {MainID:<%=mainID%>, AreaID: areaID, ClientID: <%=clientID %> },
                    function (data) {
                        //layer.open({
                        //    type: 1,
                        //    title: '设备信息',
                        //    area: ['450px', '250px'],
                        //    shadeClose: true, //开启遮罩关闭
                        //    content: '\<\div style="padding:20px;">'+data+'\<\/div>'
                        //});
                        $("#assetList").html(data);
                    }
                    );
            });
        }









    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style=" width:700px; height:510px; margin-left:50px; float:left; ">
        <div style="width:100%;height:50px;background-color:#FFFFFF;"><p id="hallName" runat="server" style="text-align:center;color:#5E5E5E;margin-top:0px;padding-top:5px;font-size:20px;"></p></div>
        <div class="main">
           <%-- <div style="width:100%;height:40px;background-color:#FFFFFF;"><p id="hallName" runat="server" style="text-align:center;color:#5E5E5E;margin-top:0px;padding-top:5px;font-size:20px;"></p></div>
        	<div class="xianjinqu" title="点击查看"><span class="SpanFontStyle">现金区</span></div>
            <div class="shebeiqu" title="点击查看"><span class="SpanFontStyle">设备区</span></div>
            <div class="bangongshi" title="点击查看"><span class="SpanFontStyle">办公室</span></div>
            <div class="licaiqu" title="点击查看"><span class="SpanFontStyle">理财区</span></div>
            <div class="VIP" title="点击查看"><span class="SpanFontStyle">VIP</span></div>
            <div class="ATM" title="点击查看"><span class="SpanFontStyle">ATM</span></div>
            <div class="zidongfuwuqu" title="点击查看"><span class="SpanFontStyle">自助服务</span></div>
            <div class="jiaohaoji" title="点击查看"><span class="SpanFontStyle">叫号机</span></div>
            <div class="damenkou" title="点击查看"><span class="SpanFontStyle">大门口</span></div>--%>
        
            	<div class="xianjinqu" title="点击查看"></div>
                <div class="shebeiqu" title="点击查看"></div>
                <div class="bangongshi" title="点击查看"></div>
                <div class="licaiqu" title="点击查看"></div>
                <div class="VIP" title="点击查看"></div>
                <div class="ATM" title="点击查看"></div>
                <div class="zidongfuwuqu" title="点击查看"></div>
                <div class="jiaohaoji" title="点击查看"></div>
                <div class="damenkou" title="点击查看"></div>




        </div>
            </div>
         <div class="main-l border-c float-right float-r-s" style="margin-top:20px;width:25%;margin-right:20px;float:right;overflow:scroll;">
            <ul>
                <li class="background-c font-color-r" style="text-align: left; font-size: 20px;"><span> &nbsp; 设备列表</span></li>
                <li>
                   <div id="assetList" style="margin-left:10px;">点击大厅区域查看设备...</div>
                    
                 </li>
            </ul>
        </div>



    </form>
</body>
</html>
