
function popup(width, height, src) {
    if ($('div').is('.page')) {
        $('.page').show();
        $('.cont').show();
        $('.page').click(function () {
            $(this).remove();
            $('.cont').remove();
        })
        $('.close').click(function () {
            $('.page').remove();
            $('.cont').remove();
        })
    } else {
        if (src.indexOf('?') >= 0) {
            src += '&r=' + Math.random() * 10;
        }
        else {
            src += '?r=' + Math.random() * 11;
        }
        $('body').append('<div class="page"></div>');
        $('body').append('<div class="cont"  style="width: ' + width + 'px;height: ' + height + 'px;"><p class="close">x</p></div>');
        $(".close").after('<iframe src="' + src + '" id="iframepage" frameborder="0" scrolling="auto" marginheight="0" marginwidth="0" style="width: ' + width + 'px;height: ' + height + 'px;margin-top:5px;"></iframe>');
        $('.page').click(function () {
            $(this).remove();
            $('.cont').remove();
        })
        $('.close').click(function () {
            $('.page').remove();
            $('.cont').remove();
        });

        var top = ($(window).height() - $(".cont").height()) / 2;
        var left = ($(window).width() - $(".cont").width()) / 2;
        var scrollTop = $(document).scrollTop();
        var scrollLeft = $(document).scrollLeft();
        $(".cont").css({ position: 'absolute', 'top': top + scrollTop, left: left + scrollLeft });

    }
}
function closep() {
    $('.page').remove();
    $('.cont').remove();
}


