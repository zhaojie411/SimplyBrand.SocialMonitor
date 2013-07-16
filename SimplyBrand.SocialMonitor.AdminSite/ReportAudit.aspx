<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportAudit.aspx.cs" Inherits="SimplyBrand.SocialMonitor.AdminSite.ReportAudit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <ul class="breadcrumb">
            <li>
                <a href="#" id="sb_myprofile">主页</a>
                <span class="divider">/</span>
            </li>
            <li>
                <a href="#">报表审核</a>
            </li>
        </ul>
    </div>

    <div class="row-fluid sortable ui-sortable">
        <div class="box">
            <div class="box-header well" data-original-title>
                <h2>
                    <i class="icon-book"></i>
                    <span>网络口碑分析报告</span>
                </h2>
            </div>
            <div class="box-content">
                <ul class="nav nav-tabs" id="myTab">
                    <li class="active"><a href="#day" data="1">日报</a></li>
                    <li><a href="#weekday" data="2">周报</a></li>
                    <li><a href="#month" data="3">月报</a></li>
                </ul>
                <div id="myTabContent" class="tab-content">
                    <%--<div class="tab-pane active" id="day">
                    </div>
                    <div class="tab-pane" id="weekday">
                    </div>
                    <div class="tab-pane" id="month">
                    </div>--%>
                    <table class="table table-striped table-bordered bootstrap-datatable datatable">
                        <thead>
                            <tr>
                                <th>文件名</th>
                                <th>创建时间</th>
                                <th>报告类型</th>
                                <th>是否是系统生成</th>
                                <th>状态</th>
                                <th>开始时间</th>
                                <th>结束时间</th>
                                <th>操作</th>
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
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var pageindex = 1;
        var reporttype = 1;
        var pagesize = 20;
        var lock = false;
        getReportType = function (reporttype) {
            if (reporttype == 1) {
                return "日报";
            }
            if (reporttype == 2) {
                return "周报";
            }
            if (reporttype == 3) {
                return "月报";
            }
        }
        getReportStatus = function (reportstatus) {
            if (reportstatus == 1) {
                return "未审核"
            }
            if (reportstatus == 2) {
                return "审核通过"
            }
            if (reportstatus == 3) {
                return "未审核通过"
            }
        }
        UpdateReportStatus = function (userreportid, reportstatus) {
            if (lock)
                return false;
            $.ajax({
                "type": "POST",
                "url": "/Ajax/UpdateReportStatus.ashx",
                "data": { userreportid: userreportid, reportstatus: reportstatus },
                beforeSend: function () { lock = true; },
                success: function (data) {
                    lock = false;
                    try {
                        data = JSON.parse(data);
                        if (data.issucc) {
                            if (reportstatus == 2) {
                                $("tr[data=" + userreportid + "]").find("td").eq(4).html("审核通过");
                            }
                            else if (reportstatus == 3) {
                                $("tr[data=" + userreportid + "]").find("td").eq(4).html("未审核通过");
                            }
                        }
                    } catch (e) { }
                },
                error: function () { }
            });
        }
        PageClick = function (pageclickednumber) {
            $.ajax({
                "type": "POST",
                "url": "/Ajax/GetUserReport.ashx",
                "data": { pageindex: pageclickednumber, pagesize: pagesize, reporttype: reporttype },
                beforeSend: function () { },
                success: function (data) {
                    try {
                        var json = JSON.parse(data);
                        var html = "";
                        $.each(json.data.items, function (index, item) {
                            html += '<tr data="' + item.id + '" >';
                            html += '<td class="center">' + item.filename + '</td>';
                            html += '<td class="center">' + item.createdate + '</td>';
                            html += '<td class="center">' + getReportType(item.reporttype) + '</td>';
                            html += '<td class="center">' + (item.issysgen ? "是" : "否") + '</td>';
                            html += '<td class="center">' + getReportStatus(item.reportstatus) + '</td>';
                            html += '<td class="center">' + item.starttime + '</td>';
                            html += '<td class="center">' + item.endtime + '</td>';
                            html += '<td class="center">';
                            html += '<a class="btn btn-info" target="_blank" href="' + (item.filepath + "/" + item.filename) + '"><i class="icon-edit icon-white"></i>下载</a> ';
                            html += '<a class="btn" title="通过审核"><i class="icon icon-color icon-check"></i></a> ';
                            html += '<a class="btn" title="未通过审核"><i class="icon icon-color icon-close"></i></a>';
                            html += '</td></tr>';
                        });
                        $("#tbodydata").empty().html(html);
                        $("#pager").pager({ pagenumber: pageclickednumber, recordCount: json.data.count, pageSize: pagesize, buttonClickCallback: PageClick });
                    } catch (e) { }
                },
                error: function () { }
            });
        }
        $(document).ready(function () {

            PageClick(pageindex);
            $('#myTab a').click(function (e) {
                $(this).tab('show');
                pageindex = 1;
                reporttype = $(this).attr("data");
                PageClick(pageindex);
            });
            //通过审核
            $(".icon-check").live("click", function (e) {
                UpdateReportStatus($(this).parent().parent().parent().attr("data"), 2);
            });
            //未通过审核
            $(".icon-close").live("click", function (e) {
                UpdateReportStatus($(this).parent().parent().parent().attr("data"), 3);
            });

        });


    </script>
</asp:Content>
