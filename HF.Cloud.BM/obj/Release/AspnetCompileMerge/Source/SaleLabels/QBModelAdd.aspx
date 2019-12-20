<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QBModelAdd.aspx.cs" Inherits="HF.Cloud.BM.SaleLabels.QBModelAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>标签管理</title>
    <link href="/css/reset.css" rel="stylesheet" />
    <link href="/css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-1.8.3.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            var m = document.getElementById("tb_entag");
            m.onkeyup = function () {
                var demovalue = this.value;
                this.value = demovalue.replace(/^[^a-zA-Z]*$/g, "");  //前缀只能输入字母
            };

            //预览
            $("#btnPreView").click(function () {
                $("#p1").text($("#othertext1").val());
                $("#p2").text($("#othertext2").val());
                //$("#p3").text($("#othertext3").val());
                //$("#p4").text($("#othertext4").val());
                //$("#p5").text($("#othertext5").val());
                //$("#p6").text($("#othertext6").val());
                // alert("10000000000".substring($("#tb_entag").val().length + 2));
                $("#p7").text("编号：" + $("#tb_entag").val() + "10000000000".substring($("#tb_entag").val().length + 2) + "1");
            });
            //选择模板(更多不能选择)
            $(".label-template-p:lt(2)").click(function () {
                $(".label-template-p").css("border", "1px solid #ccc");
                $(this).css("border", "1px solid #13CDD0");
                $("#hidModel").val($(this).parent().index());
            });
            $("#btnSave").click(function () {
                if ($("#hidModel").val() == "" || $("#hidModel").val() == "2") {
                    alert("请选择打印方式！");
                    return false;
                }
                if ($("#tb_modelName").val() == "") {
                    alert("请输入模板名称！");
                    return false;
                }
            });
            //下一步
            $("#btnNext").click(function () {
                $("#PrintModel").css("display", "none");
                $("#Model").css("display", "block");
            });
            //返回
            $("#btnQuit").click(function () {
                document.location.href = 'QBModelList.aspx';
            });
            //上一步
            $("#btnPre").click(function () {
                $("#PrintModel").css("display", "block");
                $("#Model").css("display", "none");
            });
            //编辑模板时设定模板的打印模式（样式选中）
            var hidModel = $("#hidModel").val();
            $(".label-template:eq(" + hidModel + ") .label-template-p").css("border", "1px solid #13CDD0");
        });

    </script>
</head>
<body class="eweic-box">
    <form id="form1" runat="server">
        <div class="manage-title" style="margin-top: 20px; height: 30px;">
            <p class="m-title" style="color: #1fb5ac; width: 200px; margin-left: 10px; font-family: Microsoft Yahei,Helvetica Neue,Helvetica,Arial,sans-serif">
                编辑模板><span style="color: black;">模板选择</span>
            </p>
            <p class="cut-off"></p>
        </div>
        <div class="tabl border-c margin-top-s">
            <div class="cont-workorder">
                <div class="form bor-radius box-s" style="overflow: hidden;">
                    <div id="PrintModel" style="display: block;">
                        <h2></h2>
                        <div class="label">
                            <div class="label-template">
                                <p class="label-template-p">尺寸：63.5mm*46.6mm</p>
                                <p>打印A4:3*6</p>
                            </div>
                            <div class="label-template">
                                <p class="label-template-p">尺寸：63.5mm*38.1mm</p>
                                <p>打印A4:3*7</p>
                            </div>
                            <div class="label-template">
                                <p class="label-template-p">更多尺寸，请联系客服</p>
                                <p></p>
                            </div>
                        </div>
                        <div style="margin-left: 10px;">
                            <input type="button" id="btnNext" style="border: none;margin-right:10px;" class="button-m background-r float-left" value="下一步" />&nbsp;
                            <input type="button" id="btnQuit" style="border: none;" class="button-m background-cc float-left" value="取消" />
                        </div>
                    </div>
                    <div class="label" id="Model" style="display: none;">
                        <div class="bo label-facade">
                            <div class="label-con-l"  style="margin-left:50px;">
                                  <p id="p1">
                                    <asp:Literal runat="server" ID="lt1">标签内容1</asp:Literal>
                                </p>
                                <p id="p2">
                                    <asp:Literal runat="server" ID="lt2">标签内容2</asp:Literal>
                                </p>
                                <img src="/image/QRCode.png" alt="" class="bo" />
                                <p id="p7">编号：<asp:Literal runat="server" ID="ltCode">EWC000001</asp:Literal></p>
                            </div>
                          <%--  <div class="label-con-r">
                                <p id="p1">
                                    <asp:Literal runat="server" ID="lt1"></asp:Literal>
                                </p>
                                <p id="p2">
                                    <asp:Literal runat="server" ID="lt2"></asp:Literal>
                                </p>
                                <p id="p3">
                                    <asp:Literal runat="server" ID="lt3"></asp:Literal>
                                </p>
                                <p id="p4">
                                    <asp:Literal runat="server" ID="lt4"></asp:Literal>
                                </p>
                                <p id="p5">
                                    <asp:Literal runat="server" ID="lt5"></asp:Literal>
                                </p>
                                <p id="p6">
                                    <asp:Literal runat="server" ID="lt6"></asp:Literal>
                                </p>
                            </div>--%>
                        </div>
                        <div class="label-compile">
                            <p><span>模板名称：&nbsp;&nbsp;</span><asp:TextBox runat="server" ID="tb_modelName" MaxLength="10"></asp:TextBox></p>
                            <p><span>编号前缀：&nbsp;&nbsp;</span><asp:TextBox runat="server" ID="tb_entag" MaxLength="5"></asp:TextBox><label style="font-size:x-small"> (前缀只可输入字母)</label></p>
                           
                            <p><span>标签内容1：</span><asp:TextBox runat="server" ID="othertext1" MaxLength="15"></asp:TextBox></p>
                            <p><span>标签内容2：</span><asp:TextBox runat="server" ID="othertext2" MaxLength="15"></asp:TextBox></p>
                           <%-- <p><span>标签内容3：</span><asp:TextBox runat="server" ID="othertext3" MaxLength="15"></asp:TextBox><span>(15字以内)</span></p>
                            <p><span>标签内容4：</span><asp:TextBox runat="server" ID="othertext4" MaxLength="15"></asp:TextBox><span>(15字以内)</span></p>
                            <p><span>标签内容5：</span><asp:TextBox runat="server" ID="othertext5" MaxLength="15"></asp:TextBox><span>(15字以内)</span></p>
                            <p><span style="display: block; width: 78px; float: left;">备注：</span><asp:TextBox runat="server" ID="othertext6" MaxLength="15"></asp:TextBox><span>(15字以内)</span></p>--%>
                            <div style="margin-top: 8px">
                                <input type="button" id="btnPre" style="border: none; height: 40px;margin-right:10px;" class="button-m background-r float-left" value="上一步" />
                                <input type="button" id="btnPreView" style="border: none; height: 40px;margin-right:10px;" class="button-m background-r float-left" value="预览" />
                                <asp:Button runat="server" ID="btnSave" BorderWidth="0" Height="40px" CssClass="button-m background-r float-left" Text="保存" OnClick="btnSave_Click" />
                            </div>
                        </div>
                        <div class="blank"></div>
                    </div>
                    <div class="blank"></div>
                </div>
            </div>
        </div>
        <asp:HiddenField runat="server" ID="hidModel" />
    </form>
</body>
</html>
