(function ($) {
    $.request = {
        querystring: function (item) {
            var svalue = location.search.match(new RegExp("[\?\&]" + item + "=([^\&]*)(\&?)", "i"));
            return svalue ? svalue[1] : svalue;
        }
    };
    $.strLenth = function (s) {
        var l = 0;
        var a = s.split("");
        for (var i = 0; i < a.length; i++) {
            if (a[i].charCodeAt(0) < 299) {
                l = l + 0.5;
            } else {
                l += 1;
            }
        }
        return l;
    };

})(jQuery);