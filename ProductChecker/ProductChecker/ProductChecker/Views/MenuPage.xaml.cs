using ProductChecker.Models;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProductChecker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Homepage, Title="Trang chủ" },
                new HomeMenuItem {Id = MenuItemType.Import, Title = "Nhập kho"},
                new HomeMenuItem {Id = MenuItemType.Export, Title = "Xuất kho"},
                new HomeMenuItem {Id = MenuItemType.Check, Title = "Kiểm kho"},
                new HomeMenuItem {Id = MenuItemType.List, Title = "Danh sách sản phẩm"},
                new HomeMenuItem {Id = MenuItemType.Report, Title="Báo cáo" },
                new HomeMenuItem {Id = MenuItemType.Setting, Title="Cài đặt" },
                new HomeMenuItem {Id = MenuItemType.About, Title="Giới thiệu" },
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                RootPage.NavigateFromMenu(id);
            };
        }
    }
}