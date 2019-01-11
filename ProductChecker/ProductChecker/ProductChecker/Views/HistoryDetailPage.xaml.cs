using ProductChecker.Models;
using ProductChecker.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProductChecker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HistoryDetailPage : ContentPage
	{
        HistoryDetailViewModel viewModel;

        public HistoryDetailPage(HistoryDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public HistoryDetailPage()
        {
            InitializeComponent();

            var item = new History
            {
                ItemName = "Item 1",
                ItemBarcode = 12345678
            };

            viewModel = new HistoryDetailViewModel(item);
            BindingContext = viewModel;
        }

        private async void UpdateItem_Clicked(object sender, EventArgs e)
        {
            History.Update(viewModel.Item);
            MessagingCenter.Send(this, "modifyHistory", viewModel.Item);
            await Navigation.PopAsync();
        }

        private async void DeleteItem_Clicked(object sender, EventArgs e)
        {
            var confirm = await DisplayAlert("Xác nhận", "Bạn có chắc muốn xóa?", "Có", "Hủy");
            if (confirm)
            {
                History.Delete(viewModel.Item.Id);
                MessagingCenter.Send(this, "modifyHistory", viewModel.Item);
                await Navigation.PopAsync();
            }
        }
    }
}