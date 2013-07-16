<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TodayDynamic.aspx.cs" Inherits="SimplyBrand.SocialMonitor.Web.TodayDynamic" %>

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
                <a href="#" id="sb_todaydynamic">今日动态</a>
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
                    <input type="checkbox" id="cbAllEmotional" value="">全部 
                        <input type="checkbox" name="emotionvalues" value="1">正面&nbsp;&nbsp;
                        <input type="checkbox" name="emotionvalues" value="0">中性&nbsp;&nbsp;
                        <input type="checkbox" name="emotionvalues" value="-1">负面&nbsp;&nbsp;
                </p>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <button type="submit" class="btn btn-primary" id="sb_search">搜索</button>

            </div>
        </div>

    </div>

    <div class="row-fluid sortable">
        <div id="container" class="box span8" style="min-height: 400px;"></div>
        <div class="box span4">
            <div class="box-header well" data-original-title="" style="margin-top: 0px;">
                <h2><span id="sb_sentimentanalysis">情感分析</span></h2>

            </div>
            <div class="box-content">
                <div id="piechart" style="height: 150px;">
                </div>
            </div>

        </div>
        <div class="circle_Corner box span4" id="key_Words">
            <div class="box-header well" data-original-title="" style="margin-top: 0px;">
                <h2><span id="sb_keyword">关键词</span></h2>
            </div>

            <div class="box-content">
                <div id="containers" class="jqcloud" style="height: 160px;">
                </div>
            </div>
        </div>
    </div>




    <link href="Themes/Default/js/jqcloud-1.0.0.css" rel="stylesheet" />
    <script src="Themes/Default/js/jqcloud-1.0.0.js"></script>
    <script src="Themes/Default/js/excanvas.js"></script>
    <script src="Themes/Default/js/jquery.flot.min.js"></script>
    <script src="Themes/Default/js/jquery.flot.pie.min.js"></script>
    <script src="Themes/Default/js/jquery.flot.stack.js"></script>
    <script src="Themes/Default/js/jquery.flot.resize.min.js"></script>
    <script src="Themes/Default/js/basedata.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            drawPieChart("", "", false);
            drawLineChart("", "", false, "");
            drawCloudWord();
            $("#sb_search").click(function () {
                drawPieChart(getcbkeywordfamily(), getcbplatsource(), false);
                drawLineChart(getcbkeywordfamily(), getcbplatsource(), false, getcbemotionvalues());
                //drawCloudWord();
            });
        });
    </script>

</asp:Content>
