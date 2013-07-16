<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SettingInfo.aspx.cs" Inherits="SimplyBrand.SocialMonitor.Web.SettingInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div>
        <ul class="breadcrumb">
            <li>
                <a href="#">我的资料</a>
                <span class="divider">/</span>
            </li>
            <li>
                <a href="#">系统设置</a>
            </li>
        </ul>
    </div>
    <div class="row-fluid sortable">
        <div class="box span12">
            <div class="box-header well" data-original-title>
                <h2><i class="icon-edit"></i>修改密码</h2>
            </div>
            <div class="box-content">
                <div class="form-horizontal">
                    <div class="control-group">
                            <label class="control-label">当前使用密码：</label>
                            <div class="controls">
                                <input type="password" id="txt_oldpwd" /> 
                            </div>
                        </div>      
                   <div class="control-group">
                            <label class="control-label">新密码：</label>
                            <div class="controls">
                                <input type="password"  id="txt_Newpwd"  /> 
                            </div>
                        </div>                      
                        <div class="control-group">
                            <label class="control-label" for="txtTel">再输入一次新密码：</label>
                            <div class="controls">
                                <input type="password" id="txt_Newpwdagain" />
                            </div>
                        </div>    
                    
                      <a href="#" class="btn btn-primary" style="margin-left:15%;" id="updateInfo">提 交</a>  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span id="ErrorMsg" style="color:red;"></span>                              
                </div>
            </div>
        </div>
    </div>
    <script  type="text/javascript">      
        $("#updateInfo").click(function () {
            $("#ErrorMsg").text("")
            var oldpwd = $.trim($("#txt_oldpwd").val());
            var newpwd = $.trim($("#txt_Newpwd").val());
            var newpwdagain = $.trim($("#txt_Newpwdagain").val());
            if (oldpwd.length == 0 || newpwdagain.length == 0 || newpwd.length == 0)
            {
                $("#ErrorMsg").text("密码不能为空！");
                return false;
            }
            if (newpwdagain.length != newpwd.length)
            {
                $("#ErrorMsg").text("密码长度不一致！");
                return false;
            }
            if (newpwdagain!= newpwd) {
                $("#ErrorMsg").text("两次输入密码不一样！");
                return false;
            }
                $.ajax({
                    "type": "POST",
                    "url": "/Ajax/GetSettingInfo.ashx",
                    "data": { oldpwd: oldpwd, newpwd: newpwd },
                    beforeSend: function () { },
                    success: function (data) {
                        var json = JSON.parse(data);
                        $("#txt_oldpwd").val("");
                        $("#txt_Newpwd").val("");
                        $("#txt_Newpwdagain").val("");
                           alert("修改成功！");
                    },
                    error: function (data) {
                        var json = JSON.parse(data);
                        try {
                            $("#ErrorMsg").text(json.errormsg);
                            } catch (e) { }
                    }
                });

            });

     </script>
</asp:Content>
