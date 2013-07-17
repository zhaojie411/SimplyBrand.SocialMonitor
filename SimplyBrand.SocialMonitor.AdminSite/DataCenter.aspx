<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DataCenter.aspx.cs" Inherits="SimplyBrand.SocialMonitor.AdminSite.DataCenter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <!-- content starts -->
        <div>
            <ul class="breadcrumb">
                <li>
                    <a href="#">数据中心</a>                  
                </li>             
            </ul>
        </div>
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-content">                
                    <p>
                        <b>情&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;感：</b>
                        <input type="checkbox" id="cbAllEmotional">全部 
                        <input type="checkbox" value="1" name="emotionvalues">正面&nbsp;&nbsp;
                        <input type="checkbox"  value="0"  name="emotionvalues">中性&nbsp;&nbsp;
                        <input type="checkbox" value="-1" name="emotionvalues">负面&nbsp;&nbsp;
                    </p>
                    <p><b>在结果中搜索：</b>
                    包含：<input type="text" id="txtcontain">
                    排除：<input type="text" id="txtnotcontain">
              <button type="submit" class="btn btn-primary" style="margin-bottom: 10px;">确定</button></p>
               <ul class="nav nav-tabs" id="myTab">
                    <li class="active"><a href="#day" data="0">全部(<span id="allcount" ></span>)</a></li>
                    <li><a href="#weibo" data="1">微博(<span id="weibocount"></span>)</a></li>
                    <li><a href="#blog" data="2">博客(<span id="blogcount"></span>)</a></li>
                    <li><a href="#lunt" data="3">论坛(<span id="luntcount"></span>)</a></li>
                    <li><a href="#news" data="4">新闻(<span id="newscount"></span>)</a></li>
                </ul>                 
                    <table class="table table-bordered table-striped table-condensed">
                        <thead>
                            <tr>
                                <th class="center">序号</th>
                                <th class="center">来源</th>                             
                                <th class="center">情感</th>
                                <th class="center">内容</th>
                                <th class="center">作者</th>
                                <th class="center">转发/浏览量</th>
                                <th class="center">评论/回复量</th>
                                <th class="center">网站名称</th>
                                <th class="center">时间</th>  
                                <th class="center">操作</th>
                            </tr>
                        </thead>
                        <tbody role="alert" aria-live="polite" aria-relevant="all" id="tbodydata" >                           
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
    <script src="Themes/Default/js/jquery.simple.pager.js"></script>   
    <script type="text/javascript"> 
        var pageindex = 1;
        var Getdatatype = 0;
        function check_all(obj, cName) {
            var checkboxs = document.getElementsByName(cName);
            for (var i = 0; i < checkboxs.length; i++) { checkboxs[i].checked = obj.checked; }
        }
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
        function change_emotional(obj,objvalue) {
            switch (obj) {
                case -1:
                    return "<a class=\"btn updatemotional\" href=\"javascript:MotionalClick(" + objvalue + ",1)\"> 正</a> <a class=\"btn updatemotional\" href=\"javascript:MotionalClick(" + objvalue + ",0)\"> 中</a>";
                    break;
                case 0:
                    return "<a class=\"btn updatemotional\" href=\"javascript:MotionalClick(" + objvalue + ",1)\"> 正</a> <a class=\"btn updatemotional\" href=\"javascript:MotionalClick(" + objvalue + ",-1)\"> 负</a>";
                    break;
                    break;
                case 1:
                    return "<a class=\"btn updatemotional\" href=\"javascript:MotionalClick(" + objvalue + ",0)\"> 中</a> <a class=\"btn updatemotional\" href=\"javascript:MotionalClick(" + objvalue + ",-1)\"> 负</a>";
                    break;
                default:
                    return "<a class=\"btn updatemotional\" href=\"javascript:MotionalClick(" + objvalue + ",1)\"> 正</a> <a class=\"btn updatemotional\" href=\"javascript:MotionalClick(" + objvalue + ",0)\"> 中</a> <a class=\"btn updatemotional\" href=\"javascript:MotionalClick(" + objvalue + ",-1)\"> 负</a>"; break;

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
        var keyid="0";
        $(document).ready(function () {      
            $(".btn-primary").live("click", function (e) {
                console.info($("#txtcontain").val());
                console.info($("#txtnotcontain").val());
                pageindex = 1;
                PageClick(pageindex);
             
            }); 

            $('.updatemotional').live("click", function (e) {          
         
                PageClick(1);
            });

            $('#myTab a').live("click", function (e) {
                $(this).tab('show');        
                Getdatatype = $(this).attr("data");
                pageindex = 1;
                PageClick(pageindex);
            });

            PageClick(1);
        });
        MotionalClick = function (objid,value)
        {
            $.ajax({
                "type": "POST",
                "url": "/Ajax/UpdateDataEmotional.ashx",
                "data": { dataid: objid, motionalvalue: value},
                beforeSend: function () { },
                success: function (data) {
                    try {                      
                       
                    } catch (e) { }
                },
                error: function () { }
            });

        }
        DeleteClick = function (objid) {
            $.ajax({
                "type": "POST",
                "url": "/Ajax/DeleteDataInfo.ashx",
                "data": { dataid: objid},
                beforeSend: function () { },
                success: function (data) {
                    try {

                    } catch (e) { }
                },
                error: function () { }
            });

        }
        PageClick = function (pageclickednumber) {                     
            var emotionalcheckboxs = document.getElementsByName("emotionvalues");         
            //舆情分析
            var emotionals = "";
            for (var z = 0; z < emotionalcheckboxs.length; z++) {
                if (emotionalcheckboxs[z].checked == true) {
                    emotionals = emotionals + emotionalcheckboxs[z].value + ",";
                }
            }  
            var starttime = $("#starttime").val();
            var endtime = $("#endtime").val(); 
            var emotional = 0;
            $.ajax({
                "type": "POST",
                "url": "/Ajax/GetDataPageListInfo.ashx",
                "data": { sourceids: Getdatatype, keyvalue: $("#txtcontain").val(), notkeyvalue: $("#txtnotcontain").val(), starttime: starttime, endtime: endtime, emotional: emotionals, pageindex: pageclickednumber },
                beforeSend: function () { },
                success: function (data) {
                    try {
                        var json = JSON.parse(data);
                        var html = "";                      
                        console.info(json);
                        $.each(json.data.items, function (index, item) {
                            html += '<tr data="' + item.dataid + '" datasource="' + item.datasourceid +  '" emotionalvalue="' + item.emotionalvalue + '" datatitle="' + item.datatitle.toString() + '" sitename="' + item.sitename.toString() + '" datatime="' + item.datatime + '" >';
                            html += '<td class="center">' + item.dataid + '</td>';
                            html += '<td class="center">' +GetplatType(item.datasourceid) + '</td>';                         
                            html += '<td class="center">' + change_txt(item.emotionalvalue) + '</td>';
                            html += '<td class="center" title="' + item.datatitle + '">' + substringlength(item.datatitle, 15) + '</td>';
                            html += '<td class="center" title="' + item.dataauthor + '">' + substringlength(item.dataauthor, 6) + '</td>';
                            html += '<td class="center">' + item.dataforward + '</td>';
                            html += '<td class="center">' + item.datacomment + '</td>';
                            html += '<td class="center">' + item.sitename + '</td>';
                            html += '<td class="center">' + item.datatime + '</td>';
                            html += '<td class="center">' + change_emotional(item.emotionalvalue, item.dataid);
                            html += '<a class="btn btn-danger" href="javascript:DeleteClick("' + item.dataid + '")"><i class="icon-trash icon-white"></i> 删除</a></td></tr>';
                        });
                        $("#tbodydata").empty().html(html);                   
                        $('[rel="tooltip"],[data-rel="tooltip"]').tooltip({ "placement": "bottom", "width": "300px", delay: { show: 400, hide: 200 } });                 
                        $("#pager").pager({ pagenumber: pageclickednumber, recordCount: json.data.count, pageSize: "30", buttonClickCallback: PageClick });                      
                         $("#allcount").html(json.data.count);
                        $("#weibocount").html(json.Weibocount);
                        $("#newscount").html(json.Newscount);
                        $("#blogcount").html(json.Blogcount);
                        $("#luntcount").html(json.Forumcount);                      
                    } catch (e) { }
                },
                error: function () { }
            });
        }

    </script>   
         
</asp:Content>
