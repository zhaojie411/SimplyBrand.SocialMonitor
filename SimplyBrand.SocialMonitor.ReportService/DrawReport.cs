using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SimplyBrand.SocialMonitor.DAL.Data;
using SimplyBrand.SocialMonitor.DAL.Data.Bases;
using SimplyBrand.SocialMonitor.DAL.Data.SqlClient;
using SimplyBrand.SocialMonitor.DAL.Entities;
using System.Configuration;
using SimplyBrand.SocialMonitor.Business;
using SimplyBrand.SocialMonitor.Business.Utility;
using SimplyBrand.SocialMonitor.Business.DB;
using SimplyBrand.SocialMonitor.Business.JsonEntity;
using System.Text.RegularExpressions;
namespace SimplyBrand.SocialMonitor.ReportService
{

    public enum EnumReportType
    {
        DayReport = 1,
        WeekDayReport = 2,
        MonthReport = 3
    }
    public class DrawReport
    {

        DrawChart drawChart = new DrawChart();
        public static BaseFont baseFont = null;

        #region 配置路径
        public string PDFSavePath
        {
            get
            {
                return ConfigurationManager.AppSettings["PDFSavePath"].ToString();
            }
        }

        public string PDFSaveRelativePath
        {
            get
            {
                return ConfigurationManager.AppSettings["PDFSaveRelativePath"].ToString();
            }
        }
        public string LogoPath
        {
            get
            {
                return ConfigurationManager.AppSettings["LogoPath"].ToString();
            }
        }

        #endregion

        #region Font
        public Font BigTitleFont
        {
            get
            {
                return new Font(baseFont, 40);
            }
        }
        public Font RepostDateFont
        {
            get
            {
                Font font = new Font(baseFont, 20);
                font.SetStyle(Font.BOLD);
                return font;
            }
        }
        public Font TitleFont
        {
            get
            {
                return new Font(baseFont, 30);
            }
        }

        public Font SmallTitleFont
        {
            get
            {
                return new Font(baseFont, 16);
            }
        }


        public Font SmallBlodTitleFont
        {
            get
            {
                Font font = new Font(baseFont, 16);
                font.SetStyle(Font.BOLD);
                return font;
            }
        }
        public Font SmallGrayTitleFont
        {
            get
            {
                Font font = new Font(baseFont, 16);
                font.Color = BaseColor.GRAY;
                return font;
            }

        }

        public Font ContentFont
        {
            get
            {
                return new Font(baseFont, 12);
            }
        }

        public Font ContentSmallFont
        {
            get
            {
                return new Font(baseFont, 8);
            }
        }
        #endregion

        public void RegisterFont()
        {
            string fontPath = System.Environment.GetEnvironmentVariable("windir") + @"\Fonts\simsunb.ttf";
            FontFactory.Register(fontPath);
            baseFont = BaseFont.CreateFont(
                    "C:\\WINDOWS\\FONTS\\SIMHEI.TTF",
                    BaseFont.IDENTITY_H,
                    BaseFont.NOT_EMBEDDED);

        }




        public void DrawLogo(string logofile, Document document, PdfWriter writer)
        {

            Image image = Image.GetInstance(logofile);
            image.SetAbsolutePosition(220, 700);
            document.Add(image);
        }



        private string GetReportDate(EnumReportType type)
        {
            string strDate = "";
            string start = "";
            string end = "";
            switch (type)
            {
                case EnumReportType.DayReport:
                    strDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 00：00 - hh:00");
                    break;
                case EnumReportType.WeekDayReport:
                    start = DateTime.Now.AddDays(-8).ToString("yyyy-MM-dd 00：00");
                    end = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 17：00");
                    strDate = start + " - " + end;
                    break;
                case EnumReportType.MonthReport:
                    start = DateTime.Now.AddMonths(-1).AddDays(1).ToString("yyyy-MM-dd 00：00");
                    end = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 17：00");
                    strDate = start + " - " + end;
                    break;
            }
            return strDate;
        }
        private string GetReportTypeName(EnumReportType type)
        {
            switch (type)
            {
                case EnumReportType.DayReport:
                    return "日报";
                case EnumReportType.WeekDayReport:
                    return "周报";
                case EnumReportType.MonthReport:
                    return "月报";
            }
            return "";
        }
        private string GetPlatform(int platform)
        {
            switch (platform)
            {
                case 1:
                    return "新浪微博";
                case 2:
                    return "博客";
                case 3:
                    return "论坛";
                case 4:
                    return "新闻";
            }
            return "";
        }
        private string GetEmotional(int emotional)
        {
            switch (emotional)
            {
                case 1:
                    return "正面";
                case -1:
                    return "负面";
                case 0:
                    return "中性";
            }
            return "";
        }

        public void DrawReportDate(Document document, PdfWriter writer, EnumReportType type)
        {

            Phrase phraseDate = new Phrase(string.Format("报告期间：{0}", GetReportDate(type)), RepostDateFont);
            ColumnText colTextDate = new ColumnText(writer.DirectContent);
            colTextDate.AddText(phraseDate);
            colTextDate.SetSimpleColumn(20, 550, 700, 50, 1, Element.ALIGN_CENTER);
            colTextDate.Go();
        }

        public void DrawReportInfo(Document document, PdfWriter writer, int sysUserId, DateTime dtGen, EnumReportType type, string realName, string customerContact)
        {

            PdfPTable tableInfo = new PdfPTable(2);

            PdfPCell pdfCell = new PdfPCell(new Phrase(string.Format("客户：{0}", realName), SmallBlodTitleFont));
            PdfPCell pdfCellNone = new PdfPCell();
            pdfCellNone.Border = 0;
            pdfCell.Border = 0;
            tableInfo.AddCell(pdfCell);
            pdfCell = new PdfPCell(new Phrase(string.Format("报告类型：{0}", GetReportTypeName(type)), SmallBlodTitleFont));
            pdfCell.Border = 0;
            tableInfo.AddCell(pdfCell);
            pdfCell = new PdfPCell(new Phrase(string.Format("客户联系人：{0}", customerContact), SmallBlodTitleFont));
            pdfCell.Border = 0;
            pdfCell.Colspan = 2;
            tableInfo.AddCell(pdfCell);
            pdfCell = new PdfPCell(new Phrase("服务方联系人：test1@simplybrand.com", SmallBlodTitleFont));
            pdfCell.Border = 0;
            pdfCell.Colspan = 2;
            tableInfo.AddCell(pdfCell);
            pdfCell = new PdfPCell(new Phrase(string.Format("报告生成时间：{0}", dtGen.ToString("yyyy-MM-dd hh:mm")), SmallBlodTitleFont));
            pdfCell.Border = 0;
            pdfCell.Colspan = 2;
            tableInfo.AddCell(pdfCell);
            tableInfo.TotalWidth = document.PageSize.Width - 50;
            tableInfo.WriteSelectedRows(0, -1, 100, 200, writer.DirectContent);

        }

        private void DrawText(Document document, PdfWriter writer, Font font, string text, float x, float y, float width, float height)
        {
            Phrase phrase = new Phrase(text, font);
            ColumnText coltext = new ColumnText(writer.DirectContent);
            coltext.AddText(phrase);
            coltext.SetSimpleColumn(x, y, width, height);
            coltext.Go();
        }

        private void DrawListItem(Document document, PdfWriter writer, Font font, List<string> contents, float x, float y, float width, float height)
        {
            List list = new List();
            foreach (string item in contents)
            {
                list.Add(new ListItem(new Chunk(item, font)));
            }
            ColumnText coltextList = new ColumnText(writer.DirectContent);
            coltextList.AddElement(list);
            coltextList.SetSimpleColumn(x, y, width, height);
            coltextList.Go();
        }

        private void DrawImage(Document document, byte[] bytes, float x, float y, float width, float height)
        {
            Image imageHotwords = Image.GetInstance(bytes);
            imageHotwords.SetAbsolutePosition(x, y);
            imageHotwords.ScaleAbsolute(width, height);
            document.Add(imageHotwords);
        }
        private void DrawImage(Document document, string filename, float x, float y, float width, float height)
        {
            Image imageHotwords = Image.GetInstance(filename);
            imageHotwords.SetAbsolutePosition(x, y);
            imageHotwords.ScaleAbsolute(width, height);
            document.Add(imageHotwords);
        }

        private void DrawConfidentialityNotice(Document document, PdfWriter writer)
        {
            PdfPTable tableNotice = new PdfPTable(1);
            PdfPCell pdfCellNoticeTitle = new PdfPCell(new Phrase("保密声明", SmallGrayTitleFont));
            pdfCellNoticeTitle.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCellNoticeTitle.Border = 1;
            pdfCellNoticeTitle.BorderColorTop = BaseColor.GRAY;
            tableNotice.AddCell(pdfCellNoticeTitle);
            PdfPCell pdfCellNoticeContent = new PdfPCell(new Phrase("  本报告是针对simplyBrand客户的相关品牌和企业的商业文件。未经simplyBrand的书面许可, 本文件或文件中的任何内容不得转交给第三方, 同时也不得发表本文件或文件中的任何内容。", SmallGrayTitleFont));
            pdfCellNoticeContent.Border = 0;
            tableNotice.AddCell(pdfCellNoticeContent);
            tableNotice.TotalWidth = document.PageSize.Width - 50;
            tableNotice.WriteSelectedRows(0, -1, 20, 200, writer.DirectContent);
        }


        private void DrawBackgroundInfo(Document document, PdfWriter writer, string daterange, string keywords, string sitecount, string datacount)
        {
            PdfPTable tableBackgroundInfo = new PdfPTable(4);
            PdfPCell pdfCellBackgroundInfo = new PdfPCell();
            pdfCellBackgroundInfo = new PdfPCell(new Phrase("报告期间：", ContentFont));
            pdfCellBackgroundInfo.Border = 0;
            tableBackgroundInfo.AddCell(pdfCellBackgroundInfo);
            pdfCellBackgroundInfo = new PdfPCell(new Phrase(daterange, ContentFont));
            pdfCellBackgroundInfo.Colspan = 3;
            pdfCellBackgroundInfo.Border = 0;
            tableBackgroundInfo.AddCell(pdfCellBackgroundInfo);
            pdfCellBackgroundInfo = new PdfPCell(new Phrase("监测品牌：", ContentFont));
            pdfCellBackgroundInfo.Border = 0;
            tableBackgroundInfo.AddCell(pdfCellBackgroundInfo);
            pdfCellBackgroundInfo = new PdfPCell(new Phrase(keywords, ContentFont));
            pdfCellBackgroundInfo.Colspan = 3;
            pdfCellBackgroundInfo.Border = 0;
            tableBackgroundInfo.AddCell(pdfCellBackgroundInfo);
            pdfCellBackgroundInfo = new PdfPCell(new Phrase("监测站点数：", ContentFont));
            pdfCellBackgroundInfo.Border = 0;
            tableBackgroundInfo.AddCell(pdfCellBackgroundInfo);
            pdfCellBackgroundInfo = new PdfPCell(new Phrase(sitecount, ContentFont));
            pdfCellBackgroundInfo.Border = 0;
            tableBackgroundInfo.AddCell(pdfCellBackgroundInfo);
            pdfCellBackgroundInfo = new PdfPCell(new Phrase("采集数据量：", ContentFont));
            pdfCellBackgroundInfo.Border = 0;
            tableBackgroundInfo.AddCell(pdfCellBackgroundInfo);
            pdfCellBackgroundInfo = new PdfPCell(new Phrase(datacount, ContentFont));
            pdfCellBackgroundInfo.Border = 0;
            tableBackgroundInfo.AddCell(pdfCellBackgroundInfo);
            tableBackgroundInfo.TotalWidth = document.PageSize.Width - 50;
            tableBackgroundInfo.WriteSelectedRows(0, -1, 50, document.PageSize.Height - 120, writer.DirectContent);
        }

        private void DrawData(Document document, PdfWriter writer, List<DataCenterJson> data)
        {
            PdfPTable tableData = new PdfPTable(9);
            tableData.LockedWidth = true;

            //for (int j = 0; j < 200; j++)
            //{
            //    for (int i = 0; i < 10; i++)
            //    {

            //        PdfPCell pdfCellData = new PdfPCell();
            //        pdfCellData = new PdfPCell(new Phrase("测试" + j + i, ContentSmallFont));
            //        if (i == 4)
            //            pdfCellData = new PdfPCell(new Phrase("测试测试测试测试测试测试测试测试" + j + i, ContentSmallFont));

            //        if (j == 0)
            //        {
            //            pdfCellData.PaddingTop = 100;
            //            pdfCellData.Border = 0;
            //        }
            //        else
            //        {
            //            pdfCellData.Border = 1;
            //        }

            //        tableData.AddCell(pdfCellData);
            //    }
            //}
            Regex reg = new Regex("<.+?>");
            List<string> listdata = new List<string>() { "来源", "关键词", "情感", "内容", "原作者", "转发/浏览量", "评论/回复量", "网站名称", "时间" };

            foreach (DataCenterJson json in data)
            {
                json.datatitle = reg.Replace(json.datatitle, "");
                List<string> clist = new List<string>() { GetPlatform(json.datasourceid), "", GetEmotional(json.emotionalvalue), json.datatitle, json.dataauthor, json.dataforward, json.datacomment, json.sitename, json.datatime };
                foreach (string citem in clist)
                {
                    listdata.Add(citem);
                }
            }
            int i = 0;
            foreach (string item in listdata)
            {

                PdfPCell pdfCellData = new PdfPCell(new Phrase(item, ContentSmallFont));
                if (i < 9)
                {
                    pdfCellData.PaddingTop = 100;
                    pdfCellData.Border = 0;
                }
                else
                {
                    pdfCellData.Border = 1;
                }
                pdfCellData.HorizontalAlignment = 1;
                tableData.AddCell(pdfCellData);
                i++;
            }

            tableData.TotalWidth = document.PageSize.Width - 20;
            float maxlength = document.PageSize.Width;
            float clength = (maxlength - 300) / 8;
            tableData.SetWidths(new float[] { clength, clength, clength, 300, clength, clength, clength, clength, clength });
            //tableData.WriteSelectedRows(0, -1, 20, document.PageSize.Height - 30, writer.DirectContent);

            document.Add(tableData);
        }
        private void DrawCompanyInfo(Document document, PdfWriter writer)
        {
            PdfPTable table = new PdfPTable(1);
            string[] contents = { "联系我们：", "信励（上海）信息科技有限公司", "电话：+86 21 6339 0208", "地址：上海市黄浦区汉口路300号解放日报大厦2412室", "邮编：200002" };
            foreach (string item in contents)
            {
                PdfPCell pdfCell = new PdfPCell();
                pdfCell = new PdfPCell(new Phrase(item, SmallBlodTitleFont));
                pdfCell.Border = 0;
                table.AddCell(pdfCell);
            }
            table.TotalWidth = document.PageSize.Width - 50;
            table.WriteSelectedRows(0, -1, 20, document.PageSize.Height - 340, writer.DirectContent);
        }

        private void DrawCompanyLogo(Document document, PdfWriter writer)
        {
            PdfPTable table = new PdfPTable(2);
            string[] images = { "c:\\logo_sina.jpg", "c:\\logo_facebook.jpg", "c:\\logo_3.jpg", "c:\\logo_4.jpg" };
            foreach (string item in images)
            {
                PdfPCell pdfCell = new PdfPCell();
                Image image = Image.GetInstance(item);
                image.ScaleAbsolute(55, 42);
                pdfCell.AddElement(image);
                pdfCell.AddElement(new Phrase("@SimplyBrand", SmallTitleFont));
                pdfCell.Border = 0;
                pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(pdfCell);
            }
            table.TotalWidth = document.PageSize.Width - 50;
            table.WriteSelectedRows(0, -1, 20, document.PageSize.Height - 520, writer.DirectContent);
        }

        private string GetSearchInfo(string platforms, string keywordfamily, string emotionvalues)
        {

            List<int> listplatforms = platforms.Split(',').ToList().ConvertAll(p => int.Parse(p)).ToList();
            List<int> listemotionvalues = emotionvalues.Split(',').ToList().ConvertAll(p => int.Parse(p)).ToList();
            platforms = "";
            foreach (int item in listplatforms)
            {
                platforms = platforms + GetPlatform(item) + " ";
            }
            emotionvalues = "";
            foreach (int item in listemotionvalues)
            {
                emotionvalues = emotionvalues + GetEmotional(item) + " ";
            }
            return platforms + " " + keywordfamily.Replace(",", " ") + " " + emotionvalues;
        }
        public void GeneratePDF(int sysUserId = 0, EnumReportType type = EnumReportType.DayReport, string platforms = null, string keywordFamilyIDs = null, string emotionvalues = null, bool isToday = true, string searchstarttime = null, string searchendtime = null, string notkeyword = null)
        {
            string keywordfamily = string.Empty;//品牌名
            SysUser sysUser = DataRepository.SysUserProvider.GetBySysUserId(sysUserId);
            TList<ContactUser> contactUserTlist = DataRepository.ContactUserProvider.GetBySysUserId(sysUserId);
            List<ContactUser> contactUserList = contactUserTlist.Where(p => bool.Parse(p.IsPrimary.ToString())).ToList();
            TList<KeywordFamily> tlistKeywordFamily = DataRepository.KeywordFamilyProvider.GetBySysUserId(sysUserId);
            //获取品牌名称
            if (string.IsNullOrEmpty(keywordFamilyIDs))
            {
                keywordfamily = string.Join(",", tlistKeywordFamily.Select(p => p.KeywordFamilyName).ToList());
            }
            else
            {
                keywordfamily = string.Join(",", (from t in tlistKeywordFamily
                                                  where keywordFamilyIDs.Contains(t.KeywordFamilyId.ToString())
                                                  select t.KeywordFamilyName).ToList());
            }

            IDBProvider dbProvider = DBProviderFactory.GetDBProvider(DBType.SqlServer);
            var emotionalJson = dbProvider.GetEmotionalData(sysUserId, keywordFamilyIDs, platforms, isToday);
            ResponseDataEmotionalJson responseDataEmotionalJson = JsonHelper.ParseJSON<ResponseDataEmotionalJson>(emotionalJson);
            List<DataCenterSummaryItemJson> pieData = new List<DataCenterSummaryItemJson>();
            if (responseDataEmotionalJson != null)
                pieData = responseDataEmotionalJson.data;//饼图数据
            var summaryJson = dbProvider.GetSummaryData(sysUserId, keywordFamilyIDs, platforms, emotionvalues, isToday);
            ResponseDataCenterSummaryJson responseDataCenterSummaryJson = JsonHelper.ParseJSON<ResponseDataCenterSummaryJson>(summaryJson);
            //折线图数据
            Dictionary<string, List<DataCenterSummaryItemJson>> dicSummary = new Dictionary<string, List<DataCenterSummaryItemJson>>();
            if (responseDataCenterSummaryJson != null && responseDataCenterSummaryJson.data != null)
                dicSummary = responseDataCenterSummaryJson.data.items;

            //获取热门关键词数据
            HotKeywordBLL hotKeywordBLL = new HotKeywordBLL();
            string hotkeywordJson = hotKeywordBLL.GetHotKeyword(sysUserId, keywordFamilyIDs);
            ResponseHotKeywordPageJson responseHotKeywordPageJson = JsonHelper.ParseJSON<ResponseHotKeywordPageJson>(hotkeywordJson);
            List<HotKeywordJson> hotKeywordJsonList = new List<HotKeywordJson>();
            if (responseHotKeywordPageJson.data != null && responseHotKeywordPageJson.data != null)
                hotKeywordJsonList = responseHotKeywordPageJson.data.items;

            //string datacenterjson = dbProvider.GetSeach(platforms, keywordFamilyIDs, "", notkeyword, searchstarttime, searchendtime, emotionvalues, 100, 1, sysUserId);
            string datacenterjson = dbProvider.GetSeach("", "", "", "", "", "", "", 100, 1, sysUserId);
            ResponseDataCenterJson responseDataCenterJson = JsonHelper.ParseJSON<ResponseDataCenterJson>(datacenterjson);
            List<DataCenterJson> dataCenterJsonList = new List<DataCenterJson>();
            if (responseDataCenterJson.data != null && responseDataCenterJson.data.items != null)
                dataCenterJsonList = responseDataCenterJson.data.items;


            string customerContact = "";
            string realName = "";
            if (contactUserList.Count > 0)
                customerContact = contactUserList[0].ContactUserEmail;
            if (sysUser != null)
                realName = sysUser.SysUserRealName;
            string logofile = "";
            //string fileName = "网络舆情监测报告.pdf";
            string fileName = Guid.NewGuid().ToString() + ".pdf";
            RegisterFont();
            Font font = new Font(baseFont, 14);
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                using (Document document = new Document(PageSize.A4))
                {

                    using (PdfWriter writer = PdfWriter.GetInstance(document, fs))
                    {
                        document.Open();
                        document.NewPage();
                        var evnet = new itsEvents();
                        writer.PageEvent = evnet;
                        //logo
                        if (string.IsNullOrEmpty(sysUser.SysUserLogo))
                        {
                            logofile = "c:\\sb_logo_small.jpg";
                        }
                        else
                        {
                            logofile = PDFSavePath + sysUser.SysUserLogo;
                        }
                        DrawLogo(logofile, document, writer);
                        //titile
                        DrawText(document, writer, BigTitleFont, "网络舆情监测报告", 135, 600, 600, 50);
                        //date
                        if (type == EnumReportType.DayReport)
                        {
                            DrawText(document, writer, RepostDateFont, string.Format("报告期间：{0}", GetReportDate(type)), 110, 550, 800, 50);
                        }
                        else
                        {
                            DrawText(document, writer, RepostDateFont, string.Format("报告期间：{0}", GetReportDate(type)), 20, 550, 800, 50);
                        }
                        //info
                        DrawReportInfo(document, writer, sysUserId, DateTime.Now, EnumReportType.WeekDayReport, realName, customerContact);
                        document.NewPage();
                        //directory
                        DrawText(document, writer, TitleFont, "目录", document.PageSize.Width / 2 - 50, document.PageSize.Height - 100, 500, 50);
                        //list
                        List<string> listcontent = new List<string>() { "报告概述", "背景", "网络提及量", "情感分析", "网民讨论热门词", "网络舆情相关数据" };
                        DrawListItem(document, writer, SmallTitleFont, listcontent, 50, document.PageSize.Height - 150, 500, 200);
                        //Confidentiality Notice
                        DrawConfidentialityNotice(document, writer);
                        document.NewPage();
                        //overview 
                        DrawText(document, writer, TitleFont, "报告概述", document.PageSize.Width / 2 - 50, document.PageSize.Height - 75, 500, 50);
                        //一、Background
                        DrawText(document, writer, SmallTitleFont, "一、背景", 20, document.PageSize.Height - 100, 500, 50);
                        //BackgroundInfo
                        DrawBackgroundInfo(document, writer, GetReportDate(type), keywordfamily, "2,000", "40,000");
                        //一、Network mentioned quantity 网络提及量
                        DrawText(document, writer, SmallTitleFont, "二、网络提及量", 20, document.PageSize.Height - 200, 500, 50);
                        DrawImage(document, new DrawChart().DrawLineChart(dicSummary), 50, document.PageSize.Height - 420, 420, 200);
                        //三、Sentiment analysis 情感分析
                        DrawText(document, writer, SmallTitleFont, "三、情感分析", 20, document.PageSize.Height - 420, 500, 50);
                        DrawImage(document, new DrawChart().DrawPie(pieData), 70, document.PageSize.Height - 600, 150, 150);
                        //四、Hot words 网民讨论热门词
                        DrawText(document, writer, SmallTitleFont, "四、网民讨论热门词", 20, document.PageSize.Height - 620, 500, 50);
                        DrawImage(document, new DrawChart().DrawWordCloud(hotKeywordJsonList), 70, document.PageSize.Height - 770, 240, 120);
                        //data
                        document.NewPage();
                        DrawText(document, writer, TitleFont, "网络舆情相关数据", document.PageSize.Width / 2 - 140, document.PageSize.Height - 75, 500, 50);
                        DrawText(document, writer, ContentFont, string.Format("搜索条件：{0}", GetSearchInfo(platforms, keywordfamily, emotionvalues)), 20, document.PageSize.Height - 110, document.PageSize.Width - 20, 50);
                        DrawData(document, writer, dataCenterJsonList);
                        //tableData.WriteSelectedRows(0, -1, 20, document.PageSize.Height - 130, writer.DirectContent)
                        document.NewPage();
                        DrawText(document, writer, SmallTitleFont, "感谢您的阅读", 20, document.PageSize.Height - 300, 500, 50);
                        DrawCompanyInfo(document, writer);
                        DrawText(document, writer, SmallTitleFont, "欢迎关注我们", 20, document.PageSize.Height - 500, 500, 50);
                        DrawCompanyLogo(document, writer);
                        document.Close();
                        writer.Close();
                        fs.Close();
                    }

                }
            }
            Process.Start(fileName);

        }

    }
    public class itsEvents : PdfPageEventHelper
    {

        public override void OnStartPage(PdfWriter writer, Document doc)
        {

            PdfPTable headerTbl = new PdfPTable(2);
            headerTbl.TotalWidth = doc.PageSize.Width;

            Font font = new Font(DrawReport.baseFont, 10);

            PdfPCell cellleft = new PdfPCell(new Phrase("网络舆情监测报告", font));
            cellleft.Border = 0;
            cellleft.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTbl.AddCell(cellleft);

            Image image = Image.GetInstance("c:\\sb_logo_small.jpg");
            image.ScaleAbsolute(110, 18);
            image.Transparency = new int[] { 0xF0, 0xFF };
            PdfPCell cell = new PdfPCell(image);
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = 0;
            cell.PaddingBottom = 1;
            headerTbl.AddCell(cell);

            PdfPCell cellline = new PdfPCell();
            cellline.Colspan = 2;
            cellline.BorderColorTop = BaseColor.BLACK;
            headerTbl.AddCell(cellline);
            headerTbl.TotalWidth = doc.PageSize.Width - 50;
            headerTbl.WriteSelectedRows(0, -1, 20, (doc.PageSize.Height - 5), writer.DirectContent);
        }
        //public override void OnEndPage(PdfWriter writer, Document doc)
        //{
        //    //PdfPTable footerTbl = new PdfPTable(2);
        //    //footerTbl.TotalWidth = doc.PageSize.Width;
        //    //footerTbl.HorizontalAlignment = Element.ALIGN_CENTER;
        //    //Paragraph para = new Paragraph("Some footer text", footer);
        //    //para.Add(Environment.NewLine);
        //    //para.Add("Some more footer text");
        //    //PdfPCell cell = new PdfPCell(para);
        //    //cell.Border = 0;
        //    //cell.PaddingLeft = 10;
        //    //footerTbl.AddCell(cell);
        //    //para = new Paragraph("Some text for the second cell", footer);
        //    //cell = new PdfPCell(para);
        //    //cell.HorizontalAlignment = Element.ALIGN_RIGHT;
        //    //cell.Border = 0;
        //    //cell.PaddingRight = 10;
        //    //footerTbl.AddCell(cell);
        //    //footerTbl.WriteSelectedRows(0, -1, 0, (doc.BottomMargin + 10), writer.DirectContent);
        //}
    }



}
