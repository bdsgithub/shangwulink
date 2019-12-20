$(document).ready(function () {
    $('.cont-left-set ul').css('display', 'block');

    $('.cont-left-set').on('click', function (event) {
        if ($(this).hasClass("cont-left-sett")) {
            $(this).removeClass('cont-left-sett').addClass('cont-left-set cont-left-opacity')
            event.stopPropagation();
            $(this).next().show()

        } else {
            $(this).removeClass('cont-left-set cont-left-opacity').addClass('cont-left-sett')
            $(this).next().hide()
        }
    })
    //获取客户列表
    $("#txtClientID").focus(function () {
        //获取所属服务商
        var url = "/dropdownfilter/dfClient.aspx";
        var mainID = $("#HidMainId").val();
        url = url + "?mainID=" + mainID;
        onFocus("txtClientID", url);
    });
    //为选择数据内容绑定下拉筛选
    $(".input-item input[type=text]").live("focus", function () {
        var id = $(this).attr("id");
        var url = "/dropdownfilter/dfClient.aspx";
        var mainID = $("#HidMainId").val();
        url = url + "?mainID=" + mainID;
        onFocus(id, url);
    });
    //添加数据范围
    $("#btnAddRange").click(function () {
        var id = $("#hidRangeCount").val() + 1;
        var html = "<div class=\"input-item\">";
        html += "<input type=\"text\"  style='width:350px;' id=\"clientID" + id.toString() + "\" />"
        html += "<input type=\"hidden\" id=\"clientID" + id.toString() + "_hidden\" />"
        html += "<a class=\"removeRange\" >&nbsp;&nbsp;&nbsp;&nbsp;</a>";
        html += "</div>";
        $(".input-con").append(html);//增加数据范围选择
        $("#hidRangeCount").val($("#hidRangeCount").val() + 1);//增加数据范围个数
    });
    //添加数据内容
    $("#btnAddCon").click(function () {
        var id = $("#hidConCount").val() + 1;
        var html = "<div class=\"select-item\">";
        html += "<select class=\"add-inp bor-radius-s\" id=\"select-con" + id + "\" >";
        html += "</select>";
        html += "<input type=\"button\" value=\"删除\" class=\"removeRange\" />";
        html += "<div class=\"select-con-check\">";
        html += "</div>";
        html += "</div>";
        $(".select-con").append(html);//增加数据范围选择
        $(".select-item select:last").load("/ashx/ExecuteDataSet.ashx?op=GetDataDic");
        $("#hidConCount").val($("#hidConCount").val() + 1);//增加数据范围个数
    });
    $(".removeRange").live("click", function () { $.removeRange(this) });//绑定删除数据范围/数据内容事件
    //数据内容下拉
    $(".select-item select").live("change", function () {
        var val = $(this).find("option:selected").val();
        $t = $(this);
        if (val == "001" || val == "002") {
            $.post("/ashx/ExecuteDataSet.ashx?op=GetItem&itemtype=" + val + "&mainID=" + $("#HidMainId").val(), function (data) {
                $t.parent().find('.select-con-check').html(data)
            })
        }
    });
    //获取复选框的选中值
    $.getCheckedValue = function (className) {
        var vals = "";
        $(className).find(".select-con-check input[type=checkbox]").each(function (i) {
            if (this.checked) {
                vals = vals + $(this).val() + "|";
            }
        });
        return vals;
    }

    //保存内容
    $("#lbtnSave").click(function () {
        var info = {};
        info["setID"] = $("#hidID").val();
        info["customerUniqueCode"] = $("#spCustomerUniqueCode").text().toString().trim();
        var dataRange = "";//数据范围
        $(".input-item input[type=hidden]").each(function (i) {
            if ($(this).val() != "") {
                dataRange += $(this).val() + "|";
            }
        })
        info["dataRange"] = dataRange;
        $(".select-item select").find("option:selected").each(function (i) {
            var val = $(this).val();
            if (val == "001") {
                info["sheetType"] = "1";
                info["sheetTypeVal"] = $.getCheckedValue($(this).parent().parent());
            }
            else if (val == "002") {
                info["assetType"] = "1";//设备数据类型
                info["assetTypeVal"] = $.getCheckedValue($(this).parent().parent());//选中的设备分类
            }
            else if (val == "003") {
                info["patrol"] = "1";
            }
        });
        $.post("/ashx/ExecuteDataSet.ashx?op=NewDataSet", info, function (data) {
            if (data == "success") {
                top.layer.confirm("保存成功，是否返回？", function (index) {
                    top.layer.close(index);
                    location.href = "/BasicSet/CustomerDataSet/DataSetList.aspx";
                })
            }
            else if (data == "noCustomer") {
                top.layer.alert("系统无此编号客户！");
            }
        })
    });

    $.getJSON("/ashx/ExecuteDataSet.ashx", { id: $("#hidID").val() }, function () {

    })
    $.removeRange = function (className) {
        $(className).parent().remove();
    }
});


