using Microcharts;
using ProductChecker.Models;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.Entry;

namespace ProductChecker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportPage : ContentPage
    {
        List<Entry> entries = new List<Entry>();

        public ReportPage()
        {
            InitializeComponent();
            GetDataChart();
            loadChart();
            MessagingCenter.Subscribe<SettingPage, Setting>(this, "changeChart", (obj, item) => { loadChart(); });
        }

        public void GetDataChart()
        {
            List<Item> ListItem = Item.GetAll();
            List<string> ListColor = listHexColor();
            int i = 0;
            int NumColor = ListColor.Count;
            foreach (var item in ListItem)
            {
                int Percent = item.TotalImport - item.Remain;
                //Console.WriteLine("Name: " + item.Name);
                //Console.WriteLine("Import: " + item.TotalImport);
                //Console.WriteLine("Remain: " + item.Remain);
                //Console.WriteLine("Percen-------: {0}", x);
                //Console.WriteLine("Percen-------: {0}", x/y);
                //Console.WriteLine("Name: " + item.Name);
                string color = (i < NumColor) ? ListColor[i] : ListColor[0];
                i = (i < NumColor) ? i+1 : 0;
                Entry et = new Entry(Percent)
                {
                    //Color = SKColor.Parse("#FF1493"),
                    Color = SKColor.Parse(color),
                    ValueLabel = Percent.ToString(),
                    Label = item.Name,
                };
                entries.Add(et);
            }
        }

        public List<string> listHexColor()
        {
            List<string> list = new List<string>
            {
                "#2c3e50",
                "#77d065",
                "#b455b6",
                "#3498db",
                "#ff6262",
                "#ffcf62",
                "#62ffcc"
            };
            return list;
        }

        public void loadChart()
        {
            Setting st = Setting.GetSetting();
            Console.WriteLine("type: " + st.ChartType);
            if(st.ChartType == Constant.CHART_TYPE_BAR)
                MyChar.Chart = new BarChart() { Entries = entries };
            if (st.ChartType == Constant.CHART_TYPE_DONUT)
                MyChar.Chart = new DonutChart() { Entries = entries };
            if (st.ChartType == Constant.CHART_TYPE_LINE)
                MyChar.Chart = new LineChart() { Entries = entries };
        }
    }
}