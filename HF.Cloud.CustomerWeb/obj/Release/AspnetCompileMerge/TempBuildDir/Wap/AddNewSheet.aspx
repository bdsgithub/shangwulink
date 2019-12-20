<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddNewSheet.aspx.cs" Inherits="HF.Cloud.CustomerWeb.Wap.AddNewSheet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>报修</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent">
    <meta name="format-detection" content="telephone=no">
    <meta name="full-screen" content="yes">
    <meta name="x5-page-mode" content="app">
    <%--<link rel="stylesheet" href="/Wechats/css/style.css">--%>
    <script src="/Wechats/js/zepto.min.js"></script>
   <%-- <script type="text/javascript" src="/js/jquery-1.8.3.min.js"></script>--%>
    <%--<script type="text/javascript" src="/js/layer/layer.js"></script>--%>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/weui/1.1.2/style/weui.min.css" rel="stylesheet"/>
    <script type="text/javascript">
        //$(document).ready(function () {
        //    $('#btnSubmit').click(function () {
        //        if ($('#txtLinkMan').val().trim() == '') {
        //            alert('请输入联系人！');
        //            return false;
        //        }
        //        if ($('#txtLinkTel').val().trim() == '') {
        //            alert('请输入联系电话！');
        //            return false;
        //        }
        //        if ($('#txtDetails').val().trim() == '') {
        //            alert('请输入描述！');
        //            return false;
        //        }
        //    });
        //});


        $(function () {
            $("#btnSubmit").bind("click", function () {
                var dl_title = $("#dl_title");
                var dl_content = $("#dl_content");
                var myreg = /^(((13[0-9]{1})|(14[0-9]{1})|(17[0]{1})|(15[0-3]{1})|(15[5-9]{1})|(18[0-9]{1}))+\d{8})$/;
                var current_tel = $('#txtLinkTel').val().trim();
                if (current_tel === '') {
                    dl_title.text("手机号不能为空");
                    dl_content.text("手机号不能为空");
                    $("#dl").css("display", "block");
                    return false;
                }
                if (current_tel.length !== 11) {
                    dl_title.text("手机号格式错误");
                    dl_content.text("请输入正确的手机号");
                    $("#dl").css("display", "block");
                    return false;
                }
                if (!myreg.test(current_tel)) {
                    dl_title.text("手机号格式错误");
                    dl_content.text("请输入正确的手机号");
                    $("#dl").css("display", "block");
                    return false;
                }

                
                //提交数据库
                var info = {};
                info["assetId"] = $("#hf_assetId").val();//设备ID
                info["openid"] = $("#hf_openid").val();//微信openid

                info["mid"] = $("#hf_mid").val();//服务商ID

                info["LinkMan"] = $("#txtLinkMan").val()  //联系人
                info["LinkTel"] = $("#txtLinkTel").val();//联系人电话
                info["Details"] = $("#txtDetails").val();//描述

                $.post("ashx/AddSheet.ashx", info, function (addSheetData) {
                    if (addSheetData == "success") {
                        alert('已经成功报修，等待工程师和您联系！');
                        location.href = "AseetInfo.aspx?cid="+$("#hf_cid").val()+"&mid="+$("#hf_mid").val();
                    }
                });
                // return false;

            })
        });
    </script>
    <style type="text/css">
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <%--<div class="eweic-chunk-white">
            <div class="eweic-chunk-cont">
                <p class="eweic-left">联系人 </p>
                <asp:TextBox ID="txtLinkMan" runat="server" placeholder="请输入联系人" CssClass="eweic-right"></asp:TextBox>
            </div>
        </div>
        <div class="eweic-chunk-white">
            <div class="eweic-chunk-cont">
                <p class="eweic-left">电话 </p>
                <asp:TextBox ID="txtLinkTel" runat="server" placeholder="请输入电话" CssClass="eweic-right"></asp:TextBox>
            </div>
        </div>
        <div class="eweic-chunk-white">
            <div class="eweic-chunk-cont">
                <p class="eweic-left">工单描述 </p>
                <asp:TextBox ID="txtDetails" Rows="5" Style="border: 0px; margin-top: 19px;" placeholder="请输入工单描述" TextMode="MultiLine" runat="server" CssClass="eweic-right"></asp:TextBox>
            </div>
        </div>

        <div class="eweic-bottom eweic-top">
            <ul>
                <li class="operate-list-gray eweic-bottom-repairs">
                    <p>取消</p>
                    <a href="AseetInfo.aspx?cid=<%= GetCid() %>&mid=<%= GetMid() %>"></a>
                </li>
                <li class="operate-list-blue eweic-bottom-repairs">
                    <p>发布</p>
                    <asp:LinkButton ID="btnSubmit" OnClick="btnSubmit_Click" runat="server"></asp:LinkButton>
                </li>
            </ul>
        </div>--%>


           <div class="weui-cells__title">提交工单</div>
        <div class="weui-cells weui-cells_form">
           
            <div class="weui-cell">
                <div class="weui-cell__hd"><label class="weui-label">手机号</label></div>
                <div class="weui-cell__bd">
                    <input id="txtLinkTel" class="weui-input" type="tel" placeholder="请输入手机号(必填)" />
                </div>
            </div>
             <div class="weui-cell">
                <div class="weui-cell__hd"><label class="weui-label">联系人</label></div>
                <div class="weui-cell__bd">
                    <input id="txtLinkMan" class="weui-input" type="text" placeholder="请输入联系人"/>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd" style="margin-top: -50px"><label class="weui-label">工单描述</label></div>
                <div class="weui-cell__bd">
                    <textarea id="txtDetails" class="weui-textarea" placeholder="请输入工单描述" rows="3" style="text-align: right;overflow:auto"></textarea>
                </div>
            </div>

        </div>
        <div class="option">
            <a  class="weui-btn weui-btn_primary" id="btnSubmit">提交工单</a>
            <a href="AseetInfo.aspx?cid=<%= GetCid() %>&mid=<%= GetMid() %>" class="weui-btn weui-btn_primary" style="background-color: #E0E1E2;color: black">取消</a>
     
        </div>





        <asp:HiddenField ID="hf_cid" runat="server" />
        <asp:HiddenField ID="hf_mid" runat="server" />

        <asp:HiddenField ID="hf_assetId" runat="server" />
        <asp:HiddenField ID="hf_openid" runat="server" />

        <asp:HiddenField ID="hf_" runat="server" />
        <asp:HiddenField ID="HiddenField4" runat="server" />
        <asp:HiddenField ID="HiddenField5" runat="server" />


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
