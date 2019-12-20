<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowCustForMap.aspx.cs" Inherits="HF.Cloud.CustomerWeb.map.ShowCustForMap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../js/jquery-1.8.3.min.js"></script>
    <script src="../js/layer/layer.js"></script>
    <title>地图</title>
    <link type="text/css" href="/css/home.css" rel="stylesheet" />
    <script src="http://api.map.baidu.com/api?v=2.0&ak=eDcoUom0ElOGDhyXCYpszq0U6WdTW3QQ" type="text/javascript"></script>
    <style>
        html, body{
            height: 100%;
            width: 100%;
        }

         #dmap {
             height:100%;
             width:70%;
             float:left;
         }
    </style>
    <script>
        var map;
        window.onload = function () {
            map = new BMap.Map('dmap');
            var point = new BMap.Point(113.666167, 34.75245);
            map.centerAndZoom(point, 12);

            map.setCurrentCity('郑州');
            map.addControl(new BMap.NavigationControl());
            map.addControl(new BMap.ScaleControl());
            map.addControl(new BMap.MapTypeControl());
            map.enableScrollWheelZoom();

            //地图上显示图标
            var mapClients = document.getElementById('mapClients').value;
            var mapClientsJson = eval('(' + mapClients + ')');

            for (var mapClient in mapClientsJson)
            {
                var name = mapClientsJson[mapClient].Name;
                var clientID = mapClientsJson[mapClient].ClientID;
                var lng = mapClientsJson[mapClient].lng;
                var lat = mapClientsJson[mapClient].lat;
                ShowMarke(name,clientID, lng, lat);//地图上添加一个标注
            }

        }
        //添加标注
        function ShowMarke(name,clientID,lng,lat)
        {
            if (lng != '' && lat != '') {
                var iponit = new BMap.Point(lng, lat);
                var imarker = new BMap.Marker(iponit);
                var ilable = new BMap.Label(name);
                
                ilable.setStyles({
                    backgroundColor: 'Red', color: '#FFFFFF', fontSize: '14px', fontFamily: '微软雅黑',
                    cursor: 'pointer',
                    title:'点击可查看详情---！'
                    });
                imarker.setLabel(ilable);
                //给标注添加点击事件
                imarker.getLabel().addEventListener('click', function (e) {
                    window.location.href = "HallDistribute.aspx?name="+name+"&clientID="+clientID;
                });
                map.addOverlay(imarker);
               
            }
        }
    </script>
</head>
<body>
    <div id="dmap" >
    </div>
        <div class="main-l border-c float-right float-r-s" style="margin-top:20px;width:27%;margin-right:20px;float:right;overflow:scroll;">
            <ul>
                <li class="background-c font-color-r" style="text-align: left; font-size: 20px;"><span> &nbsp; 客户列表</span></li>
                <li>
                   <%=clientList %>
                    
                 </li>
            </ul>
        </div>



     <form id="form1" runat="server">
        <asp:HiddenField ID="mapClients" runat="server" />
    </form>
</body>
</html>
