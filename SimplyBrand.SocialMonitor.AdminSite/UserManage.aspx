<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserManage.aspx.cs" Inherits="SimplyBrand.SocialMonitor.AdminSite.UserManage" %>

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
                    <h2><i class="icon-info-sign"></i>用户列表</h2>
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
                                    <th>用户ID</th>
                                    <th>用户名</th>
                                    <th>真实姓名</th>
                                    <th>创建时间</th>
                                    <th>截止时间</th>
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
    <div class="sortable ui-sortable modal hide fade" id="updateMemberForm" style="display: none;">
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
                            <label class="control-label" for="txtname">用户名</label>
                            <div class="controls">
                                <div class="input-prepend">
                                    <input id="txtname" size="20" maxlength="20" type="text" disabled="disabled" />
                                </div>

                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="txtname">真实姓名</label>
                            <div class="controls">
                                <div class="input-prepend">
                                    <input id="txtrealname" name="txtrealname" size="20" maxlength="20" type="text" />
                                </div>
                            </div>
                        </div>
                        <%-- <div class="control-group">
                            <label class="control-label" for="txtname">用户邮箱</label>
                            <div class="controls">
                                <div class="input-prepend">
                                    <input id="txtmail" name="txtmail"  size="20" maxlength="20" type="text" />
                                </div>
                            </div>
                        </div>--%>
                        <div class="control-group">
                            <label class="control-label" for="txtpassword">密码</label>
                            <div class="controls">
                                <div class="input-append">
                                    <input id="txtpassword" name="txtpassword" size="32" maxlength="32" type="password" />
                                </div>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="txtpassword">密码确认</label>
                            <div class="controls">
                                <div class="input-append">
                                    <input id="txtpasswordcm" name="txtpasswordcm" size="32" maxlength="32" type="password" />
                                </div>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="txtpassword">截止时间</label>
                            <div class="controls">
                                <div class="input-append">
                                    <input id="txtenddate" class="" type="text" name="txtenddate" />
                                </div>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label">功能模块</label>
                            <div class="controls" id="permission">
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label">平台</label>
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
    </div>
    <script type="text/javascript">
        var lock = false;
        var pageIndex = 1;

        InitData = function () {
            //$.get("/Ajax/GetRole.ashx?r=" + new Date().getTime(), function (data) {
            //    try {
            //        data = JSON.parse(data);
            //        var html = '<option value="">请选择</option>';
            //        $.each(data.data, function (index, item) {
            //            html += '<option value="' + item.id + '">' + item.desc + '</option>';
            //        });
            //        $("#selRoles").empty().html(html);
            //    } catch (e) { }

            //});
            $.get("/Ajax/GetPlatform.ashx?r=" + new Date().getTime(), function (data) {
                try {
                    data = JSON.parse(data);
                    var html = "";
                    $.each(data.data, function (index, item) {
                        html += '<label class="checkbox inline"><div class="checker"><span class="">';
                        html += '<input type="checkbox"  name="cbplatform"  id="cb' + item.id + '" value="' + item.id + '" style="opacity: 0;" /></span></div>' + item.name + '</label>';
                    });
                    $("#platform").empty().html(html);
                } catch (e) { }
            });
            $.get("/Ajax/GetPermission.ashx", function (data) {
                try {
                    data = JSON.parse(data);
                    var html = "";
                    $.each(data.data, function (index, item) {
                        html += '<label class="checkbox inline"><div class="checker"><span class="">';
                        html += '<input type="checkbox" name="cbpermission"  id="cb_permission_' + item.id + '" value="' + item.id + '" style="opacity: 0;" /></span></div>' + item.desc + '</label>';
                    });
                    $("#permission").empty().html(html);
                } catch (e) { }
            });
        };
        setContact = function (contact) {
            var html = '<i class="icon-info-sign" data-rel="tooltip" data-original-title="{0}" style="cursor: pointer;"></i>';
            var formathtml = "";
            $.each(contact, function (index, item) {
                formathtml += "手机：" + item.ContactUserTel + "</br> 邮箱：" + item.ContactUserEmail + "</br>";
            });
            if (contact.length > 0)
                return '<i class="icon-info-sign" data-rel="tooltip" data-original-title="' + formathtml + '" style="cursor: pointer;"></i>';
            return "";

        };
        PageClick = function (pageclickednumber) {
            $.ajax({
                "type": "POST",
                "url": "/Ajax/GetSysUserListPage.ashx",
                "data": { pageindex: pageclickednumber, pagesize: $("#selpagesize").val(), name: $("#txtSearchName").val() },
                beforeSend: function () { },
                success: function (data) {
                    try {
                        var json = JSON.parse(data);
                        var html = "";
                        $.each(json.data.items, function (index, item) {

                            html += '<tr data="' + item.id + '" pwd="' + item.password + '" name="' + item.name + '" realname="' + item.realname + '" permission="' + item.permissions.toString() + '" platform="' + item.platformids.toString() + '" createdate="' + item.createdate + '" enddate="' + item.enddate + '">';
                            html += '<td>' + item.id + '</td>';
                            html += '<td>' + item.name + setContact(item.contacts) + '</td>';

                            html += '<td>' + item.realname + '</td>';
                            html += '<td class="center">' + item.createdate + '</td>';
                            html += '<td class="center">' + item.enddate + '</td>';
                            html += '<td class="center">' + (item.status == 1 ? '<span class="label label-success" name="updatestatus" style="cursor:pointer">启用</span>' : '<span class="label label-important" name="updatestatus" style="cursor:pointer">停用</span>') + '</td>';
                            html += '<td class="center"><a class="btn" href="KeywordList.aspx?q=' + item.id + '">设置关键词</a> <a class="btn btn-info" href="javascript:void(0)">';
                            html += '<i class="icon-edit icon-white"></i>更新</a></td></tr>';

                        });
                        $("#tbodydata").empty().html(html);
                        $('[rel="tooltip"],[data-rel="tooltip"]').tooltip({ "placement": "bottom", "width": "300px", delay: { show: 400, hide: 200 } });
                        $("#pager").pager({ pagenumber: pageclickednumber, recordCount: json.data.count, pageSize: $("#selpagesize").val(), buttonClickCallback: PageClick });
                    } catch (e) { }
                },
                error: function () { }
            });
        }
        $(document).ready(function () {

            PageClick(pageIndex);
            InitData();
            $("#txtenddate").datepicker({ changeMonth: true, changeYear: true, dateFormat: 'yy/mm/dd' }).focus(function () {
                $("#ui-datepicker-div").css("z-index", "1051");
            });
            $("#search").live("click", function (e) {
                pageIndex = 1;
                PageClick(pageIndex);
            });

            $("span[name=updatestatus]").live("click", function (e) {
                var $this = $(this);
                if (lock)
                    return;
                $.ajax({
                    "type": "POST",
                    "url": "/Ajax/StopOrEnableSysUser.ashx",
                    "data": { sysUserId: $this.parent().parent().attr("data") },
                    beforeSend: function () { lock = true; },
                    success: function (data) {
                        lock = false;
                        try {
                            data = JSON.parse(data);
                            if (data.issucc) {
                                if ($this.attr("class") == "label label-success") {
                                    notycommon("用户：" + $this.parent().prev().prev().html() + " 已停用");
                                    $this.parent().html('<span class="label label-important" name="updatestatus" style="cursor:pointer">停用</span>');

                                } else {
                                    notycommon("用户：" + $this.parent().prev().prev().html() + " 已启动");
                                    $this.parent().html('<span class="label label-success" name="updatestatus" style="cursor:pointer">启用</span>');

                                }
                            }
                        } catch (e) { }
                    },
                    error: function () { }
                });
            });

            //显示新建
            $("#create").live("click", function (e) {
                $("#updateMemberForm").attr("data", "");
                $("#txtname").attr("value", "");
                $("#txtrealname").attr("value", "");
                $("#txtpassword").attr("value", "");
                $("#txtpasswordcm").attr("value", "");
                $("#htitle").html('<i class="icon-plus"></i><span>添加用户</span>');
                $("#txtname").removeAttr("disabled");
                $("#txtrealname").val("");
                $("#txtenddate").val("");
                //$("#selRoles").val("");

                $("input[type=checkbox]").each(function (index, item) {
                    if ($(this).parent().hasClass("checked"))
                        $(this).parent().removeClass("checked");
                });
                $('#updateMemberForm').modal('show');
            });
            //显示更新
            $(".btn-info").live("click", function (e) {
                e.preventDefault();
                $("#updateMemberForm").attr("data", $(this).parent().parent().attr("data"));
                $("#txtname").attr("value", $(this).parent().parent().attr("name"));
                $("#txtpassword").attr("value", $(this).parent().parent().attr("pwd"));
                $("#txtpasswordcm").attr("value", $(this).parent().parent().attr("pwd"));
                $("#htitle").html('<i class="icon-edit"></i><span>更新用户</span>');
                $("#txtname").attr("disabled", "disabled");
                //$("#selRoles").val($(this).parent().parent().attr("role"));
                $("#txtrealname").val($(this).parent().parent().attr("realname"));
                var platform = $(this).parent().parent().attr("platform").split(',');
                var permission = $(this).parent().parent().attr("permission").split(',');
                $("#txtenddate").val($(this).parent().parent().attr("enddate"));

                $("input[name=cbplatform]").each(function (index, item) {
                    if ($.inArray($(this).val(), platform) > -1) {
                        if (!$(this).parent().hasClass("checked"))
                            $(this).parent().addClass("checked");
                    }
                    else {
                        if ($(this).parent().hasClass("checked"))
                            $(this).parent().removeClass("checked");
                    }
                });
                $("input[name=cbpermission]").each(function (index, item) {
                    if ($.inArray($(this).val(), permission) > -1) {
                        if (!$(this).parent().hasClass("checked"))
                            $(this).parent().addClass("checked");
                    }
                    else {
                        if ($(this).parent().hasClass("checked"))
                            $(this).parent().removeClass("checked");
                    }
                });

                $('#updateMemberForm').modal('show');
            });
            $("#selpagesize").live("change", function () {
                PageClick(1);
            });
            $("input[name=cbplatform],input[name=cbpermission]").live("click", function (e) {
                e.preventDefault();
                if (!$(this).parent().hasClass("checked"))
                    $(this).parent().addClass("checked");
                else
                    $(this).parent().removeClass("checked");
            });

            $("#btnSave").live("click", function () {
                var url = $("#updateMemberForm").attr("data") != "" ? "/Ajax/UpdateSysUser.ashx" : "/Ajax/CreateSysUser.ashx";
                if (lock)
                    return false;
                var sysuserid = $("#updateMemberForm").attr("data");
                var username = $.trim($("#txtname").val());
                var userpassword = $.trim($("#txtpassword").val());
                var roleid = $.trim($("#selRoles").val());
                var userpasswordcm = $.trim($("#txtpasswordcm").val());
                var mail = $("#txtmail").val();
                var realname = $("#txtrealname").val();
                var platform = [];
                var permission = [];
                var endtime = $.trim($("#txtenddate").val());
                $("input[name=cbplatform]").each(function (item) {
                    if ($(this).parent().hasClass('checked'))
                        platform.push($(this).val());
                });
                $("input[name=cbpermission]").each(function (item) {
                    if ($(this).parent().hasClass('checked'))
                        permission.push($(this).val());
                });
                //var flag = jQuery('#form').validationEngine('validate');

                //window.setTimeout(function () {
                //    if ($(".formErrorContent").children().length > 0)
                //        return false;

                //}, 500);
                if (username.length == 0) {
                    notycommon("请输入用户名");
                    return false;
                }
                if (realname.length == 0) {
                    notycommon("请输入真实姓名");
                    return false;
                }
                //if (mail.length == 0) {
                //    notycommon("请输入邮箱");
                //    return false;
                //}
                if (userpassword.length == 0) {
                    notycommon("请输入密码");
                    return false;
                }
                if (endtime.length == 0) {
                    notycommon("请选择截止日期");
                    return false;
                }
                //if (roleid == "") {
                //    notycommon("请选择角色");
                //    return false;
                //}
                if (permission.toString() == "") {
                    notycommon("请选择模块");
                    return false;
                }
                if (platform.toString() == "") {
                    notycommon("请选择平台");
                    return false;
                }
                if (userpassword != userpasswordcm) {
                    notycommon("两次输入的密码不一致");
                    return false;
                }
                var sysuserid = $("#updateMemberForm").attr("data");

                $.ajax({
                    url: url,
                    type: "POST",
                    data: {
                        username: username, userpassword: userpassword, roleid: roleid, platform: platform.toString(),
                        mail: mail, realname: encodeURIComponent(realname), permission: permission.toString(), endtime: endtime, sysuserid: sysuserid
                    },
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
                                PageClick(pageIndex);
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
        });
    </script>

</asp:Content>
