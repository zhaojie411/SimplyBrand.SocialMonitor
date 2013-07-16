<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KeywordListDetail.aspx.cs" Inherits="SimplyBrand.SocialMonitor.AdminSite.KeywordListDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div>
        <ul class="breadcrumb">
            <li>
                <a href="#">主页</a> <span class="divider">/</span>
            </li>
            <li>
                <a href="#">用户管理</a>
            </li>
        </ul>
        <div class="row-fluid">
            <div class="box span12">
                <div class="box-header well">
                    <h2><i class="icon-info-sign"></i>关键词列表</h2>
                </div>
                <div class="box-content">
                    <div class="dataTables_wrapper" role="grid">
                        <div class="row-fluid"> 
                             <div class="span6">
                                 </div>                          
                            <div class="span6">
                                <div class="dataTables_filter" id="DataTables_Table_0_filter">
                                    <label>
                                        <input type="text" id="txtSearchName" aria-controls="DataTables_Table_0" />
                                        <a href="javascript:void(0)" class="btn btn-small" id="search"><i class="icon-search"></i>搜索</a>
                                        <a href="javascript:void(0)" class="btn btn-small" id="create"><i class="icon-plus"></i>新建</a>
                                    </label>
                                </div>
                            </div>
                        </div>
                        <table class="table table-striped table-bordered bootstrap-datatable datatable">
                            <thead>
                                <tr>
                                    <th>关键词ID</th>
                                    <th>关键词名称</th>                                 
                                    <th>关键词状态</th>
                                    <th>时间</th>
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
    </div>
    <div class="sortable ui-sortable modal hide fade" id="updateMemberForm" style="display: none; ">
        <div class="box span12">
            <div class="box-header well" data-original-title="">
                <h2 id="htitle"></h2>
                <div class="box-icon">
                    <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                    <a class="btn" data-dismiss="modal"><i class="icon-remove"></i></a>
                </div>
            </div>

            <div class="box-content">
                <div class="form-horizontal">
                    <fieldset>
                        <div class="control-group">
                            <label class="control-label" for="txtname">关键词名称</label>
                            <div class="controls">
                                <div class="input-prepend">
                                    <input id="txtname" size="20" maxlength="20" type="text" disabled="disabled" />
                                </div>
                            </div>
                        </div>                   
                     <div class="control-group">
                            <label class="control-label">状态</label>
                            <div class="controls" id="platform">  
                        </div>
                       </div>
                        <div class="form-actions">
                            <a class="btn btn-primary" id="btnSave">保存</a>
                            <a class="btn" data-dismiss="modal" id="btnCancel">取消</a>
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>   
     <script type="text/javascript">
         function getUrlParam(name) {
             var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
             var r = window.location.search.substr(1).match(reg);  //匹配目标参数
             if (r != null) return unescape(r[2]); return null; //返回参数值
         }
         var lock = false;
         $(document).ready(function () {
             var familyid = getUrlParam("keyid");            
             if (familyid.match("^\d+$")) {
                 notycommon(" <span style='color:red;'>参数有误</span>");
             }
             else {
                 PageClick(familyid);
                 $("#search").live("click", function (e) {                    
                     PageClick(familyid);
                 });
                 //显示新建
                 $("#create").live("click", function (e) {
                     $("#platform").html("");
                     $("#updateMemberForm").attr("data", "");
                     $("#txtname").attr("value", "");
                     $("#htitle").html('<i class="icon-plus"></i><span>添加关键词</span>');
                     $("#txtname").removeAttr("disabled");
                     var html = "<label class=\"checkbox inline\"><div class=\"checker\"  ><span id=\"cb0\" class=\"checked\" ><input type=\"checkbox\" name=\"cbplatform\"  value=\"0\" style=\"opacity: 0;\" /></span></div>关键词</label>";
                     html += "<label class=\"checkbox inline\"><div class=\"checker\"><span  id=\"cb1\" class= ><input type=\"checkbox\" name=\"cbplatform\"   value=\"1\" style=\"opacity: 0;\" /></span></div>静止关键词</label>";
                     $("#platform").html(html);
                     $('#updateMemberForm').modal('show');
                 });
                 $("input[name=cbplatform]").live("click", function (e) {
                     e.preventDefault();
                     if (!$(this).parent().hasClass("checked")) {
                         if ($(this).parent().context.value == 1) {
                             $("#cb1").addClass("checked");
                             $("#cb0").removeClass("checked");
                         }
                         else {
                             $("#cb0").addClass("checked");
                             $("#cb1").removeClass("checked");
                         }
                     }
                 });
                 //显示更新
                 $(".btn-info").live("click", function (e) {
                     e.preventDefault();
                     $("#updateMemberForm").attr("data", $(this).parent().parent().attr("data"));                   
                     $("#txtname").removeAttr("disabled");
                     $("#txtname").attr("value", $(this).parent().prev().prev().prev().html());
                     var cbbox = $(this).parent().prev().prev().html();
                   
                     if (cbbox.match("禁用关键词")) {                       
                         var html = "<label class=\"checkbox inline\"><div class=\"checker\"  ><span id=\"cb0\" class= ><input type=\"checkbox\" name=\"cbplatform\"  value=\"0\" style=\"opacity: 0;\" /></span></div>关键词</label>";
                         html += "<label class=\"checkbox inline\"><div class=\"checker\"><span  id=\"cb1\" class=\"checked\"><input type=\"checkbox\" name=\"cbplatform\"   value=\"1\" style=\"opacity: 0;\" /></span></div>静止关键词</label>";
                         $("#platform").html(html);
                     }
                     else {                       
                         var html = "<label class=\"checkbox inline\"><div class=\"checker\"  ><span  id=\"cb0\" class=\"checked\"><input type=\"checkbox\" name=\"cbplatform\"  value=\"0\" style=\"opacity: 0;\" /></span></div>关键词</label>";
                         html += "<label class=\"checkbox inline\"><div class=\"checker\"><span id=\"cb1\" class= ><input type=\"checkbox\" name=\"cbplatform\"   value=\"1\" style=\"opacity: 0;\" /></span></div>静止关键词</label>";
                         $("#platform").html(html);
                     }

                     $('#updateMemberForm').modal('show');
                 });
                 //删除操作<a href="Ajax/DeleteUserKeyWord.ashx">Ajax/DeleteUserKeyWord.ashx</a>
                 $(".btn-danger").live("click", function (e) {
                     e.preventDefault();
                     var keyid = $(this).parent().parent().attr("data");
                     console.info(keyid);
                     $.ajax({
                         url: "Ajax/DeleteUserKeyWord.ashx",
                         type: "POST",
                         data: { familyid: familyid, keyid: keyid },
                         beforeSend: function () { lock = true; },
                         success: function (data) {
                             lock = false;
                             try {
                                 data = JSON.parse(data);
                                 if (data.issucc) {                                    
                                     notycommon("删除成功");                                     
                                     PageClick(familyid);                               
                                 }
                                 else {
                                     notycommon(data.errormsg);
                                 }
                             } catch (e) { }
                         },
                         error: function () { }
                     });

                 });
                 $("#btnSave").live("click", function () {
                     var url = $("#updateMemberForm").attr("data") != "" ? "/Ajax/UpdateUserKeyword.ashx" : "/Ajax/CreateUserKeyword.ashx"
                     var username = $.trim($("#txtname").val());
                     var keystatus = 0;
                     if ($("#cb0").hasClass('checked')) {
                         keystatus = 1;
                     }
                     if (username.length == 0) {
                         notycommon("请输入关键词名称");
                         return false;
                     }                
                
                     console.info(familyid); 

                     $.ajax({
                         url: url,
                         type: "POST",
                         data: { keyname: username, familyid: familyid, isforbid: keystatus, keyid: $("#updateMemberForm").attr("data") },
                         beforeSend: function () { lock = true; },
                         success: function (data) {
                             lock = false;
                             try {
                                 data = JSON.parse(data);
                                 if (data.issucc) {                                    
                                     if ($("#updateMemberForm").attr("data") == "") {
                                         notycommon("添加成功");

                                     } else {
                                         notycommon("更新成功");
                                     }
                                     PageClick(familyid);
                                     $("#btnCancel").click();
                                 }
                                 else {
                                     console.info(2);
                                     notycommon(data.errormsg);
                                 }
                             } catch (e) { }
                         },
                         error: function () { }
                     });
                 });
             }
         });

         PageClick = function (userid) {
             $.ajax({
                 "type": "POST",
                 "url": "/Ajax/GetUserKeyword.ashx",
                 "data": { keyname: $("#txtSearchName").val(), familyid: userid },
                 beforeSend: function () { },
                 success: function (data) {
                     try {
                         var json = JSON.parse(data);
                         var html = "";
                         if (json.issucc == false) {
                             notycommon(" <span style='color:red;'>" + json.errormsg + "</span>");
                         }
                         else {
                             $.each(json.data, function (index, item) {
                                 html += '<tr data="' + item.id + '" name="' + item.name + '" CreateTime="' + item.CreateTime.toString() + '" IsForbid="' + item.IsForbid + '">';
                                 html += '<td>' + item.id + '</td>';
                                 html += '<td>' + item.name + '</td>';
                                 html += '<td class="center">' + (item.IsForbid == false ? '<span class="label label-important" name="updatestatus" style="cursor:pointer">禁用关键词</span>' : '<span class="label label-success" name="updatestatus" style="cursor:pointer">关键词</span>') + '</td>';
                                 html += '<td>' + item.CreateTime + '</td>';
                                 html += '<td class="center"><a class="btn btn-info" href="javascript:void(0)">';
                                 html += '<i class="icon-edit icon-white"></i>更新</a> &nbsp;&nbsp;&nbsp;';
                                 html += '<a class="btn btn-danger" href="#"><i class="icon-trash icon-white"></i> 删除</a></td></tr>';
                             });
                             $("#tbodydata").empty().html(html);
                         }
                     } catch (e) { }
                 },
                 error: function () {
                     notycommon(" <span style='color:red;'>请求错误...</span>");
                 }
             });
         };
     </script>

</asp:Content>
