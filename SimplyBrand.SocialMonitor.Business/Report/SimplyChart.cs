using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Gma.CodeCloud.Controls;
using Gma.CodeCloud.Controls.TextAnalyses.Blacklist;
using Gma.CodeCloud.Controls.TextAnalyses.Blacklist.En;
using Gma.CodeCloud.Controls.TextAnalyses.Extractors;
using Gma.CodeCloud.Controls.TextAnalyses.Processing;
using SimplyBrand.SocialMonitor.Business.JsonEntity;

namespace SimplyBrand.SocialMonitor.Business.Report
{
    public class SimplyChart
    {
        public byte[] DrawWordCloud(List<HotKeywordJson> items)
        {
            using (CloudControl cc = new CloudControl())
            {
                cc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                      | System.Windows.Forms.AnchorStyles.Left)
                                                                      | System.Windows.Forms.AnchorStyles.Right)));
                cc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                cc.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                cc.LayoutType = Gma.CodeCloud.Controls.LayoutType.Spiral;
                cc.Location = new System.Drawing.Point(12, 151);
                cc.MaxFontSize = 50;
                cc.MinFontSize = 10;
                cc.BorderStyle = BorderStyle.Fixed3D;
                cc.Name = "cloudControl";
                cc.Palette = new System.Drawing.Color[] {
                                    System.Drawing.Color.DarkRed,
                                    System.Drawing.Color.DarkBlue,
                                    System.Drawing.Color.DarkGreen,
                                    System.Drawing.Color.Navy,
                                    System.Drawing.Color.DarkCyan,
                                    System.Drawing.Color.DarkOrange,
                                    System.Drawing.Color.DarkGoldenrod,
                                    System.Drawing.Color.DarkKhaki,
                                    System.Drawing.Color.Blue,
                                    System.Drawing.Color.Red,
                                    System.Drawing.Color.Green};
                cc.Size = new System.Drawing.Size(600, 300);
                cc.TabIndex = 6;
                ProgressBar bar1 = new ProgressBar();
                IBlacklist blacklist = new CommonWords();
                IProgressIndicator progress = new ProgressBarWrapper(bar1);
                StringBuilder sbl = new StringBuilder();
                foreach (HotKeywordJson item in items)
                {
                    for (int i = 0; i < item.weight; i++)
                        sbl.Append(item.name + " ");
                }
                IEnumerable<string> terms = new StringExtractor(sbl.ToString(), progress);

                IEnumerable<IWord> words = terms
                   .Filter(blacklist)
                        .CountOccurences()
                        .SortByOccurences();
                if (words == null || words.Count() == 0)
                {
                    return null;
                }
                cc.WeightedWords = words;
                using (Bitmap bitmap = new Bitmap(cc.Width, cc.Height))
                {
                    cc.DrawToBitmap(bitmap, new Rectangle(0, 0, cc.Width, cc.Height));
                    //bitmap.Save("c:\\" + Guid.NewGuid().ToString() + ".png", ImageFormat.Png);
                    return CommonConvertHelper.BitmapToBytes(bitmap, ImageFormat.Bmp);
                }
            }
        }


        public byte[] DrawPie(List<DataCenterSummaryItemJson> data)
        {
            using (Chart chart = new Chart())
            {
                ChartArea chartArea = new ChartArea();


                chart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(223)))), ((int)(((byte)(240)))));
                chart.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
                chart.BackSecondaryColor = System.Drawing.Color.White;
                chart.BorderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
                chart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
                chart.BorderlineWidth = 1;
                chart.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Emboss;


                chartArea.AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
                chartArea.AxisX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
                chartArea.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
                chartArea.AxisY.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
                chartArea.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
                chartArea.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
                chartArea.BackColor = System.Drawing.Color.Transparent;
                chartArea.BackSecondaryColor = System.Drawing.Color.White;
                chartArea.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
                chartArea.BorderWidth = 0;

                chartArea.ShadowColor = System.Drawing.Color.Empty;
                Series series = new Series();
                series.Name = "series";
                chart.Series.Add(series);
                chart.ChartAreas.Add(chartArea);
                chart.Size = new Size(300, 300);
                chart.Series["series"].ChartType = SeriesChartType.Pie;

                //double[] yValues = { 26, 19, 55 };
                List<double> yValues = new List<double>();
                List<string> xValues = new List<string>();
                //string[] xValues = { "正面", "中性", "负面" };
                foreach (DataCenterSummaryItemJson item in data)
                {
                    yValues.Add(item.value);
                    xValues.Add(item.title);
                }

                chart.Series["series"].Points.DataBindXY(xValues, yValues);
                foreach (DataPoint point in chart.Series["series"].Points)
                {
                    point["Exploded"] = "false";
                    if (point.AxisLabel == "正面")
                    {
                        point["Exploded"] = "true";
                        point.Color = Color.Green;
                    }
                    else if (point.AxisLabel == "负面")
                    {
                        point["Exploded"] = "true";
                        point.Color = Color.Red;
                    }
                    else if (point.AxisLabel == "中性")
                    {
                        point.Color = Color.Orange;
                    }
                }
                chart.Series["series"]["PieStartAngle"] = "10";
                using (MemoryStream ms = new MemoryStream())
                {
                    chart.SaveImage(ms, ChartImageFormat.Bmp);
                    return ms.GetBuffer();
                }
            }
        }
        public byte[] DrawLineChart(Dictionary<string, List<DataCenterSummaryItemJson>> dicData)
        {
            using (Chart chart = new Chart())
            {
                chart.Size = new Size(420, 200);
                ChartArea chartArea = new ChartArea();
                chartArea.Name = "Default";
                chartArea.BorderDashStyle = ChartDashStyle.NotSet;
                chartArea.ShadowColor = System.Drawing.Color.Transparent;
                chartArea.BackColor = System.Drawing.Color.OldLace;
                chartArea.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
                chartArea.BackSecondaryColor = System.Drawing.Color.White;
                chartArea.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
                chartArea.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;

                chart.ChartAreas.Add(chartArea);
                Legend legend1 = new Legend();
                legend1.Alignment = System.Drawing.StringAlignment.Center;
                legend1.IsTextAutoFit = false;
                legend1.BackColor = System.Drawing.Color.Transparent;
                legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
                legend1.IsDockedInsideChartArea = false;
                legend1.DockedToChartArea = "Default";
                legend1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
                legend1.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Row;
                legend1.Name = "Default";
                chart.Legends.Add(legend1);

                List<DateTime> dtList = new List<DateTime>();

                Random random = new Random();
                foreach (string key in dicData.Keys)
                {
                    Series series = new Series();
                    series.BorderColor = Color.White;
                    series.BorderWidth = 2;
                    series.ChartArea = "Default";
                    series.ChartType = SeriesChartType.Line;
                    series.MarkerSize = 4;
                    series.MarkerStyle = MarkerStyle.None;
                    series.Name = key;
                    series.ShadowColor = System.Drawing.Color.Gray;
                    series.ShadowOffset = 1;
                    series.YValueType = ChartValueType.Double;
                    series.XValueType = ChartValueType.DateTime;
                    series.ChartType = SeriesChartType.Spline;
                    chart.Series.Add(series);

                    var query = from t in dicData[key]
                                orderby t.key ascending
                                select t;
                    foreach (var item in query)
                    {
                        chart.Series[key].Points.AddXY(DateTime.Parse(item.title), item.value);
                        dtList.Add(DateTime.Parse(item.title));
                    }
                    //foreach (var item in dicData[key])
                    //{
                    //    //chart.Series[key].Points.AddXY(DateTime.Parse(item.title), item.value);
                    //    chart.Series[key].Points.AddXY(item.key, item.value);
                    //    dtList.Add(DateTime.Parse(item.title));
                    //}



                }
                dtList = dtList.OrderByDescending(p => p).ToList();

                chart.Legends["Default"].BackColor = Color.Transparent;
                chart.Legends["Default"].BackSecondaryColor = Color.White;
                chart.Legends["Default"].BackGradientStyle = GradientStyle.None;
                chart.Legends["Default"].BorderColor = Color.Transparent;
                chart.Legends["Default"].BorderWidth = 1;
                chart.Legends["Default"].BorderDashStyle = ChartDashStyle.Solid;
                chart.Legends["Default"].ShadowOffset = 2;
                chart.ChartAreas["Default"].AxisX.MajorGrid.Enabled = true;
                chart.ChartAreas["Default"].AxisY.MajorGrid.Enabled = true;
                //chart.ChartAreas["Default"].AxisY.MajorGrid.Interval = 1;
                //chart.ChartAreas["Default"].AxisX.MajorGrid.Interval = 1;
                chart.ChartAreas["Default"].AxisY.MajorGrid.LineColor = Color.LightGray;
                chart.ChartAreas["Default"].AxisX.MajorGrid.LineColor = Color.LightGray;
                if (Math.Abs((dtList[0] - dtList[dtList.Count - 1]).Days) < 6)
                {
                    chart.ChartAreas["Default"].AxisX.LabelStyle.Format = "MM-dd hh";
                    chart.ChartAreas["Default"].CursorX.Interval = 2;
                    chart.ChartAreas["Default"].CursorX.IntervalType = DateTimeIntervalType.Hours;
                }
                else
                {

                    chart.ChartAreas["Default"].AxisX.LabelStyle.Format = "MM-dd";
                    chart.ChartAreas["Default"].CursorX.Interval = 1;
                    chart.ChartAreas["Default"].CursorX.IntervalType = DateTimeIntervalType.Days;
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    chart.SaveImage(ms, ChartImageFormat.Bmp);
                    return ms.GetBuffer();
                }
            }
        }
    }

    public static class CommonConvertHelper
    {
        public static byte[] ImageToBytes(Image Image, ImageFormat imageFormat)
        {
            if (Image == null) { return null; }
            byte[] data = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (Bitmap Bitmap = new Bitmap(Image))
                {
                    Bitmap.Save(ms, imageFormat);
                    ms.Position = 0;
                    data = new byte[ms.Length];
                    ms.Read(data, 0, Convert.ToInt32(ms.Length));
                    ms.Flush();
                }
            }
            return data;
        }
        public static byte[] BitmapToBytes(Bitmap bitmap, ImageFormat imageFormat)
        {
            if (bitmap == null) { return null; }
            byte[] data = null;
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, imageFormat);
                ms.Position = 0;
                data = new byte[ms.Length];
                ms.Read(data, 0, Convert.ToInt32(ms.Length));
                ms.Flush();
            }
            return data;
        }

    }
    internal class ProgressBarWrapper : IProgressIndicator
    {
        private readonly ProgressBar m_ProgressBar;

        public ProgressBarWrapper(ProgressBar toolStripProgressBar)
        {
            m_ProgressBar = toolStripProgressBar;
        }

        public int Value
        {
            get { return m_ProgressBar.Value; }
            set { m_ProgressBar.Value = value; }
        }

        public virtual int Maximum
        {
            get { return m_ProgressBar.Maximum; }
            set { m_ProgressBar.Maximum = value; }
        }

        public virtual void Increment(int value)
        {
            m_ProgressBar.Increment(value);
            Application.DoEvents();
        }
    }
}
