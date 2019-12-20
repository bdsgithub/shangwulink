//获取焦点
function onFocus()//获得焦点，模仿select，显示模拟下拉框
{
    var id = arguments[0];
    var url = arguments[1];
    $("#div" + id).remove();//删除已有的元素

    //加载数据
    $.post(url, { keyword: $("#" + id).val().trim() }, function (data) {
        if ($('#div' + id).length > 0)//判断是否已存在该元素
            $("#div" + id).remove();//删除已有的元素
        data = "<div id='div" + id + "' class='pre'>" + data + "</div>";
        $('#' + id).parent().append(data);
        $('#div' + id).css("width", ($("#" + id).width() + 10) + "px").offset({ top: ($("#" + id).offset().top + $("#" + id).height() + 5), left: $("#" + id).offset().left });
        //
    });
    $('#div' + id).slideDown(250);//列表显示方式
    var txtValue = "";//文本框值


    //为输入框绑定keyup事件
    $(document).on("keyup", "#" + id, function () {
        var currentValue = $("#" + id).val().trim();//得到当前控件值
        if (currentValue != txtValue) {//判断如果当前值不等于原来的值则执行keyup事件
            onKeyUp(id, url);
            txtValue = currentValue;//赋值到文本框值
        }
    });
    var argLen = arguments.length;//得到参数个数
    $(document).on('mousedown', '#div' + id + ' .soption', function () {//选项点击获取值事件
        var html = $(this).html();
        var val = $(this).data('val');
        if (argLen > 2) {
            //如果传入参数大于2个，则表示为下拉多选
            var currVal = $("#" + id + "_hidden").val();
            var currHtml = $("#" + id + "_select").val();
            var Strspilt = currVal == "" ? "" : ",";//分隔符（如果原有无内容则为空，有值则添加逗号）
            $("#" + id + "_hidden").val(currVal + Strspilt + val);
            $("#" + id + "_select").val(currHtml + Strspilt + html);
            $("#" + id).val("");//清空当前输入框内容
        }
        else {
            $("#" + id + "_hidden").val(val);
            $("#" + id).val(html);
            //赋值后隐藏下拉框
            $('#div' + id).remove();//隐藏选择区域
        }
    });

    $(document).on("blur", "#" + id, function () {
        onBlur(id);
    });
}
function onBlur(id)//失去焦点判断光标是否在选择区域，否则隐藏模拟下拉框
{
    //if ($('#div' + id + " .soption").length < 1)
    //{
    //    alert("没有匹配的数据！");
    //    $("#" + id).val("");
    //}

    $('#div' + id).remove();//隐藏选择区域
    $('#div' + id).unbind("keyup");//取消keyup事件的绑定
    $('#div' + id).unbind("blur");//取消blur事件的绑定
}

//文本框keyup事件
function onKeyUp(id, url) {
    $.post(url, { keyword: $("#" + id).val().trim() }, function (data) {
        if ($('#div' + id).length > 0)
            $("#div" + id).remove();//删除已有的元素
        data = "<div id='div" + id + "' class='pre'>" + data + "</div>";
        $('#' + id).parent().append(data);
        $('#div' + id).css("width", ($("#" + id).width() + 10) + "px").offset({ top: ($("#" + id).offset().top + $("#" + id).height() + 5), left: $("#" + id).offset().left });
    });
    $('#div' + id).slideDown(250);//列表显示方式
}
