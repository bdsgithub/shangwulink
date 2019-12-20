<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Finish.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Wap.Finish" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>工单完成</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent">
    <meta name="format-detection" content="telephone=no">
    <meta name="full-screen" content="yes">
    <meta name="x5-page-mode" content="app">
<%--    <link rel="stylesheet" href="/Wechats/css/style.css">--%>
    <script src="/Wechats/js/zepto.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/weui/1.1.2/style/weui.min.css" rel="stylesheet"/>
    <style type="text/css">
         body{
            background-color: #f5f5f5;
            padding-top: 20px;
        }
        .option{
            position: absolute;
            bottom: 10px;
            left:10px;
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
    </style>
      <script type="text/javascript">


         $(function () {
             $("#btnSubmit").bind("click", function () {
                
                 //提交数据库
                 var info = {};
                 info["mid"] = $("#hf_mid").val();//服务商id
                 info["cid"] = $("#hf_cid").val();//标签id

                 info["sheetid"] = $("#hf_sheetid").val();//工单id

                 $.post("ashx/Finish.ashx", info, function (addSheetData) {
                     if (addSheetData == "success") {
                         alert('提交成功！');
                        
                     }
                     else
                     {
                         alert('提交失败！');
                     }
                     location.href = "AseetInfo.aspx?cid=" + $("#hf_cid").val() + "&mid=" + $("#hf_mid").val();
                 });
                 // return false;



             })
         });

     </script>

</head>
<body>
     <div class="header">
        <div class="title">工单操作</div>
     </div>
    <form id="form1" runat="server">
   <%-- <div>
       <div class="operate">
            <div class="eweic-title">
                <p>工单操作</p>
            </div>
        </div>

        <div class="eweic-chunk-white">
            <div class="eweic-chunk-cont">
                <p class="eweic-left">
                    工单类型：
                </p>
                <span class="eweic-p-right">
                    <asp:Literal runat="server" ID="ltSheetType"></asp:Literal></span>
            </div>
        </div>
        <div class="eweic-chunk-white">
            <div class="eweic-chunk-cont">
                <p class="eweic-left">
                    工单描述： 
                </p>
                <span class="eweic-p-right">
                    <asp:Literal runat="server" ID="ltSheetDetail"></asp:Literal></span>
            </div>
        </div>
     
        <div class="eweic-chunk-white">
            <div class="eweic-chunk-cont">
                <p class="eweic-left">
                    受理服务商：
                </p>
                <span class="eweic-p-right">
                    <asp:Literal runat="server" ID="ltMain"></asp:Literal></span>
            </div>
        </div>


        <div class="eweic-bottom eweic-top">
            <ul>
                <li class="operate-list-gray eweic-bottom-repairs">
                    <p>返 回</p>
                    <a href="AseetInfo.aspx?cid=<%= GetCid() %>&mid=<%= GetMid() %>"></a>
                </li>
                <li class="operate-list-blue eweic-bottom-repairs">
                    <p>完成工单</p>
                    <asp:LinkButton ID="btnSubmit"  runat="server" OnClick="btnSubmit_Click"></asp:LinkButton>
                </li>
            </ul>
        </div>
    </div>--%>

        
           <div class="weui-cells">
            <div class="weui-cell">
                <div class="weui-cell__bd">
                    <p>工单类型：</p>
                </div>
                <div class="weui-cell__ft" ><label id="ltSheetType" runat="server"></label></div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__bd">
                    <p>所属客户</p>
                </div>
                <div class="weui-cell__ft"><label id="ltClient" runat="server"></label></div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__bd">
                    <p>工单描述</p>
                </div>
                <div class="weui-cell__ft"><label id="ltSheetDetail" runat="server"></label></div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__bd">
                    <p>受理服务商</p>
                </div>
                <div class="weui-cell__ft"><label id="ltMain" runat="server"></label></div>
            </div>
        </div>
            
        <div class="option">
            <a class="weui-btn weui-btn_primary" id="btnSubmit">完成工单</a>
            <a  href="AseetInfo.aspx?cid=<%= GetCid() %>&mid=<%= GetMid() %>" class="weui-btn weui-btn_primary" style="background-color: #E0E1E2;color:black;">返 回</a>
        </div>
        <asp:HiddenField ID="hf_cid" runat="server" />
         <asp:HiddenField ID="hf_mid" runat="server" />
         <asp:HiddenField ID="hf_sheetid" runat="server" />


    </form>
</body>
</html>
