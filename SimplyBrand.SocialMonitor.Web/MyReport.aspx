<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyReport.aspx.cs" Inherits="SimplyBrand.SocialMonitor.Web.MyReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <ul class="breadcrumb">
            <li>
                <a href="#" id="sb_myprofile">我的资料</a>
                <span class="divider">/</span>
            </li>
            <li>
                <a href="#" id="sb_myreport">我的报告</a>
            </li>
        </ul>
    </div>

    <div class="row-fluid sortable ui-sortable">
        <div class="box">
            <div class="box-header well" data-original-title>
                <h2>
                    <i class="icon-book"></i>
                    <span id="sb_internetwomar">网络口碑分析报告</span>
                </h2>
            </div>
            <div class="box-content">
                <ul class="nav nav-tabs" id="myTab">
                    <li class="active"><a href="#day" id="sb_dailyreport">日报</a></li>
                    <li><a href="#weekday" id="sb_weeklyreport">周报</a></li>
                    <li><a href="#month" id="sb_monthlyreport">月报</a></li>
                </ul>
                <div id="myTabContent" class="tab-content">
                    <div class="tab-pane active" id="day">
                        <p>
                            <span id="sb_selecttime"></span>：
                            <input type="text" class="datepicker" id="txtDay" value="">
                            <a id="download_day" style="margin-top: -9px; margin-left: 150px;" class="btn btn-primary"><i class="icon-download-alt"></i><span name="sb_download">下载</span></a>
                        </p>
                    </div>
                    <div class="tab-pane" id="weekday">
                        <p>
                            <span id="sb_selectweek"></span>:
                            <input type="text" class="datepicker" id="txtWeekday" value="">

                            <a id="download_weekday" style="margin-top: -9px; margin-left: 150px;" class="btn btn-primary"><i class="icon-download-alt"></i><span name="sb_download">下载</span></a>
                        </p>
                        <div class="week-picker hide"></div>
                    </div>
                    <div class="tab-pane" id="month">
                        <p>
                            <span id="sb_selectmonth"></span>:
                            <input type="text" class="datepicker" id="txtMonth" value="">
                            <a id="download_month" style="margin-top: -9px; margin-left: 150px;" class="btn btn-primary"><i class="icon-download-alt"></i><span name="sb_download">下载</span></a>
                        <p>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            getUserReport = function (reporttype, date) {
                $.ajax({
                    url: "/Ajax/GetUserReport.ashx",
                    type: "POST",
                    data: { reporttype: reporttype, date: date },
                    beforeSend: function () { },
                    success: function (data) {
                        console.info(data);
                    },
                    error: function () { }
                });
            }
            $("#download_day").click(function (e) {
                if ($("#txtDay").val().length == 0) {
                    return false;
                }
                getUserReport(1, $("#txtDay").val());
            });
            $("#download_weekday").click(function (e) {
                if ($("#txtWeekday").val().length == 0) {
                    return false;
                }
                getUserReport(2, $("#txtWeekday").val());
            });
            $("#download_month").click(function (e) {
                if ($("#txtMonth").val().length == 0) {
                    return false;
                }
                getUserReport(3, $("#txtMonth").val());
            });


        });
    </script>
</asp:Content>
