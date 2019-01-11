using ProductChecker.Models;
using ProductChecker.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProductChecker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IECPage : ContentPage
	{
        HistoryViewModel viewModel;
        public int Type { get; set; }
        public IECPage (int type)
		{
			InitializeComponent ();
            Type = type;
            BindingContext = viewModel = new HistoryViewModel(type);
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as History;
            if (item == null)
                return;

            await Navigation.PushAsync(new HistoryDetailPage(new HistoryDetailViewModel(item)));

            // Manually deselect item.
            HistoryListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Htr.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        private async void AddImport_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewIECPage(Type)));
        }
    }
}