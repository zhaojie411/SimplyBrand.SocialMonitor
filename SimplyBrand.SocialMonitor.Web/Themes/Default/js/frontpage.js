(function ($) {
    $(function () {

        
        var chart1 = new Highcharts.Chart({
            chart: {
                defaultSeriesType: 'bar',
                renderTo: 'chart_competitors',

            },
            title: {
                text: 'UGC话题中涉及的迈腾主要竞品'
            },
            subtitle: {
                text: 'Source: Wikipedia.org'
            },
            xAxis: {
                categories: ['锐志', '凯美洲', '雅阁', '新天', '领驭', '马六', '君威', '蒙迪欢', 'A4', '凯旋'],
                title: {
                    text: null
                }
            },
            yAxis: {
                min: 0,
                title: {
                    text: 'Population (millions)',
                    align: 'high'
                },
                labels: {
                    overflow: 'justify'
                }
            },
            tooltip: {
                valueSuffix: ' millions'
            },
            plotOptions: {
                bar: {
                    dataLabels: {
                        enabled: true
                    }
                }
            },
            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'top',
                x: -100,
                y: 100,
                floating: true,
                borderWidth: 1,
                backgroundColor: '#FFFFFF',
                shadow: true
            },
            credits: {
                enabled: true
            },
            series: [{
                name: '影响力',
                data: [149, 136, 131, 114, 68, 56, 47, 39, 26, 18]
            }]
        });
         
       

        var chart2 = new Highcharts.Chart({
            chart: {
                defaultSeriesType: 'column',
                renderTo: 'chart_praise',
            },
            title: {
                text: '迈腾口碑调性分布'
            },
            xAxis: {
                categories: ['UGC', '总计']
            },
            yAxis: {
                min: 0,
                title: {
                    text: ''
                },
                stackLabels: {
                    enabled: true,
                    style: {
                        fontWeight: 'bold',
                        color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                    }
                }
            },
            legend: {
                align: 'right',
                x: -100,
                verticalAlign: 'top',
                y: 20,
                floating: true,
                backgroundColor: (Highcharts.theme && Highcharts.theme.legendBackgroundColorSolid) || 'white',
                borderColor: '#CCC',
                borderWidth: 1,
                shadow: false
            },
            tooltip: {
                formatter: function () {
                    return '<b>' + this.x + '</b><br/>' +
                        this.series.name + ': ' + this.y + '<br/>' +
                        '总计: ' + this.point.stackTotal;
                }
            },
            plotOptions: {
                column: {
                    stacking: 'normal',
                    dataLabels: {
                        enabled: true,
                        color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white'
                    }
                }
            },
            series: [{
                name: '负面',
                data: [2, 2]
            }, {
                name: '中性',
                data: [5, 3]
            }, {
                name: '正面',
                data: [3, 4]
            }]
        });


        // The Clock
        function getNow() {
            var now = new Date();

            return {
                hours: now.getHours() + now.getMinutes() / 60,
                minutes: now.getMinutes() * 12 / 60 + now.getSeconds() * 12 / 3600,
                seconds: now.getSeconds() * 12 / 60
            };
        };


        function pad(number, length) {
            // Create an array of the remaining length +1 and join it with 0's
            return new Array((length || 2) + 1 - String(number).length).join(0) + number;
        }
        var now = getNow();

        // Create the chart
        var chart3 = new Highcharts.Chart({

            chart: {
                defaultSeriesType: 'bar',
                renderTo: 'chart_features',

            },
            title: {
                text: 'UGC话题中涉及的迈腾主要商品性'
            },
            subtitle: {
                text: ''
            },
            xAxis: {
                categories: ['价格', '配置', '保养/维修', '油耗', '安全', '舒适性'],
                title: {
                    text: null
                }
            },
            yAxis: {
                min: 0,
                title: {
                    text: 'Population (millions)',
                    align: 'high'
                },
                labels: {
                    overflow: 'justify'
                }
            },
            tooltip: {
                valueSuffix: ''
            },
            plotOptions: {
                bar: {
                    dataLabels: {
                        enabled: true
                    }
                }
            },
            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'top',
                x: -100,
                y: 100,
                floating: true,
                borderWidth: 1,
                backgroundColor: '#FFFFFF',
                shadow: true
            },
            credits: {
                enabled: true
            },
            series: [{
                name: '视觉感受',
                data: [149, 136, 131, 114, 68, 56]
            },
            {
                name: '驾驶感受',
                data: [150, 70, 160, 20, 50, 80]
            },
            {
                name: '其他',
                data: [60, 55, 120, 90, 40, 90]
            }
            ]
        });

    });


    // Extend jQuery with some easing (copied from jQuery UI)
    $.extend($.easing, {
        easeOutElastic: function (x, t, b, c, d) {
            var s = 1.70158;
            var p = 0;
            var a = c;
            if (t == 0)
                return b;
            if ((t /= d) == 1)
                return b + c;
            if (!p)
                p = d * .3;
            if (a < Math.abs(c)) {
                a = c;
                var s = p / 4;
            } else
                var s = p / (2 * Math.PI) * Math.asin(c / a);
            return a * Math.pow(2, -10 * t) * Math.sin((t * d - s) * (2 * Math.PI) / p) + c + b;
        }
    });


}(jQuery));