function setInputDefaultValue(id, type) {
    if (!id) return false;

    var $txtClientID = $('#' + id);
    var inputValue = '请输入关键字选择';
    if (type) { inputValue = inputValue + type; }

    if ($txtClientID.val() == '') {
        $txtClientID.val(inputValue);
    }

    $txtClientID.click(function () {
        if ($(this).val() == inputValue) {
            $(this).val('').css('border', '1px solid red');
            $(this).val('');
        }
        else {
            $(this).css('border', '');
        }
    });

    $txtClientID.blur(function () {
        if ($(this).val().trim() == '') {
            $(this).css('border', '').val(inputValue);
        }
        else if ($(this).val().trim() != '' && $(this).val().trim() != inputValue) {
            $(this).css('border', '');
        }
    });
}
function bindAutoComplete(bindId, valueId, ops, mainId, uri) {

    if (!bindId) return false;
    if (!ops) return false;
    if (!uri) return false;
    if (!mainId) return false;

    var inputWidth = $('#' + bindId).css('width').replace("px", '');

    $('#' + bindId).autocomplete({
        serviceUrl: uri,
        paramName: 'cusname',//此名称不能更改，如若更改，则后台也需更改
        width: inputWidth,
        minChars: 0,
        onSelect: function (suggestion) {
            if (bindId) {
                $("#" + valueId).val(suggestion.data);
            }
        },
        deferRequestBy: 500,
        params: { op: ops, mid: mainId },
        noCache: false
    });
}