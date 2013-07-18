<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DataCenter.aspx.cs" Inherits="SimplyBrand.SocialMonitor.Web.DataCenter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- content starts -->
    <div>
        <ul class="breadcrumb">
            <li>
                <a href="#" id="sb_networkopinion">网络舆情</a>
                <span class="divider">/</span>
            </li>
            <li>
                <a href="#" id="sb_todaydetails">今日详情</a>
            </li>
        </ul>
    </div>
    <div class="row-fluid sortable">
        <div class="box span12">
            <div class="box-content">
                <p id="platList">
                </p>
                <p id="p_keywordfamily">
                </p>
                <p>
                    <b>情&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;感：</b>
                    <input type="checkbox" id="cbAllEmotional">全部 
                        <input type="checkbox" value="1" name="emotionvalues">正面&nbsp;&nbsp;
                        <input type="checkbox" value="0" name="emotionvalues">中性&nbsp;&nbsp;
                        <input type="checkbox" value="-1" name="emotionvalues">负面&nbsp;&nbsp;
                </p>
                <p>
                    <b>在结果中搜索：</b>
                    包含：<input type="text" id="txtcontain">
                    排除：<input type="text" id="txtnotcontain">
                    <button type="submit" class="btn btn-primary" style="margin-bottom: 10px;">确定</button>
                </p>

                <ul class="nav nav-tabs" id="myTab">
                    <li class="active"><a href="#day" data="0">全部(<span id="allcount"></span>)</a></li>
                    <li><a href="#weekday" data="1">微博(<span id="weibocount"></span>)</a></li>
                    <li><a href="#month" data="2">博客(<span id="blogcount"></span>)</a></li>
                    <li><a href="#month" data="3">论坛(<span id="luntcount"></span>)</a></li>
                    <li><a href="#month" data="4">新闻(<span id="newscount"></span>)</a></li>

                </ul>
                <%-- <p style="background-color:#B0C4DE;"><b>全部</b> &nbsp;&nbsp;&nbsp;&nbsp;<b>微博</b>&nbsp;&nbsp;<b>博客</b> &nbsp;&nbsp; <b>论坛</b> &nbsp;&nbsp; <b>新闻</b>(<span id="newscount"></span>)</p>--%>
                <table class="table table-bordered table-striped table-condensed">
                    <thead>
                        <tr>
                            <th class="center">序号</th>
                            <th class="center">来源</th>
                            <th class="center">关键词</th>
                            <th class="center">情感</th>
                            <th class="center">内容</th>
                            <th class="center">作者</th>
                            <th class="center">转发/浏览量</th>
                            <th class="center">评论/回复量</th>
                            <th class="center">网站名称</th>
                            <th class="center">时间</th>
                        </tr>
                    </thead>
                    <tbody role="alert" aria-live="polite" aria-relevant="all" id="tbodydata">
                    </tbody>
                </table>
                <div class="row-fluid">
                    <div class="span12 center">
                        <div class="dataTables_paginate paging_bootstrap pagination" id="pager"></div>
                    </div>
                </div>
               <%-- <div class="row-fluid">
                    <div class="span8">
                        <span>生成报告：<select style="width: 90px;">
                            <option>发表时间</option>
                            <option>发表时间</option>
                        </select>
                            开始 &nbsp;&nbsp;
								<input type="text" class="datepicker" id="txtstarttime" value="">
                            结束 &nbsp;&nbsp;<input type="text" class="datepicker" id="txtendtime"></span>
                    </div>
                    <%-- 生成类型<select style="width: 80px;">
                        <option>HTML</option>
                        <option>PDF</option>
                        <option>Execl</option>
                    </select>
                    <div><span class="span2"><a class="btn btn-primary">生成报告</a></span></div>
                </div>--%>
            </div>
        </div>
    </div>
    <script src="Themes/Default/js/jquery.simple.pager.js"></script>
    <script src="Themes/Default/js/basedata.js"></script>   
    <script type="text/javascript">
        var Getdatatype = 0;
        function check_all(obj, cName) {
            var checkboxs = document.getElementsByName(cName);
            for (var i = 0; i < checkboxs.length; i++) { checkboxs[i].checked = obj.checked; }
        }
        function change_txt(obj) {
            switch (obj) {
                case -1:
                    return "负";
                    break;
                case 0:
                    return "中";
                    break;
                case 1:
                    return "正";
                    break;

                default:
                    return "中"; break;

            }
        }

        function substringlength(str, length) {
            if (str.length > length) {
                str = str.substring(0, length) + "...";
                return str;
            }
            else {
                return str;
            }
        }
        $(document).ready(function () {
            $(".btn-primary").live("click", function (e) {;
                PageClick(1);
            });
            $('#myTab a').live("click", function (e) {
                $(this).tab('show');
                pageindex = 1;
                Getdatatype = $(this).attr("data");
                PageClick(pageindex);
            });

            PageClick(1);
        });
        function GetplatType(obj) {
            switch (obj) {
                case 1:
                    return "微博";
                    break;
                case 2:
                    return "博客";
                    break;
                case 3:
                    return "论坛";
                    break;
                case 4:
                    return "新闻";
                    break;
                default:
                    return "其它"
                    break;
            }
        }
        PageClick = function (pageclickednumber) {
            var plancheckboxs = document.getElementsByName("platsource");
            var checkboxs = document.getElementsByName("keywordfamily");
            var emotionalcheckboxs = document.getElementsByName("emotionvalues");
            //平台信息         
            for (var i = 0; i < plancheckboxs.length; i++) {
                if (plancheckboxs[i].checked == true) {
                    Getdatatype = Getdatatype + plancheckboxs[i].value + ",";
                }
            }
            //品牌ID列表
            var familyids = "";
            for (var y = 0; y < checkboxs.length; y++) {
                if (checkboxs[y].checked == true) {
                    familyids = familyids + checkboxs[y].value + ",";
                }
            }
            //品牌ID列表
            var emotionals = "";
            for (var z = 0; z < emotionalcheckboxs.length; z++) {
                if (emotionalcheckboxs[z].checked == true) {
                    emotionals = emotionals + emotionalcheckboxs[z].value + ",";
                }
            }
            // sourceids, familyids,keyvalue, notkeyvalue,starttime, endtime, emotional, pageindex, pagesize)
            var starttime = $("#starttime").val();
            var endtime = $("#endtime").val();

            var emotional = 0;
            $.ajax({
                "type": "POST",
                "url": "/Ajax/GetDataPageListInfo.ashx",
                "data": { sourceids: Getdatatype, familyids: familyids, keyvalue: $("#txtcontain").val(), notkeyvalue: $("#txtnotcontain").val(), starttime: starttime, endtime: endtime, emotional: emotionals, pageindex: pageclickednumber },
                beforeSend: function () { },
                success: function (data) {
                    try {
                        var json = JSON.parse(data);
                        var html = "";
                        $.each(json.data.items, function (index, item) {
                            html += '<tr data="' + item.dataid + '" datasource="' + item.datasourceid + '" emotionalvalue="' + item.emotionalvalue + '" datatitle="' + item.datatitle.toString() + '" sitename="' + item.sitename.toString() + '" datatime="' + item.datatime + '" >';
                            html += '<td class="center">' + item.dataid + '</td>';
                            html += '<td class="center">' + GetplatType(item.datasourceid) + '</td>';
                            html += '<td class="center">' + item.dataKey + '</td>';
                            html += '<td class="center">' + change_txt(item.emotionalvalue) + '</td>';
                            html += '<td class="center"><a href="#"  data-rel="popover" data-content="' + item.datatitle + '" title="' + GetplatType(item.datasourceid) + '">' + substringlength(item.datatitle, 15) + '<a/></td>';
                            html += '<td class="center" title="' + item.dataauthor + '">' + substringlength(item.dataauthor, 6) + '</td>';
                            html += '<td class="center">' + item.dataforward + '</td>';
                            html += '<td class="center">' + item.datacomment + '</td>';
                            html += '<td class="center">' + item.sitename + '</td>';
                            html += '<td class="center">' + item.datatime + '</td>';

                        });
                        $("#tbodydata").empty().html(html);

                        $('[rel="tooltip"],[data-rel="tooltip"]').tooltip({ "placement": "bottom", "width": "300px", delay: { show: 400, hide: 200 } });

                        $("#pager").pager({ pagenumber: pageclickednumber, recordCount: json.data.count, pageSize: "30", buttonClickCallback: PageClick });
                        if (json.data.count == 0) {
                            $("#allcount").html(json.data.count);
                        }
                        else {
                            $("#allcount").html("<a herf=''>" + json.data.count);
                        }
                        $("#weibocount").html(json.Weibocount);
                        $("#newscount").html(json.Newscount);
                        $("#blogcount").html(json.Blogcount);
                        $("#luntcount").html(json.Forumcount);
                        //popover
                        $('[rel="popover"],[data-rel="popover"]').popover();

                    } catch (e) { }
                },
                error: function () { }
            });
        }

    </script>

</asp:Content>
