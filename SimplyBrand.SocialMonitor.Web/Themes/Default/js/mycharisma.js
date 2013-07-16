$(document).ready(function () {
    //themes, change CSS with JS
    //default theme(CSS) is cerulean, change it if needed
    if ($.cookie('sysuserlogo') != null)
        $("#logo").attr("src", $.cookie('sysuserlogo'));

    var current_theme = $.cookie('current_theme') == null ? 'cerulean' : $.cookie('current_theme');
    switch_theme(current_theme);

    $('#themes a[data-value="' + current_theme + '"]').find('i').addClass('icon-ok');

    $('#themes a').click(function (e) {
        e.preventDefault();
        current_theme = $(this).attr('data-value');
        $.cookie('current_theme', current_theme, { expires: 365 });
        switch_theme(current_theme);
        $('#themes i').removeClass('icon-ok');
        $(this).find('i').addClass('icon-ok');
    });
    var current_lang = $.cookie('current_lang') == null ? 'cn' : $.cookie('current_lang');
    switch_lang(current_lang);

    $('#lang a[data-value="' + current_lang + '"]').find('i').addClass('icon-ok');

    $('#lang a').click(function (e) {
        e.preventDefault();
        current_lang = $(this).attr('data-value');
        $.cookie('current_lang', current_lang, { expires: 365 });
        switch_lang(current_lang);
        $('#lang i').removeClass('icon-ok');
        $(this).find('i').addClass('icon-ok');
    });
    function switch_theme(theme_name) {
        $('#bs-css').attr('href', '/Themes/Default/css/bootstrap-' + theme_name + '.css');
    }
    function switch_lang(lang_name) {
        loadBundles(lang_name);
        if (lang_name == "cn") {
            $("#splangname").html("中文");
            $.datepicker.setDefaults($.extend($.datepicker.regional['zh-CN']));
        } else {
            $("#splangname").html("English");
            $.datepicker.setDefaults($.extend($.datepicker.regional['']));
        }

    }
    function loadBundles(lang) {

        jQuery.i18n.properties({
            name: 'simplybrand',
            path: '/Themes/Default/i18n/bundles/',
            mode: 'both',
            language: lang,
            callback: function () {
                changeLanguage();
            }
        });
    }
    function changeLanguage() {

        var mainmenu = '<li class="nav-header hidden-tablet">' + jQuery.i18n.prop("sb_myprofile") + '</li>' +
                       '<li><a class="ajax-link" href="setting.aspx"><span class="hidden-tablet">&nbsp;&nbsp;&nbsp;&nbsp;' + jQuery.i18n.prop("sb_syssetting") + '</span></a></li>' +
                      '<li><a class="ajax-link" href="infocenter.aspx"><span class="hidden-tablet">&nbsp;&nbsp;&nbsp;&nbsp;' + jQuery.i18n.prop("sb_infocenter") + '</span></a></li>' +
                      '<li><a class="ajax-link" href="myreport.aspx"><span class="hidden-tablet">&nbsp;&nbsp;&nbsp;&nbsp;' + jQuery.i18n.prop("sb_myreport") + '</span></a></li>' +
                      '<li class="nav-header hidden-tablet">' + jQuery.i18n.prop("sb_networkopinion") + '</li>' +
                      '<li><a class="ajax-link" href="todaydynamic.aspx"><span class="hidden-tablet">&nbsp;&nbsp;&nbsp;&nbsp;' + jQuery.i18n.prop("sb_todaydynamic") + '</span></a></li>' +
                      '<li><a class="ajax-link" href="historydata.aspx"><span class="hidden-tablet">&nbsp;&nbsp;&nbsp;&nbsp;' + jQuery.i18n.prop("sb_historydata") + '</span></a></li>' +
                      '<li><a class="ajax-link" href="datacenter.aspx"><span class="hidden-tablet">&nbsp;&nbsp;&nbsp;&nbsp;' + jQuery.i18n.prop("sb_datacenter") + '</span></a></li>';
        //'<li><a class="ajax-link" href="searchlist.aspx"><span class="hidden-tablet">&nbsp;&nbsp;&nbsp;&nbsp;' + jQuery.i18n.prop("sb_searchrecord") + '</span></a></li>'
        $("#main_menu").empty().append(mainmenu);//菜单

        $("[id^=sb_],[name^=sb_]").each(function (index, item) {
            var key = $(this).attr("id");
            if (key != "sb_rmonitoringreportsinfo") {
                if (key == "" || key == undefined) {
                    key = $(this).attr("name");
                }
                $(this).html(jQuery.i18n.prop(key));
            }
        });
        $("#sb_rmonitoringreportsinfo").attr("data-original-title", jQuery.i18n.prop("sb_rmonitoringreportsinfo"));
    }

    //ajax menu checkbox
    $('#is-ajax').click(function (e) {
        $.cookie('is-ajax', $(this).prop('checked'), { expires: 365 });
    });
    $('#is-ajax').prop('checked', $.cookie('is-ajax') === 'true' ? true : false);

    //disbaling some functions for Internet Explorer
    if ($.browser.msie) {
        $('#is-ajax').prop('checked', false);
        $('#for-is-ajax').hide();
        $('#toggle-fullscreen').hide();
        $('.login-box').find('.input-large').removeClass('span10');

    }


    //highlight current / active link
    $('ul.main-menu li a').each(function () {

        if ($($(this))[0].href == String(window.location))
            $(this).parent().addClass('active');
    });
    //establish history variables
    var
		History = window.History, // Note: We are using a capital H instead of a lower h
		State = History.getState(),
		$log = $('#log');

    //bind to State Change
    History.Adapter.bind(window, 'statechange', function () { // Note: We are using statechange instead of popstate
        var State = History.getState(); // Note: We are using History.getState() instead of event.state
        $.ajax({
            url: State.url,
            success: function (msg) {
                $('#content').html($(msg).find('#content').html());
                $('#loading').remove();
                $('#content').fadeIn();
                var newTitle = $(msg).filter('title').text();
                $('title').text(newTitle);
                docReady();
            }
        });
    });

    //ajaxify menus
    $('a.ajax-link').click(function (e) {
        if ($.browser.msie) e.which = 1;
        if (e.which != 1 || !$('#is-ajax').prop('checked') || $(this).parent().hasClass('active')) return;
        e.preventDefault();
        if ($('.btn-navbar').is(':visible')) {
            $('.btn-navbar').click();
        }
        $('#loading').remove();
        $('#content').fadeOut().parent().append('<div id="loading" class="center">Loading...<div class="center"></div></div>');
        var $clink = $(this);
        History.pushState(null, null, $clink.attr('href'));
        $('ul.main-menu li.active').removeClass('active');
        $clink.parent('li').addClass('active');
    });

    //animating menus on hover
    $('ul.main-menu li:not(.nav-header)').hover(function () {
        $(this).animate({ 'margin-left': '+=5' }, 300);
    },
	function () {
	    $(this).animate({ 'margin-left': '-=5' }, 300);
	});

    $('#myTab a:first').tab('show');
    $('#myTab a').click(function (e) {
        e.preventDefault();
        $(this).tab('show');
        $(".week-picker").hide();
        $("#myTabContent").find("input").attr("value", "");
    });
    if (current_lang == "cn") {
        $.datepicker.setDefaults($.extend($.datepicker.regional['zh-CN']));
    } else {
        $.datepicker.setDefaults($.extend($.datepicker.regional['']));
    }

    $('#txtDay').datepicker({ changeMonth: true, changeYear: true, dateFormat: 'yy-mm-dd' });
    $('#txtMonth').datepicker({
        changeMonth: true,
        changeYear: true,
        selectWeek: true,
        showButtonPanel: true,
        dateFormat: 'yy-mm',
        onClose: function (dateText, inst) {
            var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
            var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
            $(this).datepicker('setDate', new Date(year, month, 1));

        }
    }).focus(function () {
        $(".ui-datepicker-calendar").hide();
    });


    var startDate;
    var endDate;
    var selectCurrentWeek = function () {
        window.setTimeout(function () {
            $('.week-picker').find('.ui-datepicker-current-day a').addClass('ui-state-active')
        }, 1);
    }
    $('.week-picker').datepicker({
        showOtherMonths: true,
        selectOtherMonths: true,
        onSelect: function (dateText, inst) {
            var date = $(this).datepicker('getDate');
            startDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - date.getDay() + 1);
            endDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - date.getDay() + 7);
            var dateFormat = inst.settings.dateFormat || $.datepicker._defaults.dateFormat;
            //$('#startDate').text($.datepicker.formatDate(dateFormat, startDate, inst.settings));
            //$('#endDate').text($.datepicker.formatDate(dateFormat, endDate, inst.settings));
            $("#txtWeekday").attr("startdate", $.datepicker.formatDate(dateFormat, startDate, inst.settings));
            $("#txtWeekday").attr("enddate", $.datepicker.formatDate(dateFormat, endDate, inst.settings));
            $("#txtWeekday").val($.datepicker.formatDate(dateFormat, startDate, inst.settings) + " ~ " + $.datepicker.formatDate(dateFormat, endDate, inst.settings));

            selectCurrentWeek();
            $(".week-picker").css("display", "none");
        },
        beforeShowDay: function (date) {
            var cssClass = '';
            if (date >= startDate && date <= endDate)
                cssClass = 'ui-datepicker-current-day';
            return [true, cssClass];
        },
        onChangeMonthYear: function (year, month, inst) {
            selectCurrentWeek();
        }
    });
    $('.week-picker .ui-datepicker-calendar tr').live('mousemove', function () { $(this).find('td a').addClass('ui-state-hover'); });
    $('.week-picker .ui-datepicker-calendar tr').live('mouseleave', function () { $(this).find('td a').removeClass('ui-state-hover'); });

    $("#txtWeekday").click(function () {
        $(".week-picker").css("display", "block");
        $(".ui-datepicker-calendar").show();
    });

    //uploadify - multiple uploads
    $('#file_upload').uploadify({
        'swf': '/Themes/Default/misc/uploadify.swf',
        'uploader': '/Themes/Default/misc/uploadify.php'
    });
    //tooltip
    $('[rel="tooltip"],[data-rel="tooltip"]').tooltip({ "placement": "bottom", delay: { show: 400, hide: 200 } });
    $('#file_upload').uploadify({
        'swf': '/Themes/Default/misc/uploadify.swf',
        'uploader': '/Ajax/UploadLogo.ashx',
        'fileTypeExts': '',
        'multi': 'false',
        'fileSizeLimit': '1024',
        'simUploadLimit': 1,
        'onUploadError': function (file, errorCode, errorMsg, errorString) {
        },
        'onUploadSuccess': function (file, data, response) {
            try {
                data = JSON.parse(data);
                if (data.issucc) {
                    notycommon("上传图片成功");
                    $.cookie('sysuserlogo', data.data.logo, { expires: 365 });
                    $("#logo").attr("src", data.data.logo);
                }
            } catch (e) { }
        },
        'onSelect': function (file) {
            file = file;
        }
    });
});
