<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BinDing.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Wap.BinDing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>绑定标签</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent">
    <meta name="format-detection" content="telephone=no">
    <meta name="full-screen" content="yes">
    <meta name="x5-page-mode" content="app">
   <%-- <link rel="stylesheet" href="../css/style.css" />
    <link href="../Wechats/css/style.css" rel="stylesheet" />--%>
   <%-- <script type="text/javascript" src="../js/jquery-1.8.3.min.js"></script>--%>
     <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/weui/1.1.2/style/weui.min.css" rel="stylesheet"/>

      <style type="text/css">
        /*.b-title {
            text-align: center;
            font-size: 18px;
        }
        .de_m {
            margin-top: 12%;
            margin-bottom: 12%;
            color: #aed9f5;
            font-size: 24px;
        }*/
        .weui-input{
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
      </style>
     <script type="text/javascript">

         //$(document).ready(function () {

         //    $('#LoginBtn').click(function () {
         //        if ($("#ddlCustomer").val() == '-1') {
         //            alert('请先选择客户！');
         //            return false;
         //        }
         //        if ($("#ddlAssetType").val() == '-1') {
         //            alert('请先选择设备类型！');
         //            return false;
         //        }
         //        if ($("#ddlAssetBrand").val() == '-1') {
         //            alert('请先选择设备品牌！');
         //            return false;
         //        }
         //        if ($("#ddlAssetModel").val() == '-1') {
         //            alert('请先选择设备型号！');
         //            return false;
         //        }


         //    });


         //});

         $(function () {
             $("#ltBut").bind("click", function () {
                 var dl_title = $("#dl_title");
                 var dl_content = $("#dl_content");
                 if ($("#ddlCustomer").val().trim() === "") {
                     dl_title.text("错误！");
                     dl_content.text("请输入客户信息");
                     $("#dl").css("display", "block");
                     return false;
                 } else if ($("#ddlAssetType").val().trim() === "") {
                     dl_title.text("错误！");
                     dl_content.text("请输入设备类型");
                     $("#dl").css("display", "block");
                     return false;
                 } else if ($("#ddlAssetType").val().trim() === "") {
                     dl_title.text("错误！");
                     dl_content.text("请输入设备品牌");
                     $("#dl").css("display", "block");
                     return false;
                 } else if ($("#ddlAssetModel").val().trim() === "") {
                     dl_title.text("错误！");
                     dl_content.text("请输入设备类型");
                     $("#dl").css("display", "block");
                     return false;
                 }


                 //提交数据库
                 var info = {};
                 info["mid"] = $("#hf_mid").val();//服务商id
                 info["cid"] = $("#hf_cid").val();//标签id

                 info["Customer"] = $("#ddlCustomer").val();//客户
                 info["AssetType"] = $("#ddlAssetType").val();//设备类型
                 info["AssetBrand"] = $("#ddlAssetBrand").val();//设备品牌
                 info["AssetModel"] = $("#ddlAssetModel").val();//设备型号

                 $.post("ashx/BinDing.ashx", info, function (addSheetData) {
                     if (addSheetData == "success") {
                         alert('绑定成功！');
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
        <i class="fa fa-sign-in"></i>
        <div class="title">绑定标签</div>

    </div>
    <form id="form1" runat="server">
    <%--<div>
      <div class="b-title de_m">标签绑定</div>
         <div class="eweic-chunk-white">
                <div class="eweic-chunk-cont">
                    <p class="eweic-left">客户名称</p>
                    <asp:DropDownList Style="border: 0px; margin-top: 18px;" runat="server" ID="ddlCustomer"></asp:DropDownList>
                </div>
         </div>
         <div class="eweic-chunk-white">
                <div class="eweic-chunk-cont">
                    <p class="eweic-left">设备类型</p>
                    <asp:DropDownList Style="border: 0px; margin-top: 18px;" runat="server" ID="ddlAssetType"></asp:DropDownList>
                </div>
         </div>
         <div class="eweic-chunk-white">
                <div class="eweic-chunk-cont">
                    <p class="eweic-left">设备品牌</p>
                    <asp:DropDownList Style="border: 0px; margin-top: 18px;" runat="server" ID="ddlAssetBrand"   AutoPostBack="true" OnSelectedIndexChanged="ddlAssetBrand_SelectedIndexChanged"></asp:DropDownList>
                </div>
         </div>
         <div class="eweic-chunk-white">
                <div class="eweic-chunk-cont">
                    <p class="eweic-left">设备型号</p>
                    <asp:DropDownList Style="border: 0px; margin-top: 18px;" runat="server" ID="ddlAssetModel"></asp:DropDownList>
                </div>
         </div>


         <div class="eweic-bottom">
            <ul>
                <li class="operate-list-blue eweic-bottom-back">
                    <p> 
                        <asp:Literal runat="server" ID="ltBut">绑 定</asp:Literal></p>
                    <asp:LinkButton Width="100%" Style="display: block; color: white;" ID="LoginBtn" runat="server" OnClick="LoginBtn_Click" ></asp:LinkButton>
                </li>
            </ul>
        </div>
    </div>--%>

        <div class="content">
                <div class="weui-cells">
                    <div class="weui-cell">
                        <div class="weui-cell__hd"><label class="weui-label">客户名称</label></div>
                        <div class="weui-cell__bd">
                            <input id="ddlCustomer" class="weui-input" type="text" />
                        </div>
                    </div>
                    <div class="weui-cell">
                        <div class="weui-cell__hd"><label class="weui-label">设备类型</label></div>
                        <div class="weui-cell__bd">
                            <input id="ddlAssetType" class="weui-input" type="text" />
                        </div>
                    </div>
                    <div class="weui-cell">
                        <div class="weui-cell__hd"><label class="weui-label">品牌</label></div>
                        <div class="weui-cell__bd">
                            <input id="ddlAssetBrand" class="weui-input" type="text" />
                        </div>
                    </div>
                    <div class="weui-cell">
                        <div class="weui-cell__hd"><label class="weui-label">型号</label></div>
                        <div class="weui-cell__bd">
                            <input id="ddlAssetModel" class="weui-input" type="text"/>
                        </div>
                    </div>
                </div>
        </div>
        <div class="option">
            <a id="ltBut" class="weui-btn weui-btn_primary">绑 定</a>
        </div>
        <asp:HiddenField ID="hf_mid" runat="server" />
        <asp:HiddenField ID="hf_cid" runat="server" />

    </form>
     <div id="dl" style="display: none;">
        <div class="weui-mask"></div>
        <div class="weui-dialog">
            <div id="dl_title" class="weui-dialog__hd"><strong class="weui-dialog__title">弹窗标题</strong></div>
            <div id="dl_content" class="weui-dialog__bd">弹窗内容，告知当前页面信息等</div>
            <div class="weui-dialog__ft">
                <a href="javascript:" class="weui-dialog__btn weui-dialog__btn_primary" onclick="(function() {
    $('#dl').css('display','none');
                })();">确定</a>
            </div>
        </div>
    </div>


</body>
</html>
