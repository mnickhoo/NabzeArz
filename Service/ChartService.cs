using NabzeArz.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using Telegram.Bot.Types;

namespace NabzeArz.Service
{
    public class ChartService
    {
        public async static Task<Message> SendChart(long chatId)
        {
            MakeChartImage("BTC");
            var stream = FileService.GetFileStream("BTC.png");
            return await TelegramService.UploadImage(stream, chatId, "نمودار هفته ی گذشته ی بیت_کوین");
        }
        //create by symbol name 
        public static void MakeChartImage(string symbol)
        {
            #region comment 
            //should be insted of this 
            //double[] yValues = { 15, 60, 12, 13, 50 ,22 , 45 };
            //double[] yValues = data;
            //string[] xValues = { "ش", "ی", "د", "س", "چ" , "پ" , "ج" };
            #endregion

            //decimal[] yValues = GetSevenDaysData(); //data shall reverse
            //double[] yValues = new double[] { 100, 120, 50, 40, 48, 50, 55 };
            decimal[] yValues = GetSevenDaysData();
            string[] xValues = PersianHelper.GetSevenDaysEarly().ToArray();
            Chart chart = new Chart(); // make chart 

            #region chart configuration
            chart.BackSecondaryColor = Color.Green;
            chart.BorderlineColor = Color.Red;
            chart.BackImageTransparentColor = Color.HotPink;
            //chart.BackColor = Color.Black;
            //chart.BorderlineColor = Color.Red;
            //chart.ForeColor = Color.White;
            //string image = "/images/favicon.png";
            #endregion

            Series series = new Series("Default");

            #region series configuration 

            series.ChartType = SeriesChartType.Line; //set chart type
            series.Color = ColorTranslator.FromHtml("#e14d43");
            series.LabelForeColor = Color.White;
            series.LabelBackColor = Color.White;
            series.MarkerColor = Color.Red;
            series.MarkerBorderColor = Color.Blue;
            series.BorderColor = ColorTranslator.FromHtml("#E04D43");
            series.LabelBorderColor = Color.WhiteSmoke;
            series.BorderWidth = 6;
            //series.BackImage = image; 
            //series["PieLabelStyle"] = "Disabled";
            #endregion

            //add series to chart
            chart.Series.Add(series);

            ChartArea chartArea = new ChartArea();
            #region chartArea Configuration 

            chartArea.BackImage ="~/Images/background.png"; //should be in AppSetting
            chartArea.BackImageAlignment = ChartImageAlignmentStyle.Center;
            chartArea.BackImageTransparentColor = Color.Yellow;
            chartArea.BackColor = ColorTranslator.FromHtml("#eeeeee");
            chartArea.BackImageWrapMode = ChartImageWrapMode.Unscaled;
            chartArea.BorderWidth = 500;
            chartArea.AxisY.Minimum = ((int)Math.Round(((double)yValues.Min() + ((double)yValues.Min() / 100)) / 10.0)) *10;
            chartArea.AxisY.IsStartedFromZero = false; //disable start from zero
            chartArea.AxisY.Maximum = (double)Math.Round((yValues.Max() + (yValues.Max() / 100))); //set maximum label
            chartArea.AxisY.Interval = interval((double)yValues.Min(), (double)yValues.Max()); ; //set interval

            #endregion

            Axis yAxis = new Axis(chartArea, AxisName.Y);
            Axis xAxis = new Axis(chartArea, AxisName.X);

            //chart.Series["Default"].Color = Color.Red;

            chart.Series["Default"].LabelFormat = "N1";
            //chart.Series["Default"].Points.DataBindXY(xValues, "", yValues, "");
            chart.Series["Default"].Points.DataBindXY(xValues, yValues);
            chart.ChartAreas.Add(chartArea);

            #region image config
            chart.Width = new Unit(500, System.Web.UI.WebControls.UnitType.Pixel); //set height image
            chart.Height = new Unit(400, System.Web.UI.WebControls.UnitType.Pixel); //set width image
            var chartImageFormat = ChartImageFormat.Png; //set image format
            #endregion

            #region image path
            string path = System.Web.Hosting.HostingEnvironment.MapPath($"~/Chart/{symbol}/");
            string fileName = $"{symbol}.{chartImageFormat.ToString()}";
            string FullName = path + fileName;
            #endregion

            if (!Directory.Exists(path)) //check is directory is exist shall be make a chart image else make a direction
            {
                Directory.CreateDirectory(path);
            }
            chart.SaveImage(FullName, chartImageFormat);  //save image in path

        }
        public static decimal[] GetSevenDaysData()
        {
            var coinApi = new CoinApiService("3A087081-F203-4840-9DF6-049E54153853"); //set API for CoinAPI
            var result = coinApi.Ohlcv_latest_data("BITSTAMP_SPOT_BTC_USD", "1DAY", 7); //get seven days ago 
            var data = result.Select(c => c.price_close).ToArray();
            return data.Reverse().ToArray(); ;
        }

        public static double interval(double min, double max)
        {
            // 10800 - 10000 = 800
            double different = max - min;
            //800 ? type
            var round = Math.Round(different);
            switch (round.ToString().Length)
            {
                case 5: return 10000;
                case 4: return 1000;
                case 3: return 100;
                case 2: return 10;
                default: return 1; 
            }

        }

    }
}