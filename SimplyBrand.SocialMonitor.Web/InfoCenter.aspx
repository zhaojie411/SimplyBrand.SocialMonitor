<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InfoCenter.aspx.cs" Inherits="SimplyBrand.SocialMonitor.Web.InfoCenter" %>

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
                <a href="#" id="sb_infocenter">信息中心</a>
            </li>
        </ul>
    </div>

    <div class="row-fluid sortable">
        <div class="box span12">
            <div class="box-header well" data-original-title>
                <h2><i class="icon-info-sign"></i><span id="sb_notice">通知</span></h2>

            </div>
            <div class="box-content">
                <p>- 2013年6月份的月度报告已经生成，请点击这里查看详情</p>
                <p>- 2013年7月4日20:00-24:00系统升级维护，届时部分功能将受影响，给您带来不便，深表歉意。</p>
            </div>
        </div>
        <!--/span-->

    </div>

    <div class="row-fluid sortable">
        <div class="box span12">
            <div class="box-header well" data-original-title>
                <h2 ><i class="icon-info-sign"></i><span id="sb_myinformation">我的信息</span></h2>
            </div>
            <div class="box-content">
                <div class="span3">
                    <span>监测站点数：<span class="red">2192</span> 个</span>
                </div>
                <div class="span3">
                    <span>近30天累计信息数：<span class="red">20,312</span> 条</span>
                </div>
                <div class="span6">
                    <span>今日新增信息数：<span class="red">234</span> 条，其中负面信息 <span class="red">21</span> 条【点击查看】</span>
                </div>

            </div>
            <br />
            <div class="box-content">
                <div class="span3">
                    <span>监测对象数： <span class="red">5</span> 个</span>
                </div>
                <div class="span3">
                    <span></span>
                </div>
                <div class="span6">
                    <span>历史报告数：<span class="red">5</span> 份</span>
                </div>

            </div>
        </div>


    </div>
</asp:Content>
