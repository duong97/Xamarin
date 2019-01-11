using ProductChecker.Models;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProductChecker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();
            InitDatabase initdb = new InitDatabase();

            MasterBehavior = MasterBehavior.Popover;

            Detail = new NavigationPage(new HomePage()); //Homepage
            MenuPages.Add((int)MenuItemType.Homepage, (NavigationPage)Detail);
        }

        public void NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Homepage:
                        MenuPages.Add(id, new NavigationPage(new HomePage()));
                        break;
                    case (int)MenuItemType.Import:
                        MenuPages.Add(id, new NavigationPage(new IECPage(Constant.MENU_TYPE_IMPORT)));
                        break;
                    case (int)MenuItemType.Export:
                        MenuPages.Add(id, new NavigationPage(new IECPage(Constant.MENU_TYPE_EXPORT)));
                        break;
                    case (int)MenuItemType.Check:
                        MenuPages.Add(id, new NavigationPage(new IECPage(Constant.MENU_TYPE_CHECK)));
                        break;
                    case (int)MenuItemType.List:
                        MenuPages.Add(id, new NavigationPage(new ItemsPage()));
                        break;
                    case (int)MenuItemType.Report:
                        MenuPages.Add(id, new NavigationPage(new ReportPage()));
                        break;
                    case (int)MenuItemType.Setting:
                        MenuPages.Add(id, new NavigationPage(new SettingPage()));
                        break;
                    case (int)MenuItemType.About:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                //if (Device.RuntimePlatform == Device.Android)
                //await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}