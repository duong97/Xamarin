using ProductChecker.Models;
using ProductChecker.Views;
using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ProductChecker
{
    public partial class App : Application
    {

        public App()
        {
            new InitDatabase();
            InitializeComponent();
            MessagingCenter.Subscribe<SettingPage, Setting>(this, "changeTheme", (obj, item) => { loadTheme(); });
            loadTheme();
            MainPage = new Views.MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public void loadTheme()
        {
            string DarkTheme = "ProductChecker.Style.DarkTheme.css";
            string LightTheme = "ProductChecker.Style.LightTheme.css";
            string themeAdd = LightTheme;
            
            Setting st = Setting.GetSetting();
            if (st.IsDarkTheme)
            {
                themeAdd = DarkTheme;
            }
            StyleSheet s = StyleSheet.FromAssemblyResource(
                IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly,
            themeAdd);
            Resources.Add(s);
        }

    }
}
