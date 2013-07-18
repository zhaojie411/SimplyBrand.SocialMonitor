<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Setting.aspx.cs" Inherits="SimplyBrand.SocialMonitor.Web.Setting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <ul class="breadcrumb">
            <li>
                <a href="#" id="sb_myprofile">我的资料</a>
                <span class="divider">/</span>
            </li>
            <li>
                <a href="#" id="sb_syssetting">系统设置</a>
            </li>
        </ul>
    </div>
    <div class="row-fluid sortable">
        <div class="box span12">
            <div class="box-header well" data-original-title>
                <h2><i class="icon-edit"></i><span id="sb_basicinformation">基本信息</span></h2>
            </div>
            <div class="box-content">
                <div class="form-horizontal">
                    <fieldset>
                        <div class="control-group">
                            <label class="control-label" id="sb_loginname"></label>
                            <div class="controls">
                                <input type="text" id="username" disabled="disabled" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a href="SettingInfo.aspx" id="sb_changepassword">修改密码</a>
                            </div>
                        </div>
                        <div class="control-group">
                            <label id="sb_fpemergencysituations">紧急情况下第一联系人</label>

                        </div>
                        <div class="control-group">
                            <label class="control-label" for="txtTel" name="sb_contact">联系人：</label>
                            <div class="controls">
                                <input type="text" id="linkName" />&nbsp;&nbsp;&nbsp; <span id="Span1"></span>
                                <label id="linkId" style="visibility: hidden;"></label>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="txtTel" name="sb_cellphoneno">手机号码：</label>
                            <div class="controls">
                                <input type="text" id="linkTel" />&nbsp;&nbsp;&nbsp; <span id="moileMsg"></span>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="txtEmail" name="sb_email">联系邮箱：</label>
                            <div class="controls">
                                <input type="text" id="linkEmail" name="txtEmail" />
                                <i class="icon-info-sign" id="sb_rmonitoringreportsinfo" data-rel="tooltip" data-original-title="接受系统发送的监测报告" style="cursor: pointer;"></i>

                            </div>
                        </div>
                        <div class="control-group">
                            <label id="sb_spemergencysituations">紧急情况下第二联系人</label>

                        </div>
                        <div class="control-group">
                            <label class="control-label" for="txtTel" name="sb_contact">联系人：</label>
                            <div class="controls">
                                <input type="text" id="txtName" /><label id="txtId" style="visibility: hidden;"></label>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="txtTel" name="sb_cellphoneno">手机号码：</label>
                            <div class="controls">
                                <input type="text" id="txtTel" />
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="txtEmail" name="sb_email">联系邮箱：</label>
                            <div class="controls">
                                <input type="text" id="txtEmail" name="txtEmail" />
                                <span class="help-inline"></span>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label"></label>
                            <div class="controls">
                                <a href="#" class="btn btn-primary" id="sb_save">保存</a>  &nbsp;&nbsp;&nbsp;<span id="setMsg" style="color: red;"> </span>
                            </div>

                        </div>
                    </fieldset>
                </div>

            </div>
        </div>

    </div>
    <div class="row-fluid sortable">
        <div class="box span12">
            <div class="box-header well" data-original-title>
                <h2><i class="icon-edit"></i><span id="sb_customlogo">自定义LOGO</span></h2>
            </div>
            <div class="box-content">
                <div class="form-horizontal">
                    <fieldset>
                        <div class="control-group">
                            <label class="control-label"  id="sb_selectimage">选择图片：</label>
                            <div class="controls">
                                <input data-no-uniform="true" type="file" name="file_upload" id="file_upload" />
                            </div>
                        </div>

                    </fieldset>
                </div>

            </div>
        </div>

    </div>


    <script type="text/javascript">
        $(document).ready(function () {
            $("#username").val($.cookie("sysusername"));
            InitData();
        });
        $("#sb_save").click(function () {
            $("#setMsg").text("");
            var isMobile = /^(?:13\d|15\d|18\d)\d{5}(\d{3}|\*{3})$/;

            var linkman = $("#linkName").val().trim();
            var linkTel = $("#linkTel").val().trim();
            var linkEmail = $("#linkEmail").val().trim();
            var linkId = $("#linkId").val().trim();
            var txtman = $("#txtName").val().trim();
            var txtTel = $("#txtTel").val().trim();
            var txtEmail = $("#txtEmail").val().trim();
            var txtId = $("#txtId").val().trim();

            if (linkman.length == 0 || linkTel.length == 0 || linkEmail.length == 0) {
                $("#setMsg").text("请先填写第一联系人...");
                return false;
            }
            if (!isMobile.test(linkTel)) {
                $("#setMsg").text("请正确填写电话号码");
                $("#linkTel").focus();
                return false;
            }
            if (txtTel != "") {
                if (!isMobile.test(linkTel)) {
                    $("#setMsg").text("请正确填写电话号码");
                    $("#txtTel").focus();
                    return false;
                }
            }
            if (!$("#linkEmail").val().match(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/)) {
                $("#setMsg").html("邮箱格式不正确！请重新输入！");
                $("#linkEmail").focus();
                return false;
            }
            if (txtEmail != "") {
                if (!$("#txtEmail").val().match(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/)) {
                    $("#setMsg").html("邮箱格式不正确！请重新输入！");
                    $("#txtEmail").focus();
                    return false;
                }
            }

            $.ajax({
                "type": "POST",
                "url": "/Ajax/PostContactUser.ashx",
                "data": { linkman: linkman, linkTel: linkTel, linkEmail: linkEmail, txtman: txtman, txtTel: txtTel, linkId: linkId, txtId: txtId, txtEmail: txtEmail },
                beforeSend: function () { },
                success: function (data) {
                    var json = JSON.parse(data);
                    if (json.errormsg.trim() != "null") {
                        $("#setMsg").text(json.errormsg);
                    }
                    InitData();
                },
                error: function (data) {
                    var json = JSON.parse(data);
                    try {
                        $("#setMsg").text(json.errormsg);
                    } catch (e) { }
                }
            });
        });
        InitData = function () {
            $.get("/Ajax/GetContactUser.ashx?r=" + new Date().getTime(), function (data) {
                try {
                    data = JSON.parse(data);
                    $.each(data.item, function (index, item) {
                        if (item.ContactUserIsprimary == true) {
                            $("#linkId").val(item.ContactUserId);
                            $("#linkName").val(item.ContactUserName);
                            $("#linkTel").val(item.ContactUserTel);
                            $("#linkEmail").val(item.ContactUserEmail);
                        }
                        else {
                            $("#txtId").val(item.ContactUserId);
                            $("#txtName").val(item.ContactUserName);
                            $("#txtTel").val(item.ContactUserTel);
                            $("#txtEmail").val(item.ContactUserEmail);
                        }
                    });


                } catch (e) { }
            });
        };

        $("#updateInfo").click(function () {
            checkSubmitMobil();

        });
        function checkSubmitMobil() {
            if ($("#txtTel").val() == "") {
                $("#moileMsg").html("<font color='red'>手机号码不能为空！</font>");
                $("#txtTel").focus();
                return false;
            }
            if (!$("#txtTel").val().match("1[3|4|5|8][0-9]\d{4,8}")) {
                $("#moileMsg").html("<font color='red'>手机号码格式不正确！请重新输入！</font>");
                $("#txtTel").focus();
                return false;
            }
        }
    </script>
</asp:Content>
