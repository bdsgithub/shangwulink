//linkbutton回发操作
function confirmLbtn(msg, eventTarget) {
    $(eventTarget).attr("href", "javascript:void(0)");//阻断linkbutton的提交
    top.layer.confirm(msg, function (index) {
        top.layer.closeAll();
        __doPostBack(eventTarget.id, '');//提交
    });
}
//列表中删除使用
function confirm(id) {
    top.layer.confirm("确认删除？", function (index) {
        $.post(document.location.href, { op: "delete", id: id }, function (data) {
            top.layer.closeAll();
            document.location = document.location.href;
        });
    });
}
//预览图片
function PreviewImg(imgFile, imgCol) {
    //预览代码，支持 IE6、IE7。
    var newPreview = document.getElementById(imgCol);
    var t;
    if (document.all) //IE
        t = imgFile.value;
    else
        t = window.URL.createObjectURL(imgFile.files[0]); //FF
    newPreview.src = t;
}
function OpenPage(pageurl) {
    if (pageurl.indexOf("?") > 0) {
        pageurl += + "&powerid=" + getQueryString("powerid");
    }
    else {
        pageurl = pageurl + "?" + "powerid=" + getQueryString("powerid");
    }
    $("#mainFrame").attr("src", pageurl);
}
//根据权限获取按钮，显示有权限的按钮（默认所有的字典中的按钮不显示）
function GetButton(PMID, RoleID, mainID) {
    $.post("/ashx/ExecutePowerMenu.ashx", { mainID: mainID, PMID: PMID, roleID: RoleID, op: "GetRoleMenuButton" }, function (data) {
        var datas = data.split('&')[0].split('|');//获取所有权限按钮，循环显示
        $.each(datas, function (key, val) {
            if (val != "") {
                $("#" + val).parent().css("display", "none");//隐藏按钮
                if (data.split('&')[1].indexOf(val) > -1)//如果拥有此按钮权限
                {
                    $("#" + val).parent().css("display", "");//显示按钮
                }
            }
        })
    })
}

