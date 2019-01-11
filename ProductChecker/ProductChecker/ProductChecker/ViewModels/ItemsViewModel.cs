using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using ProductChecker.Models;
using ProductChecker.Views;

namespace ProductChecker.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Danh sách";
            Items = new ObservableCollection<Item>();
            
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            // Binding new item when press save on create (listen from MessagingCenter.send)
            MessagingCenter.Subscribe<NewItemPage, Item>(this, "modifyItem", (obj, item) => { refreshList(); });
            MessagingCenter.Subscribe<ItemDetailPage, Item>(this, "modifyItem", (obj, item) => { refreshList(); });
            MessagingCenter.Subscribe<NewIECPage, History>(this, "modifyHistory", (obj, item) => { refreshList(); });
            MessagingCenter.Subscribe<HistoryDetailPage, History>(this, "modifyHistory", (obj, item) => { refreshList(); });
        }

        // Refresh list view
        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void refreshList()
        {
            Items.Clear();
            var items = Item.GetAll();
            foreach (var item in items)
            {
                Items.Add(item);
            }
        }
    }
}