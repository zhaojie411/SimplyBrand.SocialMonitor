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
                <a href="#" id="sb_datacenter">数据中心</a>
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
                        <input type="checkbox" name="emotionvalues">正面&nbsp;&nbsp;
                        <input type="checkbox" name="emotionvalues">中性&nbsp;&nbsp;
                        <input type="checkbox" name="emotionvalues">负面&nbsp;&nbsp;
                </p>
                <p>
                    <b>在结果中搜索：</b>
                    包含：<input type="text" id="txtcontain">
                    排除：<input type="text" id="txtnotcontain">
                    <button type="submit" class="btn btn-primary" style="margin-bottom: 10px;">确定</button>
                </p>

                <p><b>全部</b>(21001) &nbsp;&nbsp;&nbsp;&nbsp;<b>微博</b>(112)&nbsp;&nbsp;<b>博客(223)</b> &nbsp;&nbsp; <b>论坛</b> &nbsp;&nbsp; <b>新闻</b>(9)</p>
                <table class="table table-bordered table-striped table-condensed">
                    <thead>
                        <tr>
                            <th class="center">序号</th>
                            <th class="center">来源</th>
                            <th class="center">关键词</th>
                            <th class="center">情感</th>
                            <th class="center">内容</th>
                            <th class="center">原作者</th>
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
                <div class="pagination pagination-centered" style="float: left;">
                    生成报告：<select style="width: 90px;">
                        <option>发表时间</option>
                        <option>发表时间</option>
                    </select>
                    开始 &nbsp;&nbsp;
								<input type="text" class="input-xlarge datepicker" id="Text1" value="">
                    结束 &nbsp;&nbsp;<input type="text" style="width: 100px;">
                    生成类型<select style="width: 80px;">
                        <option>HTML</option>
                        <option>PDF</option>
                        <option>Execl</option>
                    </select>
                    <button type="button" style="width: 90px; margin-bottom: 10px;">生成报告</button>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function check_all(obj, cName) {
            var checkboxs = document.getElementsByName(cName);
            for (var i = 0; i < checkboxs.length; i++) { checkboxs[i].checked = obj.checked; }
        }
        $(document).ready(function () {
            $(".btn-primary").live("click", function (e) {
                alert("1");
                PageClick(1);
            });
            PageClick(1);

        });
        PageClick = function (pageclickednumber) {
            var plancheckboxs = document.getElementsByName("platsource");
            var checkboxs = document.getElementsByName("keywordfamily");
            //平台信息
            var planidlist = "";
            for (var i = 0; i < plancheckboxs.length; i++) {
                if (plancheckboxs[i].checked == true) {
                    planidlist = planidlist + plancheckboxs[i].value + ",";
                }
            }
            //品牌ID列表
            var familyids = "";
            for (var y = 0; y < checkboxs.length; y++) {
                if (checkboxs[y].checked == true) {
                    familyids = familyids + checkboxs[y].value + ",";
                }
            }
            // sourceids, familyids,keyvalue, notkeyvalue,starttime, endtime, emotional, pageindex, pagesize)
            var starttime = $("#starttime").val();
            var endtime = $("#endtime").val();

            var emotional = 0;
            $.ajax({
                "type": "POST",
                "url": "/Ajax/GetDataPageListInfo.ashx",
                "data": { sourceids: planidlist, familyids: familyids, keyvalue: $("#txtcontain").val(), notkeyvalue: $("#txtnotcontain").val(), starttime: starttime, endtime: endtime, emotional: emotional, pageindex: pageclickednumber },
                beforeSend: function () { },
                success: function (data) {
                    try {
                        var json = JSON.parse(data);
                        var html = "";
                        $.each(json.data.items, function (index, item) {

                            html += '<tr data="' + item.dataid + '" datasource="' + item.datasource + '" dataKey="' + item.dataKey + '" emotionalvalue="' + item.emotionalvalue + '" datatitle="' + item.datatitle.toString() + '" sitename="' + item.sitename.toString() + '" datatime="' + item.datatime + '" >';
                            html += '<td>' + item.dataid + '</td>';
                            html += '<td>' + item.datasource + '</td>';
                            html += '<td>' + item.dataKey + '</td>';
                            html += '<td>' + item.emotionalvalue + '</td>';

                            html += '<td class="center">' + item.datatitle + '</td>';
                            html += '<td class="center">' + item.dataauthor + '</td>';
                            html += '<td class="center">' + item.dataforward + '</td>';
                            html += '<td class="center">' + item.datacomment + '</td>';
                            html += '<td class="center">' + item.sitename + '</td>';
                            html += '<td class="center">' + item.datatime + '</td>';

                        });
                        $("#tbodydata").empty().html(html);
                        console.info("1");
                        $('[rel="tooltip"],[data-rel="tooltip"]').tooltip({ "placement": "bottom", "width": "300px", delay: { show: 400, hide: 200 } });
                        console.info("2");
                        console.info(pageclickednumber);
                        console.info(json.data.count);
                        console.info(PageClick);
                        $("#pager").pager({ pagenumber: pageclickednumber, recordCount: json.data.count, pageSize: "30", buttonClickCallback: PageClick });
                        console.info("3");


                    } catch (e) { }
                },
                error: function () { }
            });
        }

    </script>

</asp:Content>
