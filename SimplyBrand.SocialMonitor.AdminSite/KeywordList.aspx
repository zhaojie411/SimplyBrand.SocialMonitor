<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KeywordList.aspx.cs" Inherits="SimplyBrand.SocialMonitor.AdminSite.KeywordList" %>
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
                    <h2><i class="icon-info-sign"></i>品牌列表</h2>
                </div>
                <div class="box-content">
                    <div class="dataTables_wrapper" role="grid">
                        <div class="row-fluid">
                            <div class="span6">
                                <div id="DataTables_Table_0_length" class="dataTables_length">
                                    <label>
                                        <select size="1" id="selpagesize" name="DataTables_Table_0_length" aria-controls="DataTables_Table_0">
                                            <option value="10" selected="selected">10</option>
                                            <option value="25">25</option>
                                            <option value="50">50</option>
                                        </select>
                                        条记录/每页</label>
                                </div>
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
                                    <th>品牌ID</th>
                                    <th>品牌名称</th>                                 
                                    <th>状态</th>
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
                            <label class="control-label" for="txtname">品牌名称</label>
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
       
             var userid = getUrlParam("q");
             var pageIndex = 1;
          
             if (userid.match("^\d+$")) {
                     notycommon(" <span style='color:red;'>参数有误</span>");  
                 }
             else
                {
                     PageClick(pageIndex, userid);                 
                     $("#selpagesize").live("change", function () {
                         PageClick(1, userid);
                     });
                     $("span[name=updatestatus]").live("click", function (e) {
                         var $this = $(this);
                         if (lock)
                             return;
                         $.ajax({
                             "type": "POST",
                             "url": "/Ajax/StopOrEnableKeywordFamily.ashx",
                             "data": { sysUserId: $this.parent().parent().attr("data"), q: userid },
                             beforeSend: function () { lock = true; },
                             success: function (data) {
                                 lock = false;
                                 try {                              
                                     data = JSON.parse(data);                                  
                                     if (data.issucc) {                                  
                                         if ($this.attr("class") == "label label-success") {
                                             notycommon("品牌：" + $this.parent().prev().html() + " 已停用");
                                             $this.parent().html('<span class="label label-important" name="updatestatus" style="cursor:pointer">停用</span>');
                                            } else {
                                             notycommon("品牌：" + $this.parent().prev().html() + " 已启动");                                      
                                             $this.parent().html('<span class="label label-success" name="updatestatus" style="cursor:pointer">启用</span>');
                                         }
                                         PageClick(pageIndex, userid);
                                     }
                                 } catch (e) { }
                             },
                             error: function () { }
                         });
                     });

                     $("#search").live("click", function (e) {
                         pageIndex = 1;
                         PageClick(pageIndex, userid);
                     });
                 //显示新建
                     $("#create").live("click", function (e) {
                         $("#platform").html("");
                         $("#updateMemberForm").attr("data", "");
                         $("#txtname").attr("value", "");                      
                         $("#htitle").html('<i class="icon-plus"></i><span>添加品牌名称</span>');
                         $("#txtname").removeAttr("disabled");                                      
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

                 //删除操作<a href="Ajax/DeleteUserKeyWord.ashx">Ajax/DeleteUserKeyWord.ashx</a>
                     $(".btn-danger").live("click", function (e) {
                         e.preventDefault();
                         var familyid = $(this).parent().parent().attr("data");
                         console.info(familyid);
                         $.ajax({
                             url: "Ajax/DeleteKeywordFamily.ashx",
                             type: "POST",
                             data: { familyid: familyid,},
                             beforeSend: function () { lock = true; },
                             success: function (data) {
                                 lock = false;
                                 try {
                                     data = JSON.parse(data);
                                     if (data.issucc) {
                                         notycommon("删除成功");
                                         PageClick(1, userid);
                                     }
                                     else {
                                         notycommon(data.errormsg);
                                     }
                                 } catch (e) { }
                             },
                             error: function () { }
                         });

                     });

                 //显示更新
                     $(".btn-info").live("click", function (e) {
                         e.preventDefault();                     
                         $("#updateMemberForm").attr("data", $(this).parent().parent().attr("data"));
                         $("#txtname").attr("value", $(this).parent().prev().prev().html());                 
                         $("#txtname").removeAttr("disabled");

                         $("#txtname").attr("value", $(this).parent().prev().prev().html());
                       
                         var cbbox = $(this).parent().prev().html();                     
                         if (cbbox.match("停用")) {
                             var html = "<label class=\"checkbox inline\"><div class=\"checker\"  ><span id=\"cb0\" class= ><input type=\"checkbox\" name=\"cbplatform\"  value=\"0\" style=\"opacity: 0;\" /></span></div>启用</label>";
                             html += "<label class=\"checkbox inline\"><div class=\"checker\"><span  id=\"cb1\" class=\"checked\"><input type=\"checkbox\" name=\"cbplatform\"   value=\"1\" style=\"opacity: 0;\" /></span></div>停用</label>";
                               $("#platform").html(html);
                         }
                         else {
                             var html = "<label class=\"checkbox inline\"><div class=\"checker\"  ><span  id=\"cb0\" class=\"checked\"><input type=\"checkbox\" name=\"cbplatform\"  value=\"0\" style=\"opacity: 0;\" /></span></div>启用</label>";
                             html += "<label class=\"checkbox inline\"><div class=\"checker\"><span id=\"cb1\" class= ><input type=\"checkbox\" name=\"cbplatform\"   value=\"1\" style=\"opacity: 0;\" /></span></div>停用</label>";
                             $("#platform").html(html);
                         }                                      

                         $('#updateMemberForm').modal('show');
                     });

                     $("#btnSave").live("click", function () {
                         var url = $("#updateMemberForm").attr("data") != "" ? "/Ajax/UpdateKeywordFamily.ashx" : "/Ajax/CreateKeywordFamily.ashx"
                         var username = $.trim($("#txtname").val());
                         var keystatus = 2;                      
                         if ($("#cb0").hasClass('checked')) {
                             keystatus = 1;
                         }
                      
                         if (username.length == 0) {
                             notycommon("请输入品牌名称");
                             return false;
                         }  
                         var sysuserid = $("#updateMemberForm").attr("data");
                         $.ajax({
                             url: url,
                             type: "POST",
                             data: { keyname: username, userid: userid, keystatus: keystatus, keyid: $("#updateMemberForm").attr("data") },
                             beforeSend: function () { lock = true; },
                             success: function (data) {
                                 lock = false;
                                 try {
                                     data = JSON.parse(data);
                                     if (data.issucc) {
                                         if (sysuserid == "") {
                                             notycommon("添加成功");
                                         } else {
                                             notycommon("更新成功");
                                         }
                                         PageClick(pageIndex,userid);
                                         $("#btnCancel").click();
                                     }
                                     else {
                                         notycommon(data.errormsg);
                                     }
                                 } catch (e) { }
                             },
                             error: function () { }
                         });
                     });
                     }
                     });

                     PageClick = function (pageclickednumber, userid) {          
                         $.ajax({
                             "type": "POST",
                             "url": "/Ajax/GetKeywordList.ashx",
                             "data": { pageindex: pageclickednumber, pagesize: $("#selpagesize").val(), name: $("#txtSearchName").val(), q: userid },
                             beforeSend: function () { },
                             success: function (data) {
                                 try {
                                     var json = JSON.parse(data);
                                     var html = "";
                                     if (json.issucc == false) {
                                         notycommon(" <span style='color:red;'>" + json.errormsg + "</span>");
                                     }
                                     else {
                                         $.each(json.data.items, function (index, item) {
                                             html += '<tr data="' + item.id + '" name="' + item.name + '" KeywordStatus="' + item.KeywordStatus.toString() + '">';
                                             html += '<td>' + item.id + '</td>';
                                             html += '<td>' + item.name + '</td>';
                                             html += '<td class="center">' + (item.KeywordStatus == 1 ? '<span class="label label-success" name="updatestatus" style="cursor:pointer">启用</span>' : '<span class="label label-important" name="updatestatus" style="cursor:pointer">停用</span>') + '</td>';
                                             html += '<td class="center"><a class="btn btn-success" href="KeywordListDetail.aspx?keyid=' + item.id + '"><i class="icon-zoom-in icon-white"></i>详细</a>&nbsp;&nbsp;&nbsp;&nbsp;';
                                             html += '<a class="btn btn-info" href="javascript:void(0)"><i class="icon-edit icon-white"></i>更新</a>&nbsp;&nbsp;&nbsp;&nbsp;';
                                             html += '<a class="btn btn-danger" href="#"><i class="icon-trash icon-white"></i> 删除</a></td></tr>';
                                         });
                                         $("#tbodydata").empty().html(html);
                                         $("#pager").pager({ pagenumber: pageclickednumber, recordCount: json.data.count, pageSize: $("#selpagesize").val(), buttonClickCallback: PageClick });

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
