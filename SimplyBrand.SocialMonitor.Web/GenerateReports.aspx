<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GenerateReports.aspx.cs" Inherits="SimplyBrand.SocialMonitor.Web.GenerateReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <ul class="breadcrumb">
            <li>
                <a href="#" id="sb_networkopinion">网络舆情</a>
                <span class="divider">/</span>
            </li>
            <li>
                <a href="#" id="sb_generatereports">生成报告</a>
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
                    <input type="checkbox" id="cbAllEmotional" name="cbAllEmotional" value="">全部 
                        <input type="checkbox" name="emotionvalues" value="1">正面&nbsp;&nbsp;
                        <input type="checkbox" name="emotionvalues" value="0">中性&nbsp;&nbsp;
                        <input type="checkbox" name="emotionvalues" value="-1">负面&nbsp;&nbsp;
                </p>
                <p>
                    <b>时&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;间：</b>
                    <input type="text" id="from" name="from" class="input-small" />
                    ~
                    <input type="text" id="to" name="to" class="input-small" />
                </p>

                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <a class="btn btn-primary" id="gen_report">生成报告</a>

            </div>

        </div>

    </div>
    <script src="Themes/Default/js/basedata.js"></script>
    <script type="text/javascript">
        function checkDateFormate(e) { if (e == null || e == "") return !1; var t = e.length; return checkNumeric(e) && checkLegth(t) && checkSpecialChar(e) ? !0 : !1 } function checkLegth(e) { return e < 8 || e > 10 ? !1 : !0 } function checkSpecialChar(e) { var t = e.indexOf("-"), n = 0, r = 0, i = 0; if (t > -1) { var s = e.lastIndexOf("-"); if (s - t < 1 || s - t > 3) return !1; var o = e.split("-"); n = o[0], r = o[1], i = o[2] } else n = e.substring(0, 4), r = e.substring(4, 6), i = e.substring(6, 8); return Number(r) > 12 || Number(i) > 31 || Number(r) < 1 || Number(i) < 1 || n.length != 4 ? !1 : i > getLastDayOfMonth(Number(n), Number(r)) ? !1 : !0 } function checkNumeric(e) { var t = e.replace(/-/g, "1"); return isNumeric(t) } function isNumeric(e) { var t = regExpTest(e, /\d*[.]?\d*/g); return t } function regExpTest(e, t) { var n = !1; return e == null || e == "" ? !1 : (e == t.exec(e) && (n = !0), n) } function getLastDayOfMonth(e, t) { var n = 0; switch (t) { case 1: case 3: case 5: case 7: case 8: case 10: case 12: n = 31; break; case 4: case 6: case 9: case 11: n = 30; break; case 2: isLeapYear(e) ? n = 29 : n = 28 } return n } function isLeapYear(e) { return e % 4 == 0 && e % 100 != 0 || e % 400 == 0 ? !0 : !1 }
        $(document).ready(function () {
            jQuery.check
            $("#from").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                numberOfMonths: 1,
                onClose: function (selectedDate) {
                    $("#to").datepicker("option", "minDate", selectedDate);

                }
            });

            $("#to").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                numberOfMonths: 1,
                onClose: function (selectedDate) {
                    $("#from").datepicker("option", "maxDate", selectedDate);
                }
            });
            getCheckedValue = function (objname) {
                var p = [];
                $("input[name='" + objname + "']:checked").each(function (index, item) {
                    p.push($(this).val());
                });
                return p.toString();
            };
            var lock = false;
            $("#gen_report").click(function () {
                if (lock)
                    return false;
                var platforms = getCheckedValue("platsource");
                var keywordfamily = getCheckedValue("keywordfamily");
                var emotionvalues = getCheckedValue("emotionvalues");
                if (platforms.length == 0) {
                    notycommon("请选择监测平台");
                    return false;
                }
                if (keywordfamily.length == 0) {
                    notycommon("请选择监测对象");
                    return false;
                }
                if (emotionvalues.length == 0) {
                    notycommon("请选择情感");
                    return false;
                }
                if (!checkDateFormate($("#from").val()) || !checkDateFormate($("#to").val())) {
                    notycommon("日期格式不正确");
                    return false;
                }

                $.ajax({
                    url: "/Ajax/GeneratePDF.ashx",
                    type: "POST",
                    data: { from: $.trim($("#from").val()), to: $.trim($("#to").val()), platforms: platforms, keywordfamily: keywordfamily, emotionvalues: emotionvalues },
                    beforeSend: function () {
                        $(this).after('  <span id="loading"><img style="width:16px;height:11px;" src="/Themes/Default/img/ajax-loaders/ajax-loader-4.gif" title="正在生成报告..."></span>');
                        lock = true;
                    },
                    success: function (data) {
                        try {
                            $("#loading").remove();
                            lock = false;
                            console.info(data);
                            data = JSON.parse(data);
                            var url = data.data.filepath + "/" + data.data.filename;
                            var a = document.createElement('a');
                            a.href = url;
                            a.target = '_blank';
                            document.body.appendChild(a);
                            a.click();
                        } catch (e) { }
                    },
                    error: function () { }

                });



            });
        })

    </script>
</asp:Content>
