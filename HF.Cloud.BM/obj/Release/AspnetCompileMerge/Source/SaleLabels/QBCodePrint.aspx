<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QBCodePrint.aspx.cs" Inherits="HF.Cloud.BM.SaleLabels.QBCodePrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>打印</title>
    <script type="text/javascript" src="/js/jquery-1.8.3.min.js"></script>
    <script src="/js/layer/layer.js"></script>
    <style>
        /*2017-8-30*/
        /*.p1 {
            page-break-after: always;
        }

        * {
            margin: 0;
            position: 0;
        }

        .hr {
            width: 1002px;
            height: 20px;
            float: left;
        }*/

        /*.bar-code {
            overflow: hidden;
            font-size: 14px;
            line-height: 24px;
            width:332px;*/
            /*=======================height:216px;*/
            /*=======================padding: 38px 25px 0px 16px;*/ /*3*7   20160829调试打印*/
            /*=======================padding: 40px 25px 0px 20px;*/ /*3*7打印*/
            /*=======================padding: 40px 5px 40px 35px;  3*6打印*/
        /*}*/

        /*========================.barc-left img {
            width: 149px;
            height: 149px;
        }*/
        /*2017-8-30*/
        /*.barc-left {
            text-align: center;
            height:180px;
        }

        .barc-right {
            padding: 0 20px 0 20px;
            height:180px;
        }

        .bar-code, .barc-left, .barc-right {
            float: left;
        }

        .text-right {
            margin-left: 48px;
        }

        .barc-right p {
            width: 143px;
        }

        .bar {
            width: 1300px;
            page-break-after: always;
        }*/

        /*版本二2017-9-5*/
         /**{
            margin: 0;
            padding: 0;
        }
        body{
            font-size: 62.5%;
        }

        ul li{
            list-style: none;

        }
        ul{
            text-align: center;
        }
        .outer{

            position: relative;
            margin: 0 auto;
            margin-top: 22%;
            width: 20em;
            height: 25em;
            border: 1px none #000000;

        }
        .QR{
                width: 12.5em;
                height: 12.5em;
                position: absolute;
                left: 50%;
                top: 50%;
                margin-left: -6em;
                margin-top: -6em;
        }
        .num{
            display: inline-block;
            font-size: 0.1em;
            width: 15em;
            position: absolute;
            top: 50%;
            left: 50%;
            margin-left: -7.5em;
            margin-top: 7em;
        }
        .tel{
            display: inline-block;
            font-size: 1.3em;
            width: 15em;
            position: absolute;
            top: 7%;
            left: 50%;
            margin-left: -7.5em;
        }*/

         *{
            margin: 0;
            padding: 0;
        }
        body{
            font-size: 62.5%;
        }

        ul li{
            list-style: none;

        }
        ul{
            text-align: center;
        }
        .outer{

            position: relative;
            margin: 0 auto;
            margin-top: 11%;
            width: 20em;
            height: 25em;
            border: 1px none #000000;

        }
        .QR{
                width: 11em;
                height: 11em;
                position: absolute;
                left: 50%;
                top: 60%;
                margin-left: -5.5em;
                margin-top: -5.5em;
        }
        .num{
            display: inline-block;
            font-size: 1em;
            width: 15em;
            position: absolute;
            top: 83%;
            left: 50%;
            margin-left: -7.5em;
        }
        .tel{
            display: inline-block;
            font-size: 1.5em;
            width: 15em;
            position: absolute;
            top: 15%;
            left: 50%;
            margin-left: -7.5em;
        }
        .title{
            display: inline-block;
            font-size: 1em;
            width: 15em;
            position: absolute;
            top: 5%;
            left: 50%;
            margin-left: -7.5em;
        }


    </style>
     <style media=print>
        .Noprint{display:none;}
        .PageNext{page-break-after: always;}
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnPrint').click(function () {
                layer.confirm("确定打印吗？\n\n确定打印将把本页面的标签标识为已打印！", function (index) {
                    top.layer.close(index);
                    var sltv = '';
                    $('div[pkey=code]').each(function (index) {
                        sltv += $(this).attr('pvalue') + ',';
                    });

                    $.post('/ashx/ExecuteQBCode.ashx', { cl: sltv, mid: $('#hidMainID').val() });

                    btnPrint.style.display = 'none';
                    window.print();
                });
            });
        });
    </script>
</head>
<body>
    <input id="btnPrint" type="button" value="打 印" />
     <ul>
             <%= html %>
    </ul>
    <input id="hidMainID" runat="server" type="hidden" />
</body>
</html>
