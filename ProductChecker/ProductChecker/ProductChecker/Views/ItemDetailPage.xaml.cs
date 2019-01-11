using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ProductChecker.Models;
using ProductChecker.ViewModels;

namespace ProductChecker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Name = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }

        private async void UpdateItem_Clicked(object sender, EventArgs e)
        {
            Item.Update(viewModel.Item);
            MessagingCenter.Send(this, "modifyItem", viewModel.Item);
            await Navigation.PopAsync();
        }

        private async void DeleteItem_Clicked(object sender, EventArgs e)
        {
            var confirm = await DisplayAlert("Xác nhận", "Bạn có chắc muốn xóa?", "Có", "Hủy");
            if (confirm)
            {
                Item.Delete(viewModel.Item.Id);
                MessagingCenter.Send(this, "modifyItem", viewModel.Item);
                await Navigation.PopAsync();
            }
        }
    }
}