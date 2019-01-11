using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ProductChecker.Models;
using ZXing.Net.Mobile.Forms;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace ProductChecker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Item = new Item
            {
                Name        = "",
                Description = "",
                Price       = 0,
                Remain      = 0,
                TotalImport = 0,
                TotalExport = 0
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            var listProduct = Item.GetAll();
            bool isOk = true;
            foreach (var existsItem in listProduct)
            {
                if (Item.Barcode == existsItem.Barcode)
                {
                    isOk = false;
                    await DisplayAlert("Cảnh báo", "Sản phẩm '" + existsItem.Name +"' đã tồn tại", "OK");
                }
            }
            if (isOk)
            {
                Item.Add(Item);
                MessagingCenter.Send(this, "modifyItem", Item);
                await Navigation.PopModalAsync();
            }
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
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
                    barcodeEntry.Text = result.Text;
                });
            };
        }

        public async Task RefreshDataAsync(String barcode)
        {
            var uri = new Uri("https://world.openfoodfacts.org/api/v0/product/" + barcode + ".json");
            HttpClient myClient = new HttpClient();

            var response = await myClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                Models.JsonData json = JsonConvert.DeserializeObject<Models.JsonData>(content);
                String imgString = "", sttString, nameString = "", creatorString = "";
                sttString = "Trang thai: " + (String.IsNullOrEmpty(json.status_verbose) ? "404" : json.status_verbose);
                if (json.status != "0")
                {
                    Models.Product prd = json.product;
                    nameString = "Ten sp: " + (String.IsNullOrEmpty(prd.product_name) ? "404" : prd.product_name);
                    creatorString = "Creator: " + (String.IsNullOrEmpty(prd.creator) ? "404" : prd.creator);
                    imgString = String.IsNullOrEmpty(prd.image_url) ? "404" : prd.image_url;
                }
            }
        }
    }
}