Highcharts.setOptions({
    lang: {
        months: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月'],
        weekdays: ['星期日', '星期一', '星期二', '星期三', '星期四', '星期五', '星期六']
    },
    colors: ['#228B22', '#FF8000', '#E3170D']
});
//Highcharts.setOptions({ colors: ['#228B22', '#FF8000', '#E3170D'] });
function check_all(obj, cName) {
    var checkboxs = document.getElementsByName(cName);
    for (var i = 0; i < checkboxs.length; i++) { checkboxs[i].checked = obj.checked; }
}
getKeywordFamily = function () {
    $.get("/Ajax/GetKeywordFamily.ashx?r=" + new Date().getTime(), function (data) {
        try {
            json = JSON.parse(data);
            var html = '<b>监测对象：</b><input type="checkbox" id="cbAllkeywordfamily" name="cbAllkeywordfamily">全部&nbsp;&nbsp;';
            $.each(json.data.items, function (index, item) {
                html += '<input type="checkbox" value="' + item.id + '" name="keywordfamily">' + item.name + '&nbsp;&nbsp;';
            });
            $("#p_keywordfamily").html(html);
        } catch (e) { }
    });
}
getplant = function () {
    $.get("/Ajax/GetPlatFrom.ashx?r=" + new Date().getTime(), function (data) {
        try {
            json = JSON.parse(data);
            var html = '<b>监测平台：</b><input type="checkbox"  id="cbAllplat"  name="cbAllplat">全部&nbsp;&nbsp;';
            $.each(json.data, function (index, item) {
                html += '<input type="checkbox" value="' + item.id + '" name="platsource">' + item.name + '&nbsp;&nbsp;';
            });
            $("#platList").html(html);
        } catch (e) { }
    });
}
function drawPieChart(keywordfamily, platforms, isToday) {
    $.ajax({
        url: "/Ajax/GetEmotionalData.ashx",
        type: "POST",
        data: { keywordfamily: keywordfamily, platforms: platforms, isToday: isToday },
        beforeSend: function () { },
        success: function (data) {
            try {
                data = JSON.parse(data);
                var series = [];
                var cdata = []
                $.each(data.data, function (index, item) {
                    if (item.key == 1) {
                        cdata.push({
                            name: item.title,
                            y: item.value,
                            sliced: true,
                            selected: true
                        });
                    }
                    else
                        cdata.push([item.title, item.value]);

                });

                series.push({
                    type: 'pie',
                    name: '比例：',
                    data: cdata
                });
                $('#piechart').highcharts({
                    chart: {
                        plotBackgroundColor: null,
                        plotBorderWidth: null,
                        plotShadow: true
                    },
                    title: {
                        text: ''
                    },
                    tooltip: {
                        formatter: function () {
                            return '<b>' + this.point.name + '</b></br>: ' + Highcharts.numberFormat(this.percentage, 0) + '%';
                        }
                    },
                    plotOptions: {
                        pie: {
                            allowPointSelect: true,
                            cursor: 'pointer',
                            dataLabels: {
                                enabled: false
                            },
                            showInLegend: false
                        }
                    },
                    series: series
                });

            } catch (e) { }

        },
        error: function () { }
    });
}
function drawLineChart(keywordfamily, platforms, isToday, emotionvalues) {
    $.ajax({
        url: "/Ajax/GetSummaryData.ashx",
        type: "POST",
        data: { keywordfamily: keywordfamily, platforms: platforms, isToday: isToday, emotionvalues: emotionvalues },
        beforeSend: function () { },
        success: function (data) {
            try {
                data = JSON.parse(data);
                var series = [];
                $.each(data.data.items, function (index, item) {
                    var dataxy = [];

                    $.each(item.Value, function (cindex, citem) {
                        if (isToday) {
                            var date = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate(), citem.key);
                            dataxy.push([date.getTime() + 8 * 3600 * 1000, citem.value]);
                        }
                        else {
                            dataxy.push([citem.key, citem.value]);
                        }
                    });
                    series.push({
                        name: item.Key,
                        data: dataxy
                    });
                });
                var dateTimeLabelFormats;
                if (isToday) {
                    dateTimeLabelFormats = { hour: '%H:00', };
                } else {
                    dateTimeLabelFormats = { day: '%B %e日', };
                }

                $('#container').highcharts({
                    chart: {
                        type: 'spline'
                    },
                    title: {
                        text: ''
                    },
                    subtitle: {
                        text: ''
                    },
                    xAxis: {
                        type: 'datetime',
                        dateTimeLabelFormats: dateTimeLabelFormats,
                        //categories: ["1" , "2", "3", "4", "5", "6"]
                    },
                    yAxis: {
                        title: {
                            text: '数 量 (条)'
                        },
                        lineWidth: 2,
                        minorTickInterval: 'auto',
                        min: 0
                    },
                    tooltip: {
                        formatter: function () {
                            if (isToday) {
                                return '<b>' + this.series.name + '</b><br/>' +
                                Highcharts.dateFormat('%H:00', this.x) + ': ' + this.y + '条';
                            }
                            else {
                                return '<b>' + this.series.name + '</b><br/>' +
                               Highcharts.dateFormat('%B%e日', this.x) + ': ' + this.y + '条';
                            }
                        }
                    },
                    series: series
                });

            } catch (e) { }
        },
        error: function () { }
    });
}
function drawCloudWord(keywordfamily) {

    $.ajax({
        url: "/Ajax/GetHotKeyword.ashx",
        type: "POST",
        data: { keywordfamily: keywordfamily },
        beforeSend: function () { },
        success: function (data) {
            data = JSON.parse(data);
            var word_list = [];
            $.each(data.data.items, function (index, item) {
             
                word_list.push({ text: item.name, weight: item.weight });
            });
            //var word_list = [
            //          { text: "抑郁症", weight: 11, link: { href: "http://weibo.com/dmonsns/", target: "_blank" } },
            //          { text: "患者", weight: 10.5, html: { title: "My Title", "class": "custom-class" }, link: { href: "http://weibo.com/dmonsns/", target: "_blank" } },
            $("#containers").jQCloud(word_list);
        },
        error: function () { }
    });

}

function getcbplatsource() {
    var result = [];
    $("input[name=platsource]:checked").each(function () {
        result.push($(this).val());
    });
    return result.toString();
}
function getcbkeywordfamily() {
    var result = [];
    $("input[name=keywordfamily]:checked").each(function () {
        result.push($(this).val());
    });
    return result.toString();
}
function getcbemotionvalues() {
    var result = [];
    $("input[name=emotionvalues]:checked").each(function () {
        result.push($(this).val());
    });
    return result.toString();
}

$(document).ready(function () {
    getKeywordFamily();
    getplant();
    //平台checkbox
    $("#cbAllplat").live("click", function (e) {
        check_all(this, "platsource");
    });
    //数据源checkbox
    $("#cbAllkeywordfamily").live("click", function (e) {
        check_all(this, "keywordfamily");
    });

    $("#cbAllEmotional").live("click", function (e) {
        check_all(this, "emotionvalues");
    });
    $("input[type=checkbox]").live("click", function (e) {
        var name = $(this).attr("name");
        if (name.indexOf("All") <= 0) {
            if ($("input[name=" + name + "]").length == $("input[name=" + name + "]:checked").length) {
                $(this).parent().find("input")[0].checked = true;
            }
            else {
                $(this).parent().find("input")[0].checked = false;
            }
        }
    });

});