<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TicketAdd.aspx.cs" Inherits="HF.Cloud.BM.Common.TicketAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>新增兑换券</title>
    <link type="text/css" href="/css/home.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="/js/cient.js"></script>
    <script src="../js/My97DatePicker/WdatePicker.js"></script>
    <style type="text/css">
         .select {
            width: 440px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            var m = document.getElementById("txtMoney");
            m.onkeyup = function () {
                var demovalue = this.value;
                this.value = demovalue.replace(/\D/g, "");
            };
            //确认
            $("#lnkBtnSearch").click(function () {
                if ($("#txtMoney").val() == 0 || $("#txtMoney").val() == null) {
                    alert("金额输入有误！")
                    return false;
                }
                if ($("#txtEndTime").val() == null || $("#txtEndTime").val() == "")
                {
                    alert("时间输入有误！")
                    return false;
                }




            });


        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <div class=" work-center float-r-s margin-top-s padding-s">
                <div class="main-title"><span style="color: #1fb5ac">服务商></span>新增</div>

                <div class="border-c padding-top-s margin-top-s overflow-h">
                   <div class="select  float-l-s float-left margin-top-s" style="width:100%">
                        激活码：&nbsp;
                       <asp:Label ID="lbActiveCode" runat="server" ></asp:Label>
                    </div>
                    <br />
                    <div class="select float-l-s float-left margin-top-s">
                        金额：&nbsp;
                        <asp:TextBox ID="txtMoney" runat="server"></asp:TextBox>
                    </div>
                    <div class="select float-l-s float-left margin-top-s">
                        过期时间：&nbsp;
                         <asp:TextBox ID="txtEndTime"  onfocus="WdatePicker({minDate: '%y-%M-#{%d+1}'})" runat="server"></asp:TextBox>
                    </div>
                  
                    <br />
                    <div class="button margin-top-s float-left" style="margin-left: 20px">
                        <asp:LinkButton ID="lnkBtnSearch" runat="server" OnClick="lnkBtnSearch_Click"><div class="button-m background-r float-left ">保存</div></asp:LinkButton>
                        <asp:LinkButton ID="lnkBtnReset" runat="server" OnClick="lnkBtnReset_Click" > <div class="button-m background-cc float-left" id="reset" style="margin-left: 20px">取消</div></asp:LinkButton>
                    </div>
                </div>
            </div>
    </div>

        <asp:HiddenField ID="hf_ActiveCode" runat="server" />


    </form>
</body>
</html>
