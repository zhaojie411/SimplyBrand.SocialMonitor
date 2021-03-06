﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SimplyBrand.SocialMonitor.AdminSite.Login" %>

<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Themes/Default/css/bootstrap-cerulean.css" rel="stylesheet" />
    <style type="text/css">
        body
        {
            padding-bottom: 40px;
        }

        .sidebar-nav
        {
            padding: 9px 0;
        }
    </style>
    <link href="/Themes/Default/css/bootstrap-responsive.css" rel="stylesheet" />
    <link href="/Themes/Default/css/charisma-app.css" rel="stylesheet" />
    <link href="/Themes/Default/css/jquery-ui-1.8.21.custom.css" rel="stylesheet" />
    <link href="/Themes/Default/css/chosen.css" rel="stylesheet" />
    <link href="/Themes/Default/css/uniform.default.css" rel="stylesheet" />
    <link href="/Themes/Default/css/colorbox.css" rel="stylesheet">
    <link href="/Themes/Default/css/jquery.cleditor.css" rel="stylesheet" />
    <link href="/Themes/Default/css/jquery.noty.css" rel="stylesheet" />
    <link href="/Themes/Default/css/noty_theme_default.css" rel="stylesheet" />
    <link href="/Themes/Default/css/elfinder.min.css" rel="stylesheet" />
    <link href="/Themes/Default/css/elfinder.theme.css" rel="stylesheet" />
    <link href="/Themes/Default/css/jquery.iphone.toggle.css" rel="stylesheet" />
    <link href="/Themes/Default/css/opa-icons.css" rel="stylesheet" />
    <!-- The HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
	  <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
	<![endif]-->
    <!-- The fav icon -->
    <%--<link rel="shortcut icon" href="../Themes/Default/img/favicon.ico" />--%>
</head>
<body>

    <div class="container-fluid">
        <div class="row-fluid">

            <div class="row-fluid">
                <div class="span12 center login-header">
                    <h2>Welcome to Charisma</h2>
                </div>
            </div>

            <div class="row-fluid">
                <div class="well span5 center login-box">
                    <div class="alert alert-info">
                        Please login with your Username and Password.
                    </div>
                    <form id="Form1" class="form-horizontal" method="post" runat="server">
                        <fieldset>
                            <div class="input-prepend" title="Username" data-rel="tooltip">
                                <span class="add-on"><i class="icon-user"></i></span>
                                <input autofocus class="input-large span10" name="username" id="username" type="text" value="" />
                            </div>
                            <div class="clearfix"></div>

                            <div class="input-prepend" title="Password" data-rel="tooltip">
                                <span class="add-on"><i class="icon-lock"></i></span>
                                <input class="input-large span10" name="password" id="password" type="password" value="" />
                            </div>
                            <div class="clearfix"></div>

                            <div class="input-prepend">
                                <label class="remember" for="remember">
                                    <input type="checkbox" id="remember" name="remember" checked="checked" />Remember me
                
                                </label>

                            </div>
                            <div class="clearfix"></div>
                            <p class="center span5">
                                <a id="btnLogin" class="btn btn-primary">Login</a>
                            </p>
                        </fieldset>
                    </form>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="Themes/Default/js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var lock = false;
            $("#btnLogin").live("click", function (e) {
                var username = $.trim($("#username").val());
                var password = $.trim($("#password").val());
                var remember = $("#remember").is(":checked");
                if (username.length == 0 || password.length == 0)
                    return false;
                if (lock)
                    return false;
                $.ajax({
                    url: "/Ajax/Login.ashx",
                    type: "POST",
                    data: { username: username, password: password, remember: remember },
                    beforeSend: function () { lock = true; },
                    success: function (data) {
                        lock = false;
                        try {
                            data = JSON.parse(data);
                            if (data.issucc) {
                                location.href = "UserManage.aspx";
                            } else {
                                alert(data.errormsg);
                            }
                        } catch (e) { }
                    },
                    error: function () {
                        lock = false;
                    }

                });
            });
        });
    </script>
</body>
</html>

