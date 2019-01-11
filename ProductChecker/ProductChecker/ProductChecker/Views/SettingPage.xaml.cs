using ProductChecker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProductChecker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingPage : ContentPage
    {
        public Dictionary<int, string> ListChartType = Constant.CHART_LIST;
        public SettingPage()
        {
            InitializeComponent();
            Setting st = Setting.GetSetting();
            settingDarkTheme.IsToggled = st.IsDarkTheme;
            foreach (string type in ListChartType.Values)
            {
                picker.Items.Add(type);
            }
            picker.SelectedIndex = st.ChartType-1;
        }

        private void SettingDarkTheme_Toggled(object sender, ToggledEventArgs e)
        {
            bool rs = settingDarkTheme.IsToggled;
            Setting st = Setting.GetSetting();
            st.IsDarkTheme = false;
            if (rs) {
                st.IsDarkTheme = true;
            }
            Setting.Update(st);
            DisplayAlert("Thông Báo", "Giao diện mới sẽ xuất hiện vào lần tiếp theo bạn sử dụng!", "OK");
            MessagingCenter.Send(this, "changeChart", st);
        }

        private void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Setting st = Setting.GetSetting();
            st.ChartType = picker.SelectedIndex + 1;
            Setting.Update(st);
            MessagingCenter.Send(this, "changeChart", st);
            ///DisplayAlert("cc", "cc"+ ListChartType[picker.SelectedIndex+1], "cc");
        }
    }
}