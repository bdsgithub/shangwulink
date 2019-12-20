
$(document).ready(function () {
    //导航缩放
    $('.scale').on('click', function () {
        if (!($('.logo').is('.cient-left-max'))) {
            $('.logo').addClass('cient-left-max').removeClass('cient-left-min')
            $('.cient-left').addClass('cient-left-max').removeClass('cient-left-min')
            $('.cient-main').css('left', '195px')
            $('.scale-img').addClass('scale-im')
        } else {
            $('.logo').addClass('cient-left-min').removeClass('cient-left-max')
            $('.cient-left').addClass('cient-left-min').removeClass('cient-left-max')
            $('.cient-main').css('left', '85px')
            $('.scale-img').removeClass('scale-im')
        }
    })
    //导航选中状态
    $('.cient-left li').on('click', function () {
        var $classN = $(this)[0].className
        $(this).parent().parent().find('li').removeAttr('id')
        $(this).parent().parent().find('span').removeAttr('id')
        $(this).attr('id', 'nav-font-color')
        $(this).find('span').attr("id", $classN);
    })
    //导航鼠标移入效果
    $('.cient-left li').on('mouseover mouseout', function (event) {
        var $toph = $(this).offset().top + 16
        var $pText = $(this).find('p').text()
        if (event.type == 'mouseover') {
            $('.nav-title').css({ 'top': $toph, 'display': 'block' })
            $('.nav-title p').text($pText)
        } else if (event.type == 'mouseout') {
            $('.nav-title').css({ 'top': $toph, 'display': 'none' })
        }
    })

    //用户信息鼠标移入效果
    $('.cient-user,.user-message').on('mouseover mouseout', function (event) {
        if (event.type == 'mouseover') {
            $('.user-message').show()
        } else if (event.type == 'mouseout') {
            $('.user-message').hide()
        }
    })
    //用户信息鼠标移入效果
    $('#ops,.ops-message').on('mouseover mouseout', function (event) {
        if (event.type == 'mouseover') {
            $('.ops-message').show()
        } else if (event.type == 'mouseout') {
            $('.ops-message').hide()
        }
    })

    //homePage 左侧工单动态高度和右侧宽度百分比
    $(window).resize(function () {
        var $w = $(document).width()
        var $width = ($w - 365) / $w * 100;
        var $heigh = $(window).height() - 100
        $('.main-left').css('height', $heigh)
        $('.main-right').css('width', $width + '%')
    });
    var $heigh = $(window).height() - 100
    var $w = $(document).width()
    var $width = ($w - 365) / $w;

    $('.main-left').css('height', $heigh)
    $('.main-right').css('width', ($width * 100) + '%')

    //搜索工单页面重置
    $('#reset').on('click', function () {
        $('.select').find('input').val('')
    })

    //导航切换iframe
    $('.cient-left li').on('click', function () {

        var $nu = $(this).parent().index()
        if ($nu == 0) {
            //$("iframe").attr('src', '/HomePage.aspx')
        } else if ($nu == 1) {
            $("iframe").attr('src', '/Pages/ComPanyList.aspx')
        } else if ($nu == 2) {
            $("iframe").attr('src', '/Pages/Notice_System.aspx')
        } else if ($nu == 3) {
            $("iframe").attr('src', '/Pages/EditPicture.aspx')
        } else if ($nu == 4) {
            //$("iframe").attr('src', '/SaleLabels/QBCodeRecord.aspx')
        }
        else if ($nu == 5) {
            //$("iframe").attr('src', '/Common/TicketList.aspx')
        }
        else if ($nu == 6) {
            //$("iframe").attr('src', '/map/ShowCustForMap.aspx')
        }
    })
    $('#work-select').on('click', function () {
        //location.href = 'workSelect.html'
    })

})



