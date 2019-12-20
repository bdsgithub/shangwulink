<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="purchase.aspx.cs" Inherits="HF.Cloud.CustomerWeb.LabelWap.purchase" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0;" />
    <link href="../css/font-awesome.min.css" rel="stylesheet" />
    <title>购买标签</title>
    <style>
        * {
            margin: 0;
            padding: 0;
            font-size: 12px;
            font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;
        }

        .gbk {
            background-color: rgb(248,248,248);
        }

        .ccm {
            background-color: white;
            margin-top: 10px;
            margin-bottom: 15px;
            overflow: hidden;
        }

            .ccm .m-ccm {
                margin: 8px 0 8px 8px;
                overflow: hidden;
            }

                .ccm .m-ccm .lftCont {
                    float: left;
                    width: 120px;
                    height: 120px;
                }

                    .ccm .m-ccm .lftCont img {
                        width: 120px;
                        height: 120px;
                    }

                .ccm .m-ccm .rhtCont {
                    display: inline;
                }

                    .ccm .m-ccm .rhtCont p {
                        margin-bottom: 5px;
                        margin-left: 130px;
                    }

                    .ccm .m-ccm .rhtCont .pp {
                        overflow: hidden;
                    }

                        .ccm .m-ccm .rhtCont .pp .alft {
                            float: left;
                            margin-left: 10px;
                            width: 30%;
                            line-height: 40px;
                        }

                        .ccm .m-ccm .rhtCont .pp .argt {
                            float: right;
                            height: 69%;
                        }

        .suba {
            display: inline-block;
            border: 1px solid gray;
            border-radius: 4px;
            text-align: center;
            vertical-align: middle;
            color: black;
        }

        .wh {
            width: 60px;
            line-height: 40px;
            height: 40px;
            margin-right: 20px;
            cursor: pointer;
        }
    </style>
</head>
<body class="gbk">
    <form id="form1" runat="server">
        <i class="fa fa-user-circle-o"></i>
        <div>
            <%= listHtml.ToString() %>
        </div>
    </form>
</body>
</html>
