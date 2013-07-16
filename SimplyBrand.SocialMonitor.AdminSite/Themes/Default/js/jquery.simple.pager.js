/*
* jQuery pager plugin
* Version 1.0 (12/22/2008)
* @requires jQuery v1.2.6 or later
*
* Example at: http://jonpauldavies.github.com/JQuery/Pager/PagerDemo.html
*
* Copyright (c) 2008-2009 Jon Paul Davies
* Dual licensed under the MIT and GPL licenses:
* http://www.opensource.org/licenses/mit-license.php
* http://www.gnu.org/licenses/gpl.html
* 
* Read the related blog post and contact the author at http://www.j-dee.com/2008/12/22/jquery-pager-plugin/
*
* This version is far from perfect and doesn't manage it's own state, therefore contributions are more than welcome!
*
* Usage: .pager({ pagenumber: 1, pagecount: 15, buttonClickCallback: PagerClickTest });
*
* Where pagenumber is the visible page number
*       pagecount is the total number of pages to display
*       buttonClickCallback is the method to fire when a pager button is clicked.
*
* buttonClickCallback signiture is PagerClickTest = function(pageclickednumber) 
* Where pageclickednumber is the number of the page clicked in the control.
*
* The included Pager.CSS file is a dependancy but can obviously tweaked to your wishes
* Tested in IE6 IE7 Firefox & Safari. Any browser strangeness, please report.
*参数说明
*
*options.recordCount 总页数
*options.pageSize  每页大小
*options.pagenumber  当前页数
*options.customerText 是否自定义文本，默认不显示
*options.buttonClickCallback 分页的回调函数
*/
(function ($) {

	$.fn.pager = function (options) {
		//只有总页数大于每页数时才显示分页
		if (parseInt(options.recordCount) > parseInt(options.pageSize)) {

			var opts = $.extend({}, $.fn.pager.defaults, options);

			return this.each(function () {

				// empty out the destination element and then render out the pager with the supplied options
				var pagecount = GetPageCount(parseInt(options.recordCount), parseInt(options.pageSize));

				$(this).empty().append(renderpager(parseInt(options.pagenumber), pagecount, parseInt(options.pageSize), parseInt(options.recordCount), options.customerText, options.buttonClickCallback));

				// specify correct cursor activity
				// $('.pages li').mouseover(function () { document.body.style.cursor = "pointer"; }).mouseout(function () { document.body.style.cursor = "auto"; });
			});
		} else { $(this).empty().append('<ul></ul>'); }
	};
	//获得总页数
	function GetPageCount(recordCount, pageSize) {

		if (recordCount <= 0) return 1;
		if (recordCount % pageSize == 0)
			return parseInt(recordCount / pageSize);
		else
			return parseInt(recordCount / pageSize) + 1;
	}
	// render and return the pager with the supplied options
	function renderpager(pagenumber, pagecount, pageSize, recordCount, customerText, buttonClickCallback) {

		// setup $pager to hold render

		var $pager = $('<ul></ul>');

		// add in the previous and next buttons
		// $pager.append(renderButton('首页', pagenumber, pagecount, buttonClickCallback));

		$pager.append(renderButton('上一页', pagenumber, pagecount, buttonClickCallback));

		// pager currently only handles 10 viewable pages ( could be easily parameterized, maybe in next version ) so handle edge cases
		var startPoint = 1;
		var endPoint = 6;

		if (pagenumber > 3) {
			startPoint = pagenumber - 3;
			endPoint = pagenumber + 3;
		}

		if (endPoint > pagecount) {
			startPoint = pagecount - 6;
			endPoint = pagecount;
		}

		if (startPoint < 1) {
			startPoint = 1;
		}

		// loop thru visible pages and render buttons
		for (var page = startPoint; page <= endPoint; page++) {
			var currentButton = $('<li><a href="javascript:void(0);">' + (page) + '</a></li>');
			page == pagenumber ? currentButton.addClass('active') : currentButton.click(function () {  buttonClickCallback(this.firstChild.firstChild.data); });
			currentButton.appendTo($pager);
		}

		// render in the next and last buttons before returning the whole rendered control back.
		$pager.append(renderButton('下一页', pagenumber, pagecount, buttonClickCallback)); //.append(renderButton('尾页', pagenumber, pagecount, buttonClickCallback));

		return $pager;
	}

	// renders and returns a 'specialized' button, ie 'next', 'previous' etc. rather than a page number button
	function renderButton(buttonLabel, pagenumber, pagecount, buttonClickCallback) {
		var $Button;
		if (pagecount <= 1) {
			$Button = $('<li><a>' + buttonLabel + '</a></li>');
		}
		else {
			if (pagenumber == 1 && buttonLabel == "上一页")
				$Button = $('<li class="prev"><a>' + buttonLabel + '</a></li>');
			else if (pagenumber == pagecount && buttonLabel == "下一页")
			    $Button = $('<li class="next"><a>' + buttonLabel + '</a></li>');
			else
				$Button = $('<li><a href="javascript:void(0);">' + buttonLabel + '</a></li>');
		}
		var destPage = 1;

		// work out destination page for required button type
		switch (buttonLabel) {
			case "首页":
				destPage = 1;
				break;
			case "上一页":
				destPage = pagenumber - 1;
				break;
			case "下一页":
				destPage = pagenumber + 1;
				break;
			case "尾页":
				destPage = pagecount;
				break;
		}

		// disable and 'grey' out buttons if not needed.
		//.pager a.enable:hover{background: url(../images/page_aBg.png) repeat-x; color:#888;padding:0px 7px; float:left;margin:0 3px;_display:inline;border:1px solid #bdbdbd; height:19px; overflow:hidden; line-height:18px;}
		if (buttonLabel == "首页" || buttonLabel == "上一页") {
		    pagenumber <= 1 ? $Button.addClass('disabled') : $Button.click(function () { buttonClickCallback(destPage); });
		}
		else {
		    pagenumber >= pagecount ? $Button.addClass('disabled') : $Button.click(function () { buttonClickCallback(destPage); });
		}

		return $Button;
	}

	//==========================


	function GetPageNum() {
		alert("请输入要跳转的页码！");
		/*
        var pagenum=$.trim($("#pageNum").val());
		
        if(pagenum=="")
        {
        alert("请输入要跳转的页码！");
        $("#pageNum").focus();
			
        }
        */

	}

	//====================

	// pager defaults. hardly worth bothering with in this case but used as placeholder for expansion in the next version
	$.fn.pager.defaults = {
		pagenumber: 1,
		//pagecount: 1,
		recordCount: 0,
		pageSize: 10
	};

})(jQuery);





