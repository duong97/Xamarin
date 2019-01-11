using ProductChecker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace ProductChecker.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewIECPage : ContentPage
	{
        public History Htr { get; set; }
        public List<Item> Items { get; set; }
        public string PageTitle { get; set; }
        public NewIECPage (int type)
		{
			InitializeComponent ();

            Htr = new History
            {
                Type = type,
                Date = DateTime.Now.ToString(),
            };
            Items = Item.GetAll();
            PageTitle = Constant.MENU_LIST[type];
            BindingContext = this;
        }

        private async void ScanBtn_Clicked(object sender, EventArgs e)
        {
            var scan = new ZXingScannerPage();
            await Navigation.PushAsync(scan);
            scan.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    if (barcodeTxt.Text != "")
                    {
                        long rs = Int64.Parse(result.Text);
                        int matchItemIndex = Items.FindIndex(x => x.Barcode == rs);
                        picker.SelectedIndex = matchItemIndex;
                        barcodeTxt.Text = result.Text;
                    }
                });
            };
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            if (Htr.ItemBarcode == 0 && picker.SelectedIndex != -1)
            {
                Htr.ItemBarcode = Items[picker.SelectedIndex].Barcode;
                Htr.ItemName = Items[picker.SelectedIndex].Name;
            }
            if(Htr.ItemBarcode == 0)
            {
                await DisplayAlert("Thông báo", "Sản phẩm không được để trống!", "OK");
            } else
            {
                History.Add(Htr);
                MessagingCenter.Send(this, "modifyHistory", Htr);
                await Navigation.PopModalAsync();
            }
        }

        private void barcodeTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (barcodeTxt.Text != "")
            {
                long rs = Int64.Parse(barcodeTxt.Text);
                int matchItemIndex = Items.FindIndex(x => x.Barcode == rs);
                picker.SelectedIndex = matchItemIndex;
            }
        }

        private void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            barcodeTxt.Text = Items[picker.SelectedIndex].Barcode + "";
        }
    }
}